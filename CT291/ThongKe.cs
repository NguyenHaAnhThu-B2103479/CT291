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
    public partial class ThongKe : Form
    {
        public SqlConnection conn = new SqlConnection();
        public ThongKe()
        {
            InitializeComponent();
        }

        private void ThongKe_Load(object sender, EventArgs e)
        {
            Funtion f = new Funtion();
            f.ketnoi(conn);
            f.HienThiDataGridView(dataGridView1, " SELECT HOA_DON.MAHD, HOA_DON.NGAYLAP_HD,CT_HOA_DON.SL ,SUM(CT_HOA_DON.SL * LOAI_PHONG.GIA_THUE) AS TONGTIEN FROM HOA_DON INNER JOIN CT_HOA_DON ON HOA_DON.MAHD = CT_HOA_DON.MAHD INNER JOIN PHONG ON CT_HOA_DON.MA_PHONG = PHONG.MA_PHONG INNER JOIN LOAI_PHONG ON PHONG.MA_LOAI = LOAI_PHONG.MA_LOAI GROUP BY HOA_DON.MAHD, HOA_DON.NGAYLAP_HD,CT_HOA_DON.SL", conn);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Funtion f = new Funtion();

            DateTime ngayBatDau = dateTimePicker1.Value.Date;       
            DateTime ngayKetThuc = dateTimePicker2.Value.Date;
            string sqlQuery = $"SELECT HOA_DON.MAHD, HOA_DON.NGAYLAP_HD,CT_HOA_DON.SL, SUM(CT_HOA_DON.SL * LOAI_PHONG.GIA_THUE) AS TONGTIEN FROM HOA_DON INNER JOIN CT_HOA_DON ON HOA_DON.MAHD = CT_HOA_DON.MAHD INNER JOIN PHONG ON CT_HOA_DON.MA_PHONG = PHONG.MA_PHONG INNER JOIN LOAI_PHONG ON PHONG.MA_LOAI = LOAI_PHONG.MA_LOAI WHERE HOA_DON.NGAYLAP_HD >= '{ngayBatDau}' AND HOA_DON.NGAYLAP_HD <= '{ngayKetThuc}' GROUP BY HOA_DON.MAHD, HOA_DON.NGAYLAP_HD,CT_HOA_DON.SL";
            f.HienThiDataGridView(dataGridView1, sqlQuery, conn);
            int tongTien = 0;
            // Duyệt qua các dòng trong DataGridView
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (!row.IsNewRow && row.Cells["TONGTIEN"].Value != null)
                {

                    tongTien += Convert.ToInt32(row.Cells["TONGTIEN"].Value);
                }
            }
            // Hiển thị tổng tiền tính được trong Label
            label3.Text = "Tổng số tiền: " + tongTien.ToString();
        }
    }
}
