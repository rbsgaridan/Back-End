using System;

namespace YamangTao.Dto.Rsp
{
    public class ChildDto
    {
        public int Id { get; set; }
        public int PdsId { get; set; }
        public string EmployeeId { get; set; }
        public string Lastname { get; set; }
        public string Firstname { get; set; }
        public string  Middle { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Suffix { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
    }
}