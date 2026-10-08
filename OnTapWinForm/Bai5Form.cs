using System;
using System.Drawing;
using System.Windows.Forms;

namespace OnTapWinForm
{
    public partial class Bai5Form : Form
    {
        private SplitContainer splitMain = null!;
        private DataGridView dgvChiTiet = null!;
        private StatusStrip statusStrip = null!;
        private ToolStripStatusLabel lblTimer = null!, lblTongSL = null!, lblTongTL = null!, lblTongTien = null!;
        private System.Windows.Forms.Timer timerSys = null!;

        public Bai5Form()
        {
            InitializeComponent();
            SetupUI();
        }

        private void SetupUI()
        {
            this.Text = "Bài 5: Bảng Điều Khiển Quản Lý Đơn Giao Hàng (F2: Thêm dòng, Delete: Xóa dòng)";
            this.Size = new Size(950, 520);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.KeyPreview = true;

            // StatusStrip
            statusStrip = new StatusStrip();
            lblTimer = new ToolStripStatusLabel { Text = "Giờ: 00:00:00", BorderSides = ToolStripStatusLabelBorderSides.Right };
            lblTongSL = new ToolStripStatusLabel { Text = "Tổng SL: 0", BorderSides = ToolStripStatusLabelBorderSides.Right };
            lblTongTL = new ToolStripStatusLabel { Text = "Tổng TL: 0.0 kg", BorderSides = ToolStripStatusLabelBorderSides.Right };
            lblTongTien = new ToolStripStatusLabel { Text = "Tổng tiền: 0 VNĐ", ForeColor = Color.DarkRed, Font = new Font("Segoe UI", 9, FontStyle.Bold) };
            statusStrip.Items.AddRange(new ToolStripItem[] { lblTimer, lblTongSL, lblTongTL, lblTongTien });

            timerSys = new System.Windows.Forms.Timer { Interval = 1000 };
            timerSys.Tick += (s, e) => lblTimer.Text = DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy");
            timerSys.Start();

            // SplitContainer
            splitMain = new SplitContainer { Dock = DockStyle.Fill, SplitterDistance = 320, BorderStyle = BorderStyle.Fixed3D };

            Panel pnlLeft = new Panel { Dock = DockStyle.Fill, Padding = new Padding(10) };
            pnlLeft.Controls.Add(new Label { Text = "THÔNG TIN KHÁCH HÀNG", Font = new Font("Segoe UI", 10, FontStyle.Bold), Location = new Point(10, 10), AutoSize = true });
            pnlLeft.Controls.Add(new Label { Text = "Tên khách hàng:", Location = new Point(10, 45), AutoSize = true });
            TextBox txtTenKhach = new TextBox { Location = new Point(10, 70), Width = 280, Text = "Công ty TNHH Á Châu" };
            pnlLeft.Controls.Add(new Label { Text = "Địa chỉ giao:", Location = new Point(10, 105), AutoSize = true });
            TextBox txtDiaChi = new TextBox { Location = new Point(10, 130), Width = 280, Text = "Hà Nội" };
            pnlLeft.Controls.Add(new Label { Text = "Loại vận chuyển:", Location = new Point(10, 165), AutoSize = true });
            ComboBox cboVanChuyen = new ComboBox { Location = new Point(10, 190), Width = 280, DropDownStyle = ComboBoxStyle.DropDownList };
            cboVanChuyen.Items.AddRange(new object[] { "Tiêu chuẩn (2-3 ngày)", "Hỏa tốc (24h)", "Tiết kiệm" });
            cboVanChuyen.SelectedIndex = 0;
            pnlLeft.Controls.AddRange(new Control[] { txtTenKhach, txtDiaChi, cboVanChuyen });
            splitMain.Panel1.Controls.Add(pnlLeft);

            // DataGridView
            dgvChiTiet = new DataGridView { Dock = DockStyle.Fill, AllowUserToAddRows = false, AutoGenerateColumns = false };
            dgvChiTiet.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Tên hàng", Name = "ColTen", Width = 150 });
            dgvChiTiet.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Số lượng", Name = "ColSL", Width = 80 });
            dgvChiTiet.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Trọng lượng (kg)", Name = "ColTL", Width = 120 });
            dgvChiTiet.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Đơn giá", Name = "ColGia", Width = 100 });
            dgvChiTiet.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Thành tiền", Name = "ColThanhTien", Width = 120, ReadOnly = true });

            dgvChiTiet.CellEndEdit += (s, e) =>
            {
                var row = dgvChiTiet.Rows[e.RowIndex];
                row.ErrorText = string.Empty;
                bool okSL = int.TryParse(Convert.ToString(row.Cells["ColSL"].Value), out int sl);
                bool okTL = double.TryParse(Convert.ToString(row.Cells["ColTL"].Value), out double tl);
                bool okGia = decimal.TryParse(Convert.ToString(row.Cells["ColGia"].Value), out decimal gia);

                if (!okSL || sl <= 0 || !okTL || tl <= 0)
                {
                    row.ErrorText = "Số lượng và Trọng lượng phải > 0!";
                }
                else
                {
                    row.Cells["ColThanhTien"].Value = sl * (okGia ? gia : 0);
                }
                CapNhatThongKe();
            };

            this.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.F2)
                {
                    dgvChiTiet.Rows.Add("Hàng mới", 1, 1.0, 0, 0);
                    CapNhatThongKe();
                }
                else if (e.KeyCode == Keys.Delete && dgvChiTiet.SelectedRows.Count > 0)
                {
                    foreach (DataGridViewRow r in dgvChiTiet.SelectedRows) dgvChiTiet.Rows.Remove(r);
                    CapNhatThongKe();
                }
            };

            dgvChiTiet.Rows.Add("Màn hình máy tính", 2, 4.5, 3500000, 7000000);
            dgvChiTiet.Rows.Add("Chuột quang không dây", 5, 0.8, 250000, 1250000);
            splitMain.Panel2.Controls.Add(dgvChiTiet);

            this.Controls.Add(splitMain);
            this.Controls.Add(statusStrip);
            CapNhatThongKe();
        }

        private void CapNhatThongKe()
        {
            int tongSL = 0;
            double tongTL = 0;
            decimal tongTien = 0;
            foreach (DataGridViewRow r in dgvChiTiet.Rows)
            {
                if (r.IsNewRow) continue;
                int.TryParse(Convert.ToString(r.Cells["ColSL"].Value), out int sl);
                double.TryParse(Convert.ToString(r.Cells["ColTL"].Value), out double tl);
                decimal.TryParse(Convert.ToString(r.Cells["ColThanhTien"].Value), out decimal tt);
                tongSL += sl;
                tongTL += tl;
                tongTien += tt;
            }
            lblTongSL.Text = $"Tổng SL: {tongSL}";
            lblTongTL.Text = $"Tổng TL: {tongTL:F2} kg";
            lblTongTien.Text = $"Tổng tiền: {tongTien:N0} VNĐ";
        }
    }
}