using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CT291
{
    public partial class DatDV : Form
    {
        public SqlConnection conn = new SqlConnection();
        public string kh;
        public DatDV( string makh)
        {
            InitializeComponent();
            kh = makh;
        }

        

        private void DatDV_Load(object sender, EventArgs e)
        {
            Funtion funtion = new Funtion();
            funtion.ketnoi(conn);
            funtion.HienThiCombobox(comboBox2, "select Ma_DV, Ten_DV from DICH_VU", conn, "Ten_DV", "Ma_DV");
            
            string mp = "select HoTen_Kh from KHACH_HANG where ma_kh = '" + kh + "' ";
            SqlCommand command = new SqlCommand(mp, conn);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                label1.Text = reader.GetString(0);
                
            }
            reader.Close(); 
                                  
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        { 
            label4.Text = comboBox2.SelectedValue.ToString();      
        }

        private void comboBox2_SelectedValueChanged(object sender, EventArgs e)
        {
            //label1.Text = comboBox2.SelectedValue.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string mk = "SELECT tile*100 from DV_KM dk, KHUYEN_MAI k where  dk.MAKM = k.MAKM and dk.MA_DV = '" + label4.Text + "' ";
            SqlCommand command = new SqlCommand(mk, conn);


            SqlDataReader rdr = command.ExecuteReader();
            if (rdr.Read())
            {
                label5.Text = "Tỉ lệ KM: " + rdr.GetDouble(0).ToString()+"%";
            }

            rdr.Close();
            string m2 = "SELECT sum(d. gia_DV * (1-k.tile)) from DICH_VU d, DV_KM dk, KHUYEN_MAI k where d.MA_DV = dk.MA_DV and dk.MAKM = k.MAKM and d.MA_DV = '" + label4.Text + "' ";
            SqlCommand command1 = new SqlCommand(m2, conn);
            

            SqlDataReader rdr1 = command1.ExecuteReader();
            if (rdr1.Read())
            {
                label3.Text = "Giá: " + rdr1.GetDouble(0).ToString();
            }
            
            rdr1.Close();
        }
    }
}
