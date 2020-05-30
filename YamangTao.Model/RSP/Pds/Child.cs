using System;
namespace YamangTao.Model.RSP.Pds
{
    public class Child
    {
        public int Id { get; set; }
        public int PdsId { get; set; }
        public string EmployeeId { get; set; }
        public string Lastname { get; set; }
        public string Firstname { get; set; }
        public string  Middle { get; set; }
        public DateTime? BirthDate { get; set; }
    }
}