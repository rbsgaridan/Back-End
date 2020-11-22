using System;
using System.Collections.Generic;



namespace YamangTao.Dto.Pms.Template
{
    public class IpcrTemplateDto 
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int JobPositionId { get; set; }
        public string JobPosition { get; set; }
        public int OrgUnitId { get; set; }
        public string OrgUnit { get; set; }
        public List<KpiTemplateDto> Kpis { get; set; }
       
        public DateTime? DateCreated { get; set; }
        public DateTime? DateLastModified { get; set; }


    }
}