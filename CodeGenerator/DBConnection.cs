using System.Data;
using System.Data.SqlClient;

namespace CodeGenerator
{
    class DBConnection
    {
        // 数据库连接字符串
        private static string connString;
        public static bool ConnectSQLServer(string server, string dataBase, 
                                     string userName, string password, out string errorInfo)
        {
            if(server.Length <= 0 || dataBase.Length <= 0 
               || userName.Length <= 0 || password.Length <= 0)
            {
                errorInfo = "必须填写所有字段";
                return false;
            }

            connString = "Data Source=" + server + "\\SQLEXPRESS; Initial Catalog=" + dataBase + 
                         "; User ID=" + userName + "; Password=" + password;

            errorInfo = "准备连接";
            return true;
        }

        internal static DataTable ExecuteDataTable(string queryText, 
                                                   params SqlParameter[] sqlParams)
        {
            DataTable datab = new DataTable();
            
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(queryText, conn))
                {
                    cmd.Parameters.AddRange(sqlParams);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.FillSchema(datab, SchemaType.Source);
                    adapter.Fill(datab);
                }
            }

            return datab;
        }

        /// <summary>
        /// 获取所有表名
        /// </summary>
        /// <param name="errorInfo">错误信息</param>
        /// <returns>返回表名数组</returns>
        public static string[] GetAllTables(out string errorInfo)
        {
            string queryText = @"SELECT name FROM dbo.sysobjects WHERE xtype='U' ORDER BY name";
            DataTable datab = new DataTable();

            try
            {
                datab = ExecuteDataTable(queryText);
            }
            catch (SqlException ex)
            {
                errorInfo = "数据库连接错误。错误信息：" + ex.Message;
                return null;
            }

            string[] tableList = new string[datab.Rows.Count];
            for (int i = 0; i < datab.Rows.Count; i++)
            {
                tableList[i] = (string)datab.Rows[i]["name"];
            }
            errorInfo = "数据库连接成功，表单赋值完成";
            return tableList;
        }
    }
}
