using System.Collections.Generic;

namespace YamangTao.Dto
{
    public class OrgUnitDto
    {
        public int Id { get; set; }
        public int OrderNumber { get; set; }
        public string UnitName { get; set; }
        public string Code { get; set; }

        // Unit Types: College, Office, Center, Department 
        public string UnitType { get; set; }
        public string CurrentHeadId { get; set; }
        public string NameOfHead { get; set; }
        public EmployeeDto CurrentHead { get; set; }
        public string Location { get; set; }
        public int? ParentUnitId { get; set; }
        
        // Child Units
        public ICollection<OrgUnitDto> OrgUnitChildren { get; set; }

    }
}