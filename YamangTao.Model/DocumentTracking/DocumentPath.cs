namespace YamangTao.Model.DocumentTracking
{
    public class DocumentPath //Default path of the document, for reference only
    {
        public int Id { get; set; }
        public string DocumentType { get; set; }
        public int Order { get; set; }
        public int OrgUnitId { get; set; }
        public string OrgUnitName { get; set; }
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
    }
}