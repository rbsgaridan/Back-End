using System;
using System.Collections.Generic;

namespace YamangTao.Dto.Pms
{
    public class RatingPeriodDto
    {
         public int Id { get; set; }
        public string Description { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public bool IsActive { get; set; }
        public DateTime DateCreated { get; set; }
        public string CreatedBy { get; set; }
        
    }
}