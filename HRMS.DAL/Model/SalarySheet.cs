
namespace HRMS.DAL.Model
{
    public class SalarySheet
    {
        public System.String StaffID { get; set; }
        public System.String Name { get; set; }
        public System.Guid SheetID { get; set; }
        public System.Int32 BaseSalary { get; set; }
        public System.Int32 Bonus { get; set; }
        public System.Int32 Fine { get; set; }
    }
}
