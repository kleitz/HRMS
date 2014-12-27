using System;
using System.Data.SqlClient;
using HRMS.DAL;
using HRMS.DAL.Model;

namespace HRMS.BLL
{
    /// <summary>
    /// 工资单的操作
    /// </summary>
    public class SalarySheetBLL
    {
        // 工资单是否已经生成
        public static bool IsExisted { private set; get; }
        // 工资单是否已经结算
        public static bool IsSettled { private set; get; }
        // 当前处理的工资单
        private static SalarySheetHeader header = new SalarySheetHeader();

        /// <summary>
        /// 查询工资单
        /// </summary>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        /// <param name="departmentId">部门</param>
        /// <returns>返回工资单数据</returns>
        public static SalarySheet[] View(object year, object month, object departmentId)
        {
            if (year == null || month == null || departmentId == null)
                return null;

            header.Year = Convert.ToInt32(year);
            header.Month = Convert.ToInt32(month);
            header.DepartmentId = Convert.ToInt32(departmentId);

            string searchArgs = @" AND Year = @Year AND Month = @Month AND DepartmentId = @DepartmentId";
            SqlParameter[] sqlParams = new SqlParameter[] {
                                       new SqlParameter("@Year", header.Year), 
                                       new SqlParameter("@Month", header.Month), 
                                       new SqlParameter("@DepartmentId", header.DepartmentId) };

            SalarySheetHeader[] headerList = SalarySheetHeaderDAL.Select(searchArgs, sqlParams);

            if (headerList == null)
            {
                IsExisted = false;
                IsSettled = false;
                header.Id = Guid.NewGuid();
                header.IsSettled = false;

                return null;
            }

            if (headerList.Length > 1)
            {
                IsExisted = true;

                return null;
            }

            IsExisted = true;
            IsSettled = headerList[0].IsSettled;
            header.Id = headerList[0].Id;
            header.IsSettled = IsSettled;

            searchArgs = @" AND SheetID = @SheetID";
            sqlParams = new SqlParameter[] {
                        new SqlParameter("@SheetID", header.Id) };

            return SalarySheetDAL.Select(searchArgs, sqlParams);
        }

        /// <summary>
        /// 生成当月工资单
        /// </summary>
        /// <returns>返回工资单数据</returns>
        public static SalarySheet[] Generate()
        {
            string searchEmpArgs = " AND Department = @Department";
            SqlParameter[] sqlParams = new SqlParameter[] {
                                       new SqlParameter("@Department", header.DepartmentId) };
            Employee[] empList = EmployeeDAL.Select(searchEmpArgs, sqlParams);

            if (empList == null)
                return null;

            SalarySheet[] sheet = new SalarySheet[empList.Length];
            for (int i = 0; i < empList.Length; i++)
            {
                SalarySheet temp = new SalarySheet();
                temp.StaffID = empList[i].StaffID;
                temp.Name = empList[i].Name;
                temp.SheetID = header.Id;
                temp.BaseSalary = empList[i].Salary;
                temp.Bonus = 0;
                temp.Fine = 0;

                sheet[i] = temp;
            }

            if (SalarySheetHeaderDAL.Insert(header) == 1)
            {
                foreach(SalarySheet ss in sheet)
                {
                    SalarySheetDAL.Insert(ss);
                }
            }

            IsExisted = true;
            IsSettled = false;

            return sheet;
        }

        /// <summary>
        /// 计算当月工资
        /// </summary>
        /// <returns>返回结算条目</returns>
        public static int Settle()
        {
            IsSettled = true;
            return SalarySheetHeaderDAL.Update(header);
        }

        /// <summary>
        /// 清除已存在的表单
        /// </summary>
        /// <returns>返回删除表单的条目</returns>
        public static int Clear()
        {
            if (SalarySheetDAL.Delete(header.Id) > 0)
            {
                return SalarySheetHeaderDAL.Delete(header);
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 更新工资单
        /// </summary>
        /// <param name="item">需要更新项</param>
        /// <returns>返回更新条目</returns>
        public static int Update(SalarySheet item)
        {
            if (item == null)
                return 0;

            return SalarySheetDAL.Update(item);
        }
    }
}
