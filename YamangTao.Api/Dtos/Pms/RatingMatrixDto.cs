using System.Collections.Generic;

namespace YamangTao.Api.Dtos.Pms
{
    public class RatingMatrixDto
    {
        public long Id { get; set; }
        public int? KpiId { get; set; }
        // Dimension: Quality, Efficiency, Timeliness
        public string Dimension { get; set; } 
        public List<RatingDto> Ratings { get; set; }
        public string MeansOfVerification { get; set; }
    }
}