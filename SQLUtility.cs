using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace CPUFramework
{
    public class SQLUtility
    {
        //public so you can access it in different projects
        public static string ConnectionString = "";

        //static - so you dont need to instantiate the class
        //public so you can access it in different projects
        public static DataTable GetDataTable(String sqlstatement) // take a SQL statement and return data table
        {
            Debug.Print(sqlstatement);
            DataTable dt = new();
            SqlConnection conn = new();
            conn.ConnectionString = ConnectionString;
            conn.Open();
            var cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sqlstatement;
            var dr = cmd.ExecuteReader();
            dt.Load(dr);
            return dt;
        }
    }
}
//note
