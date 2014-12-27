using System.Windows;
using HRMS.UIL.Edit;
using HRMS.BLL;
using HRMS.DAL.Model;

namespace HRMS.UIL.View
{
    /// <summary>
    /// ViewWindow.xaml 的交互逻辑
    /// </summary>
    public partial class EmployeeViewWindow : Window
    {
        // 是否查看所有员工信息
        internal bool listAll = true;

        public EmployeeViewWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // 初始化检索条件
            gbcmbDepartment.ItemsSource = EmployeeEditBLL.ListAllDepartment();
            gbdtpBegin.SelectedDate = System.DateTime.Today.AddMonths(-1);
            gbdtpEnd.SelectedDate = System.DateTime.Today.AddDays(1);

            dgcmbDegree.ItemsSource = EmployeeEditBLL.GetCategory("学历");
            dgcmbDepartment.ItemsSource = EmployeeEditBLL.ListAllDepartment();

            if (listAll)
                Refresh();
            else
            {
                btnAdd.IsEnabled = false;
                btnEdit.IsEnabled = false;
                btnDelete.IsEnabled = false;
            }
        }

        /// <summary>
        /// 新增员工操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            btnDelete.IsEnabled = false;
            btnEdit.IsEnabled = false;

            EmployeeEditWindow addEmp = new EmployeeEditWindow();
            addEmp.IsEdit = false;
            // 添加成功后，更新员工信息列表
            if(addEmp.ShowDialog() == true)
            {
                Refresh();
            }

            btnDelete.IsEnabled = true;
            btnEdit.IsEnabled = true;
        }

        /// <summary>
        /// 删除指定员工信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgEmployee.SelectedItem == null)
            {
                MessageBox.Show("请选择要删除的员工信息", "提示");
                return;
            }
            Employee emp = (Employee)dgEmployee.SelectedItem;
            if(EmployeeEditBLL.DeleteEmployee(emp) == 1)
            {
                SystemLogBLL.GenerateSysLog(null, emp, MainWindow.CurrentOperator);
            }
            Refresh();
        }

        /// <summary>
        /// 编辑指定员工信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if(dgEmployee.SelectedItem == null)
            {
                MessageBox.Show("请选择要编辑的员工信息", "提示");
                return;
            }

            EmployeeEditWindow editEmp = new EmployeeEditWindow();
            editEmp.IsEdit = true;
            Employee employee = (Employee)dgEmployee.SelectedItem;
            editEmp.EditID = employee.StaffID;

            if(editEmp.ShowDialog() == true)
            {
                Refresh();
            }
        }

        /// <summary>
        /// 将员工信息导出为Excel表格
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog sfd = 
                            new Microsoft.Win32.SaveFileDialog();
            sfd.Filter = "Excel文件|*.xls";

            if(sfd.ShowDialog() != true)
            {
                return;
            }

            string fileName = sfd.FileName;
            Employee[] empList = (Employee[])dgEmployee.ItemsSource;
            EmployeeViewBLL.ExportToExcel(fileName, empList);
            MessageBox.Show("导出完成", "提示");
        }

        /// <summary>
        /// 根据搜索条件检索员工信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            if(gbdtpBegin.SelectedDate > gbdtpEnd.SelectedDate)
            {
                MessageBox.Show("日期范围设置有误", "错误");
                return;
            }

            dgEmployee.ItemsSource = EmployeeViewBLL.SearchEmployees(txtName.Text, txtStaffID.Text,
                                                             gbcmbDepartment.SelectedValue, 
                                                             gbdtpBegin.SelectedDate,
                                                             gbdtpEnd.SelectedDate);
        }

        /// <summary>
        /// 刷新员工信息列表
        /// </summary>
        private void Refresh()
        {
            dgEmployee.ItemsSource = EmployeeViewBLL.ListAllEmployees();
        }
    }
}
