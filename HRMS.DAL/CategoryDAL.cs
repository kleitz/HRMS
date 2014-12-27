using HRMS.DAL.Model;
using System;

namespace HRMS.DAL
{
    /// <summary>
    /// 通用类别信息与数据库交互处理类
    /// </summary>
    public class CategoryDAL
    {
        /// <summary>
        /// 获取类别的具体分类
        /// </summary>
        /// <param name="category">要获取的类别</param>
        /// <returns>具体类别信息的数字</returns>
        public static Category[] Select(string category)
        {
            if (category.Length <= 0)
                return null;

            string queryText = @"SELECT Id, Name FROM GeneralCategory WHERE Category = @Category";
            System.Data.SqlClient.SqlParameter sqlParameters = 
                new System.Data.SqlClient.SqlParameter("@Category", category);
            System.Data.DataTable dataTable = DBHelper.ExecuteDataTable(queryText, sqlParameters);

            if (dataTable.Rows.Count <= 0)
                return null;
            
            Category[] tempCategory = new Category[dataTable.Rows.Count];
            for (int i = 0; i < dataTable.Rows.Count; i++ )
            {
                Category temp = new Category();
                temp.Id = (int)dataTable.Rows[i]["Id"];
                temp.Name = (string)dataTable.Rows[i]["Name"];
                tempCategory[i] = temp;
            }

            return tempCategory;
        }

        /// <summary>
        /// 根据类别Id，获取类别名称
        /// </summary>
        /// <param name="id">类别标识</param>
        /// <returns>类别名称</returns>
        public static string Select(Guid id)
        {
            string cateName;
            string queryText = @"SELECT Name FROM GeneralCategory WHERE Id = @Id";
            System.Data.SqlClient.SqlParameter sqlParameters =
                new System.Data.SqlClient.SqlParameter("@Id", id);
            cateName = (string)DBHelper.ExecuteScalar(queryText, sqlParameters);

            return cateName;
        }
    }
}
