using System;
using HRMS.DAL;
using HRMS.DAL.Model;

namespace HRMS.BLL
{
    public class EmployeeEditBLL
    {
        /// <summary>
        /// 获取指定员工信息
        /// </summary>
        /// <param name="staffID">员工ID</param>
        /// <returns>返回员工信息类</returns>
        public static Employee GetEditEmployee(string staffID)
        {
            if (staffID == null || staffID.Length <= 0)
                return null;
            Employee[] empList = EmployeeViewBLL.QuickSearch(staffID);
            if (empList.Length != 1)
                return null;
            return empList[0];
        }

        /// <summary>
        /// 初始化员工基本信息
        /// </summary>
        /// <returns>返回初始化后的员工信息类</returns>
        public static Employee InitializeEmployee()
        {
            Employee emp = new Employee();
            emp.Birthday = Convert.ToDateTime("1980-01-01");
            emp.EntryDate = DateTime.Today;
            emp.ContractPeriod = emp.EntryDate.ToString("yyyy.MM.dd") + " - " 
                               + emp.EntryDate.AddYears(2).ToString("yyyy.MM.dd");

            return emp;
        }

        /// <summary>
        /// 获取通用类别信息
        /// </summary>
        /// <param name="type">类别</param>
        /// <returns>返回给类别信息列表</returns>
        public static Category[] GetCategory(string type)
        {
            if (type == null || type.Length <= 0)
                return null;
            return CategoryDAL.Select(type);
        }

        /// <summary>
        /// 获取所有部门信息
        /// </summary>
        /// <returns>返回所有部门信息</returns>
        public static Department[] ListAllDepartment()
        {
            return DepartmentDAL.Select(null);
        }

        /// <summary>
        /// 新增或更新数据库中的员工信息
        /// </summary>
        /// <param name="isEdit">标识新增或更新</param>
        /// <param name="emp">需要新增或更新的员工信息类</param>
        /// <returns>返回新增或更新的条目，失败返回0</returns>
        public static int UpdateData(bool isEdit, Employee emp)
        {
            if (emp == null)
                return 0;

            if(isEdit)
            {
                return EmployeeDAL.Update(emp);
            }
            else
            {
                return EmployeeDAL.Insert(emp);
            }
        }

        /// <summary>
        /// 删除数据库中指定的员工信息
        /// </summary>
        /// <param name="emp">需要删除的员工信息类</param>
        /// <returns>删除的员工信息条目，失败则返回0</returns>
        public static int DeleteEmployee(Employee emp)
        {
            if (emp == null)
                return 0;

            return EmployeeDAL.Delete(emp);
        }
    }
}
