using System;
namespace YamangTao.Api.Dtos.Rsp
{
    public class EligibilityDto
    {
        public int Id { get; set; }
        public int PdsId { get; set; }
        public string EmployeeId { get; set; }

        public string Description {get; set;}
        public string Rating {get; set;}
        public DateTime? ExamDate {get; set;}
        public string ExamPlace {get; set;}
        public string LicenseNumber {get; set;}
        public DateTime? ValidityDate {get; set;}
    }
}