using System;

namespace YamangTao.Model.RSP.Staffing
{
    public class PersonnelPosition
    {
        public string PositionCode { get; set; } //Plantilla Position
        public string PositionId { get; set; } // From Job Position Table
        public string PositionName { get; set; } // From Job Position Table + ()
        public bool isVacant { get; set; } // True if vacant
        public string EmployeeId { get; set; } // Name of Employee Assisgned should be nullstring if vacant
        public DateTime DateCreated { get; set; } // Date the position is created
        public float AnnualRate { get; set; }
        public float MonthlyRate { get; set; }
        public float DailyRate { get; set; }
        public float HourlyRate { get; set; }
        public string SalaryGradeId { get; set; }
        public string Category { get; set; }
    }
}