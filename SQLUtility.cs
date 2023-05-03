using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Net.NetworkInformation;

namespace CPUFramework
{
    public class SQLUtility
    {
        //public so you can access it in different projects
        public static string ConnectionString = "";

        public static SqlCommand GetSQLCommand(string sprocname)
        {
            SqlCommand cmd;
            using (SqlConnection conn = new SqlConnection(SQLUtility.ConnectionString))
            {
                cmd = new SqlCommand(sprocname, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                SqlCommandBuilder.DeriveParameters(cmd);
            }
            return cmd;
        }

        public static DataTable GetDataTable(SqlCommand cmd)
        {
            DataTable dt = new();
            using (SqlConnection conn = new SqlConnection(SQLUtility.ConnectionString))
            {
                Debug.Print("-----"+ Environment.NewLine + cmd.CommandText);
                conn.Open();
                cmd.Connection = conn;
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            SetAllColumnsAllowNulL(dt);
            return dt;
        }

        //static - so you dont need to instantiate the class
        //public so you can access it in different projects
        public static DataTable GetDataTable(string sqlstatement) // take a SQL statement and return data table
        {
            return GetDataTable(new SqlCommand(sqlstatement));
        }

        public static void ExecuteSQL(string sqlstatement)
        {
            GetDataTable(sqlstatement);
        }

        public static int GetFirstColumnFirstRowValue(string sql)
        {
            int n = 0;
            DataTable dt = GetDataTable(sql);
            if(dt.Rows.Count > 0 && dt.Columns.Count > 0)
            {
                if(dt.Rows[0][0] != DBNull.Value)
                {
                    int.TryParse(dt.Rows[0][0].ToString(), out n);
                }                
            }
            return n;
        }

        private static void SetAllColumnsAllowNulL(DataTable dt)
        {
            foreach(DataColumn c in dt.Columns)
            {
                c.AllowDBNull = true;
            }
        }

        public static void DebugPrintDataTable(DataTable dt)
        {
            foreach(DataRow r in dt.Rows)
            {
                foreach(DataColumn c in dt.Columns)
                {
                    Debug.Print(c.ColumnName + " = " + r[c.ColumnName].ToString());
                }
            }
        }


        public static SqlCommand GetTable(SqlCommand cmd)
        {
            DataTable dt = new();
            SqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);
            return cmd;
        }
    }
}
//note
