using System;
using System.Collections.Generic;

namespace YamangTao.Model.RSP.Pds
{
    public class WorkExperience
    {
        public int Id { get; set; }
        public int PdsId { get; set; }
        public string EmployeeId { get; set; }
        public DateTime? PeriodFrom { get; set; }
        public DateTime? PeriodTo { get; set; }
        public string Position { get; set; }
        public string Company { get; set; }
        public float MonthlySalary { get; set; }
        public string SalaryGrade { get; set; }
        public string AppointmentStatus { get; set; }
        public bool GovernmentService { get; set; }

    }
}