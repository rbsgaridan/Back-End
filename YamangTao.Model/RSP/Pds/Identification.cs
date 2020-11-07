using System;
using YamangTao.Core.Common;

namespace YamangTao.Model.RSP.Pds
{
    public class Identification : IIdentifyableEntity<int>
    {
        public int Id { get; set; }
        public int PdsId { get; set; }
        public string EmployeeId { get; set; }
        public string IdType { get; set; }
        public string Control { get; set; }
        public string IssuanceDatePlace { get; set; }
        public DateTime? DateIssued { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public DateTime? DateCreated { get; set; }
        public PersonalDataSheet Pds { get; set; }

        public int EntityId => Id;
    }
}