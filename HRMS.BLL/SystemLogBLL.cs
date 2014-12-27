using System;
using HRMS.DAL;
using HRMS.DAL.Model;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace HRMS.BLL
{
    /// <summary>
    /// 操作系统日志
    /// </summary>
    public class SystemLogBLL
    {
        /// <summary>
        /// 生成操作日志
        /// </summary>
        /// <param name="isUpdate">标识日志类型（true：更新； false：新增； null：删除）</param>
        /// <param name="table">操作表名</param>
        /// <param name="currentOperator">当前操作者</param>
        /// <returns>返回新增日志条目</returns>
        public static int GenerateSysLog(bool? isUpdate, object table, 
                                         string currentOperator)
        {
            if (table == null)
                return 0;

            SystemLog log = new SystemLog();
            log.Operator = currentOperator;
            log.Time = DateTime.Now;

            if (table is Employee)
            {
                log.TableName = "Employee";
                log.PrimaryKey = ((Employee)table).StaffID;

                if (isUpdate == true)
                {
                    log.Type = "更新";
                    log.Describe = "更新员工" + log.PrimaryKey + "信息";

                    return SystemLogDAL.Insert(log);
                }
                else if(isUpdate == false)
                {
                    log.Type = "新增";
                    log.Describe = "新增员工" + log.PrimaryKey + "信息";

                    return SystemLogDAL.Insert(log);
                }
                else
                {
                    log.Type = "删除";
                    log.Describe = "删除员工" + log.PrimaryKey + "信息";

                    return SystemLogDAL.Insert(log);
                }
            }
            else if (table is HR)
            {
                log.TableName = "HR";
                log.PrimaryKey = ((HR)table).UserName;

                if (isUpdate == true)
                {
                    log.Type = "更新";
                    log.Describe = "更新管理员" + log.PrimaryKey + "信息";

                    return SystemLogDAL.Insert(log);
                }
                else if(isUpdate == false)
                {
                    log.Type = "新增";
                    log.Describe = "新增管理员" + log.PrimaryKey + "信息";

                    return SystemLogDAL.Insert(log);
                }
                else
                {
                    log.Type = "删除";
                    log.Describe = "删除管理员" + log.PrimaryKey + "信息";

                    return SystemLogDAL.Insert(log);
                }
            }
            else if (table is Department)
            {
                log.TableName = "Department";
                log.PrimaryKey = ((Department)table).Name;

                if (isUpdate == true)
                {
                    log.Type = "更新";
                    log.Describe = "更新部门名称" + log.PrimaryKey + "信息";

                    return SystemLogDAL.Insert(log);
                }
                else if (isUpdate == false)
                {
                    log.Type = "新增";
                    log.Describe = "新增部门" + log.PrimaryKey + "信息";

                    return SystemLogDAL.Insert(log);
                }
                else
                {
                    log.Type = "删除";
                    log.Describe = "删除部门" + log.PrimaryKey + "信息";

                    return SystemLogDAL.Insert(log);
                }
            }
            else
                return 0;
        }

        /// <summary>
        /// 列出所有日志条目
        /// </summary>
        /// <returns>返回日志列表</returns>
        public static SystemLog[] ListAllSysLog()
        {
            // TODO
            return SystemLogDAL.Select(null, null);
        }

        public static SystemLog[] SearchSysLogs(object type, object tableName, 
                                                DateTime? beginDate, DateTime? endDate)
        {
            string searchArgs = null;
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            if(type != null)
            {
                searchArgs += " AND Type = @Type";
                sqlParams.Add(new SqlParameter("@Type", System.Convert.ToString(type)));
            }
            if (tableName != null)
            {
                searchArgs += " AND TableName = @TableName";
                sqlParams.Add(new SqlParameter("@TableName", System.Convert.ToString(tableName)));
            }
            searchArgs += " AND (Time BETWEEN @BeginDate AND @EndDate)";
            sqlParams.Add(new SqlParameter("@BeginDate", Convert.ToDateTime(beginDate)));
            sqlParams.Add(new SqlParameter("@EndDate", Convert.ToDateTime(endDate)));

            return SystemLogDAL.Select(searchArgs, sqlParams.ToArray());
        }
    }
}
