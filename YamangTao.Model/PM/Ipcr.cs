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
        public string EmployeeId { get; set; }
        public Employee Ratee { get; set; }
        public int JobPositionId { get; set; }
        public JobPosition Position { get; set; }
        public int OrgUnitId { get; set; }
        public OrgUnit Unit { get; set; }
        public string Description { get; set; }
        public DateTime? DateTargetApproved { get; set; }
        public int? RatingPeriodId { get; set; }
        public RatingPeriod RatingPeriod { get; set; }
        public double FinalQrating { get; set; }
        public double FinalErating { get; set; }
        public double FinalTrating { get; set; }
        public double FinalAverageRating { get; set; }
        public string AdjectivalRating { get; set; }
        public string TargetReviewedById { get; set; }
        public string TargetReviewedBy { get; set; }
        public string TargetReviewerDesignation { get; set; }
        public DateTime? TargetDateReviewed { get; set; }
        public string TargetAssessedById { get; set; }
        public string TargetAssessedBy { get; set; }
        public string TargetAssessorDesignation { get; set; }
        public DateTime? TargetDateAssessed { get; set; }
        public string TargetApprovedById { get; set; }
        public string TargetApprovedBy { get; set; }
        public string TargetApproverDesignation { get; set; }
        public DateTime? TargetDateApproved { get; set; }
        public string AccompReviewedById { get; set; }
        public string AccompReviewedBy { get; set; }
        public string AccompReviewerDesignation { get; set; }
        public DateTime? AccomptDateReviewed { get; set; }
        public string AccomptAssessedById { get; set; }
        public string AccompAssessedBy { get; set; }
        public string AccompAssessorDesignation { get; set; }
        public DateTime? AccomptDateAssessed { get; set; }
        public string AccompApprovedById { get; set; }
        public string AccompApprovedBy { get; set; }
        public string AccompApproverDesignation { get; set; }
        public DateTime? AccompDateApproved { get; set; }
        public string LandDRecommendation { get; set; }
        public string RnRRecommendation { get; set; }
        
        //For workflow purposes
        
        public DateTime? DateCreated { get; set; }
        public DateTime? DateLastModified { get; set; }
        public DateTime? DateLastPrinted { get; set; }
        public bool Reviewed { get; set; }
        public bool Compiled { get; set; }
        public bool Approved { get; set; }
        public bool isTargetLocked { get; set; }
        public bool isAccomplishmentLocked { get; set; }
        public string Status { get; set; }
        public string EmployeeIdLocation { get; set; }
        public int EntityId 
        { 
            get {
                    return Id;
                } 
        }

        public string PreviousHolder { get; set; }
        public string CurrentHolder { get; set; }
        public string NextUser { get; set; }

        public string GetDocumentType()
        {
            return "IPCR";
        }
    }
}
