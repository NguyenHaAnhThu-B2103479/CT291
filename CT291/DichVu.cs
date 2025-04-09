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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CT291
{
    public partial class DichVu : Form
    {
        public SqlConnection conn = new SqlConnection();
        public DichVu()
        {
            InitializeComponent();
        }

        private void DichVu_Load(object sender, EventArgs e)
        {
            Funtion kn = new Funtion();
            kn.ketnoi(conn);
            kn.HienThiDataGridView(dataGridView1, "SELECT MA_DV AS STT, TEN_DV AS TENDICHVU, GIA_DV AS DONGIA, MOTA_DV AS MOTA FROM DICH_VU", conn);
            kn.HienThiDataGridView(dataGridView2, "SELECT MAKM AS STT, TENKM AS TENKHUYENMAI, TG_BATDAU AS THOIGIANBATDAU, TG_KETTHUC AS THOIGIANKETTHUC, TILE AS TILEKHUYENMAI FROM KHUYEN_MAI", conn);
            kn.HienThiDataGridView(dataGridView3, "SELECT DICH_VU.TEN_DV AS TENDICHVU, DICH_VU.GIA_DV AS DONGIA, DICH_VU.MOTA_DV AS MOTA, KHUYEN_MAI.TENKM AS TENKHUYENMAI, KHUYEN_MAI.TG_BATDAU AS THOIGIANBATDAU, KHUYEN_MAI.TG_KETTHUC AS THOIGIANKETTHUC, KHUYEN_MAI.TILE FROM DICH_VU JOIN KHUYENMAI_DICHVU ON DICH_VU.MA_DV = KHUYENMAI_DICHVU.MA_DV JOIN KHUYEN_MAI ON KHUYENMAI_DICHVU.MAKM = KHUYEN_MAI.MAKM ", conn);
            textBox1.Enabled = false;
            textBox6.Enabled = false;

            string sqlDichVu = "SELECT MAX(SUBSTRING(MA_DV, 3, LEN(MA_DV) - 1)) FROM DICH_VU";
            string sqlKhuyenMai = "SELECT MAX(SUBSTRING(MAKM, 3, LEN(MAKM) - 1)) FROM KHUYEN_MAI";

            SqlCommand cmdDichVu = new SqlCommand(sqlDichVu, conn);
            SqlCommand cmdKhuyenMai = new SqlCommand(sqlKhuyenMai, conn);

            SqlDataReader rdrDichVu = cmdDichVu.ExecuteReader();

            if (rdrDichVu.Read())
            {
                int ttDV = Convert.ToInt32(rdrDichVu.GetValue(0).ToString()) + 1;
                if (ttDV < 10)
                {
                    textBox1.Text = "DV00" + ttDV.ToString();
                }
                else if (ttDV < 100)
                {
                    textBox1.Text = "DV0" + ttDV.ToString();
                }
                else
                {
                    textBox1.Text = "DV" + ttDV.ToString();
                }

                rdrDichVu.Close(); // Đóng DataReader sau khi sử dụng xong
            }

            SqlDataReader rdrKhuyenMai = cmdKhuyenMai.ExecuteReader();

            if (rdrKhuyenMai.Read())
            {
                int ttKM = Convert.ToInt32(rdrKhuyenMai.GetValue(0).ToString()) + 1;
                if (ttKM < 10)
                {
                    textBox6.Text = "KM00" + ttKM.ToString();
                }
                else if (ttKM < 100)
                {
                    textBox6.Text = "KM0" + ttKM.ToString();
                }
                else
                {
                    textBox6.Text = "KM" + ttKM.ToString();
                }

                rdrKhuyenMai.Close(); // Đóng DataReader sau khi sử dụng xong
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox5.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox7.Text = "";
            textBox6.Text = "";
            textBox8.Text = "";
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            textBox5.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox8.Text = dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString();
            textBox7.Text = dataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString();
            textBox6.Text = dataGridView2.Rows[e.RowIndex].Cells[4].Value.ToString();
            // Lấy giá trị từ ô cột 1 (giả sử đó là ngày sinh)
            if (dataGridView2.Rows[e.RowIndex].Cells[2].Value != null)
            {
                // Chuyển đổi giá trị ngày sinh thành kiểu DateTime
                DateTime ngaySinh = Convert.ToDateTime(dataGridView2.Rows[e.RowIndex].Cells[2].Value);

                // Cập nhật DateTimePicker với giá trị ngày sinh
                dateTimePicker1.Value = ngaySinh;
            }
            if (dataGridView2.Rows[e.RowIndex].Cells[3].Value != null)
            {
                // Chuyển đổi giá trị ngày sinh thành kiểu DateTime
                DateTime ngaySinh = Convert.ToDateTime(dataGridView2.Rows[e.RowIndex].Cells[3].Value);

                // Cập nhật DateTimePicker với giá trị ngày sinh
                dateTimePicker2.Value = ngaySinh;
            }
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            textBox5.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            textBox8.Text = dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString();
            textBox7.Text = dataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString();
            textBox6.Text = dataGridView2.Rows[e.RowIndex].Cells[4].Value.ToString();
            // Lấy giá trị từ ô cột 1 (giả sử đó là ngày sinh)
            if (dataGridView2.Rows[e.RowIndex].Cells[2].Value != null)
            {
                // Chuyển đổi giá trị ngày sinh thành kiểu DateTime
                DateTime ngaySinh = Convert.ToDateTime(dataGridView2.Rows[e.RowIndex].Cells[2].Value);

                // Cập nhật DateTimePicker với giá trị ngày sinh
                dateTimePicker1.Value = ngaySinh;
            }
            if (dataGridView2.Rows[e.RowIndex].Cells[3].Value != null)
            {
                // Chuyển đổi giá trị ngày sinh thành kiểu DateTime
                DateTime ngaySinh = Convert.ToDateTime(dataGridView2.Rows[e.RowIndex].Cells[3].Value);

                // Cập nhật DateTimePicker với giá trị ngày sinh
                dateTimePicker2.Value = ngaySinh;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string ma = textBox1.Text;
            string ten = textBox3.Text;
            string gia = textBox4.Text;
            string mota = textBox5.Text;
            string sql_them = "INSERT INTO DICH_VU(MA_DV,TEN_DV, GIA_DV, MOTA_DV) " +
                                 " VALUES ('" + ma + "',N'" + ten + "', '" + gia + "', N'" + mota + "')";
            SqlCommand comd = new SqlCommand(sql_them, conn);
            comd.ExecuteNonQuery();

            Funtion kn = new Funtion();
            kn.HienThiDataGridView(dataGridView1, "SELECT MA_DV AS STT, TEN_DV AS TENDICHVU, GIA_DV AS DONGIA, MOTA_DV AS MOTA FROM DICH_VU", conn);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string ma = textBox1.Text;
            string ten = textBox3.Text;
            string gia = textBox4.Text;
            string mota = textBox5.Text;
            string sql_them = "UPDATE DICH_VU SET MA_DV = '" + ma + "',TEN_DV = N'" + ten + "', GIA_DV = '" + gia + "', MOTA_DV =  N'" + mota + "' WHERE MA_DV = '" + ma + "' ";
            SqlCommand comd = new SqlCommand(sql_them, conn);
            comd.ExecuteNonQuery();

            Funtion kn = new Funtion();
            kn.HienThiDataGridView(dataGridView1, "SELECT MA_DV AS STT, TEN_DV AS TENDICHVU, GIA_DV AS DONGIA, MOTA_DV AS MOTA FROM DICH_VU", conn);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string ma = textBox1.Text;
            
            string sql_them = "DELETE  FROM DICH_VU WHERE MA_DV = '" + ma + "' ";
            SqlCommand comd = new SqlCommand(sql_them, conn);
            comd.ExecuteNonQuery();

            Funtion kn = new Funtion();
            kn.HienThiDataGridView(dataGridView1, "SELECT MA_DV AS STT, TEN_DV AS TENDICHVU, GIA_DV AS DONGIA, MOTA_DV AS MOTA FROM DICH_VU", conn);

        }

        private void button5_Click(object sender, EventArgs e)
        {
            string ma = textBox8.Text;
            string ten = textBox7.Text;
            string gia = textBox6.Text;
            DateTime batdau = dateTimePicker1.Value;
            string thoigian = batdau.ToString("yyyy-MM-dd");
            DateTime ketthuc = dateTimePicker2.Value;
            string tg = ketthuc.ToString("yyyy-MM-dd");

            string sql_them = "INSERT INTO KHUYEN_MAI(MAKM,TENKM, TG_BATDAU, TG_KETTHUC ,TILE) " +
                                 " VALUES ('" + ma + "',N'" + ten + "', '" + batdau + "','" + ketthuc + "',  '" + gia + "')";
            SqlCommand comd = new SqlCommand(sql_them, conn);
            comd.ExecuteNonQuery();

            Funtion kn = new Funtion();
            kn.HienThiDataGridView(dataGridView2, "SELECT MAKM AS STT, TENKM AS TENKHUYENMAI, TG_BATDAU AS THOIGIANBATDAU, TG_KETTHUC AS THOIGIANKETTHUC, TILE AS TILEKHUYENMAI FROM KHUYEN_MAI", conn);

        }

        private void button8_Click(object sender, EventArgs e)
        {
            string ma = textBox8.Text;
            string ten = textBox7.Text;
            string gia = textBox6.Text;
            DateTime batdau = dateTimePicker1.Value;
            string thoigian = batdau.ToString("yyyy-MM-dd");
            DateTime ketthuc = dateTimePicker2.Value;
            string tg = ketthuc.ToString("yyyy-MM-dd");

            string sql_them = "UPDATE KHUYEN_MAI SET MAKM = '" + ma + "',TENKM = N'" + ten + "', TG_BATDAU = '" + batdau + "', TG_KETTHUC = '" + ketthuc + "' ,TILE ='" + gia + "' WHERE MAKM =  '" + ma + "'";
            SqlCommand comd = new SqlCommand(sql_them, conn);
            comd.ExecuteNonQuery();

            Funtion kn = new Funtion();
            kn.HienThiDataGridView(dataGridView2, "SELECT MAKM AS STT, TENKM AS TENKHUYENMAI, TG_BATDAU AS THOIGIANBATDAU, TG_KETTHUC AS THOIGIANKETTHUC, TILE AS TILEKHUYENMAI FROM KHUYEN_MAI", conn);

        }

        private void button7_Click(object sender, EventArgs e)
        {
            string ma = textBox8.Text;
            

            string sql_them = "DELETE FROM KHUYEN_MAI  WHERE MAKM =  '" + ma + "'";
            SqlCommand comd = new SqlCommand(sql_them, conn);
            comd.ExecuteNonQuery();

            Funtion kn = new Funtion();
            kn.HienThiDataGridView(dataGridView2, "SELECT MAKM AS STT, TENKM AS TENKHUYENMAI, TG_BATDAU AS THOIGIANBATDAU, TG_KETTHUC AS THOIGIANKETTHUC, TILE AS TILEKHUYENMAI FROM KHUYEN_MAI", conn);
        }

        private void quanLyKhachHangToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QLKH kh = new QLKH();
            kh.Show();
            this.Close();
        }

        private void quanLyPhongToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QLPhong ph = new QLPhong();
            ph.Show();
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
