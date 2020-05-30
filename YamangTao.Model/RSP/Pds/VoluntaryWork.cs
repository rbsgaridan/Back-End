using System;
namespace YamangTao.Model.RSP.Pds
{
    public class VoluntaryWork
    {
        public int Id { get; set; }
        public int PdsId { get; set; }
        public string EmployeeId { get; set; }
        public string Address { get; set; }
        public DateTime? PeriodFrom { get; set; }
        public DateTime? PeriodTo { get; set; }
        public float Hours { get; set; }
        public string Position { get; set; }
    }
}