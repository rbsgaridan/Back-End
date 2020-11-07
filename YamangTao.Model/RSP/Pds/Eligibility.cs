using System;
using YamangTao.Core.Common;

namespace YamangTao.Model.RSP.Pds
{
    public class Eligibility : IIdentifyableEntity<int>
    {
        public int Id { get; set; }
        public int PdsId { get; set; }
        public PersonalDataSheet Pds { get; set; }
        public string EmployeeId { get; set; }

        public string Description {get; set;}
        public string Rating {get; set;}
        public DateTime? ExamDate {get; set;}
        public string ExamPlace {get; set;}
        public string LicenseNumber {get; set;}
        public DateTime? ValidityDate {get; set;}
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }

        public int EntityId => Id;
    }
}