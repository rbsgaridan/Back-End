using System.Collections.Generic;

namespace YamangTao.Api.Dtos.Pms
{
    public class KpiDto
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string OrderNumber { get; set; }
        public int IpcrId { get; set; }
        public int KpiTypeId { get; set; }
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
        public List<RatingMatrixDto> RatingMatrices { get; set; }
        public string TaskId { get; set; }
        public int ParentKpiId { get; set; }
        public KpiDto ParentKpi { get; set; }
        public List<KpiDto> Kpis { get; set; }
    }
}