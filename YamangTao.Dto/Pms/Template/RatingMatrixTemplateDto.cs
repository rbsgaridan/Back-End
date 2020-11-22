using System.Collections.Generic;
using YamangTao.Core.Common;

namespace YamangTao.Dto.Pms.Template
{
    public class RatingMatrixTemplateDto
    {
        public int Id { get; set; }
        public int? KpiId { get; set; }
        
        // Dimension: Quality, Efficiency, Timeliness
        public string Dimension { get; set; } 
        public List<RatingTemplateDto> Ratings { get; set; }
        public string MeansOfVerification { get; set; }

    }
}