using System.Windows;
using HRMS.BLL;

namespace HRMS.UIL.View
{
    /// <summary>
    /// LogViewWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LogViewWindow : Window
    {
        public LogViewWindow()
        {
            InitializeComponent();
        }

        // 检索日志
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (gbdtpBegin.SelectedDate > gbdtpEnd.SelectedDate)
            {
                MessageBox.Show("日期范围设置有误", "错误");
                return;
            }

            dgSysLog.ItemsSource = SystemLogBLL.SearchSysLogs(gbcmbType.SelectedValue, gbcmbTable.SelectedValue, 
                                                              gbdtpBegin.SelectedDate, gbdtpEnd.SelectedDate);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // 初始化Comobox
            gbcmbType.ItemsSource = new string[] { "新增", "更新", "删除" };
            gbcmbTable.ItemsSource = new string[] { "Employee", "HR", "Department"};
            gbdtpBegin.SelectedDate = System.DateTime.Today.AddMonths(-1);
            gbdtpEnd.SelectedDate = System.DateTime.Today.AddDays(1);

            Refresh();
        }

        // 刷新数据
        private void Refresh()
        {
            dgSysLog.ItemsSource = SystemLogBLL.ListAllSysLog();
        }
    }
}
