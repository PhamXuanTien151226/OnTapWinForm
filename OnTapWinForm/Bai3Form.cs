using System;
using System.Drawing;
using System.Windows.Forms;

namespace OnTapWinForm
{
    public partial class Bai3Form : Form
    {
        private TextBox txtMaVT = null!, txtTenVT = null!, txtDonGia = null!;
        private ComboBox cboDVT = null!;
        private ListView lsvVatTu = null!;
        private Button btnThem = null!, btnCapNhat = null!, btnXoaDong = null!, btnXoaTatCa = null!;

        public Bai3Form()
        {
            InitializeComponent();
            SetupUI();
        }

        private void SetupUI()
        {
            this.Text = "Bài 3: Quản Lý Danh Mục Vật Tư / Linh Kiện";
            this.Size = new Size(820, 420);
            this.StartPosition = FormStartPosition.CenterScreen;

            GroupBox grpNhap = new GroupBox { Text = "Thông tin vật tư", Location = new Point(15, 15), Size = new Size(260, 340) };
            grpNhap.Controls.Add(new Label { Text = "Mã vật tư:", Location = new Point(15, 30), AutoSize = true });
            txtMaVT = new TextBox { Location = new Point(15, 50), Width = 220 };

            grpNhap.Controls.Add(new Label { Text = "Tên vật tư:", Location = new Point(15, 80), AutoSize = true });
            txtTenVT = new TextBox { Location = new Point(15, 100), Width = 220 };

            grpNhap.Controls.Add(new Label { Text = "Đơn vị tính:", Location = new Point(15, 130), AutoSize = true });
            cboDVT = new ComboBox { Location = new Point(15, 150), Width = 220, DropDownStyle = ComboBoxStyle.DropDownList };
            cboDVT.Items.AddRange(new object[] { "Cái", "Bộ", "Kg", "Mét" });
            cboDVT.SelectedIndex = 0;

            grpNhap.Controls.Add(new Label { Text = "Đơn giá nhập:", Location = new Point(15, 180), AutoSize = true });
            txtDonGia = new TextBox { Location = new Point(15, 200), Width = 220 };

            btnThem = new Button { Text = "Thêm mới", Location = new Point(15, 240), Width = 105, Height = 32 };
            btnCapNhat = new Button { Text = "Cập nhật", Location = new Point(130, 240), Width = 105, Height = 32 };
            btnXoaDong = new Button { Text = "Xóa dòng", Location = new Point(15, 280), Width = 105, Height = 32 };
            btnXoaTatCa = new Button { Text = "Xóa toàn bộ", Location = new Point(130, 280), Width = 105, Height = 32 };

            btnThem.Click += (s, e) =>
            {
                if (!ValidateInput()) return;
                string maMoi = txtMaVT.Text.Trim();
                foreach (ListViewItem item in lsvVatTu.Items)
                {
                    if (item.Text.Equals(maMoi, StringComparison.OrdinalIgnoreCase))
                    {
                        MessageBox.Show($"Mã '{maMoi}' đã tồn tại!", "Cảnh báo trùng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                decimal gia = decimal.Parse(txtDonGia.Text.Trim());
                ListViewItem lvi = new ListViewItem(maMoi);
                lvi.SubItems.Add(txtTenVT.Text.Trim());
                lvi.SubItems.Add(cboDVT.SelectedItem?.ToString());
                lvi.SubItems.Add($"{gia:N0}");
                lsvVatTu.Items.Add(lvi);
                ResetForm();
            };

            btnCapNhat.Click += (s, e) =>
            {
                if (lsvVatTu.SelectedItems.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn 1 dòng để cập nhật!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (!ValidateInput()) return;
                ListViewItem item = lsvVatTu.SelectedItems[0];
                decimal gia = decimal.Parse(txtDonGia.Text.Trim());
                item.SubItems[1].Text = txtTenVT.Text.Trim();
                item.SubItems[2].Text = cboDVT.SelectedItem?.ToString();
                item.SubItems[3].Text = $"{gia:N0}";
                MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            };

            btnXoaDong.Click += (s, e) =>
            {
                if (lsvVatTu.SelectedItems.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn dòng cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (MessageBox.Show("Bạn có chắc chắn muốn xóa dòng này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    lsvVatTu.Items.Remove(lsvVatTu.SelectedItems[0]);
                    ResetForm();
                }
            };

            btnXoaTatCa.Click += (s, e) =>
            {
                if (lsvVatTu.Items.Count == 0) return;
                if (MessageBox.Show("Bạn có chắc muốn xóa sạch toàn bộ danh sách?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    lsvVatTu.Items.Clear();
                    ResetForm();
                }
            };

            grpNhap.Controls.AddRange(new Control[] { txtMaVT, txtTenVT, cboDVT, txtDonGia, btnThem, btnCapNhat, btnXoaDong, btnXoaTatCa });

            GroupBox grpDanhSach = new GroupBox { Text = "Danh sách vật tư", Location = new Point(290, 15), Size = new Size(500, 340) };
            lsvVatTu = new ListView
            {
                Location = new Point(15, 25),
                Size = new Size(470, 300),
                View = View.Details,
                FullRowSelect = true,
                GridLines = true,
                MultiSelect = false
            };
            lsvVatTu.Columns.Add("Mã VT", 80);
            lsvVatTu.Columns.Add("Tên VT", 160);
            lsvVatTu.Columns.Add("Đơn vị tính", 90);
            lsvVatTu.Columns.Add("Đơn giá", 120);

            lsvVatTu.SelectedIndexChanged += (s, e) =>
            {
                if (lsvVatTu.SelectedItems.Count > 0)
                {
                    ListViewItem item = lsvVatTu.SelectedItems[0];
                    txtMaVT.Text = item.Text;
                    txtMaVT.Enabled = false;
                    txtTenVT.Text = item.SubItems[1].Text;
                    cboDVT.SelectedItem = item.SubItems[2].Text;
                    txtDonGia.Text = item.SubItems[3].Text.Replace(",", "").Replace(".", "");
                }
            };

            grpDanhSach.Controls.Add(lsvVatTu);
            this.Controls.AddRange(new Control[] { grpNhap, grpDanhSach });
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtMaVT.Text) || string.IsNullOrWhiteSpace(txtTenVT.Text))
            {
                MessageBox.Show("Mã và tên vật tư không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (!decimal.TryParse(txtDonGia.Text.Trim(), out decimal gia) || gia < 0)
            {
                MessageBox.Show("Đơn giá phải là số hợp lệ (≥ 0)!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void ResetForm()
        {
            txtMaVT.Clear();
            txtMaVT.Enabled = true;
            txtTenVT.Clear();
            txtDonGia.Clear();
            cboDVT.SelectedIndex = 0;
            lsvVatTu.SelectedItems.Clear();
            txtMaVT.Focus();
        }
    }
}