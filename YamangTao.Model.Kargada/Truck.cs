using System.Collections.Generic;

namespace YamangTao.Api.Models.Delivery
{
    public class Truck
    {
        public int Id { get; set; }
        public string PlateNumber { get; set; }
        public double LoadCapacity { get; set; }
        public string Description { get; set; }
        public int SupplierId { get; set; }
        public Supplier OwnedBy { get; set; }
        public ICollection<Delivery> TruckDeliveries { get; set; }
        
    }
}