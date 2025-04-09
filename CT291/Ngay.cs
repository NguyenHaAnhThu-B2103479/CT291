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
    public partial class Ngay : Form
    {
        public SqlConnection conn = new SqlConnection();
        public string Kh;
        public string ngdn;
        public Ngay(string kh, string nv)
        {
            InitializeComponent();
            Kh = kh;
            ngdn = nv;
        }

        private void Ngay_Load(object sender, EventArgs e)
        {
            Funtion func = new Funtion();
            func.ketnoi(conn);
            string mp = "select p.Ma_phong ,Max(substring(ma_dondp,3,len(ma_dondp)-1)) from PHONG p,DON_DAT_PHONG where trangthai = '" + Kh + "' group by p.ma_phong";
            SqlCommand command = new SqlCommand(mp, conn);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                int tt = Convert.ToInt32(reader.GetValue(1).ToString()) + 1;
                
                if (tt < 10)
                {
                    label2.Text = "DP00" + tt.ToString();
                    
                }
                else if (tt < 100)
                {
                    label2.Text = "DP0" + tt.ToString();
                    
                }
                else
                {
                    label2.Text = "DP" + tt.ToString();
                    
                }
                
                label3.Text = reader.GetString(0);
                
            }
            reader.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime dat = dateTimePicker1.Value;
            string thoigian = dat.ToString("yyyy-MM-dd");
            DateTime nhan = dateTimePicker2.Value;
            string thoigian1 = nhan.ToString("yyyy-MM-dd");
            DateTime tra = dateTimePicker3.Value;
            string thoigian2 = tra.ToString("yyyy-MM-dd");
            
            

            string them = "insert into DON_DAT_PHONG(Ma_dondp,ma_phong,ma_kh,ngaydat,ngaynhan,ngaytra) values " +
                "('"+ label2.Text + "','"+label3.Text+"', '"+Kh+"','"+thoigian+"','"+ thoigian1+ "','"+ thoigian2+ "' )";
            SqlCommand cmd1 = new SqlCommand(them, conn);
            cmd1.ExecuteNonQuery();
            
            string p = "update PHONG  SET trangthai = N'Đã đặt' " + "where trangthai = '" + Kh + "'";
            SqlCommand cm = new SqlCommand(p, conn);
            cm.ExecuteNonQuery();
            MessageBox.Show("Đặt phòng thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            HoaDon t= new HoaDon(ngdn);
            t.Show();
            this.Hide();
        }

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }

        
    }
}
