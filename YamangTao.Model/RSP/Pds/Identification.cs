using System;

namespace YamangTao.Model.RSP.Pds
{
    public class Identification
    {
        public int Id { get; set; }
        public int PdsId { get; set; }
        public string EmployeeId { get; set; }
        public string IDType { get; set; }
        public string Control { get; set; }
        public DateTime? DateIssued { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public PersonalDataSheet Pds { get; set; }
    }
}