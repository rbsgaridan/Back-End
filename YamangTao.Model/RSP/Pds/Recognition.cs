using YamangTao.Core.Common;

namespace YamangTao.Model.RSP.Pds
{
    public class Recognition : IIdentifyableEntity<long>
    {
        public long Id { get; set; }
        public int PdsId { get; set; }
        public string EmployeeId { get; set; }
        public string Description { get; set; }
        public string Year { get; set; }
        public string CertNumber { get; set; }
        public PersonalDataSheet Pds { get; set; }

        public long EntityId => Id;
    }
}