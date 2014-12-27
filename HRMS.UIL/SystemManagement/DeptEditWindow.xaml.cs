using System.Windows;
using HRMS.BLL;
using HRMS.DAL.Model;

namespace HRMS.UIL.SystemManagement
{
    /// <summary>
    /// DeptEditWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DeptEditWindow : Window
    {
        // 标识新增部门或修改部门名称
        internal bool IsEdit { set; private get; }
        // 当前编辑的部门信息
        internal Department Dept { set; private get; }
        // 原始的部门名称
        private string OriginalDeptName { set; get; }

        public DeptEditWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            gdDepartment.DataContext = Dept;
            OriginalDeptName = Dept.Name;
        }

        // 保存更新
        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            bool isLegal = true;
            CommonHelper.CheckTextBoxNotEmpty(ref isLegal, txtDeptName);

            if(isLegal)
            {
                Department dept = (Department)gdDepartment.DataContext;
                if(!dept.Name.Equals(OriginalDeptName))
                {
                    int UpdateResult = DepartmentManagement.UpdateData(IsEdit, dept);
                    if(UpdateResult == 1)
                    {
                        SystemLogBLL.GenerateSysLog(IsEdit, dept, MainWindow.CurrentOperator);
                    }
                    if(UpdateResult > 1)
                    {
                        MessageBox.Show("系统中已存在此名称的部门，请修改", "错误");
                        return;
                    }
                }
                
                DialogResult = true;
            }
            else
            {
                return;
            }
        }

        // 取消更新
        private void btnCancle_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
