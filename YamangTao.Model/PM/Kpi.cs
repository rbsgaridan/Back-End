using System;
using System.Collections.Generic;
using System.Security.AccessControl;
using YamangTao.Core.Common;
// Key Performance Indicators
// 1 - Organizational Objectives (Refer to GAA)
// 2 - Organizational Outcomes (Refer to GAA)
// 3 - Function (Mandated function of the job)
// 4 - Key Result Area (from OPCR)
// 5 - Success Indicators (Success Indicators can support KRAs from different OPCR)

namespace YamangTao.Model.PM
{
    public class Kpi : IIdentifyableEntity<int>
    {
        public int Id { get; set; }
        public string Code { get; set; } // Indicator Code from indicator Inventory
        public int? OrderNumber { get; set; } // 
        public string mfoOO { get; set; } // MFO or OO from the balanced scorecard
        public int? IpcrId { get; set; } // not null if core and support
        // public virtual Ipcr Ipcr { get; set; }
        public int KpiTypeId { get; set; }
        public virtual KpiType KpiType { get; set; }
        public float Weight { get; set; }
        public string SuccessIndicator { get; set; }
        public string ActualAccomplishment { get; set; }
        public bool HasQuality { get; set; }
        public bool HasEfficiency { get; set; }
        public bool HasTimeliness { get; set; }
        public float QualityRating { get; set; }
        public float EfficiencyRating { get; set; }
        public float TimelinessRating { get; set; }
        public float AverageRating { get; set; }
        public List<RatingMatrix> RatingMatrices { get; set; }
        public string TaskId { get; set; } // Task Inventory
        public int? ParentKpiId { get; set; }
        public Kpi ParentKpi { get; set; }
        public List<Kpi> Kpis { get; set; }
        public float? MaxWeight { get; set; }
        public float? MinWeight { get; set; }
        public int? IpcrOwnerId { get; set; } // extra FK for easier access in IPCR
        public string EmployeeId { get; set; }// Owner
        public int? MinIndicators { get; set; }
        public int? TotalIndicators { get; set; }
        public bool? Selected { get; set; }
        public bool? Required { get; set; }

        public int EntityId => Id;
    }
}