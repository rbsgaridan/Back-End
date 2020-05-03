using System;

namespace YamangTao.Api.Dtos.Pms
{
    public class IpcrForListDto
    {
        public int Id { get; set; }
        public bool IsTemplate { get; set; }
        public string Ratee { get; set; }
        public string Position { get; set; }
        public string Unit { get; set; }
        public DateTime DateTargetApproved { get; set; }
        public string CompiledBy { get; set; }
        public string CompilerDesignation { get; set; }
        public DateTime PeriodFrom { get; set; }
        public DateTime PeriodTo { get; set; }
        public double FinalQrating { get; set; }
        public double FinalErating { get; set; }
        public double FinalTrating { get; set; }
        public double FinalAverageRating { get; set; }
        public string AdjectivalRating { get; set; }
        public string ReviewedBy { get; set; }
        public string ReviewerDesignation { get; set; }
        public DateTime DateReviewed { get; set; }
        public string ApprovedBy { get; set; }
        public string ApproverDesignation { get; set; }
        public DateTime DateApproved { get; set; }
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

        public string GetDocumentType()
        {
            return "IPCR";
        }
    }
}