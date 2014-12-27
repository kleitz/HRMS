using System.Windows.Controls;

namespace HRMS.UIL
{
    internal class CommonHelper
    {
        /// <summary>
        /// 检查文本框是否为空
        /// </summary>
        /// <param name="isLegal">检查是否为空的真值</param>
        /// <param name="txtList">需要检查的文本框</param>
        internal static void CheckTextBoxNotEmpty(ref bool isLegal, params TextBox[] txtList)
        {
            foreach (TextBox txt in txtList)
            {
                if (txt.Text == null || txt.Text.Length <= 0)
                {
                    isLegal = false;
                    txt.Background = System.Windows.Media.Brushes.Red;
                }
                else
                {
                    txt.Background = null;
                }
            }
        }

        // 检查下拉框内容是否为空
        internal static void CheckComoBoxNotEmpty(ref bool isLegal, params ComboBox[] cmbList)
        {
            foreach (ComboBox cmb in cmbList)
            {
                if (cmb.SelectedIndex < 0)
                {
                    isLegal = false;
                    cmb.Effect = new System.Windows.Media.Effects.DropShadowEffect 
                                     { Color = System.Windows.Media.Colors.Red };
                }
                else
                {
                    cmb.Effect = null;
                }
            }
        }

        // 检查密码框内容是否为空
        internal static void CheckPasswrodNotEmpty(ref bool isLegal, params PasswordBox[] pwdList)
        {
            foreach (PasswordBox pwd in pwdList)
            {
                if (pwd.Password == null || pwd.Password.Length <= 0)
                {
                    isLegal = false;
                    pwd.Background = System.Windows.Media.Brushes.Red;
                }
                else
                {
                    pwd.Background = null;
                }
            }
        }
    }
}
