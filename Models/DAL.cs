using Microsoft.Data.SqlClient;

namespace LucirWeb_MVC.Models
{
    public class DAL
    {
        private SqlConnection cn;
        private SqlCommand cmd;
        private string cString;
        private SqlDataReader dr;

        public DAL(string constring)
        {
            cString = constring;
            cn = new(cString);
        }
        public void Open()
        {
            if (cn.State == System.Data.ConnectionState.Closed)
            {
                cn.Open();
            }
        }
        public void Close()
        {
            if (cn.State == System.Data.ConnectionState.Open)
            {
                cn.Close();
            }
        }
        public void Set(string statement)
        {
            cmd = new(statement, cn);
        }
        public void Par(string pn, object pv)
        {
            cmd.Parameters.AddWithValue(pn, pv);
        }
        public void Clear()
        {
            cmd.Parameters.Clear();
        }

        public int Ex()
        {
            return (int)cmd.ExecuteScalar();
        }
        public void Exe(bool con = false)
        {
            if (con == true)
            {
                Open();
                cmd.ExecuteNonQuery();
                Close();
            }
            else
            {
                cmd.ExecuteNonQuery();
            }
        }
        public SqlDataReader StartRead()
        {
            dr = cmd.ExecuteReader();
            return dr;
        }
        public void EndReader()
        {
            dr.Close();
            dr.DisposeAsync();
        }
    }
}
