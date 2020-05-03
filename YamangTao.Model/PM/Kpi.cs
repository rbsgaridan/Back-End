using System;
using System.Collections.Generic;
using System.Security.AccessControl;
// Key Performance Indicators
// 1 - Organizational Objectives (Refer to GAA)
// 2 - Organizational Outcomes (Refer to GAA)
// 3 - Function (Mandated function of the job)
// 4 - Key Result Area (from OPCR)
// 5 - Success Indicators (Success Indicators can support KRAs from different OPCR)

namespace YamangTao.Model.PM
{
    public class Kpi
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string OrderNumber { get; set; }
        public int? IpcrId { get; set; }
        public virtual Ipcr Ipcr { get; set; }
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
        public string TaskId { get; set; }
        public int? ParentKpiId { get; set; }
        public Kpi ParentKpi { get; set; }
        public List<Kpi> Kpis { get; set; }
    }
}