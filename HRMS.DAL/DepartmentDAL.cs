using HRMS.DAL.Model;
using System.Data.SqlClient;

namespace HRMS.DAL
{
    /// <summary>
    /// 部门类与数据库交互的实现类
    /// </summary>
    public class DepartmentDAL
    {
        /// <summary>
        /// 向数据库中新增部门
        /// </summary>
        /// <param name="department">部门信息类</param>
        /// <returns>返回新增行数，正常则为1；否则返回0</returns>
        public static int Insert(Department dept)
        {
            if (dept.Name == null)
                return 0;

            string queryText = @"INSERT INTO Department(Name) VALUES(@Name)";
            SqlParameter sqlParameters = new SqlParameter("@Name", dept.Name);
            return DBHelper.ExecuteNonQuery(queryText, sqlParameters);
        }

        /// <summary>
        /// 根据部门Id查询部门信息
        /// </summary>
        /// <param name="id">部门Id</param>
        /// <returns>返回部门信息，如果部门信息不存在或部门已被取消，返回空引用</returns>
        public static Department[] Select(string name)
        {
            string queryText;
            System.Data.DataTable dataTable;
            
            if(name == null)
            {
                queryText = "SELECT Id, Name FROM Department WHERE IsCanceled = 0";
                dataTable = DBHelper.ExecuteDataTable(queryText);
            }
            else
            {
                queryText = @"SELECT Id, Name FROM Department 
                                 WHERE Name = @Name AND IsCanceled = 0";
                SqlParameter sqlParameters = new SqlParameter("@Name", name);
                dataTable = DBHelper.ExecuteDataTable(queryText, sqlParameters);
            }

            if (dataTable.Rows.Count <= 0)
                return null;
            
            Department[] tempDept = new Department[dataTable.Rows.Count];
            for (int i = 0; i < dataTable.Rows.Count; i++ )
            {
                Department temp = new Department();
                temp.Id = (int)dataTable.Rows[i]["Id"];
                temp.Name = (string)dataTable.Rows[i]["Name"];
                tempDept[i] = temp;
            }

            return tempDept;
        }

        /// <summary>
        /// 向数据库更新部门信息
        /// </summary>
        /// <param name="dept">需要更新的部门信息</param>
        /// <returns>返回更新条目，正常为1；否则，返回0</returns>
        public static int Update(Department dept)
        {
            if (dept.Name.Length <= 0)
                return 0;

            string queryText = @"UPDATE Department SET Name = @Name 
                                 WHERE Id = @Id AND IsCanceled = 0";
            SqlParameter[] sqlParameters =
                new SqlParameter[] { new SqlParameter("@Name", dept.Name), 
                                     new SqlParameter("@Id", dept.Id) };
            return DBHelper.ExecuteNonQuery(queryText, sqlParameters);
        }

        /// <summary>
        /// 取缔部门操作
        /// </summary>
        /// <param name="dept">需要取缔的部门信息</param>
        /// <returns>返回取缔条目，正常为1；否则，返回0</returns>
        public static int Delete(Department dept)
        {
            if (dept.Name.Length <= 0)
                return 0;

            string queryText = @"UPDATE Department SET IsCanceled = 1 
                                 WHERE Id = @Id AND IsCanceled = 0";
            SqlParameter sqlParameters = new SqlParameter("@Id", dept.Id);
            return DBHelper.ExecuteNonQuery(queryText, sqlParameters);
        }
    }
}
