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
    public partial class Dangnhap : Form
    {
        public SqlConnection conn = new SqlConnection();
        public Dangnhap()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        

        private void label4_Click(object sender, EventArgs e)
        {
            Doimk mk = new Doimk();
            mk.Show();
            this.Hide();
        }
        private void Dangnhap_Load(object sender, EventArgs e)
        {
            Funtion f = new Funtion();
            f.ketnoi(conn);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string tenDN = textBox1.Text;
            string mk = textBox2.Text;
            string sql = "Select * from NHAN_VIEN where Ma_NV = '" + tenDN + "' and MatKhau_NV = '" + mk + "' ";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read() == true)
            {
                string cv = reader.GetString(1);
                string madg = reader.GetString(0).ToString();
                if(cv == "CV002")
                {
                    
                    QLKhachSan fm = new QLKhachSan(madg);
                    fm.Show();
                    this.Hide();
                }
                else
                {
                    LeTan fm = new LeTan(madg);
                    fm.Show();
                    this.Hide();
                }
                

            }
            else
            {
                MessageBox.Show("That bai");
            }
            reader.Close();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
