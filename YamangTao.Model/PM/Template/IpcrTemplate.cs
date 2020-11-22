using System;
using System.Collections.Generic;
using YamangTao.Core.Common;
using YamangTao.Model.OrgStructure;
using YamangTao.Model.RSP;

namespace YamangTao.Model.PM.Template
{
    public class IpcrTemplate : IIdentifyableEntity<int>
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int JobPositionId { get; set; }
        public int OrgUnitId { get; set; }
        public List<KpiTemplate> Kpis { get; set; }
       
        public DateTime? DateCreated { get; set; }
        public DateTime? DateLastModified { get; set; }

        public int EntityId => Id;
    }
}