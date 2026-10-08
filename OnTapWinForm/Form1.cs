using System;
using System.Drawing;
using System.Windows.Forms;

namespace OnTapWinForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            SetupMenu();
        }

        private void SetupMenu()
        {
            this.Text = "HỆ THỐNG ÔN TẬP WINDOWS FORMS - D19QTANM1";
            this.Size = new Size(500, 420);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            Label lblTitle = new Label
            {
                Text = "DANH SÁCH BÀI TẬP ÔN TẬP",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                Location = new Point(30, 20),
                AutoSize = true,
                ForeColor = Color.DarkBlue
            };

            string[] tenBai = {
                "Bài 1: Tính Cước Dịch Vụ & Giảm Giá",
                "Bài 2: Tiếp Nhận & Phân Loại Sự Cố IT",
                "Bài 3: Quản Lý Danh Mục Vật Tư (ListView)",
                "Bài 4: Sơ Đồ Chọn Chỗ Ngồi / Đặt Bàn",
                "Bài 5: Quản Lý Đơn Giao Hàng (Dashboard)"
            };

            this.Controls.Add(lblTitle);

            for (int i = 0; i < 5; i++)
            {
                int index = i + 1;
                Button btn = new Button
                {
                    Text = tenBai[i],
                    Location = new Point(50, 70 + i * 55),
                    Size = new Size(380, 42),
                    Font = new Font("Segoe UI", 10, FontStyle.Regular),
                    TextAlign = ContentAlignment.MiddleLeft,
                    Padding = new Padding(15, 0, 0, 0)
                };

                btn.Click += (s, e) =>
                {
                    Form formToOpen = index switch
                    {
                        1 => new Bai1Form(),
                        2 => new Bai2Form(),
                        3 => new Bai3Form(),
                        4 => new Bai4Form(),
                        5 => new Bai5Form(),
                        _ => new Form()
                    };
                    formToOpen.ShowDialog();
                };

                this.Controls.Add(btn);
            }
        }
    }
}