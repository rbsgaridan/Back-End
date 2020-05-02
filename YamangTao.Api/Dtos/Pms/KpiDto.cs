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
        public int QualityRating { get; set; }
        public int EfficiencyRating { get; set; }
        public int TimelinessRating { get; set; }
        public List<RatingMatrixDto> QualityMatrix { get; set; }
        public List<RatingMatrixDto> EfficiencyMatrix { get; set; }
        public List<RatingMatrixDto> TimelinessMatrix { get; set; }
        public string TaskId { get; set; }
    }
}