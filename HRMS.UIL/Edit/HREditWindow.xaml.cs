using System.Windows;
using System.Windows.Controls;
using HRMS.DAL.Model;
using HRMS.BLL;

namespace HRMS.UIL.Edit
{
    /// <summary>
    /// HREdit.xaml 的交互逻辑
    /// </summary>
    public partial class HREditWindow : Window
    {
        // 标识当前窗口是否为编辑窗口
        internal bool IsEdit { set; private get; }
        // 当前编辑的HR信息类
        internal HR EditHR { set; private get; }

        public HREditWindow()
        {
            InitializeComponent();
            bool[] IsLocked = new bool[] { true, false };
            cmbIsLocked.ItemsSource = IsLocked;
        }

        /// <summary>
        /// 初始化窗口的逻辑处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if(IsEdit)
            {
                grdHR.DataContext = EditHR;

                if (MainWindow.CurrentOperator.Equals("admin"))
                    cmbIsLocked.IsEnabled = true;
                if (EditHR.UserName.Equals(MainWindow.CurrentOperator))
                    chkPassword.IsEnabled = true;
            }
            else
            {
                grdHR.DataContext = HREditBLL.InitializeHR();

                txtUserName.IsReadOnly = false;
                chkPassword.IsEnabled = true;
                chkPassword.IsChecked = true;
            }

            if(chkPassword.IsChecked == true)
            {
                pwdPassword.IsEnabled = true;
                pwdConfirm.IsEnabled = true;
            }
        }

        // 保存编辑
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            bool isLegal = true;

            CommonHelper.CheckTextBoxNotEmpty(ref isLegal, txtUserName);
            if(chkPassword.IsChecked == true)
            {
                CommonHelper.CheckPasswrodNotEmpty(ref isLegal, pwdPassword, pwdConfirm);
                if(pwdPassword.Password != pwdConfirm.Password)
                {
                    MessageBox.Show("两次输入密码不同，请再次确认", "错误");
                    return;
                }
            }

            if(isLegal)
            {
                HR hr = (HR)grdHR.DataContext;
                if(chkPassword.IsChecked == true)
                {
                    hr.Password = Security.GetMD5Salted(pwdConfirm.Password);
                }

                int UpdateResult = HREditBLL.UpdateData(IsEdit, hr);
                if(UpdateResult > 1)
                {
                    MessageBox.Show("系统中已存在同名用户，请修改用户名", "警告");
                    return;
                }
                if(UpdateResult == 1)
                {
                    SystemLogBLL.GenerateSysLog(IsEdit, hr, MainWindow.CurrentOperator);
                }

                this.DialogResult = true;
            }
            else
            {
                return;
            }
        }

        // 取消编辑
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void chkPassword_Checked(object sender, RoutedEventArgs e)
        {
            pwdPassword.IsEnabled = true;
            pwdConfirm.IsEnabled = true;
        }
    }
}
