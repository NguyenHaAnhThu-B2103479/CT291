using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CT291
{
    public partial class DatPhong : Form
    {
        public SqlConnection conn = new SqlConnection();
        public string manv ;
        
        public DatPhong(string nv)
        {
            InitializeComponent();
            manv=nv;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            
            pictureBox1.Image = new Bitmap(dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString());
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void DatPhong_Load(object sender, EventArgs e)
        {
            Funtion func = new Funtion();
            func.ketnoi(conn);
           
            func.HienThiDataGridView(dataGridView1, "Select * from PHONG where Trangthai = N'Còn trống' ", conn);
            func.HienThiCombobox(comboBox2, "Select Ma_Loai,Ten_Loai from LOAI_PHONG", conn, "Ten_Loai", "Ma_Loai");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Funtion func = new Funtion();
            string loai_p = comboBox2.SelectedValue.ToString();
            func.HienThiDataGridView(dataGridView1, "Select * from PHONG where Trangthai = N'Còn trống' and Ma_Loai = '"+loai_p+"' ", conn);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Funtion func = new Funtion();
            string makh = "Select Ma_KH from KHACH_HANG where SDT_KH = '" + textBox1.Text + "'";
            SqlCommand command = new SqlCommand(makh,conn);
            SqlDataReader reader1 = command.ExecuteReader();

            if (reader1.Read() == true)
            {
                label6.Text = reader1.GetString(0);
                
            }
            else
            {
                MessageBox.Show("Bạn phải lưu thông tin trước.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            reader1.Close();
            string them = "update PHONG SET trangthai = '" + label6.Text + "' where ma_phong = '" + textBox3.Text + "'";
            SqlCommand cmd = new SqlCommand(them, conn);
            cmd.ExecuteNonQuery();

            func.HienThiDataGridView(dataGridView2, "Select Ma_Phong, HinhAnh, MoTa from PHONG where trangthai = '" + label6.Text + "' ", conn);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Funtion f = new Funtion();
                string sql;
                
                sql = "select * from KHACH_HANG where SDT_KH = '" + textBox1.Text + "'";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read() == true)
                {
                    textBox1.Text = reader.GetString(4);
                    textBox4.Text = reader.GetString(1);
                    comboBox1.Text = reader.GetString(3);
                    textBox2.Text = reader.GetDateTime(2).ToString();
                    label6.Text = reader.GetString(0);
                }
                else
                {
                    MessageBox.Show("Không tìm được thông tin khách hàng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                reader.Close();
                f.HienThiDataGridView(dataGridView2, "Select Ma_Phong, HinhAnh, MoTa from PHONG where trangthai = '" + label6.Text + "'", conn);
               
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Funtion func = new Funtion();
            string makh = "Select Ma_KH from KHACH_HANG where SDT_KH = '" + textBox1.Text + "'";
            SqlCommand command = new SqlCommand(makh, conn);
            SqlDataReader reader1 = command.ExecuteReader();

            if (reader1.Read() == true)
            {
                label6.Text = reader1.GetString(0);

            }
            else
            {
                MessageBox.Show("Bạn phải lưu thông tin trước.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            reader1.Close();

            string them = "update PHONG SET trangthai = N'Còn trống' where ma_phong = '" + textBox3.Text + "'";
            SqlCommand cmd = new SqlCommand(them, conn);
            cmd.ExecuteNonQuery();

            func.HienThiDataGridView(dataGridView2, "Select Ma_Phong, HinhAnh, MoTa from PHONG where trangthai = '" + label6.Text + "' ", conn);

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox3.Text = dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString();
            pictureBox1.Image = new Bitmap(dataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string sdt = textBox1.Text;
            string makh = "Select Ma_KH from KHACH_HANG where SDT_KH = '" + sdt + "'";
            SqlCommand command = new SqlCommand(makh, conn);
            SqlDataReader reader1 = command.ExecuteReader();

            if (reader1.Read())
            {
                string m = reader1.GetString(0).ToString();
                Ngay n = new Ngay(m,manv);
                n.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Bạn phải lưu thông tin trước.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            reader1.Close();
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Funtion func = new Funtion();
            string makh = "Select Ma_KH from KHACH_HANG where SDT_KH = '" + textBox1.Text + "'";
            SqlCommand command = new SqlCommand(makh, conn);
            SqlDataReader reader1 = command.ExecuteReader();

            if (reader1.Read() == true)
            {
                label6.Text = reader1.GetString(0);
            }
            else
            {
                MessageBox.Show("Bạn phải lưu thông tin trước.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            reader1.Close();
            string them = "update PHONG SET trangthai = N'Còn trống' where trangthai = '" + label6.Text + "'";
            SqlCommand cmd = new SqlCommand(them, conn);
            cmd.ExecuteNonQuery();
            func.HienThiDataGridView(dataGridView1, "Select * from PHONG where Trangthai = N'Còn trống' ", conn);
            func.HienThiDataGridView(dataGridView2, "Select Ma_Phong, HinhAnh, MoTa from PHONG where trangthai = '" + label6.Text + "' ", conn);

        }

        private void button4_Click(object sender, EventArgs e)
        {
            /*QLKhachSan t = new QLKhachSan();
            t.Show();
            this.Hide();*/
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string sdt = textBox1.Text;
            string ten = textBox4.Text;
            string gt = comboBox1.Text;
            string ns = textBox2.Text;
            string sqlKH = "SELECT MAX(SUBSTRING(MA_KH, 3, LEN(MA_KH) - 1)) FROM KHACH_HANG";
            

            SqlCommand cmdKH = new SqlCommand(sqlKH, conn);
            

            SqlDataReader rdrDichVu = cmdKH.ExecuteReader();

            if (rdrDichVu.Read())
            {
                int ttDV = Convert.ToInt32(rdrDichVu.GetValue(0).ToString()) + 1;
                if (ttDV < 10)
                {
                    label7.Text = "KH00" + ttDV.ToString();
                }
                else if (ttDV < 100)
                {
                    label7.Text = "KH0" + ttDV.ToString();
                }
                else
                {
                    label7.Text = "KH" + ttDV.ToString();
                }

                rdrDichVu.Close(); // Đóng DataReader sau khi sử dụng xong
            }

            string sql_them = "INSERT INTO KHACH_HANG " +
                                 " VALUES ('" + label7.Text + "',N'" + ten + "', '" + ns + "',N'" + gt + "',  '" + sdt + "')";
            SqlCommand comd = new SqlCommand(sql_them, conn);
            comd.ExecuteNonQuery();

            }

        private void thôngTinĐătToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TTCN dp = new TTCN(manv);
            dp.Show();
            this.Close();
        }

        private void đăngXuâtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dangnhap dn = new Dangnhap();
            dn.Show();
            this.Close();
        }
    }
}
