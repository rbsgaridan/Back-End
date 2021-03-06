using System;
using YamangTao.Core.Common;

namespace YamangTao.Model.RSP.Pds
{
    public class Address : IIdentifyableEntity<int>, IOwnedEntity
    {
        public int Id { get; set; }
        public int? PdsId { get; set; }
        public string EmployeeId { get; set; }
        public string Description { get; set; }
        public string Block { get; set; }
        public string Street { get; set; }
        public string Purok { get; set; }
        public string Barangay { get; set; }
        public string BarangayCode { get; set; }
        public string Municipality { get; set; }
        public string MunicipalityCode { get; set; }
        public string Province { get; set; }
        public string ProvinceCode { get; set; }
        public string Region { get; set; }
        public string RegionCode { get; set; }
        public string ZipCode { get; set; }
        public bool IsActive { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public PersonalDataSheet Pds { get; set; }

        public int EntityId => Id;

        public string OwnerId => EmployeeId;
    }
}