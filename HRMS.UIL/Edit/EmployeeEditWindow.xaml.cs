using System.Windows;
using System.Windows.Controls;
using HRMS.DAL.Model;
using HRMS.BLL;

namespace HRMS.UIL.Edit
{
    /// <summary>
    /// EditWindow.xaml 的交互逻辑
    /// </summary>
    public partial class EmployeeEditWindow : Window
    {
        // 标识当前窗口是否为编辑窗口
        internal bool IsEdit { set; private get; }
        // 当前编辑的员工ID
        internal string EditID { set; private get; }

        public EmployeeEditWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cmbGender.ItemsSource = EmployeeEditBLL.GetCategory("性别");
            cmbMarital.ItemsSource = EmployeeEditBLL.GetCategory("婚姻状况");
            cmbPlitical.ItemsSource = EmployeeEditBLL.GetCategory("政治面貌");
            cmbDegree.ItemsSource = EmployeeEditBLL.GetCategory("学历");
            cmbDepartment.ItemsSource = EmployeeEditBLL.ListAllDepartment();

            if (IsEdit)
            {
                dgEmployee.DataContext = EmployeeEditBLL.GetEditEmployee(EditID);
            }
            else
            {
                dgEmployee.DataContext = EmployeeEditBLL.InitializeEmployee();
            }
        }

        /// <summary>
        ///  保存编辑员工信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            bool isLegal = true;
            CommonHelper.CheckTextBoxNotEmpty(ref isLegal, txtId, txtName, txtNation, txtBirthplace,
                                                  txtMajor, txtSchool, txtAddress, txtTelephone,
                                                  txtBankAccount, txtJobTitle, txtStaffID,
                                                  txtContract, txtSalary
                                     );
            CommonHelper.CheckComoBoxNotEmpty(ref isLegal, cmbGender, cmbMarital, cmbPlitical, 
                                              cmbDegree, cmbDepartment);

            if (isLegal)
            {
                Employee emp = (Employee)dgEmployee.DataContext;
                if(EmployeeEditBLL.UpdateData(IsEdit, emp) == 1)
                {
                    SystemLogBLL.GenerateSysLog(IsEdit, emp, MainWindow.CurrentOperator);
                }
                this.DialogResult = true;
            }
            else
            {
                return;
            }
        }

        /// <summary>
        /// 取消编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
