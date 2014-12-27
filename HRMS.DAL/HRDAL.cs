using HRMS.DAL.Model;
using System.Data.SqlClient;

namespace HRMS.DAL
{
    /// <summary>
    /// 与登录信息相关的数据库操作
    /// </summary>
    public class HRDAL
    {
        /// <summary>
        /// 向数据库中新增登录信息
        /// </summary>
        /// <param name="hr">登录信息类</param>
        /// <returns>返回新增行数，正常则为1；否则返回0</returns>
        public static int Insert(HR hr)
        {
            if (hr.UserName.Length <= 0)
                return 0;

            string queryCmdText = "INSERT INTO HR(UserName, Password) VALUES(@UserName, @Password)";
            SqlParameter[] sqlParameters =
                new SqlParameter[] { new SqlParameter("@UserName", hr.UserName),
                                     new SqlParameter("@Password", hr.Password) };

            return DBHelper.ExecuteNonQuery(queryCmdText, sqlParameters);
        }

        /// <summary>
        /// 通过用户名查询登录信息
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>返回登录信息类，如果查询不到，返回空引用</returns>
        public static HR[] Select(string userName)
        {
            string queryText;
            System.Data.DataTable datab;

            // 用户名为空
            if(userName == null)
            {
                queryText = @"SELECT UserName, Password, IsLocked FROM HR WHERE IsDeleted = 0";
                datab = DBHelper.ExecuteDataTable(queryText);
            }
            else
            {
                queryText = @"SELECT UserName, Password, IsLocked FROM HR 
                                     WHERE UserName = @UserName AND IsDeleted = 0";
                SqlParameter sqlParams = new SqlParameter("@UserName", userName);
                datab = DBHelper.ExecuteDataTable(queryText, sqlParams);
            }

            // 用户不存在
            if (datab.Rows.Count <= 0)
                return null;

            HR[] hr = new HR[datab.Rows.Count];
            System.Data.DataRow dataRow;
            for(int i = 0; i < datab.Rows.Count; i++)
            {
                dataRow = datab.Rows[i];
                HR temp = new HR();
                temp.UserName = (string)dataRow["UserName"];
                temp.Password = (string)dataRow["Password"];
                temp.IsLocked = (bool)dataRow["IsLocked"];
                hr[i] = temp;
            }

            return hr;
        }

        /// <summary>
        /// 更新登录信息
        /// </summary>
        /// <param name="hr">需要更新的登录信息</param>
        /// <returns>返回更新条目，正常为1；否则，返回0</returns>
        public static int Update(HR hr)
        {
            if (hr.UserName.Length <= 0)
                return 0;

            string queryCmdText = @"UPDATE HR SET Password = @Password, IsLocked = @IsLocked 
                                    WHERE UserName = @UserName";
            SqlParameter[] sqlParameters =
                new SqlParameter[] { new SqlParameter("@UserName", hr.UserName),
                                     new SqlParameter("@Password", hr.Password),
                                     new SqlParameter("@IsLocked", hr.IsLocked) };

            return DBHelper.ExecuteNonQuery(queryCmdText, sqlParameters);
        }

        /// <summary>
        /// 删除指定登录信息
        /// </summary>
        /// <param name="hr">需要删除的登录信息</param>
        /// <returns>返回删除条目，正常为1；否则，返回0</returns>
        public static int Delete(HR hr)
        {
            if (hr.UserName.Length <= 0)
                return 0;

            string queryCmdText = @"UPDATE HR SET IsDeleted = 1 WHERE UserName = @UserName";
            SqlParameter[] sqlParameters =
                new SqlParameter[] { new SqlParameter("@UserName", hr.UserName) };

            return DBHelper.ExecuteNonQuery(queryCmdText, sqlParameters);
        }
    }
}
