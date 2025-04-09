using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CT291
{
    public partial class Doimk : Form
    {
        public SqlConnection conn = new SqlConnection();
        public Doimk()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string oldPassword = textBox2.Text;
            string newPassword = textBox3.Text;
            string confirmNewPassword = textBox4.Text;

            // Kiểm tra xem mật khẩu mới và xác nhận mật khẩu mới có khớp nhau không
            if (newPassword != confirmNewPassword)
            {
                MessageBox.Show("Mật khẩu mới và xác nhận mật khẩu không khớp. Vui lòng thử lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Kiểm tra xem tên người dùng và mật khẩu cũ có tồn tại trong cơ sở dữ liệu không
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM USERS WHERE username = @username AND pass = @oldPassword", conn);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@oldPassword", oldPassword);



            // Thực thi câu lệnh và kiểm tra kết quả
            object result = cmd.ExecuteScalar();
            if (result != null && Convert.ToInt32(result) > 0)
            {
                // Cập nhật mật khẩu mới
                SqlCommand updateCmd = new SqlCommand("UPDATE USERS SET pass = @newPassword WHERE username = @username", conn);
                updateCmd.Parameters.AddWithValue("@newPassword", newPassword);
                updateCmd.Parameters.AddWithValue("@username", username);

                updateCmd.ExecuteNonQuery();
                MessageBox.Show("Mật khẩu đã được cập nhật thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Dangnhap dn = new Dangnhap();
                dn.Show();
                this.Close();
            }
            else
            {
                // Hiển thị thông báo lỗi nếu tên người dùng hoặc mật khẩu cũ không chính xác
                MessageBox.Show("Tên người dùng hoặc mật khẩu cũ không chính xác. Vui lòng kiểm tra lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void Doimk_Load(object sender, EventArgs e)
        {
            Funtion kn = new Funtion();
            kn.ketnoi(conn);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Dangnhap dn = new Dangnhap();
            dn.Show();
            this.Close();
        }

        private void groupBox1_Enter_1(object sender, EventArgs e)
        {

        }
    }
}

