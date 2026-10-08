using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace OnTapWinForm
{
    public partial class Bai2Form : Form
    {
        private TextBox txtMaPhieu = null!;
        private TextBox txtNguoiYeuCau = null!;
        private DateTimePicker dtpNgayGhiNhan = null!;
        private RadioButton rdoThap = null!, rdoTrungBinh = null!, rdoKhanCap = null!;
        private ComboBox cboLoaiSuCo = null!;
        private CheckBox chkMayBan = null!, chkLaptop = null!, chkMayIn = null!, chkDienThoai = null!;
        private PictureBox picAnhLoi = null!;
        private Button btnTaiAnh = null!, btnGui = null!, btnNhapLai = null!;
        private string duongDanAnh = string.Empty;

        public Bai2Form()
        {
            InitializeComponent();
            SetupUI();
        }

        private void SetupUI()
        {
            this.Text = "Bài 2: Form Tiếp Nhận & Phân Loại Sự Cố IT";
            this.Size = new Size(620, 500);
            this.StartPosition = FormStartPosition.CenterScreen;

            GroupBox grpThongTin = new GroupBox { Text = "Thông tin phiếu yêu cầu", Location = new Point(20, 15), Size = new Size(260, 260) };
            grpThongTin.Controls.Add(new Label { Text = "Mã phiếu:", Location = new Point(15, 25), AutoSize = true });
            txtMaPhieu = new TextBox { Location = new Point(15, 45), Width = 220 };

            grpThongTin.Controls.Add(new Label { Text = "Người yêu cầu:", Location = new Point(15, 75), AutoSize = true });
            txtNguoiYeuCau = new TextBox { Location = new Point(15, 95), Width = 220 };

            grpThongTin.Controls.Add(new Label { Text = "Ngày ghi nhận:", Location = new Point(15, 125), AutoSize = true });
            dtpNgayGhiNhan = new DateTimePicker { Location = new Point(15, 145), Width = 220, Format = DateTimePickerFormat.Short };

            grpThongTin.Controls.Add(new Label { Text = "Mức độ ưu tiên:", Location = new Point(15, 175), AutoSize = true });
            rdoThap = new RadioButton { Text = "Thấp", Location = new Point(15, 200), AutoSize = true };
            rdoTrungBinh = new RadioButton { Text = "Trung bình", Location = new Point(75, 200), AutoSize = true, Checked = true };
            rdoKhanCap = new RadioButton { Text = "Khẩn cấp", Location = new Point(165, 200), AutoSize = true };
            grpThongTin.Controls.AddRange(new Control[] { txtMaPhieu, txtNguoiYeuCau, dtpNgayGhiNhan, rdoThap, rdoTrungBinh, rdoKhanCap });

            GroupBox grpChiTiet = new GroupBox { Text = "Chi tiết sự cố", Location = new Point(300, 15), Size = new Size(280, 260) };
            grpChiTiet.Controls.Add(new Label { Text = "Loại sự cố:", Location = new Point(15, 25), AutoSize = true });
            cboLoaiSuCo = new ComboBox { Location = new Point(15, 45), Width = 240, DropDownStyle = ComboBoxStyle.DropDownList };
            cboLoaiSuCo.Items.AddRange(new object[] { "Phần cứng", "Phần mềm", "Mạng", "Tài khoản" });
            cboLoaiSuCo.SelectedIndex = 0;

            grpChiTiet.Controls.Add(new Label { Text = "Thiết bị ảnh hưởng:", Location = new Point(15, 80), AutoSize = true });
            chkMayBan = new CheckBox { Text = "Máy tính bàn", Location = new Point(15, 105), AutoSize = true };
            chkLaptop = new CheckBox { Text = "Laptop", Location = new Point(140, 105), AutoSize = true };
            chkMayIn = new CheckBox { Text = "Máy in", Location = new Point(15, 135), AutoSize = true };
            chkDienThoai = new CheckBox { Text = "Điện thoại", Location = new Point(140, 135), AutoSize = true };
            grpChiTiet.Controls.AddRange(new Control[] { cboLoaiSuCo, chkMayBan, chkLaptop, chkMayIn, chkDienThoai });

            GroupBox grpAnh = new GroupBox { Text = "Ảnh chụp lỗi", Location = new Point(20, 285), Size = new Size(560, 120) };
            picAnhLoi = new PictureBox { Location = new Point(15, 20), Size = new Size(130, 90), BorderStyle = BorderStyle.FixedSingle, SizeMode = PictureBoxSizeMode.StretchImage };
            btnTaiAnh = new Button { Text = "Tải ảnh lỗi...", Location = new Point(160, 45), Width = 110, Height = 35 };
            btnTaiAnh.Click += (s, e) =>
            {
                using OpenFileDialog ofd = new OpenFileDialog { Filter = "Ảnh (*.jpg;*.png)|*.jpg;*.png" };
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    duongDanAnh = ofd.FileName;
                    if (picAnhLoi.Image != null) picAnhLoi.Image.Dispose();
                    picAnhLoi.Image = Image.FromFile(duongDanAnh);
                }
            };
            grpAnh.Controls.AddRange(new Control[] { picAnhLoi, btnTaiAnh });

            btnGui = new Button { Text = "Gửi yêu cầu", Location = new Point(190, 415), Width = 110, Height = 35 };
            btnNhapLai = new Button { Text = "Nhập lại", Location = new Point(320, 415), Width = 110, Height = 35 };

            btnGui.Click += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtMaPhieu.Text) || string.IsNullOrWhiteSpace(txtNguoiYeuCau.Text))
                {
                    MessageBox.Show("Vui lòng điền đầy đủ Mã phiếu và Người yêu cầu!", "Thiếu dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string uuTien = rdoThap.Checked ? "Thấp" : (rdoTrungBinh.Checked ? "Trung bình" : "Khẩn cấp");
                StringBuilder thietBi = new StringBuilder();
                if (chkMayBan.Checked) thietBi.Append("Máy tính bàn, ");
                if (chkLaptop.Checked) thietBi.Append("Laptop, ");
                if (chkMayIn.Checked) thietBi.Append("Máy in, ");
                if (chkDienThoai.Checked) thietBi.Append("Điện thoại, ");

                string danhSachTB = thietBi.Length > 0 ? thietBi.ToString().TrimEnd(' ', ',') : "Chưa chọn";
                string thongBao = $"--- TÓM TẮT PHIẾU HỖ TRỢ IT ---\n" +
                                  $"- Mã phiếu: {txtMaPhieu.Text.Trim()}\n" +
                                  $"- Người yêu cầu: {txtNguoiYeuCau.Text.Trim()}\n" +
                                  $"- Ngày ghi nhận: {dtpNgayGhiNhan.Value:dd/MM/yyyy}\n" +
                                  $"- Mức độ ưu tiên: {uuTien}\n" +
                                  $"- Loại sự cố: {cboLoaiSuCo.SelectedItem}\n" +
                                  $"- Thiết bị: {danhSachTB}\n" +
                                  $"- Ảnh đính kèm: {(string.IsNullOrEmpty(duongDanAnh) ? "Không có" : "Đã tải")}";

                MessageBox.Show(thongBao, "Gửi thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            };

            btnNhapLai.Click += (s, e) =>
            {
                txtMaPhieu.Clear();
                txtNguoiYeuCau.Clear();
                dtpNgayGhiNhan.Value = DateTime.Now;
                rdoTrungBinh.Checked = true;
                cboLoaiSuCo.SelectedIndex = 0;
                chkMayBan.Checked = chkLaptop.Checked = chkMayIn.Checked = chkDienThoai.Checked = false;
                if (picAnhLoi.Image != null) picAnhLoi.Image.Dispose();
                picAnhLoi.Image = null;
                duongDanAnh = string.Empty;
                txtMaPhieu.Focus();
            };

            this.Controls.AddRange(new Control[] { grpThongTin, grpChiTiet, grpAnh, btnGui, btnNhapLai });
        }
    }
}