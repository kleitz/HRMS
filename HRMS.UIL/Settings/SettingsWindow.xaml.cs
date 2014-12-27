using System;
using System.Windows;
using System.Configuration;
using System.Xml;

namespace HRMS.UIL.Settings
{
    /// <summary>
    /// SettingsWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SettingsWindow : Window
    {
        private SettingItem OriginalSettings;
        public SettingsWindow()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SettingItem settings = new SettingItem();
            settings.CompanyName = ConfigurationManager.AppSettings["CompanyName"];
            settings.Website = ConfigurationManager.AppSettings["Website"];
            settings.Telephone = ConfigurationManager.AppSettings["Telephone"];
            settings.IsReminderOn = Convert.ToBoolean(ConfigurationManager.AppSettings["IsReminderOn"]);
            settings.ReminderDays = Convert.ToInt32(ConfigurationManager.AppSettings["ReminderDays"]);

            gdSettings.DataContext = settings;

            OriginalSettings = new SettingItem();
            OriginalSettings.CompanyName = settings.CompanyName;
            OriginalSettings.Website = settings.Website;
            OriginalSettings.Telephone = settings.Telephone;
            OriginalSettings.IsReminderOn = settings.IsReminderOn;
            OriginalSettings.ReminderDays = settings.ReminderDays;
        }
        // 保存编辑
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            bool IsLegal = true;
            CommonHelper.CheckTextBoxNotEmpty(ref IsLegal, txtCompanyName, txtWebsite,
                                              txtTelephone, txtReminderDays);

            if(IsLegal)
            {
                if (!txtCompanyName.Text.Equals(OriginalSettings.CompanyName))
                    SetConfig("CompanyName", txtCompanyName.Text);
                if (!txtWebsite.Text.Equals(OriginalSettings.Website))
                    SetConfig("Website", txtWebsite.Text);
                if (!txtTelephone.Text.Equals(OriginalSettings.Telephone))
                    SetConfig("Telephone", txtTelephone.Text);
                if (chkIsReminderOn.IsChecked != OriginalSettings.IsReminderOn)
                    SetConfig("IsReminderOn", chkIsReminderOn.IsChecked.ToString());
                if (!txtReminderDays.Text.Equals(OriginalSettings.ReminderDays.ToString()))
                    SetConfig("ReminderDays", txtReminderDays.Text);

                this.Close();
            }
            else
            {
                return;
            }
        }
        // 取消保存
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        // 修改配置文件
        private void SetConfig(string key, string value)
        {
            XmlDocument config = new XmlDocument();

            string configPath = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
            config.Load(configPath);
            XmlNodeList nodes = config.GetElementsByTagName("add");
            for(int i = 0; i < nodes.Count; i++)
            {
                XmlAttribute att = nodes[i].Attributes["key"];
                if(att != null && att.Value.Equals(key))
                {
                    att = nodes[i].Attributes["value"];
                    att.Value = value;
                    break;
                }
            }
            config.Save(configPath);
            System.Configuration.ConfigurationManager.RefreshSection("appSettings");
        }

        private void chkIsReminderOn_Click(object sender, RoutedEventArgs e)
        {
            if (chkIsReminderOn.IsChecked == false)
                txtReminderDays.IsEnabled = false;
        }

        private void chkIsReminderOn_Checked(object sender, RoutedEventArgs e)
        {
            txtReminderDays.IsEnabled = true;
        }
    }
}