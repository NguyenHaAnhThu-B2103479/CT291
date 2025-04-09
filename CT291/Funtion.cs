using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Security.Permissions;
using System.Windows.Forms;
using System.Data;

namespace CT291
{
    internal class Funtion
    {
        public void ketnoi(SqlConnection conn)
        {
            string chuoiketnoi = "SERVER = DESKTOP-G9N1BLI\\SQLEXPRESS; database = QL Khach San ; integrated security = True";
            conn.ConnectionString = chuoiketnoi;
            conn.Open();
        }

        public void HienThiDataGridView(DataGridView dg, string sql, SqlConnection conn)
        {
            SqlDataAdapter adapter = new SqlDataAdapter(sql,conn);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet, "abc");
            dg.DataSource = dataSet;
            dg.DataMember = "abc";
        }

        public void HienThiCombobox(ComboBox cb, string sql, SqlConnection conn, string hthi, string giau) 
        {
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);
            cb.DataSource = dt;
            cb.DisplayMember = hthi;
            cb.ValueMember = giau;
        }
    }
}
