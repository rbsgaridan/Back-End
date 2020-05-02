// Types of KPI
// 1 - Organizational Objectives
// 2 - Organizational Outcomes
// 3 - Function
// 4 - Key Result Area
// 5 - Success Indicator

using System.Collections.Generic;

namespace YamangTao.Model.PM
{
    public class KpiType
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public virtual List<Kpi> KPIs { get; set; }
    }
}