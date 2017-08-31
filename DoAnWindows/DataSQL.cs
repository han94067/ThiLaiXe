using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace DoAnWindows
{
    class DataSQL
    {
        public static string constr = ConfigurationManager.ConnectionStrings["DoAnWindows.Properties.Settings.DoAnWindowsConnectionString"].ToString();
        public static SqlConnection conn = new SqlConnection();

        public static void Open()
        {
            try
            {
                conn.ConnectionString = constr;
                conn.Open();
            }
            catch (SqlException e)
            {
                throw e;
            }
        }

        public static void Close()
        {
            conn.Close();
        }

        public static DataTable LayDuLieu(String CauLenh, SqlParameter[] param)
        {
            DataTable dt = new DataTable();
            try
            {
                Open();
                SqlDataAdapter adap = new SqlDataAdapter();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = CauLenh;
                cmd.Connection = conn;
                cmd.Parameters.AddRange(param);
                adap.SelectCommand = cmd;
                adap.Fill(dt);
            }
            catch (SqlException e)
            {
                throw e;
            }
            finally
            {
                Close();
            }
            return dt;
        }

        public static bool NhapDuLieu(String CauLenh, SqlParameter[] param)
        {
            try
            {
                Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = CauLenh;
                cmd.Connection = conn;
                cmd.Parameters.AddRange(param);
                cmd.ExecuteNonQuery();
            }
            catch
            {
                return false;
            }
            finally
            {
                Close();
            }
            return true;
        }
    }
}
