using System;
using System.Drawing;
using System.Windows.Forms;

namespace OnTapWinForm
{
    public partial class Bai1Form : Form
    {
        private TextBox txtDonGia = null!;
        private TextBox txtSoLuong = null!;
        private TextBox txtGiamGia = null!;
        private Label lblKetQua = null!;
        private Button btnTinhTien = null!;
        private Button btnLamMoi = null!;

        public Bai1Form()
        {
            InitializeComponent();
            SetupUI();
        }

        private void SetupUI()
        {
            this.Text = "Bài 1: Tính Cước Dịch Vụ & Giảm Giá";
            this.Size = new Size(420, 320);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            Label lblDonGia = new Label { Text = "Đơn giá dịch vụ:", Location = new Point(30, 30), AutoSize = true };
            txtDonGia = new TextBox { Location = new Point(160, 27), Width = 180, TabIndex = 0 };

            Label lblSoLuong = new Label { Text = "Số lượng khách:", Location = new Point(30, 70), AutoSize = true };
            txtSoLuong = new TextBox { Location = new Point(160, 67), Width = 180, TabIndex = 1 };

            Label lblGiamGia = new Label { Text = "Mã giảm giá (%):", Location = new Point(30, 110), AutoSize = true };
            txtGiamGia = new TextBox { Location = new Point(160, 107), Width = 180, TabIndex = 2, Text = "0" };

            Label lblTitleKQ = new Label { Text = "Tổng tiền:", Location = new Point(30, 155), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            lblKetQua = new Label { Text = "0 VNĐ", Location = new Point(160, 155), AutoSize = true, Font = new Font("Segoe UI", 11, FontStyle.Bold), ForeColor = Color.DarkRed };

            btnTinhTien = new Button { Text = "Tính tiền", Location = new Point(80, 210), Width = 100, Height = 35, TabIndex = 3 };
            btnLamMoi = new Button { Text = "Làm mới", Location = new Point(210, 210), Width = 100, Height = 35, TabIndex = 4 };

            btnTinhTien.Click += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtDonGia.Text) || string.IsNullOrWhiteSpace(txtSoLuong.Text))
                {
                    MessageBox.Show("Vui lòng không để trống Đơn giá hoặc Số lượng khách!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!decimal.TryParse(txtDonGia.Text.Trim(), out decimal donGia) || donGia < 0)
                {
                    MessageBox.Show("Đơn giá phải là số hợp lệ (≥ 0)!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtDonGia.Focus();
                    return;
                }

                if (!int.TryParse(txtSoLuong.Text.Trim(), out int soLuong) || soLuong <= 0)
                {
                    MessageBox.Show("Số lượng khách phải là số nguyên dương (> 0)!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtSoLuong.Focus();
                    return;
                }

                decimal phanTramGiam = 0;
                if (!string.IsNullOrWhiteSpace(txtGiamGia.Text))
                {
                    if (!decimal.TryParse(txtGiamGia.Text.Trim(), out phanTramGiam) || phanTramGiam < 0 || phanTramGiam > 100)
                    {
                        MessageBox.Show("Phần trăm giảm giá phải từ 0% đến 100%!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtGiamGia.Focus();
                        return;
                    }
                }

                decimal tongTien = (donGia * soLuong) * ((100 - phanTramGiam) / 100);
                lblKetQua.Text = $"{tongTien:N0} VNĐ";
            };

            btnLamMoi.Click += (s, e) =>
            {
                txtDonGia.Clear();
                txtSoLuong.Clear();
                txtGiamGia.Text = "0";
                lblKetQua.Text = "0 VNĐ";
                txtDonGia.Focus();
            };

            this.Controls.AddRange(new Control[] { lblDonGia, txtDonGia, lblSoLuong, txtSoLuong, lblGiamGia, txtGiamGia, lblTitleKQ, lblKetQua, btnTinhTien, btnLamMoi });
        }
    }
}