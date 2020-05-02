using System.Collections.Generic;

namespace YamangTao.Api.Dtos.Pms
{
    public class RatingMatrixDto
    {
        public uint Id { get; set; }
        public int? KpiQId { get; set; }
        public int? KpiEId { get; set; }
        public int? KpiTId { get; set; }
        
        // Dimension: Quality, Efficiency, Timeliness
        public string Dimension { get; set; } 
        public List<RatingDto> Ratings { get; set; }
        public string MeansOfVerification { get; set; }
    }
}