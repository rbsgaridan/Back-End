// An OrgUnit is an office, department, or college.
// It should act as a node for the heirachy of the organizational structure.
using System.Collections;

namespace YamangTao.Model.OrgStructure
{
    public class OrgUnit
    {
        public int Id { get; set; }
        public string UnitName { get; set; }
        public string Code { get; set; }
        public string UnitType { get; set; }
        public string NameOfHead { get; set; }
        public Employee CurrentHead { get; set; }
        public string Location { get; set; }
        public OrgUnit ParentUnit { get; set; }
        public ICollection OrgUnitChildren { get; set; }

    }
}