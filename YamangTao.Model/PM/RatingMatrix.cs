using System;
using System.Collections.Generic;

namespace YamangTao.Model.PM
{
    public class RatingMatrix
    {
        public uint Id { get; set; }
        public int? KpiQId { get; set; }
        public int? KpiEId { get; set; }
        public int? KpiTId { get; set; }
        public virtual Kpi KpiQ { get; set; }
        public virtual Kpi KpiE { get; set; }
        public virtual Kpi KpiT { get; set; }
        
        // Dimension: Quality, Efficiency, Timeliness
        public string Dimension { get; set; } 
        public List<Rating> Ratings { get; set; }
        public string MeansOfVerification { get; set; }
    }
}