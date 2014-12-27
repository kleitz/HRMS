
namespace HRMS.DAL.Model
{
    /// <summary>
    /// 部门信息类
    /// </summary>
    public class Department
    {
        // 部门Id, 不能在程序集外部修改
        public int Id { internal set; get; }
        public string Name { set; get; }
        // public bool IsCanceled { set; get; }
    }
}
