using System;
using YamangTao.Core.Common;

namespace YamangTao.Model.RSP.Pds
{
    public class Child : IIdentifyableEntity<int>
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
        public PersonalDataSheet Pds { get; set; }

        public int EntityId => Id;
    }
}