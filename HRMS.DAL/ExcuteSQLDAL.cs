using System.Text;

namespace HRMS.DAL
{
    public class ExcuteSQLDAL
    {
        // 执行插入操作
        public static string Insert(string queryText)
        {
            return DBHelper.ExecuteNonQuery(queryText).ToString();
        }
        // 执行查询操作
        public static string Select(string queryText)
        {
            System.Data.DataTable datab = DBHelper.ExecuteDataTable(queryText);
            if (datab == null)
                return null;

            StringBuilder result = new StringBuilder();

            for (int i = 0; i < datab.Columns.Count-1; i++)
            {
                result.Append(datab.Columns[i].ColumnName).Append("\t|");
            }
            result.AppendLine(datab.Columns[datab.Columns.Count - 1].ColumnName);

            int lineLength = result.Length;
            for (int i = 0; i < lineLength; i++)
                result.Append(" -");
            result.AppendLine();

            for (int i = 0; i < datab.Rows.Count; i++)
            {
                for (int j = 0; j < datab.Columns.Count-1; j++)
                    result.Append(datab.Rows[i][j].ToString()).Append("\t|");
                result.AppendLine(datab.Rows[i][datab.Columns.Count - 1].ToString());
            }

            return result.ToString();
        }
        // 执行更新操作
        public static string Update(string queryText)
        {
            return DBHelper.ExecuteNonQuery(queryText).ToString();
        }
        // 执行删除操作
        public static string Delete(string queryText)
        {
            return DBHelper.ExecuteNonQuery(queryText).ToString();
        }
    }
}
