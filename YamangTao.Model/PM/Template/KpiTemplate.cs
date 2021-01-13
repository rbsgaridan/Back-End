using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using YamangTao.Core.Common;

namespace YamangTao.Model.PM.Template
{
    public class KpiTemplate : IIdentifyableEntity<int>
    {
       public int Id { get; set; }
        public string Code { get; set; } // Indicator Code from indicator Inventory
        public int? OrderNumber { get; set; } // 
        public string mfoOO { get; set; } // MFO or OO from the balanced scorecard
        public HierarchyId Path { get; set; }
        public int? IpcrTemplateId { get; set; } // not null if core and support
        // public virtual IpcrTemplate IpcrTemplateParent { get; set; }
        public int KpiTypeId { get; set; }
        public virtual KpiType KpiType { get; set; }
        public string SuccessIndicator { get; set; }
        public bool HasQuality { get; set; }
        public bool HasEfficiency { get; set; }
        public bool HasTimeliness { get; set; }
        public float AverageRating { get; set; }
        public List<RatingMatrixTemplate> RatingMatrixTemplates { get; set; }
        public string TaskId { get; set; } // Task Inventory
        public int? ParentKpiId { get; set; }
        public KpiTemplate ParentKpi { get; set; }
        public List<KpiTemplate> Kpis { get; set; }
        public float? MaxWeight { get; set; }
        public float? MinWeight { get; set; }
        public int? MinIndicators { get; set; }
        public bool? Required { get; set; }
        // public int? IpcrOwnerId { get; set; } // extra FK for easier access in IPCR
        // public string EmployeeId { get; set; }// Owner

        public int EntityId => Id;
    }
}