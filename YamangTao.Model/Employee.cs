using System.Data;
using System;
using YamangTao.Model.OrgStructure;
using System.Collections.Generic;
using YamangTao.Model.PM;

namespace YamangTao.Model
{
    public class Employee
    {
        public string Id { get; set; }
        public string EmployeeGroup { get; set; } //Teaching or Non-Teaching
        public string CurrentStatus { get; set; } //Employment Status
        public string Lastname { get; set; }
        public string Firstname { get; set; }
        public string MiddleName { get; set; }
        public string MI { get; set; }
        public string Suffix { get; set; } // Jr., Sr., II, III
        public DateTime? BirthDate { get; set; }
        public string Sex { get; set; }
        public string Telephone { get; set; }
        public string MobileNumber { get; set; }
        public string EmailAddress { get; set; }
        public bool InActive { get; set; }
        public DateTime? DateHired { get; set; }
        public bool Resigned { get; set; }
        public DateTime? DateResigned { get; set; }
        public bool Terminated { get; set; }
        public DateTime? DateTerminated { get; set; }
        public bool Retired { get; set; }
        public DateTime? DateRetired { get; set; }
        public int? BranchCampusId { get; set; }
        public BranchCampus CurrentCampus { get; set; }
        public int? OrgUnitId { get; set; }
        public OrgUnit CurrentUnit { get; set; }
        public virtual List<OrgUnit> HeadedUnits { get; set; } // Home Department. If may designation pwede rin xa dun
        // public virtual List<Ipcr> IPCRs { get; set; }
        // public virtual List<Ipcr> ReviewedIpcrs { get; set; }
        // public virtual List<Ipcr> CompiledIpcrs { get; set; }
        // public virtual List<Ipcr> ApprovedIpcrs { get; set; }

        
        public string FullName { get; set; }

        public string GetAge() 
        { 
                return DateTime.Now.Subtract(BirthDate ?? DateTime.Now).ToString();
        }

        

    }
}
