using HRMS.DAL;
using HRMS.DAL.Model;

namespace HRMS.BLL
{
    /// <summary>
    /// 查看HR信息的逻辑处理
    /// </summary>
    public class HRViewBLL
    {
        /// <summary>
        /// 获取所有操作员信息
        /// </summary>
        /// <returns>返回HR列表</returns>
        public static HR[] ListAllHRs()
        {
            return HRDAL.Select(null);
        }

        /// <summary>
        /// 删除指定的HR信息
        /// </summary>
        /// <param name="hr">需要删除的HR</param>
        /// <returns>返回删除条目</returns>
        public static int DeleteHR(HR hr)
        {
            if (hr == null)
                return 0;

            return HRDAL.Delete(hr);
        }
    }
}
