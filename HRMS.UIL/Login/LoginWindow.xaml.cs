using HRMS.BLL;
using System.Windows;

namespace HRMS.UIL.Login
{
    /// <summary>
    /// LoginWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtUserName.Focus();    // 窗口加载时光标聚集在“用户名”文本框。
        }

        // 登录逻辑处理
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            // 获取用户名和密码
            string userName = txtUserName.Text;
            string password = pwdPasswrod.Password;
            string errorInfo;
            // 获取登录结果
            bool isLoginSucceed = LoginBLL.IsLoginSucceed(userName, password, out errorInfo);
            if(!isLoginSucceed)
            {
                MessageBox.Show(errorInfo, "错误");
                txtUserName.Text = "";
                pwdPasswrod.Password = "";
                return;
            }
            else
            {
                MainWindow.CurrentOperator = txtUserName.Text;
                DialogResult = true;
            }             
        }

        // 取消登录
        private void btnCancle_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
