using System;
using System.Collections.Generic;

namespace YamangTao.Api.Models.Delivery
{
    public class Supplier
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string ContactPerson { get; set; }
        public string MobileNumber { get; set; }
        public ICollection<Truck> DeliveryTrucks { get; set; }
    }
}
