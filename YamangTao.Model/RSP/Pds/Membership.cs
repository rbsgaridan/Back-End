using YamangTao.Core.Common;

namespace YamangTao.Model.RSP.Pds
{
    public class Membership : IIdentifyableEntity<int>
    {
        public int Id { get; set; }
        public int PdsId { get; set; }
        public string EmployeeId { get; set; }
        public string Organization { get; set; }
        public string CertNumber { get; set; }
        public PersonalDataSheet Pds { get; set; }

        public int EntityId => Id;
    }
}