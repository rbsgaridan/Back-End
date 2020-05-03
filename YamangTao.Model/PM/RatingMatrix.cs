using System;
using System.Collections.Generic;

namespace YamangTao.Model.PM
{
    public class RatingMatrix
    {
        public uint Id { get; set; }
        public int? KpiId { get; set; }
        public virtual Kpi Kpi { get; set; }
        // Dimension: Quality, Efficiency, Timeliness
        public string Dimension { get; set; } 
        public List<Rating> Ratings { get; set; }
        public string MeansOfVerification { get; set; }
    }
}