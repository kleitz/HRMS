using HRMS.DAL.Model;
using System.Data.SqlClient;

namespace HRMS.DAL
{
    public class SalarySheetDAL
    {
        // 新增
        public static int Insert(SalarySheet obj)
        {
            if (obj == null)
                return 0;

            string queryText = @"INSERT INTO SalarySheet(StaffID, Name, SheetID, BaseSalary, Bonus, Fine)
	                                              VALUES(@StaffID, @Name, @SheetID, @BaseSalary, @Bonus, @Fine)";

            SqlParameter[] sqlParams = new SqlParameter[] {
	                                   new SqlParameter("@StaffID", obj.StaffID),
                                       new SqlParameter("@Name", obj.Name), 
	                                   new SqlParameter("@SheetID", obj.SheetID),
	                                   new SqlParameter("@BaseSalary", obj.BaseSalary),
	                                   new SqlParameter("@Bonus", obj.Bonus),
	                                   new SqlParameter("@Fine", obj.Fine) };

            return DBHelper.ExecuteNonQuery(queryText, sqlParams);
        }

        // 查询
        public static SalarySheet[] Select(string searchCondition, SqlParameter[] sqlParams)
        {
            string queryText = @"SELECT * FROM SalarySheet WHERE 1=1 ";
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

            SalarySheet[] objList = new SalarySheet[datab.Rows.Count];
            for (int i = 0; i < datab.Rows.Count; i++)
            {
                SalarySheet temp = new SalarySheet();
                temp.StaffID = (System.String)datab.Rows[i]["StaffID"];
                temp.Name = (System.String)datab.Rows[i]["Name"];
                temp.SheetID = (System.Guid)datab.Rows[i]["SheetID"];
                temp.BaseSalary = (System.Int32)datab.Rows[i]["BaseSalary"];
                temp.Bonus = (System.Int32)datab.Rows[i]["Bonus"];
                temp.Fine = (System.Int32)datab.Rows[i]["Fine"];

                objList[i] = temp;
            }

            return objList;
        }

        // 更新
        public static int Update(SalarySheet obj)
        {
            if (obj == null)
                return 0;

            string queryText = @"UPDATE SalarySheet SET Bonus = @Bonus, Fine = @Fine 
	                                                WHERE StaffID = @StaffID AND SheetID = @SheetID";
            SqlParameter[] sqlParams = new SqlParameter[] {
	                                   new SqlParameter("@Bonus", obj.Bonus),
	                                   new SqlParameter("@Fine", obj.Fine), 
                                       new SqlParameter("@StaffID", obj.StaffID), 
                                       new SqlParameter("@SheetID", obj.SheetID) };

            return DBHelper.ExecuteNonQuery(queryText, sqlParams);
        }

        // 删除，根据表单ID删除整张表
        public static int Delete(System.Guid sheetID)
        {
            if (sheetID == null)
                return 0;

            string queryText = @"DELETE FROM SalarySheet 
	                             WHERE SheetID = @SheetID";
            SqlParameter sqlParams = new SqlParameter("@SheetID", sheetID);

            return DBHelper.ExecuteNonQuery(queryText, sqlParams);
        }
    }
}
