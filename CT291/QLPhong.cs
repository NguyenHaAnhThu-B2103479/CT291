using CT291;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CT291
{
    public partial class QLPhong : Form
    {
        public SqlConnection conn = new SqlConnection();
        public string path = AppDomain.CurrentDomain.BaseDirectory;
        public QLPhong()
        {
            InitializeComponent();
        }

        private void QLPhong_Load(object sender, EventArgs e)
        {
            Funtion f = new Funtion();
            f.ketnoi(conn);
            f.HienThiDataGridView(dataGridView1, "SELECT PHONG.MA_PHONG, LOAI_PHONG.TEN_LOAI AS TenLoai, PHONG.HINHANH, PHONG.MOTA, PHONG.TRANGTHAI FROM PHONG JOIN LOAI_PHONG ON PHONG.MA_LOAI = LOAI_PHONG.MA_LOAI", conn);
            f.HienThiCombobox(comboBox1, "SELECT MA_LOAI,ten_loai FROM LOAI_PHONG", conn, "TEN_LOAI", "MA_LOAI");
            f.HienThiCombobox(comboBox2, "SELECT distinct TRANGTHAI FROM PHONG", conn, "MA_PHONG", "TRANGTHAI");
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string mp = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            textBox1.Text = mp;
            //textBox1.Text = row.Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            comboBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            comboBox2.SelectedValue = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();

            pictureBox1.Image = new Bitmap(dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string ma = textBox1.Text;
            string loai = comboBox1.SelectedValue.ToString();
            string tthai = comboBox2.SelectedValue.ToString();
            string mota = textBox2.Text;


            // Kiểm tra xem có hình ảnh nào được chọn không
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Vui lòng chọn một hình ảnh cho phòng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string img = pictureBox1.ImageLocation;
            string newImgPath = Path.Combine("D:\\CT291\\HinhAnh", Path.GetFileName(img));
            
            string directoryPath = Path.GetDirectoryName(newImgPath);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            string sql_them = "INSERT INTO PHONG (MA_PHONG, MA_LOAI, HINHANH, MOTA, TRANGTHAI) " +
                                  " VALUES ('" + ma + "',N'" + loai + "', '" + newImgPath + "' ,N'" + mota + "', N'" + tthai + "')";
            SqlCommand comd = new SqlCommand(sql_them, conn);
            comd.ExecuteNonQuery();
            Funtion f = new Funtion();
            f.HienThiDataGridView(dataGridView1, "SELECT PHONG.MA_PHONG, LOAI_PHONG.TEN_LOAI AS TenLoai, PHONG.HINHANH, PHONG.MOTA, PHONG.TRANGTHAI FROM PHONG JOIN LOAI_PHONG ON PHONG.MA_LOAI = LOAI_PHONG.MA_LOAI", conn);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string ma = textBox1.Text;
            string loai = comboBox1.SelectedValue.ToString();
            string tthai = comboBox2.SelectedValue.ToString();
            string mota = textBox2.Text;

            // Kiểm tra xem có hình ảnh nào được chọn không
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Vui lòng chọn một hình ảnh cho phòng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string img = pictureBox1.ImageLocation;
            string newImgPath = Path.Combine("D:\\CT291\\HinhAnh", Path.GetFileName(img));

            // Kiểm tra xem thư mục HinhAnh đã tồn tại chưa, nếu chưa thì tạo mới
            string directoryPath = Path.GetDirectoryName(newImgPath);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            string sql_sua = "UPDATE PHONG SET MA_LOAI = '" + loai + "', HINHANH = '" + newImgPath + "', MOTA = N'" + mota + "', TRANGTHAI = N'" + tthai + "' WHERE MA_PHONG = '" + ma + "'";
            SqlCommand comd = new SqlCommand(sql_sua, conn);
            comd.ExecuteNonQuery();

            // Hiển thị lại dữ liệu trong DataGridView
            Funtion f = new Funtion();
            f.HienThiDataGridView(dataGridView1, "SELECT PHONG.MA_PHONG, LOAI_PHONG.TEN_LOAI AS TenLoai, PHONG.HINHANH, PHONG.MOTA, PHONG.TRANGTHAI FROM PHONG JOIN LOAI_PHONG ON PHONG.MA_LOAI = LOAI_PHONG.MA_LOAI", conn);
        }
        

       
        private void button3_Click(object sender, EventArgs e)
        {
            string mp = textBox1.Text;
            string sql_xoa = "DELETE FROM PHONG WHERE MA_PHONG = @MA_PHONG";
            SqlCommand comd = new SqlCommand(sql_xoa, conn);

            comd.Parameters.AddWithValue("@MA_PHONG", mp);

            try
            {
                int rowsAffected = comd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Xóa phòng thành công");
                    // Refresh the DataGridView after successful deletion
                    Funtion f = new Funtion();
                    f.HienThiDataGridView(dataGridView1, "SELECT PHONG.MA_PHONG, LOAI_PHONG.TEN_LOAI AS TenLoai, PHONG.HINHANH, PHONG.MOTA, PHONG.TRANGTHAI FROM PHONG JOIN LOAI_PHONG ON PHONG.MA_LOAI = LOAI_PHONG.MA_LOAI", conn);
                }
                else
                {
                    MessageBox.Show("Không tìm thấy mã phòng " + mp);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xảy ra khi xóa phòng: " + ex.Message);
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            DialogResult result = ofd.ShowDialog();
            if (result == DialogResult.OK)
            {
                string filename = ofd.FileName;
                string destinationPath = Path.Combine("D:\\CT291\\HinhAnh", Path.GetFileName(filename));
                File.Copy(filename, destinationPath);
                pictureBox1.ImageLocation = destinationPath;
            }
        }

        private void quanLyKhachHangToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void quanLyDichVuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DichVu d = new DichVu();
            d.Show();
            this.Close();
        }

        private void đăngXuâtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dangnhap d = new Dangnhap();
            d.Show();
            this.Close();
        }

        private void quanLyKhachHangToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            QLKH d = new QLKH();
            d.Show();
            this.Close();
        }
    }

}
