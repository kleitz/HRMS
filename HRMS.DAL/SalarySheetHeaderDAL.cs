using HRMS.DAL.Model;
using System.Data.SqlClient;

namespace HRMS.DAL
{
    public class SalarySheetHeaderDAL
    {
        // 新增
        public static int Insert(SalarySheetHeader obj)
        {
            if (obj == null)
                return 0;

            string queryText = @"INSERT INTO SalarySheetHeader(Id, Year, Month, DepartmentId)
	                                                    VALUES(@Id, @Year, @Month, @DepartmentId)";

            SqlParameter[] sqlParams = new SqlParameter[] {
	                                   new SqlParameter("@Id", obj.Id),
	                                   new SqlParameter("@Year", obj.Year),
	                                   new SqlParameter("@Month", obj.Month),
	                                   new SqlParameter("@DepartmentId", obj.DepartmentId) };

            return DBHelper.ExecuteNonQuery(queryText, sqlParams);
        }

        // 查询
        public static SalarySheetHeader[] Select(string searchCondition, SqlParameter[] sqlParams)
        {
            string queryText = @"SELECT * FROM SalarySheetHeader WHERE 1=1 ";
            System.Data.DataTable datab;

            if (searchCondition == null)
            {
                datab = DBHelper.ExecuteDataTable(queryText);
            }
            else
            {
                queryText += searchCondition;
                datab = DBHelper.ExecuteDataTable(queryText, sqlParams);
            }

            if (datab.Rows.Count <= 0)
                return null;

            SalarySheetHeader[] objList = new SalarySheetHeader[datab.Rows.Count];
            for (int i = 0; i < datab.Rows.Count; i++)
            {
                SalarySheetHeader temp = new SalarySheetHeader();
                temp.Id = (System.Guid)datab.Rows[i]["Id"];
                temp.Year = (System.Int32)datab.Rows[i]["Year"];
                temp.Month = (System.Int32)datab.Rows[i]["Month"];
                temp.DepartmentId = (System.Int32)datab.Rows[i]["DepartmentId"];
                temp.IsSettled = (System.Boolean)datab.Rows[i]["IsSettled"];

                objList[i] = temp;
            }

            return objList;
        }

        // 更新
        public static int Update(SalarySheetHeader obj)
        {
            if (obj == null)
                return 0;

            string queryText = @"UPDATE SalarySheetHeader SET IsSettled = 1
	                                                    WHERE Id = @Id";
            SqlParameter[] sqlParams = new SqlParameter[] { 
                                       new SqlParameter("@Id", obj.Id) };

            return DBHelper.ExecuteNonQuery(queryText, sqlParams);
        }

        // 删除
        public static int Delete(SalarySheetHeader obj)
        {
            if (obj == null)
                return 0;

            string queryText = @"DELETE FROM SalarySheetHeader
	                             WHERE Id = @Id";
            SqlParameter sqlParams = new SqlParameter("@Id", obj.Id);

            return DBHelper.ExecuteNonQuery(queryText, sqlParams);
        }
    }
}
