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
    public partial class HoaDon : Form
    {
        public SqlConnection conn = new SqlConnection();
        public string ngdn;
        public HoaDon(string nv)
        {
            InitializeComponent();
            ngdn = nv;
        }

        private void HoaDon_Load(object sender, EventArgs e)
        {
            Funtion func = new Funtion();
            func.ketnoi(conn);
            textBox1.Enabled = false;
            string mp = "select CONVERT(date,getdate()), Max(substring(mahd,3,len(mahd)-1)) from HOA_DON";
            SqlCommand command = new SqlCommand(mp, conn);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                int tt = Convert.ToInt32(reader.GetValue(1).ToString()) + 1;
                
                if (tt < 10)
                {
                    textBox1.Text = "HD00" + tt.ToString();

                }
                else if (tt < 100)
                {
                    textBox1.Text = "HD0" + tt.ToString();

                }
                else
                {
                    textBox1.Text = "HD" + tt.ToString();

                }

                label8.Text = reader.GetDateTime(0).ToString();
            }
            reader.Close();
            func.HienThiDataGridView(dataGridView2, "select * from don_dat_phong where mahd is null",conn);
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            label4.Text = dataGridView2.Rows[e.RowIndex].Cells[3].Value.ToString();
            label5.Text = dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString();
            label7.Text = dataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString();
            string mp = "select Hoten_kh,gia_thue from Khach_hang,phong,loai_phong where ma_kh='"+label4.Text+"' and phong.ma_loai=loai_phong.ma_loai and ma_phong='"+label7.Text+"'";
            SqlCommand command = new SqlCommand(mp, conn);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                label3.Text = reader.GetString(0);
                label11.Text = reader.GetInt32(1).ToString();
            }
            reader.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Funtion func = new Funtion();
            DateTime d = Convert.ToDateTime(label8.Text);
            string thoigian = d.ToString("yyyy-MM-dd");
            int n = Convert.ToInt32(label11.Text);
            
            string them = "insert into HOA_DON values ('"+textBox1.Text+"','"+ngdn+"','"+thoigian+ "','"+n+"')";
            SqlCommand cmd = new SqlCommand(them, conn);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Lưu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            string t = "update DON_DAT_PHONG set mahd = '"+textBox1.Text+ "' where ma_dondp='"+label5.Text+"'";
            SqlCommand cmd1 = new SqlCommand(t, conn);
            cmd1.ExecuteNonQuery();
            func.HienThiDataGridView(dataGridView2, "select * from don_dat_phong where mahd is null  ", conn);

        }
    }
}
