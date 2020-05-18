// An OrgUnit is an office, department, or college.
// It should act as a node for the heirachy of the organizational structure.
using System.Collections.Generic;
using YamangTao.Model.PM;

namespace YamangTao.Model.OrgStructure
{
    public class OrgUnit
    {
        public int Id { get; set; }
        // public int OrderNumber { get; set; }
        public string UnitName { get; set; }
        public string Code { get; set; }

        // Unit Types: College, Office, Center, Department 
        public string UnitType { get; set; }
        public string NameOfHead { get; set; }
        public string CurrentHeadId { get; set; }
        public Employee CurrentHead { get; set; }
        public string Location { get; set; }

        public int? ParentUnitId { get; set; }
        public OrgUnit ParentUnit { get; set; }
        // Child Units
        public virtual List<OrgUnit> OrgUnitChildren { get; set; }
        public virtual List<Employee> Employees { get; set; }
        public virtual List<Ipcr> IpcrsUnderThisUnit { get; set; }


    }
}