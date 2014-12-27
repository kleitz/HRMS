using HRMS.DAL;
using HRMS.DAL.Model;

namespace HRMS.BLL
{
    public class HREditBLL
    {
        /// <summary>
        /// 初始化新增HR信息
        /// </summary>
        /// <returns>返回初始化的HR信息</returns>
        public static HR InitializeHR()
        {
            HR temp = new HR();
            temp.IsLocked = false;

            return temp;
        }

        /// <summary>
        /// 新增或更新数据库中的HR信息
        /// </summary>
        /// <param name="isEdit">标识新增或更新</param>
        /// <param name="hr">需要新增或更新的HR信息类</param>
        /// <returns>返回新增或更新的条目，失败返回0</returns>
        public static int UpdateData(bool isEdit, HR hr)
        {
            if (hr == null)
                return 0;

            if(isEdit)
            {
                return HRDAL.Update(hr);
            }
            else
            {
                // 新增前检查是否存在同名条目
                if (HRDAL.Select(hr.UserName) != null)
                    return 2;
                return HRDAL.Insert(hr);
            }
        }
    }
}
