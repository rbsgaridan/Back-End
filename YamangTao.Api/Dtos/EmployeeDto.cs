using System;

namespace YamangTao.Api.Dtos
{
    public class EmployeeDto
    {
        public string Id { get; set; }
        public string EmployeeGroup { get; set; } //Teaching or Non-Teaching
        public string CurrentStatus { get; set; } //Employment Status
        public string Lastname { get; set; }
        public string Firstname { get; set; }
        public string MiddleName { get; set; }
        public string MI { get; set; }
        public string Suffix { get; set; } // Jr., Sr., II, III
        public DateTime? BirthDate { get; set; }
        public string Sex { get; set; }
        public string Telephone { get; set; }
        public string MobileNumber { get; set; }
        public string EmailAddress { get; set; }
        public bool InActive { get; set; }
        public DateTime? DateHired { get; set; }
        public bool Resigned { get; set; }
        public DateTime? DateResigned { get; set; }
        public bool Terminated { get; set; }
        public DateTime? DateTerminated { get; set; }
        public bool Retired { get; set; }
        public DateTime? DateRetired { get; set; }
        public int? BrachCampusId { get; set; }
        public int? OrgUnitId { get; set; }
        public string FullName { get; set; }

       
    }
}