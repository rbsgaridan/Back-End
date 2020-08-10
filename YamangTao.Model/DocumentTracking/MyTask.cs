using System.ComponentModel;
using System;

namespace YamangTao.Model.DocumentTracking
{
    public class MyTask
    {
        public long Id { get; set; } 
        public DateTime DateCreated { get; set; }
        public DateTime? DateDue { get; set; }
        public string Description { get; set; }
        public bool IsDone { get; set; }
        public DateTime DateCompleted { get; set; }
        public string Url { get; set; }
        public string DocumentType { get; set; }
        public int DocumentId { get; set; }
    }
}