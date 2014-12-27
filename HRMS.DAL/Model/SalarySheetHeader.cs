
namespace HRMS.DAL.Model
{
    public class SalarySheetHeader
    {
        public System.Guid Id { get; set; }
        public System.Int32 Year { get; set; }
        public System.Int32 Month { get; set; }
        public System.Int32 DepartmentId { get; set; }
        public System.Boolean IsSettled { get; set; }
    }
}
