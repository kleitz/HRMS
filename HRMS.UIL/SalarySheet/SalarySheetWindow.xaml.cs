using System;
using System.Windows;
using HRMS.BLL;
using HRMS.DAL.Model;

namespace HRMS.UIL.SalarySheet
{
    /// <summary>
    /// SalarySheetWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SalarySheetWindow : Window
    {
        public SalarySheetWindow()
        {
            InitializeComponent();
        }

        // 窗口加载时将检索条件ComoBox初始化
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            int Year = DateTime.Today.Year;
            int Backward = 2;    // 年份范围设定
            int Forward = 3;     // 年份范围设定
            
            int[] YearList = new int[Forward + Backward + 1];
            for(int i = 0; i <= Backward; i++)
            {
                YearList[i] = Year - Backward + i;
            }
            for(int i = 0; i < Forward; i++)
            {
                YearList[Backward + i + 1] = Year + i + 1;
            }
            cmbYear.ItemsSource = YearList;
            cmbYear.SelectedValue = Year;    // 默认当前年份

            int Month = DateTime.Today.Month;
            int[] MonthList = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
            cmbMonth.ItemsSource = MonthList;
            cmbMonth.SelectedValue = Month;    // 默认当前月份

            cmbDepartment.ItemsSource = EmployeeEditBLL.ListAllDepartment();
        }

        // 查看工资单
        private void btnView_Click(object sender, RoutedEventArgs e)
        {
            if(cmbDepartment.SelectedValue == null)
            {
                MessageBox.Show("请选择部门", "错误");
                return;
            }

            dgSalarySheet.ItemsSource = SalarySheetBLL.View(cmbYear.SelectedValue, cmbMonth.SelectedValue, 
                                                            cmbDepartment.SelectedValue);

            if(dgSalarySheet.ItemsSource != null)
            {
                if (SalarySheetBLL.IsSettled == false)
                {
                    btnGenerate.IsEnabled = true;
                    btnSettle.IsEnabled = true;
                }   
                else
                {
                    btnGenerate.IsEnabled = false;
                    btnSettle.IsEnabled = false;
                    dgSalarySheet.IsReadOnly = true;
                }         
            }
            else if (dgSalarySheet.ItemsSource == null && SalarySheetBLL.IsExisted == false)
            {
                btnGenerate.IsEnabled = true;
                btnSettle.IsEnabled = true;

                MessageBoxResult result = MessageBox.Show("没有数据，是否生成？", "错误", MessageBoxButton.YesNo);
                if(result == MessageBoxResult.Yes)
                {
                    dgSalarySheet.ItemsSource = SalarySheetBLL.Generate();
                }
            }
            else
            {
                MessageBox.Show("系统错误！工资单重复", "错误");
                return;
            }
        }

        // 生成工资单
        private void btnGenerate_Click(object sender, RoutedEventArgs e)
        {
            if (SalarySheetBLL.IsExisted == true)
            {
                MessageBoxResult result = MessageBox.Show("工资单已经存在，是否重新生成？", "提示", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    SalarySheetBLL.Clear();
                }
            }
            dgSalarySheet.ItemsSource = SalarySheetBLL.Generate();  
        }

        // 结算工资单
        private void btnSettle_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("工资单结算后无法修改，是否确认操作？", "提示", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                SalarySheetBLL.Settle();
                btnGenerate.IsEnabled = false;
                btnSettle.IsEnabled = false;
                dgSalarySheet.IsReadOnly = true;
            }
        }

        // 当检索条件发生变化时的处理
        private void cmb_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            btnGenerate.IsEnabled = false;
            btnSettle.IsEnabled = false;
            dgSalarySheet.ItemsSource = null;
            dgSalarySheet.IsReadOnly = false;
        }

        // 更新工资单
        private void dgSalarySheet_RowEditEnding(object sender, System.Windows.Controls.DataGridRowEditEndingEventArgs e)
        {
            HRMS.DAL.Model.SalarySheet item = (HRMS.DAL.Model.SalarySheet)e.Row.DataContext;
            SalarySheetBLL.Update(item);
        }
    }
}
