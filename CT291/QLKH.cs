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
    public partial class QLKH : Form
    {
        public SqlConnection conn = new SqlConnection();
        public QLKH()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void QLKH_Load(object sender, EventArgs e)
        {
            Funtion kn = new Funtion();
            kn.ketnoi(conn);
            kn.HienThiDataGridView(dataGridView1, "SELECT Ma_KH AS STT, HOTEN_KH AS HOTEN, NGAYSINH_KH AS NGAYSINH, GIOITINH_KH AS GIOITINH,SDT_KH AS SODIENTHOAI  FROM Khach_Hang ", conn);
            //kn.HienThiDataGridView(dataGridView2, "SELECT PHONG.Ma_Phong AS STT, LOAI_PHONG.Ten_Loai AS TENPHONG, PHONG.HINHANH, PHONG.MOTA, PHONG.TRANGTHAI \r\nFROM PHONG \r\nJOIN LOAI_PHONG ON PHONG.Ma_Loai = LOAI_PHONG.Ma_Loai;\r\n ", conn);
            textBox3.Enabled = false;
            string sql = "select Max(substring(Ma_KH,3,len(Ma_KH)-1)) from KHACH_HANG";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                int tt = Convert.ToInt32(rdr.GetValue(0).ToString()) + 1;
                if (tt < 10)
                {
                    textBox3.Text = "KH00" + tt.ToString();
                }
                else if (tt < 100)
                {
                    textBox3.Text = "KH0" + tt.ToString();
                }
                else
                {
                    textBox3.Text = "KH" + tt.ToString();
                }
            }
            rdr.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string ma = textBox3.Text;
            string sdt = textBox1.Text;
            string hoten = textBox2.Text;
            DateTime ngaysinh = dateTimePicker1.Value;
            string ngaySinh = ngaysinh.ToString("yyyy-MM-dd");
            string gioitinh = "";

            if (radioButton1.Checked)
            {
                gioitinh = "Nam";
            }
            else if (radioButton2.Checked)
            {
                gioitinh = "Nữ";
            }


            string sql_them = "INSERT INTO KHACH_HANG(Ma_KH,HOTEN_KH, NGAYSINH_KH, GIOITINH_KH, SDT_KH) " +
                                  " VALUES ('" + ma + "',N'" + hoten + "', '" + ngaysinh + "', N'" + gioitinh + "',  '" + sdt + "')";
            SqlCommand comd = new SqlCommand(sql_them, conn);
            comd.ExecuteNonQuery();

            Funtion kn = new Funtion();
            kn.HienThiDataGridView(dataGridView1, "SELECT Ma_KH AS STT, HOTEN_KH AS HOTEN, NGAYSINH_KH AS NGAYSINH, GIOITINH_KH AS GIOITINH, SDT_KH AS SODIENTHOAI  FROM Khach_Hang  ", conn);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            // Lấy giá trị từ ô cột 4 (giả sử đó là giới tính)
            if (dataGridView1.Rows[e.RowIndex].Cells[3].Value != null)
            {
                // Chuyển đổi giá trị giới tính thành chuỗi
                string gioiTinh = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();

                // Kiểm tra giới tính và cập nhật RadioButton
                if (gioiTinh == "Nam")
                {
                    radioButton1.Checked = true;
                }
                else if (gioiTinh == "Nữ")
                {
                    radioButton2.Checked = true;
                }
            }

            // Lấy giá trị từ ô cột 1 (giả sử đó là ngày sinh)
            if (dataGridView1.Rows[e.RowIndex].Cells[2].Value != null)
            {
                // Chuyển đổi giá trị ngày sinh thành kiểu DateTime
                DateTime ngaySinh = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[2].Value);

                // Cập nhật DateTimePicker với giá trị ngày sinh
                dateTimePicker1.Value = ngaySinh;
            }

            // Lấy giá trị từ ô cột 0 (giả sử đó là mã khách hàng)
            if (dataGridView1.Rows[e.RowIndex].Cells[0].Value != null)
            {
                // Cập nhật TextBox với giá trị mã khách hàng
                textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string ma = textBox3.Text;
            string sdt = textBox1.Text;
            string hoten = textBox2.Text;
            DateTime ngaysinh = dateTimePicker1.Value;
            string ngaySinh = ngaysinh.ToString("yyyy-MM-dd");
            string gioitinh = "";

            if (radioButton1.Checked)
            {
                gioitinh = "Nam";
            }
            else if (radioButton2.Checked)
            {
                gioitinh = "Nữ";
            }


            string sql_them = "UPDATE KHACH_HANG SET Ma_KH = '" + ma + "', HOTEN_KH = N'" + hoten + "' , NGAYSINH_KH = '" + ngaysinh + "', GIOITINH_KH = N'" + gioitinh + "', SDT_KH =  '" + sdt + "' WHERE Ma_KH = '" + ma + "' ";
            SqlCommand comd = new SqlCommand(sql_them, conn);
            comd.ExecuteNonQuery();

            Funtion kn = new Funtion();
            kn.HienThiDataGridView(dataGridView1, "SELECT Ma_KH AS STT, HOTEN_KH AS HOTEN, NGAYSINH_KH AS NGAYSINH, GIOITINH_KH AS GIOITINH, SDT_KH AS SODIENTHOAI  FROM Khach_Hang  ", conn);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string ma = textBox3.Text;
            

            string sql_them = "DELETE FROM KHACH_HANG WHERE Ma_KH = '" + ma + "' ";
            SqlCommand comd = new SqlCommand(sql_them, conn);
            comd.ExecuteNonQuery();

            Funtion kn = new Funtion();
            kn.HienThiDataGridView(dataGridView1, "SELECT Ma_KH AS STT, HOTEN_KH AS HOTEN, NGAYSINH_KH AS NGAYSINH, GIOITINH_KH AS GIOITINH, SDT_KH AS SODIENTHOAI  FROM Khach_Hang  ", conn);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            dateTimePicker1.Value = DateTime.Now;
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                Funtion kn = new Funtion();
                kn.HienThiDataGridView(dataGridView1, "SELECT * FROM KHACH_HANG WHERE SDT_KH = '" + textBox1.Text + "'", conn);
                //kn.HienThiDataGridView(dataGridView2, "select p.Ma_phong,HinhAnh,HOTEN_KH from PHONG p,KHACH_HANG k,DON_DAT_PHONG d where d.MA_KH = k.MA_KH and p.MA_PHONG = d.MA_PHONG and SDT_KH = '" + textBox1.Text + "'", conn);
            }
        }

        
        private void đătPhongToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void quanLyPhongToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            QLPhong ph = new QLPhong();
            ph.Show();
            this.Close();
        }

        private void quanLyDichVuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DichVu ph = new DichVu();
            ph.Show();
            this.Close();
        }

        private void đăngXuâtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dangnhap ph = new Dangnhap();
            ph.Show();
            this.Close();
        }
    }
}
