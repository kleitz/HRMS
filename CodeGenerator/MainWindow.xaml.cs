using System.Windows;

namespace CodeGenerator
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            {
                txtServer.Text = "localhost";
                txtDB.Text = "hrdb";
                txtUserName.Text = "hrms";
                pwbPassword.Password = "hryspa";
            }
            cmbTable.IsEnabled = false;
            btnGenerator.IsEnabled = false;
        }

        /// <summary>
        /// 连接数据库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            string errorInfo;
            bool connResult = DBConnection.ConnectSQLServer(txtServer.Text, txtDB.Text, 
                                                            txtUserName.Text, pwbPassword.Password, out errorInfo);
            if(connResult == false)
            {
                MessageBox.Show(errorInfo, "错误");
                return;
            }

            cmbTable.ItemsSource = DBConnection.GetAllTables(out errorInfo);
            if(cmbTable.ItemsSource == null)
            {
                MessageBox.Show(errorInfo, "错误");
            }
            cmbTable.IsEnabled = true;
            btnGenerator.IsEnabled = true;
        }

        /// <summary>
        /// 生成代码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGenerator_Click(object sender, RoutedEventArgs e)
        {
            string tableName = (string)cmbTable.SelectedItem;
            if(tableName == null)
            {
                MessageBox.Show("请选择需要生成代码的表名", "错误");
                return;
            }
            txtModel.Text = ModelGenerator.GenerateModel(tableName);
            txtDAL.Text = DALGenerator.GenerateDAL(tableName);
        }
    }
}
