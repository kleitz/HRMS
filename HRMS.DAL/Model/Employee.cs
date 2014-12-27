using System;

namespace HRMS.DAL.Model
{
    /// <summary>
    /// 员工信息类
    /// </summary>
    public class Employee
    {
        public string Id { set; get; }
        public string Name { set; get; }
        public int Gender { set; get; }
        public DateTime Birthday { set; get; }
        public DateTime EntryDate { set; get; }
        public int MaritalStatus { set; get; }
        public int PoliticalStatus { set; get; }
        public string Nation { set; get; }
        public string Birthplace { set; get; }
        public int Degree { set; get; }
        public string Major { set; get; }
        public string School { set; get; }
        public string Address { set; get; }
        public string Email { set; get; }
        public string Telephone { set; get; }
        public string BankAccount { set; get; }
        public int Department { set; get; }
        public string JobTitle { set; get; }
        public string StaffID { set; get; }
        public string ContractPeriod { set; get; }
        public string Remark { set; get; }
        public int Salary { set; get; }
    }
}
