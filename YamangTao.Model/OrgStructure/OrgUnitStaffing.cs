namespace YamangTao.Model.OrgStructure
{
    public class OrgUnitStaffing
    {
        public int Id { get; set; }
        public string ControlNumber { get; set; }
        public string Status { get; set; } //Permanent, JO, COS
        public string JobTitle { get; set; }
        public int JobPositionId { get; set; }
        public string SalaryGrade { get; set; }
        public float AnnualSalary { get; set; }
        public float MonthlySalary { get; set; }
       // ToDo 
    }
}