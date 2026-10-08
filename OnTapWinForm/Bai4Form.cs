using System;
using System.Drawing;
using System.Windows.Forms;

namespace OnTapWinForm
{
    public partial class Bai4Form : Form
    {
        private TableLayoutPanel tblLayout = null!;
        private Label lblSoLuong = null!;
        private Label lblTamTinh = null!;
        private ComboBox cboKhungGio = null!;
        private Button btnXacNhan = null!, btnHuyChon = null!;

        private readonly Color MAU_TRONG = Color.WhiteSmoke;
        private readonly Color MAU_DANG_CHON = Color.LightGreen;
        private readonly Color MAU_DA_DAT = Color.IndianRed;

        public Bai4Form()
        {
            InitializeComponent();
            SetupUI();
            KhoiTaoSoDoGhe();
        }

        private void SetupUI()
        {
            this.Text = "Bài 4: Sơ Đồ Chọn Vị Trí Chỗ Ngồi / Đặt Bàn Hẹn Giờ";
            this.Size = new Size(650, 480);
            this.StartPosition = FormStartPosition.CenterScreen;

            tblLayout = new TableLayoutPanel
            {
                Location = new Point(20, 20),
                Size = new Size(420, 380),
                RowCount = 4,
                ColumnCount = 5,
                BorderStyle = BorderStyle.FixedSingle
            };
            for (int i = 0; i < 5; i++) tblLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20f));
            for (int i = 0; i < 4; i++) tblLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 25f));

            GroupBox grpInfo = new GroupBox { Text = "Thông tin đặt bàn", Location = new Point(460, 20), Size = new Size(160, 380) };
            grpInfo.Controls.Add(new Label { Text = "Khung giờ:", Location = new Point(15, 30), AutoSize = true });
            cboKhungGio = new ComboBox { Location = new Point(15, 55), Width = 130, DropDownStyle = ComboBoxStyle.DropDownList };
            cboKhungGio.Items.AddRange(new object[] { "Sáng (100.000đ)", "Tối (150.000đ)" });
            cboKhungGio.SelectedIndex = 0;
            cboKhungGio.SelectedIndexChanged += (s, e) => CapNhatThongKe();

            grpInfo.Controls.Add(new Label { Text = "Vị trí đang chọn:", Location = new Point(15, 100), AutoSize = true });
            lblSoLuong = new Label { Text = "0", Location = new Point(15, 125), AutoSize = true, Font = new Font("Segoe UI", 12, FontStyle.Bold), ForeColor = Color.Blue };

            grpInfo.Controls.Add(new Label { Text = "Tạm tính tiền:", Location = new Point(15, 165), AutoSize = true });
            lblTamTinh = new Label { Text = "0 VNĐ", Location = new Point(15, 190), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold), ForeColor = Color.DarkRed };

            btnXacNhan = new Button { Text = "Xác nhận đặt", Location = new Point(15, 260), Width = 130, Height = 35 };
            btnHuyChon = new Button { Text = "Hủy chọn tất cả", Location = new Point(15, 310), Width = 130, Height = 35 };

            btnXacNhan.Click += (s, e) =>
            {
                int count = 0;
                foreach (Control ctrl in tblLayout.Controls)
                {
                    if (ctrl is Button btn && btn.BackColor == MAU_DANG_CHON)
                    {
                        btn.BackColor = MAU_DA_DAT;
                        btn.ForeColor = Color.White;
                        count++;
                    }
                }
                if (count > 0)
                {
                    MessageBox.Show($"Đặt thành công {count} vị trí!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CapNhatThongKe();
                }
                else MessageBox.Show("Vui lòng chọn ít nhất 1 vị trí!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            };

            btnHuyChon.Click += (s, e) =>
            {
                foreach (Control ctrl in tblLayout.Controls)
                {
                    if (ctrl is Button btn && btn.BackColor == MAU_DANG_CHON)
                    {
                        btn.BackColor = MAU_TRONG;
                    }
                }
                CapNhatThongKe();
            };

            grpInfo.Controls.AddRange(new Control[] { cboKhungGio, lblSoLuong, lblTamTinh, btnXacNhan, btnHuyChon });
            this.Controls.AddRange(new Control[] { tblLayout, grpInfo });
        }

        private void KhoiTaoSoDoGhe()
        {
            int stt = 1;
            for (int r = 0; r < 4; r++)
            {
                for (int c = 0; c < 5; c++)
                {
                    Button btn = new Button
                    {
                        Text = $"Bàn {stt:00}",
                        Dock = DockStyle.Fill,
                        BackColor = MAU_TRONG,
                        Margin = new Padding(3),
                        Font = new Font("Segoe UI", 9, FontStyle.Bold)
                    };

                    if (stt == 3 || stt == 12)
                    {
                        btn.BackColor = MAU_DA_DAT;
                        btn.ForeColor = Color.White;
                    }

                    btn.Click += (s, e) =>
                    {
                        if (s is Button b)
                        {
                            if (b.BackColor == MAU_DA_DAT)
                            {
                                MessageBox.Show("Vị trí này đã có người đặt!", "Khóa", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                return;
                            }
                            b.BackColor = (b.BackColor == MAU_TRONG) ? MAU_DANG_CHON : MAU_TRONG;
                            CapNhatThongKe();
                        }
                    };

                    tblLayout.Controls.Add(btn, c, r);
                    stt++;
                }
            }
        }

        private void CapNhatThongKe()
        {
            int soGhe = 0;
            foreach (Control ctrl in tblLayout.Controls)
            {
                if (ctrl is Button btn && btn.BackColor == MAU_DANG_CHON) soGhe++;
            }
            decimal gia = (cboKhungGio.SelectedIndex == 0) ? 100000m : 150000m;
            lblSoLuong.Text = soGhe.ToString();
            lblTamTinh.Text = $"{soGhe * gia:N0} VNĐ";
        }
    }
}