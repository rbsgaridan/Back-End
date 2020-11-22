using System.Collections.Generic;



namespace YamangTao.Dto.Pms.Template
{
    public class KpiTemplateDto
    {
       public int Id { get; set; }
        public string Code { get; set; } // Indicator Code from indicator Inventory
        public string OrderNumber { get; set; } // 
        public string mfoOO { get; set; } // MFO or OO from the balanced scorecard
        public string Path { get; set; }
        public int? IpcrTemplateId { get; set; } // not null if core and support
        public int KpiTypeId { get; set; }
        public string KpiType { get; set; }
        public string SuccessIndicator { get; set; }
        public bool HasQuality { get; set; }
        public bool HasEfficiency { get; set; }
        public bool HasTimeliness { get; set; }
        public float AverageRating { get; set; }
        public List<RatingMatrixTemplateDto> RatingMatrixTemplates { get; set; }
        public string TaskId { get; set; } // Task Inventory
        public int? ParentKpiId { get; set; }
       
        public List<KpiTemplateDto> Kpis { get; set; }
        public float? MaxWeight { get; set; }
        public float? MinWeight { get; set; }
        public int? IpcrOwnerId { get; set; } // extra FK for easier access in IPCR
        public string EmployeeId { get; set; }// Owner

       
    }
}