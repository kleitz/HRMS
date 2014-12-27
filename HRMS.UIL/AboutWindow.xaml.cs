using System.Windows;
using System.Configuration;

namespace HRMS.UIL
{
    /// <summary>
    /// AboutWindow.xaml 的交互逻辑
    /// </summary>
    public partial class AboutWindow : Window
    {
        public AboutWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SettingItem about = new SettingItem();
            about.ProductName = "明科人事管理系统";
            about.Version = "V0.0.1";
            about.CopyLeft = "Michael 保留署名权利";

            about.CompanyName = ConfigurationManager.AppSettings["CompanyName"];
            about.Website = ConfigurationManager.AppSettings["Website"];
            about.Telephone = ConfigurationManager.AppSettings["Telephone"];

            gdAboutInfo.DataContext = about;
        }

        private void hylWebsite_Click(object sender, RoutedEventArgs e)
        {
            string Website = ConfigurationManager.AppSettings["Website"];
            System.Diagnostics.Process.Start(Website);
        }
    }
}
