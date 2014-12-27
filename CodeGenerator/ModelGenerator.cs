using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace CodeGenerator
{
    class ModelGenerator
    {
        /// <summary>
        /// 对可空类型的处理
        /// </summary>
        /// <param name="column">列名</param>
        /// <returns>返回字段类型</returns>
        internal static string GetDataType(DataColumn column)
        {
            if(column.AllowDBNull && column.DataType.IsValueType)
            {
                return column.DataType.ToString() + "?";
            }
            else
            {
                return column.DataType.ToString();
            }
        }

        /// <summary>
        /// 生成指定表名的Model类
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <returns>返回生成的Model类</returns>
        public static string GenerateModel(string tableName)
        {
            string queryText = @"SELECT TOP 0 * FROM " + tableName;
            DataTable datab = DBConnection.ExecuteDataTable(queryText);

            StringBuilder builder = new StringBuilder();
            builder.Append("public class ").AppendLine(tableName);
            builder.AppendLine("{");

            foreach(DataColumn column in datab.Columns)
            {
                string dataType = GetDataType(column);
                builder.Append("    public ").Append(dataType).Append(" ");
                builder.Append(column.ColumnName).AppendLine(" { set; get; }");
            }

            builder.AppendLine("}");

            return builder.ToString();
        }
    }
}
