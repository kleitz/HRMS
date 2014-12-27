using HRMS.DAL;
using System.Text;

namespace HRMS.BLL
{
    public class ExcuteSQLBLL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryText"></param>
        /// <returns></returns>
        public static string ExcuteSQL(string queryText)
        {
            if (queryText == null || queryText.Length <= 6)
                return null;

            StringBuilder result = new StringBuilder();
            string type = queryText.Substring(0, 6);

            switch(type.ToUpper())
            {
                case "SELECT":
                    result.AppendLine(ExcuteSQLDAL.Select(queryText));
                    break;
                case "UPDATE":
                    result.Append(ExcuteSQLDAL.Update(queryText)).AppendLine("行已更新");
                    break;
                case "INSERT":
                    result.Append(ExcuteSQLDAL.Insert(queryText)).AppendLine("行已插入");
                    break;
                case "DELETE":
                    result.Append(ExcuteSQLDAL.Delete(queryText)).AppendLine("行已删除");
                    break;
                default:
                    result.AppendLine("SQL语句无法识别，请检查");
                    break;
            }

            return result.ToString();
        }
    }
}
