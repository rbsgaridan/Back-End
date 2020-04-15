using System;
using System.Collections.Generic;

namespace YamangTao.Model.PM
{
    public class RatingMatrix
    {
        public uint Id { get; set; }
        public int PerformanceIndicatorId { get; set; }
        public string RatingFor { get; set; }
        public List<Rating> Rating { get; set; }
        public string MeansOfVerification { get; set; }
    }
}