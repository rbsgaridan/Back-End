namespace YamangTao.Model.DocumentTracking
{
    public class DocumentPath //Default path of the document, for reference only
    {
        public int Id { get; set; }
        public string DocumentType { get; set; }
        public int Order { get; set; }
        public string Designation { get; set; } // Department Head, Dean, Director, Vice President
        public int OrgUnitId { get; set; } // for specific org unit
        public string OrgUnitName { get; set; }
        public string EmployeeId { get; set; } // for specific employee
        public string EmployeeName { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
    }
}