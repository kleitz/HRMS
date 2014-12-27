using System.Windows;
using HRMS.DAL.Model;
using HRMS.BLL;

namespace HRMS.UIL.View
{
    /// <summary>
    /// HRView.xaml 的交互逻辑
    /// </summary>
    public partial class HRViewWindow : Window
    {
        public HRViewWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 向系统添加HR信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Edit.HREditWindow hrAddWin = new Edit.HREditWindow();
            hrAddWin.IsEdit = false;
            hrAddWin.EditHR = null;

            if (hrAddWin.ShowDialog() == true)
                Refresh();
        }

        /// <summary>
        /// 删除选中的HR信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            HR hr = (HR)dgHR.SelectedItem;
            if(hr == null)
            {
                MessageBox.Show("未选中任何列", "提示");
                return;
            }

            if(HRViewBLL.DeleteHR(hr) == 1)
            {
                SystemLogBLL.GenerateSysLog(null, hr, MainWindow.CurrentOperator);
            }
            Refresh();
        }

        /// <summary>
        /// 编辑选中的HR信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            HR hr = (HR)dgHR.SelectedItem;
            if(hr == null)
            {
                MessageBox.Show("未选中任何列", "提示");
                return;
            }

            Edit.HREditWindow hrEditWin = new Edit.HREditWindow();
            hrEditWin.IsEdit = true;
            hrEditWin.EditHR = hr;

            if (hrEditWin.ShowDialog() == true)
                Refresh();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if(MainWindow.CurrentOperator.Equals("admin"))
            {
                btnAdd.IsEnabled = true;
                btnDelete.IsEnabled = true;
            }
            Refresh();    // 窗口加载时刷新数据
        }

        /// <summary>
        /// 刷新DataGrid数据
        /// </summary>
        private void Refresh()
        {
            dgHR.ItemsSource = HRViewBLL.ListAllHRs();
        }
    }
}
