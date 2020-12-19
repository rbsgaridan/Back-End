using System;

namespace YamangTao.Model.DocumentTracking
{
    public class DocumentLog // History of document
    {
        public long Id { get; set; }
        public DateTime LogDate { get; set; } // Date and time of log
        public string EmployeeSourceId { get; set; } // EmployeeID of source
        public string EmployeeSourceName { get; set; }
        public string EmployeeDestinationId { get; set; } //EmployeeID of Destination
        public string EmployeeDestName { get; set; }
        public string Status { get; set; } // For review, Return to sender, ...
        public int DocumentId { get; set; } // Primary key of the document
        public string DocumentType { get; set; } // Document Type
        public int OrgUnitSourceId { get; set; }
        public string OrgUnitSource { get; set; }
        public int OrgUnitDestinationId { get; set; }
        public int OrgUnitDestination { get; set; }
    }
}