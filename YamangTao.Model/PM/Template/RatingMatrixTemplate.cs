using System.Collections.Generic;
using YamangTao.Core.Common;

namespace YamangTao.Model.PM.Template
{
    public class RatingMatrixTemplate : IIdentifyableEntity<int>
    {
        public int Id { get; set; }
        public int? KpiId { get; set; }
        public virtual KpiTemplate Kpi { get; set; }
        // Dimension: Quality, Efficiency, Timeliness
        public string Dimension { get; set; } 
        public List<RatingTemplate> Ratings { get; set; }
        public string MeansOfVerification { get; set; }

        public int EntityId => Id;
    }
}