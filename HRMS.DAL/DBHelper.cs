using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace HRMS.DAL
{
    /// <summary>
    /// 封装对数据库的实际操作
    /// </summary>
    internal class DBHelper
    {
        // 从项目配置文件App.config获取数据库连接字符串
        private static readonly string connString = 
            ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;

        /// <summary>
        /// 对连接执行 Transact-SQL 语句并返回受影响的行数。
        /// </summary>
        /// <param name="cmdText">查询文本</param>
        /// <param name="sqlParameters">查询参数</param>
        /// <returns>受影响的行数</returns>
        public static int ExecuteNonQuery(string cmdText, params SqlParameter[] sqlParameters)
        {
            using(SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                using(SqlCommand cmd = new SqlCommand(cmdText, conn))
                {
                    cmd.Parameters.AddRange(sqlParameters);
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// 执行查询，并返回查询结果集中第一行的第一列。忽略其他行或列。
        /// </summary>
        /// <param name="cmdText">查询文本</param>
        /// <param name="sqlParameters">查询参数</param>
        /// <returns>结果集中第一行的第一列；如果结果集为空，则为空引用</returns>
        public static object ExecuteScalar(string cmdText, params SqlParameter[] sqlParameters)
        {
            using(SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                using(SqlCommand cmd = new SqlCommand(cmdText, conn))
                {
                    cmd.Parameters.AddRange(sqlParameters);
                    return cmd.ExecuteScalar();
                }
            }
        }

        /// <summary>
        /// 执行查询，并返回查询结果集中的第一张表。
        /// </summary>
        /// <param name="cmdText">查询文本</param>
        /// <param name="sqlParameters">查询参数</param>
        /// <returns>结果集中的第一张表；如果结果集为空，则为空引用</returns>
        public static DataTable ExecuteDataTable(string cmdText, params SqlParameter[] sqlParameters)
        {
            using(SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                using(SqlCommand cmd = new SqlCommand(cmdText, conn))
                {
                    cmd.Parameters.AddRange(sqlParameters);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                    DataSet dataSet = new DataSet();
                    adapter.FillSchema(dataSet, SchemaType.Source);
                    adapter.Fill(dataSet);
                    return dataSet.Tables[0];
                }
            }
        }

        /// <summary>
        /// 将数据库中的可空字段值DBNull.Value转换成空引用
        /// </summary>
        /// <param name="dbData">数据库字段值</param>
        /// <returns>如果数据库字段值为DBNull.Value，返回空引用；否则，返回字段值本身</returns>
        public static object ConvertToCSValue(object dbData)
        {
            if (dbData == System.DBNull.Value)
                return null;
            else
                return dbData;
        }

        /// <summary>
        /// 将空引用转换成数据库空值DBNull.Value
        /// </summary>
        /// <param name="csData">可空对象</param>
        /// <returns>如果对象为空引用，返回DBNull.Value；否则返回对象本身</returns>
        public static object ConvertToDBValue(object csData)
        {
            if (csData == null)
                return System.DBNull.Value;
            else
                return csData;
        }
    }
}
