//Template success indicators for specific positions in IPCR

using System.Collections.Generic;
using YamangTao.Model.RSP;

namespace YamangTao.Model.PM
{
    public class KPItemplate
    {
        public string Code { get; set; }
        public string JobPositionId { get; set; }
        public JobPosition Position { get; set; }
        public string SuccessIndicator { get; set; }
        public bool HasQuality { get; set; }
        public bool HasEfficiency { get; set; }
        public bool HasTimeliness { get; set; }
        public List<RatingMatrix> QualityMatrix { get; set; }
        public List<RatingMatrix> EfficiencyMatrix { get; set; }
        public List<RatingMatrix> TimelinessMatrix { get; set; }
    }
}