using System;
using System.Collections.Generic;
using YamangTao.Core.Common;

namespace YamangTao.Model.PM
{
    public class RatingMatrix : IIdentifyableEntity<long>
    {
        public long Id { get; set; }
        public int? KpiId { get; set; }
        public virtual Kpi Kpi { get; set; }
        // Dimension: Quality, Efficiency, Timeliness
        public string Dimension { get; set; } 
        public List<Rating> Ratings { get; set; }
        public string MeansOfVerification { get; set; }
        public long EntityId => Id;
    }
}