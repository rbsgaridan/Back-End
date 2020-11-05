using System;
namespace YamangTao.Model.RSP.Pds
{
    public class TrainingAttended
    {
        public int Id { get; set; }
        public int PdsId { get; set; }
        public string EmployeeId { get; set; }
        public string Title { get; set; }
        public DateTime? PeriodFrom { get; set; }
        public DateTime? PeriodTo { get; set; }
        public float Hours { get; set; }
        public string LndType { get; set; }
        public string Sponsor { get; set; }
        public string TrainingCode { get; set; }
        public string CertNumber { get; set; }
        public bool Verified { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public PersonalDataSheet Pds { get; set; }
    }
}