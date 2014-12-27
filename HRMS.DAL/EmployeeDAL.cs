using System;
using System.Data;
using System.Data.SqlClient;
using HRMS.DAL.Model;

namespace HRMS.DAL
{
    /// <summary>
    /// 员工信息的数据库操作
    /// </summary>
    public class EmployeeDAL
    {
        /// <summary>
        /// 向数据库中新增员工信息
        /// </summary>
        /// <param name="emp">需要新增的员工信息类</param>
        /// <returns>返回新增的条目，失败返回0</returns>
    	public static int Insert(Employee emp)
    	{
    		if(emp == null)
    			return 0;

    		string queryText = @"INSERT INTO Employee(Id, Name, Gender, Birthday, EntryDate, MaritalStatus, PoliticalStatus, 
                                                      Nation, Birthplace, Degree, Major, School, [Address], Email, Telephone, 
                                                      BankAccount, Department, JobTitle, StaffID, ContractPeriod, Remark, Salary)
			  								   VALUES(@Id, @Name, @Gender, @Birthday, @EntryDate, @MaritalStatus, @PoliticalStatus, 
                                                      @Nation, @Birthplace, @Degree, @Major, @School, @Address, @Email, @Telephone, 
                                                      @BankAccount, @Department, @JobTitle, @StaffID, @ContractPeriod, @Remark, @Salary)";
			SqlParameter[] sqlParameters = new SqlParameter[] {
				new SqlParameter("@Id", emp.Id), 
                new SqlParameter("@Name", emp.Name),
				new SqlParameter("@Gender", emp.Gender), 
                new SqlParameter("@Birthday", emp.Birthday),
				new SqlParameter("@EntryDate", emp.EntryDate), 
                new SqlParameter("@MaritalStatus", emp.MaritalStatus),
				new SqlParameter("@PoliticalStatus", emp.PoliticalStatus), 
                new SqlParameter("@Nation", emp.Nation),
                new SqlParameter("@Birthplace", emp.Birthplace),
                new SqlParameter("@Degree", emp.Degree),
				new SqlParameter("@Major", emp.Major), 
                new SqlParameter("@School", emp.School),
				new SqlParameter("@Address", emp.Address), 
                new SqlParameter("@Email", DBHelper.ConvertToDBValue(emp.Email)),
				new SqlParameter("@Telephone", emp.Telephone), 
                new SqlParameter("@BankAccount", emp.BankAccount),
				new SqlParameter("@Department", emp.Department), 
                new SqlParameter("@JobTitle", emp.JobTitle),
				new SqlParameter("@StaffID", emp.StaffID), 
                new SqlParameter("@ContractPeriod", emp.ContractPeriod), 
                new SqlParameter("@Remark", DBHelper.ConvertToDBValue(emp.Remark)),
				new SqlParameter("@Salary", emp.Salary) };

			return DBHelper.ExecuteNonQuery(queryText, sqlParameters);
    	}

        /// <summary>
        /// 查询员工信息
        /// </summary>
        /// <param name="searchKey">查询文本（姓名；工号；空：查询所有）</param>
        /// <returns>返回员工信息</returns>
        public static Employee[] Select(string searchKey, SqlParameter[] sqlParams)
    	{
            string queryText = @"SELECT * FROM Employee WHERE IsFired = 0";
            DataTable dataTable;

            if(searchKey == null)
            {
                dataTable = DBHelper.ExecuteDataTable(queryText);
            } 
            else
            {
                queryText += searchKey;
                dataTable = DBHelper.ExecuteDataTable(queryText, sqlParams);
            }

    		if(dataTable.Rows.Count <= 0)
    			return null;

    		Employee[] empList = new Employee[dataTable.Rows.Count];
    		for(int i = 0; i < dataTable.Rows.Count; i++)
    		{
                Employee temp = new Employee();
    			temp.Id = (string)dataTable.Rows[i]["Id"];
                temp.Name = (string)dataTable.Rows[i]["Name"];
                temp.Gender = (int)dataTable.Rows[i]["Gender"];
                temp.Birthday = (DateTime)dataTable.Rows[i]["Birthday"];
                temp.EntryDate = (DateTime)dataTable.Rows[i]["EntryDate"];
                temp.MaritalStatus = (int)dataTable.Rows[i]["MaritalStatus"];
                temp.PoliticalStatus = (int)dataTable.Rows[i]["PoliticalStatus"];
                temp.Nation = (string)dataTable.Rows[i]["Nation"];
                temp.Birthplace = (string)dataTable.Rows[i]["Birthplace"];
                temp.Degree = (int)dataTable.Rows[i]["Degree"];
                temp.Major = (string)dataTable.Rows[i]["Major"];
                temp.School = (string)dataTable.Rows[i]["School"];
                temp.Address = (string)dataTable.Rows[i]["Address"];
                temp.Email = (string)DBHelper.ConvertToCSValue(dataTable.Rows[i]["Email"]);
                temp.Telephone = (string)dataTable.Rows[i]["Telephone"];
                temp.BankAccount = (string)dataTable.Rows[i]["BankAccount"];
                temp.Department = (int)dataTable.Rows[i]["Department"];
                temp.JobTitle = (string)dataTable.Rows[i]["JobTitle"];
                temp.StaffID = (string)dataTable.Rows[i]["StaffID"];
                temp.ContractPeriod = (string)dataTable.Rows[i]["ContractPeriod"];
                temp.Remark = (string)DBHelper.ConvertToCSValue(dataTable.Rows[i]["Remark"]);
                temp.Salary = (int)dataTable.Rows[i]["Salary"];

                empList[i] = temp;
    		}

    		return empList;
    	}

