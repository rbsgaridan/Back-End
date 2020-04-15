using System;
using System.Collections.Generic;
using YamangTao.Core.Document;

namespace YamangTao.Model.PM
{
    public class Ipcr : IDocument
    {
        public int Id { get; set; }
        public string EmployeeId { get; set; }
        public Employee Ratee { get; set; }
        public int OfficeId { get; set; }
        public DateTime DateTargetApproved { get; set; }
        public Employee CompiledBy { get; set; }
        public string CompilerDesignation { get; set; }
        public DateTime PeriodFrom { get; set; }
        public DateTime PeriodTo { get; set; } 
        public List<PerformanceIndicator> KPIs { get; set; }
        public double FinalQrating { get; set; }
        public double FinalErating { get; set; }
        public double FinalTrating { get; set; }
        public double FinalAverageRating { get; set; }
        public string AdjectivalRating { get; set; }
        public Employee ReviewedBy { get; set; }
        public string ReviewerDesignation { get; set; }
        public DateTime DateReviewed { get; set; }
        public Employee ApprovedBy { get; set; }
        public string ApproverDesignation { get; set; }
        public DateTime DateApproved { get; set; }
        public string LandDRecommendation { get; set; }
        public string RnRRecommendation { get; set; }
        public Employee HrDirector { get; set; }
        
        //For workflow purposes
        
        public DateTime? DateCreated { get; set; }
        public DateTime? DateLastModified { get; set; }
        public DateTime? DateLastPrinted { get; set; }
        public bool isLocked { get; set; }

        public string GetDocumentType()
        {
            return "IPCR";
        }
    }
}
