using System;
using System.Collections.Generic;

namespace YamangTao.Dto.Pms
{
    public class IpcrForCreateDto
    {
        public int Id { get; set; }
        public bool IsTemplate { get; set; }
        public string EmployeeId { get; set; }
        public int JobPositionId { get; set; }
        public int OrgUnitId { get; set; }
        public DateTime DateTargetApproved { get; set; }
        public DateTime PeriodFrom { get; set; }
        public DateTime PeriodTo { get; set; } 
        public List<KpiDto> KPIs { get; set; }
        public double FinalQrating { get; set; }
        public double FinalErating { get; set; }
        public double FinalTrating { get; set; }
        public double FinalAverageRating { get; set; }
        public string AdjectivalRating { get; set; }
        public string ReviewedById { get; set; }
        public string ApprovedById { get; set; }
        public string LandDRecommendation { get; set; }
        public string RnRRecommendation { get; set; }
        
        //For workflow purposes
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