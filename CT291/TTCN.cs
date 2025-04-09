using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CT291
{
    public partial class TTCN : Form
    {
        public SqlConnection conn = new SqlConnection();
        public string manv;
        public TTCN(string Manv)
        {
            InitializeComponent();
            manv = Manv;
        }
        private void TTCN_Load(object sender, EventArgs e)
        {
            Funtion funtion = new Funtion();
            funtion.ketnoi(conn);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Funtion f = new Funtion();
            string sql;

            sql = "select * from KHACH_HANG where SDT_KH = '" + textBox1.Text + "'";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read() == true)
            {
                label3.Text = reader.GetString(1);
                label4.Text = reader.GetString(0);
                
            }
            reader.Close();
            f.HienThiDataGridView(dataGridView2, "select p.Ma_phong,HinhAnh,HOTEN_KH from PHONG p,KHACH_HANG k,DON_DAT_PHONG d where d.MA_KH = k.MA_KH and p.MA_PHONG = d.MA_PHONG and SDT_KH = '"+textBox1.Text+"'", conn);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            /*TrangChu trangChu = new TrangChu();
            trangChu.Show();
            this.Hide();*/
        }

        

        private void button3_Click_1(object sender, EventArgs e)
        {
            DatDV d = new DatDV(label4.Text);
            d.Show();
            this.Close();
        }

        private void đătPhongToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DatPhong dp = new DatPhong(manv);
            dp.Show();
            this.Close();
        }

        private void đăngXuâtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dangnhap dn = new Dangnhap();
            dn.Show();
            this.Close();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            HoaDon hd = new HoaDon(manv); hd.Show();this.Close();
        }
    }
}
