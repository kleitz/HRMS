
namespace HRMS.DAL.Model
{
    public class SystemLog
    {
        public System.Int32 Id { get; set; }
        public System.String Type { get; set; }
        public System.String Operator { get; set; }
        public System.String TableName { get; set; }
        public System.String PrimaryKey { get; set; }
        public System.String Describe { get; set; }
        public System.DateTime Time { get; set; }
    }
}
