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
    public partial class LeTan : Form
    {
        public SqlConnection conn = new SqlConnection();
        public string ngdn;
        
        public LeTan(string manv)
        {
            InitializeComponent();
            ngdn = manv;
        }

        private void đătPhongToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DatPhong dp = new DatPhong(ngdn);
            dp.Show();
            this.Close();
        }

        private void thôngTinĐătToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TTCN dp = new TTCN(ngdn);
            dp.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Dangnhap dn = new Dangnhap();
            dn.Show();
            this.Close();
        }

        private void LeTan_Load(object sender, EventArgs e)
        {
            Funtion func = new Funtion();
            func.ketnoi(conn);
            string sql;
            sql = "select HoTen_NV from NHAN_VIEN where Ma_NV = '" + ngdn + "'";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read() == true)
            {
                label3.Text = "Xin chào " + reader.GetString(0);
            }
            reader.Close();
        }
    }
}
