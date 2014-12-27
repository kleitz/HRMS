using System.Windows;
using HRMS.BLL;
using HRMS.DAL.Model;

namespace HRMS.UIL.SystemManagement
{
    /// <summary>
    /// DeptViewWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DeptViewWindow : Window
    {
        public DeptViewWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();
        }
        // 刷新数据
        private void Refresh()
        {
            dgDepartment.ItemsSource = EmployeeEditBLL.ListAllDepartment();
        }

        // 新增部门
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            DeptEditWindow deptAddWin = new DeptEditWindow();
            deptAddWin.IsEdit = false;
            deptAddWin.Dept = DepartmentManagement.InitializeDept();

            if (deptAddWin.ShowDialog() == true)
                Refresh();
        }

        // 取缔部门
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Department dept = (Department)dgDepartment.SelectedItem;

            if (dept == null)
            {
                MessageBox.Show("未选中任何列", "提示");
                return;
            }
            if(EmployeeViewBLL.SearchEmployees("", "", dept.Id, null, null) == null)
            {
                if(DepartmentManagement.DeleteDepartment(dept) == 1)
                {
                    SystemLogBLL.GenerateSysLog(null, dept, MainWindow.CurrentOperator);
                    Refresh();
                }  
            }
            else
            {
                MessageBox.Show("该部门存在员工，无法取缔", "错误");
                return;
            }
        }

        // 编辑部门名称
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            Department dept = (Department)dgDepartment.SelectedItem;

            if(dept == null)
            {
                MessageBox.Show("未选中任何列", "提示");
                return;
            }

            DeptEditWindow deptEditWin = new DeptEditWindow();
            deptEditWin.IsEdit = true;
            deptEditWin.Dept = dept;

            if (deptEditWin.ShowDialog() == true)
                Refresh();
        }
    }
}
