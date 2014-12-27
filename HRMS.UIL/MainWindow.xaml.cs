using System.Windows;
using HRMS.UIL.View;
using HRMS.UIL.Edit;
using HRMS.UIL.SystemManagement;
using HRMS.UIL.Settings;

namespace HRMS.UIL
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        // 记录当前的操作者的用户名
        internal static string CurrentOperator { set; get; }

        public MainWindow()
        {
            Login.LoginWindow login = new Login.LoginWindow();
            login.ShowDialog();

            if (login.DialogResult == true)
            {
                InitializeComponent();
            }
            else
                this.Close();
        }

        /// <summary>
        /// 新增员工
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuAddEmployee_Click(object sender, RoutedEventArgs e)
        {
            EmployeeEditWindow edit = new EmployeeEditWindow();
            edit.ShowDialog();
        }

        /// <summary>
        /// 浏览员工信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnnViewEmployee_Click(object sender, RoutedEventArgs e)
        {
            EmployeeViewWindow view = new EmployeeViewWindow();
            view.ShowDialog();
        }

        /// <summary>
        /// 搜索员工信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (txtSearchKey.Text.Length <= 0)
            {
                MessageBox.Show("搜索字段不能为空", "错误");
                return;
            }

            EmployeeViewWindow view = new EmployeeViewWindow();
            view.listAll = false;
            view.dgEmployee.ItemsSource = 
                HRMS.BLL.EmployeeViewBLL.QuickSearch(txtSearchKey.Text);
            if(view.dgEmployee.ItemsSource == null)
            {
                MessageBox.Show("系统中无此员工信息", "警告");
                return;
            }
            else
            {
                view.ShowDialog();
            }
        }

        /// <summary>
        /// 新增HR
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuAddOperator_Click(object sender, RoutedEventArgs e)
        {
            HREditWindow hrAddWin = new HREditWindow();
            hrAddWin.ShowDialog();
        }

        /// <summary>
        /// 浏览HR信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuViewOperator_Click(object sender, RoutedEventArgs e)
        {
            HRViewWindow hrViewWin = new HRViewWindow();
            hrViewWin.ShowDialog();
        }

        // 窗口加载
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (CurrentOperator.Equals("admin"))
            {
                mnuAddOperator.IsEnabled = true;
                mnuSystemManagement.Visibility = System.Windows.Visibility.Visible;
            }
            // 是否启用生日提醒判断
            bool IsReminderOn = System.Convert.ToBoolean(
                System.Configuration.ConfigurationManager.AppSettings["IsReminderOn"]);
            if(IsReminderOn == true)
            {
                ReminderWindow reminderWin = new ReminderWindow();
                if (reminderWin.IsNull == false)
                {
                    // 打开新窗口并返回，不会阻塞主窗口
                    reminderWin.Show();
                }
                else
                {
                    reminderWin.Close();
                }
            }
        }

        // 查看日志
        private void mnuViewLog_Click(object sender, RoutedEventArgs e)
        {
            LogViewWindow logWin = new LogViewWindow();
            logWin.ShowDialog();
        }

        // 工资单处理
        private void mnuSalarSheet_Click(object sender, RoutedEventArgs e)
        {
            SalarySheet.SalarySheetWindow sheetWin = new SalarySheet.SalarySheetWindow();
            sheetWin.ShowDialog();
        }

        // 切换用户
        private void mnuChangeUser_Click(object sender, RoutedEventArgs e)
        {
            mnuAddOperator.IsEnabled = false;
            mnuSystemManagement.Visibility = System.Windows.Visibility.Collapsed;

            Login.LoginWindow changeUser = new Login.LoginWindow();
            changeUser.ShowDialog();

            if (changeUser.DialogResult == true)
            {
                this.Window_Loaded(sender, e);
            }
        }
        // 部门管理
        private void mnuDeptManagement_Click(object sender, RoutedEventArgs e)
        {
            DeptViewWindow deptWin = new DeptViewWindow();
            deptWin.ShowDialog();
        }
        // 执行SQL语句
        private void mnnExecuteSQL_Click(object sender, RoutedEventArgs e)
        {
            ExcuteSQLWindow sqlWin = new ExcuteSQLWindow();
            sqlWin.Show();
        }
        // 系统设置
        private void mnuSettings_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow setWin = new SettingsWindow();
            setWin.Show();
        }
        // 关于
        private void mnuAbout_Click(object sender, RoutedEventArgs e)
        {
            AboutWindow aboutWin = new AboutWindow();
            aboutWin.ShowDialog();
        }
    }
}
