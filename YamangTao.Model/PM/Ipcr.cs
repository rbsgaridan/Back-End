using System;
using System.Collections.Generic;
using YamangTao.Core.Common;
using YamangTao.Core.Document;
using YamangTao.Model.OrgStructure;
using YamangTao.Model.RSP;

namespace YamangTao.Model.PM
{
    public class Ipcr : IDocument, IIdentifyableEntity<int>
    {
        public int Id { get; set; }
        public bool IsTemplate { get; set; }
        public string EmployeeId { get; set; }
        public Employee Ratee { get; set; }
        public int JobPositionId { get; set; }
        public JobPosition Position { get; set; }
        public int OrgUnitId { get; set; }
        public OrgUnit Unit { get; set; }
        public DateTime? DateTargetApproved { get; set; }
        public string CompiledById { get; set; }
        public Employee CompiledBy { get; set; }
        public string CompilerDesignation { get; set; }
        public DateTime PeriodFrom { get; set; }
        public DateTime PeriodTo { get; set; } 
        public List<Kpi> KPIs { get; set; }
        public double FinalQrating { get; set; }
        public double FinalErating { get; set; }
        public double FinalTrating { get; set; }
        public double FinalAverageRating { get; set; }
        public string AdjectivalRating { get; set; }
        public string ReviewedById { get; set; }
        public Employee ReviewedBy { get; set; }
        public string ReviewerDesignation { get; set; }
        public DateTime? DateReviewed { get; set; }
        public string ApprovedById { get; set; }
        public Employee ApprovedBy { get; set; }
        public string ApproverDesignation { get; set; }
        public DateTime? DateApproved { get; set; }
        public string LandDRecommendation { get; set; }
        public string RnRRecommendation { get; set; }
        
        //For workflow purposes
        
        public DateTime? DateCreated { get; set; }
        public DateTime? DateLastModified { get; set; }
        public DateTime? DateLastPrinted { get; set; }
        public bool Reviewed { get; set; }
        public bool Compiled { get; set; }
        public bool Approved { get; set; }
        public bool isLocked { get; set; }
        public string Status { get; set; }
        public string EmployeeIdLocation { get; set; }
        public int EntityId 
        { 
            get {
                    return Id;
                } 
        }

        public string GetDocumentType()
        {
            return "IPCR";
        }
    }
}
