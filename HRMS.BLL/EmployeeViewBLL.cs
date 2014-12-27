using System;
using System.IO;
using System.Collections.Generic;
using System.Data.SqlClient;
using HRMS.DAL;
using HRMS.DAL.Model;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace HRMS.BLL
{
    /// <summary>
    /// 处理显示员工信息列表逻辑
    /// </summary>
    public class EmployeeViewBLL
    {
        /// <summary>
        /// 获取所有员工信息
        /// </summary>
        /// <returns>所有员工</returns>
        public static Employee[] ListAllEmployees()
        {
            return EmployeeDAL.Select(null, null);
        }

        /// <summary>
        /// 根据员工ID或姓名快速查找员工信息
        /// </summary>
        /// <param name="searchKey">员工ID或姓名</param>
        /// <returns>返回符合条件的员工信息类列表</returns>
        public static Employee[] QuickSearch(string searchKey)
        {
            string searchArgs = " AND (Name = @SearchKey OR StaffID = @SearchKey)";
            SqlParameter[] sqlParameters = new SqlParameter[] 
                                               { new SqlParameter("@SearchKey", searchKey) };
            return EmployeeDAL.Select(searchArgs, sqlParameters);
        }
        
        /// <summary>
        /// 根据搜索条件，检索员工信息
        /// </summary>
        /// <param name="name">姓名</param>
        /// <param name="staffID">工号</param>
        /// <param name="department">部门</param>
        /// <param name="beginDate">入职日期范围起始</param>
        /// <param name="endDate">入职日期范围结束</param>
        /// <returns>返回符合检索条件的员工信息</returns>
        public static Employee[] SearchEmployees(string name, string staffID, object department, 
                                                 DateTime? beginDate, DateTime? endDate)
        {
            string searchArgs = null;
            List<SqlParameter> sqlParameters = new List<SqlParameter>();

            if (name.Length > 0)
            {
                searchArgs += " AND Name = @Name";
                sqlParameters.Add(new SqlParameter("@Name", name));
            }       
            if (staffID.Length > 0)
            {
                searchArgs += " AND StaffID = @StaffID";
                sqlParameters.Add(new SqlParameter("@StaffID", staffID));
            }        
            if (department != null)
            {
                searchArgs += " AND Department = @Department";
                sqlParameters.Add(new SqlParameter("@Department", 
                                                   Convert.ToInt32(department)));
            }
            if(beginDate != null && endDate != null)
            {
                searchArgs += " AND (EntryDate BETWEEN @BeginDate AND @EndDate)";
                sqlParameters.Add(new SqlParameter("@BeginDate", Convert.ToDateTime(beginDate)));
                sqlParameters.Add(new SqlParameter("@EndDate", Convert.ToDateTime(endDate)));
            }

            return EmployeeDAL.Select(searchArgs, sqlParameters.ToArray());
        }

        /// <summary>
        /// 将员工信息导出为Excel文件
        /// </summary>
        /// <param name="fileName">导出文件名</param>
        /// <param name="empList">需要导出的员工信息列表</param>
        public static void ExportToExcel(string fileName, Employee[] empList)
        {
            HSSFWorkbook workBook = new HSSFWorkbook();
            ISheet sheet = workBook.CreateSheet("员工信息");

            IRow rowHeader = sheet.CreateRow(0);    // 创建表头
            InitCell(rowHeader, 0, "身份证号");
            InitCell(rowHeader, 1, "姓名");
            InitCell(rowHeader, 2, "性别");
            InitCell(rowHeader, 3, "生日");
            InitCell(rowHeader, 4, "入职日期");
            InitCell(rowHeader, 5, "婚姻状况");
            InitCell(rowHeader, 6, "政治面貌");
            InitCell(rowHeader, 7, "民族");
            InitCell(rowHeader, 8, "籍贯");
            InitCell(rowHeader, 9, "学历");
            InitCell(rowHeader, 10, "专业");
            InitCell(rowHeader, 11, "毕业院校");
            InitCell(rowHeader, 12, "联系地址");
            InitCell(rowHeader, 13, "邮箱");
            InitCell(rowHeader, 14, "联系电话");
            InitCell(rowHeader, 15, "银行账号");
            InitCell(rowHeader, 16, "部门");
            InitCell(rowHeader, 17, "职称");
            InitCell(rowHeader, 18, "工号");
            InitCell(rowHeader, 19, "合同期限");
            InitCell(rowHeader, 20, "备注");

            for(int i = 0; i < empList.Length; i++)
            {
                IRow row = sheet.CreateRow(i + 1);
                InitCell(row, 0, empList[i].Id);
                InitCell(row, 1, empList[i].Name);
                InitCell(row, 2, empList[i].Gender);
                InitCell(workBook, row, 3, empList[i].Birthday);
                InitCell(workBook, row, 4, empList[i].EntryDate);
                InitCell(row, 5, empList[i].MaritalStatus);
                InitCell(row, 6, empList[i].PoliticalStatus);
                InitCell(row, 7, empList[i].Nation);
                InitCell(row, 8, empList[i].Birthplace);
                InitCell(row, 9, empList[i].Degree);
                InitCell(row, 10, empList[i].Major);
                InitCell(row, 11, empList[i].School);
                InitCell(row, 12, empList[i].Address);
                InitCell(row, 13, empList[i].Email);
                InitCell(row, 14, empList[i].Telephone);
                InitCell(row, 15, empList[i].BankAccount);
                InitCell(row, 16, empList[i].Department);
                InitCell(row, 17, empList[i].JobTitle);
                InitCell(row, 18, empList[i].StaffID);
                InitCell(row, 19, empList[i].ContractPeriod);
                InitCell(row, 20, empList[i].Remark);
            }

            using(Stream stream = File.OpenWrite(fileName))
            {
                workBook.Write(stream);
            }
        }

        // 处理字符串类型的单元格
        private static void InitCell(IRow row, int column, string value)
        {
            row.CreateCell(column, CellType.String).SetCellValue(value);
        }
        // 处理整形的单元格
        private static void InitCell(IRow row, int column, int value)
        {
            row.CreateCell(column, CellType.Numeric).SetCellValue(value);
        }
        // 处理日期类型的单元格
        private static void InitCell(IWorkbook workBook, IRow row, int column, System.DateTime value)
        {
            
            ICellStyle style = workBook.CreateCellStyle();
            IDataFormat format = workBook.CreateDataFormat();
            style.DataFormat = format.GetFormat("yyyy\"年\"m\"月\"d\"日\"");

            ICell cell = row.CreateCell(column, CellType.Numeric);
            cell.CellStyle = style;
            cell.SetCellValue(value);
        }
    }
}
