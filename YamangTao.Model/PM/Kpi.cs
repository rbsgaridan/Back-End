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
        public int IpcrId { get; set; }
        public virtual Ipcr Ipcr { get; set; }
        public int KpiTypeId { get; set; }
        public virtual KpiType KpiType { get; set; }
        public float Weight { get; set; }
        public string SuccessIndicator { get; set; }
        public string ActualAccomplishment { get; set; }
        public bool HasQuality { get; set; }
        public bool HasEfficiency { get; set; }
        public bool HasTimeliness { get; set; }
        public int QualityRating { get; set; }
        public int EfficiencyRating { get; set; }
        public int TimelinessRating { get; set; }
        public List<RatingMatrix> QualityMatrix { get; set; }
        public List<RatingMatrix> EfficiencyMatrix { get; set; }
        public List<RatingMatrix> TimelinessMatrix { get; set; }
        public string TaskId { get; set; }
    }
}