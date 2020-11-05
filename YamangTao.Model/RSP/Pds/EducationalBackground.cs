using System;
namespace YamangTao.Model.RSP.Pds
{
    public class EducationalBackground
    {
        public int Id { get; set; }
        public int PdsId { get; set; }
        public string EmployeeId { get; set; }
        public string OrderNumber { get; set; }
        public string Level { get; set; }
        public string School { get; set; }
        public string Course { get; set; }
        public DateTime? AttendanceFrom { get; set; }
        public DateTime? AttendanceTo { get; set; }
        public string HighestLevel { get; set; }
        public string YearGraduated { get; set; }
        public string Honors { get; set; }
        public bool showInPds { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public PersonalDataSheet Pds { get; set; }

    }
}