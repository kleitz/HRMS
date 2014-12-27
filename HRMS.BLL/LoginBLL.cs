using HRMS.DAL;
using HRMS.DAL.Model;

namespace HRMS.BLL
{
    /// <summary>
    /// 登录逻辑处理类
    /// </summary>
    public class LoginBLL
    {
        // 用户输错密码最大次数
        private static int tryCount = 3;
        /// <summary>
        /// 判断用户是否登录成功
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="errorInfo">错误信息</param>
        /// <returns>成功返回ture；否则，返回false</returns>
        public static bool IsLoginSucceed(string userName, string password, out string errorInfo)
        {
            if (userName.Length <= 0 || password.Length <= 0)
            {
                errorInfo = "用户名或密码不能为空";
                return false;
            }

            HR[] hrList = HRDAL.Select(userName);
            if (hrList == null)
            {
                errorInfo = "用户名不存在";
                return false;
            }
            if (hrList.Length > 1)
            {
                errorInfo = "严重错误！用户名重复";
                return false;
            }

            HR tempHR = hrList[0];
            if (tempHR.IsLocked == true)
            {
                errorInfo = "对不起，该用户已被锁定";
                return false;
            }
            // 将用户输入的密码加盐MD5
            password = Security.GetMD5Salted(password);
            
            if (tempHR.Password != password)
            {
                if (tryCount == 0)
                {
                    // 锁定当前用户
                    tempHR.IsLocked = true;
                    HRDAL.Update(tempHR);
                    errorInfo = "对不起，该用户已被锁定";
                    return false;
                }

                errorInfo = "密码错误，还可以尝试" + tryCount.ToString() + "次";
                tryCount--;
                return false;
            }
            else
            {
                errorInfo = "登录成功";
                return true;
            }        
        }
    }
}
