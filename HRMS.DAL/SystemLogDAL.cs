using HRMS.DAL.Model;
using System.Data.SqlClient;

namespace HRMS.DAL
{
    public class SystemLogDAL
    {
        public static int Insert(SystemLog obj)
        {
            if (obj == null)
                return 0;

            string queryText = @"INSERT INTO SystemLog(Type, Operator, TableName, 
                                                       PrimaryKey, Describe, Time)
	                                            VALUES(@Type, @Operator, @TableName, 
                                                       @PrimaryKey, @Describe, @Time)";

            SqlParameter[] sqlParams = new SqlParameter[] {
	                                   new SqlParameter("@Type", obj.Type),
	                                   new SqlParameter("@Operator", obj.Operator),
	                                   new SqlParameter("@TableName", obj.TableName),
	                                   new SqlParameter("@PrimaryKey", obj.PrimaryKey),
	                                   new SqlParameter("@Describe", obj.Describe),
	                                   new SqlParameter("@Time", obj.Time) };

            return DBHelper.ExecuteNonQuery(queryText, sqlParams);
        }

        public static SystemLog[] Select(string searchCondition, SqlParameter[] sqlParams)
        {
            string queryText = @"SELECT * FROM SystemLog WHERE 1=1 ";
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
            SystemLog[] objList = new SystemLog[datab.Rows.Count];
            for (int i = 0; i < datab.Rows.Count; i++)
            {
                SystemLog temp = new SystemLog();
                temp.Id = (System.Int32)datab.Rows[i]["Id"];
                temp.Type = (System.String)datab.Rows[i]["Type"];
                temp.Operator = (System.String)datab.Rows[i]["Operator"];
                temp.TableName = (System.String)datab.Rows[i]["TableName"];
                temp.PrimaryKey = (System.String)datab.Rows[i]["PrimaryKey"];
                temp.Describe = (System.String)datab.Rows[i]["Describe"];
                temp.Time = (System.DateTime)datab.Rows[i]["Time"];

                objList[i] = temp;
            }

            return objList;
        }
    }
}
