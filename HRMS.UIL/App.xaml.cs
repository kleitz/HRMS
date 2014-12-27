using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace HRMS.UIL
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        // 程序异常统一处理
        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("程序中出现错误！错误信息：" + e.Exception.Message + "\n是否继续？", 
                                                      "错误", MessageBoxButton.YesNo, MessageBoxImage.Error);
            if(result == MessageBoxResult.Yes)
            {
                // 标识异常已处理，其实未做详细处理，待完善
                e.Handled = true;
            }
        }
    }
}
