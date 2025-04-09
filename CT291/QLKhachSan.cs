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
    public partial class QLKhachSan : Form
    {
        public SqlConnection conn = new SqlConnection();
        public string ngdn;
        public QLKhachSan(string madg)
        {
            InitializeComponent();
            ngdn = madg;
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

        private void QLKhachSan_Load(object sender, EventArgs e)
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

        private void thôngTinĐătToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        

        private void đăngXuâtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dangnhap dn = new Dangnhap();
            dn.Show();
            this.Close();
        }

        private void quanLyDichVuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DichVu d = new DichVu();
            d.Show();
            this.Close();
        }

        private void thốngKêToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ThongKe t = new ThongKe();
            t.Show();
            this.Close();
        }
    }
}
