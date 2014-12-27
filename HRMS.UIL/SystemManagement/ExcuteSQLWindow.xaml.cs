using System.Windows;
using HRMS.BLL;

namespace HRMS.UIL.SystemManagement
{
    /// <summary>
    /// ExcuteSQLWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ExcuteSQLWindow : Window
    {
        public ExcuteSQLWindow()
        {
            InitializeComponent();
        }
        // 执行SQL语句
        private void btnExcute_Click(object sender, RoutedEventArgs e)
        {
            if(txtQueryText.Text == null || txtQueryText.Text.Length <= 0)
            {
                MessageBox.Show("SQL语句为空，无法执行", "错误");
                return;
            }

            txtResult.Text = ExcuteSQLBLL.ExcuteSQL(txtQueryText.Text);
        }
    }
}
