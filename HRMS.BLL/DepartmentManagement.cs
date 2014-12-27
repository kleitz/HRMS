using HRMS.DAL;
using HRMS.DAL.Model;

namespace HRMS.BLL
{
    /// <summary>
    /// 部门管理
    /// </summary>
    public class DepartmentManagement
    {
        // 初始化新部门
        public static Department InitializeDept()
        {
            Department dept = new Department();
            return dept;
        }

        /// <summary>
        /// 新增部门或修改部门名称
        /// </summary>
        /// <param name="isEdit">标识新增或修改</param>
        /// <param name="dept">需要更新的部门类</param>
        /// <returns>返回更新条目（0：未更新成功 1：更新成功 2：已有同名部门）</returns>
        public static int UpdateData(bool isEdit, Department dept)
        {
            if (dept == null)
                return 0;

            if (DepartmentDAL.Select(dept.Name) != null)
                return 2;

            if(isEdit)
            {
                return DepartmentDAL.Update(dept);
            }

            return DepartmentDAL.Insert(dept);
        }

        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="dept">需要删除的部门</param>
        /// <returns>返回删除条目</returns>
        public static int DeleteDepartment(Department dept)
        {
            if (dept == null)
                return 0;

            return DepartmentDAL.Delete(dept);
        }
    }
}