        /// <summary>
        /// 更新数据库员工信息
        /// </summary>
        /// <param name="emp">需要更新的员工信息类</param>
        /// <returns>返回更新条目，失败返回0</returns>
    	public static int Update(Employee emp)
    	{
    		if(emp == null)
    		    return 0;

    		string queryText = @"UPDATE Employee SET Id = @Id, Name = @Name, Gender = @Gender, Birthday = @Birthday, EntryDate = @EntryDate, 
                                                     MaritalStatus = @MaritalStatus, PoliticalStatus = @PoliticalStatus, Nation = @Nation, 
                                                     Birthplace = @Birthplace, Degree = @Degree, Major = @Major, School = @School, 
                                                     Address = @Address, Email = @Email, Telephone = @Telephone, BankAccount = @BankAccount, 
                                                     Department = @Department, JobTitle = @JobTitle, ContractPeriod = @ContractPeriod, 
                                                     Remark = @Remark, Salary = @Salary 
                                                     WHERE StaffID = @StaffID AND IsFired = 0";
    		SqlParameter[] sqlParameters = new SqlParameter[] {
				new SqlParameter("@Id", emp.Id), 
                new SqlParameter("@Name", emp.Name),
				new SqlParameter("@Gender", emp.Gender), 
                new SqlParameter("@Birthday", emp.Birthday),
				new SqlParameter("@EntryDate", emp.EntryDate), 
                new SqlParameter("@MaritalStatus", emp.MaritalStatus),
				new SqlParameter("@PoliticalStatus", emp.PoliticalStatus), 
                new SqlParameter("@Nation", emp.Nation),
                new SqlParameter("@Birthplace", emp.Birthplace),
                new SqlParameter("@Degree", emp.Degree),
				new SqlParameter("@Major", emp.Major), 
                new SqlParameter("@School", emp.School),
				new SqlParameter("@Address", emp.Address), 
                new SqlParameter("@Email", DBHelper.ConvertToDBValue(emp.Email)),
				new SqlParameter("@Telephone", emp.Telephone), 
                new SqlParameter("@BankAccount", emp.BankAccount),
				new SqlParameter("@Department", emp.Department), 
                new SqlParameter("@JobTitle", emp.JobTitle),
				new SqlParameter("@StaffID", emp.StaffID), 
                new SqlParameter("@ContractPeriod", emp.ContractPeriod), 
                new SqlParameter("@Remark", DBHelper.ConvertToDBValue(emp.Remark)),
				new SqlParameter("@Salary", emp.Salary) };

			return DBHelper.ExecuteNonQuery(queryText, sqlParameters);
    	}

        /// <summary>
        /// 删除员工信息
        /// </summary>
        /// <param name="emp">需要删除的员工信息</param>
        /// <returns>返回删除条目，失败返回0</returns>
    	public static int Delete(Employee emp)
    	{
    		if(emp == null)
    			return 0;

    		string queryText = @"UPDATE Employee SET IsFired = 1 WHERE StaffID = @StaffID";
    		SqlParameter sqlParameters = new SqlParameter("@StaffID", emp.StaffID);

    		return DBHelper.ExecuteNonQuery(queryText, sqlParameters);
    	}
    }
}
