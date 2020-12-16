// Types of KPI
// 1 - Organizational Objectives
// 2 - Organizational Outcomes
// 3 - Strategic Goals
// 4 - Strategic Objectives
// 5 - Function
// 6 - Key Result Area
// 7 - Key Performance Indicator
// 8 - Success Indicator

using System.Collections.Generic;
using YamangTao.Core.Common;

namespace YamangTao.Model.PM
{
    public class KpiType : IIdentifyableEntity<int>
    {
        public int Id { get; set; }
        public string Description { get; set; }

        public int EntityId => Id;
        // public virtual List<Kpi> KPIs { get; set; }
    }
}