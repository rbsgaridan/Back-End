using System;
using System.Collections.Generic;

namespace YamangTao.Api.Models.Delivery
{
    public class Project
    {
        public int Id { get; set; }
        public string Company { get; set; }
        public string ProjectName { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public ICollection<Delivery> Deliveries { get; set; }
    }
}
