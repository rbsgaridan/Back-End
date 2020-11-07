using YamangTao.Core.Common;

namespace YamangTao.Model.RSP.Pds
{
    public class CharacterReference: IIdentifyableEntity<int>, IOwnedEntity
    {
        public int Id { get; set; }
        public int PdsId { get; set; }
        public string EmployeeId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Mobile { get; set; }
        public PersonalDataSheet Pds { get; set; }

        public int EntityId => Id;

        public string OwnerId => EmployeeId;
    }
}