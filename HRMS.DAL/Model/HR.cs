
namespace HRMS.DAL.Model
{
    /// <summary>
    /// 登录信息类
    /// </summary>
    public class HR
    {
        // public Guid Id { set; get; }
        public string UserName { set; get; }
        public string Password { set; get; }
        public bool IsLocked { set; get; }
    }
}
