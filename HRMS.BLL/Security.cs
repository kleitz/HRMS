using System.Text;

namespace HRMS.BLL
{
    public class Security
    {
        // 获取配置文件中的Salt值
        private static readonly string salt = 
            System.Configuration.ConfigurationManager.AppSettings["Salt"];

        /// <summary>
        /// 计算输入字符串的MD5
        /// </summary>
        /// <param name="strInput">输入字符串</param>
        /// <returns>MD5</returns>
        public static string GetMD5Salted(string strInput)
        {
            System.Security.Cryptography.MD5 md5 = 
                System.Security.Cryptography.MD5.Create();
            strInput += salt;    // 加盐处理
            byte[] inputBytes = Encoding.UTF8.GetBytes(strInput);
            byte[] hash = md5.ComputeHash(inputBytes);

            StringBuilder md5Hash = new StringBuilder();
            for(int i = 0; i < hash.Length; i++)
            {
                md5Hash.Append(hash[i].ToString("x2"));
            }

            return md5Hash.ToString();
        }
    }
}
