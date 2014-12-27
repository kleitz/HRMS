using System;
using System.Windows;
using System.Configuration;
using System.Collections.Generic;
using HRMS.BLL;
using HRMS.DAL.Model;

namespace HRMS.UIL
{
    /// <summary>
    /// ReminderWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ReminderWindow : Window
    {
        // 标识是否有员工生日需要提醒
        internal bool IsNull { private set; get; }
        public ReminderWindow()
        {
            InitializeComponent();

            Employee[] allEmps = EmployeeViewBLL.ListAllEmployees();
            List<Employee> empList = new List<Employee>();
            int RemindDay = Convert.ToInt32(ConfigurationManager.AppSettings["ReminderDays"]);

            foreach (Employee emp in allEmps)
            {
                DateTime birth = new DateTime(DateTime.Now.Year, emp.Birthday.Month, emp.Birthday.Day);
                if (birth >= DateTime.Now && birth <= DateTime.Now.AddDays(RemindDay))
                {
                    empList.Add(emp);
                }
            }

            if (empList.Count <= 0)
            {
                IsNull = true;
            }
            else
            {
                IsNull = false;
                dgBirthday.ItemsSource = empList;
            }  
        }
    }
}
