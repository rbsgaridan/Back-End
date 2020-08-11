using System;

namespace YamangTao.Api.Models.Delivery
{
    public class Delivery
    {
        public int Id { get; set; }
        public string DrNumber { get; set; }
        public int TruckId { get; set; }
        public Truck DeliveryTruck { get; set; }
        public int SupplierId { get; set; }
        public Supplier DeliveredBy { get; set; }
        public string LoadType { get; set; }
        public double Quantity { get; set; }
        public int VolumePerLoad { get; set; }
        public int ProjectId { get; set; }
        public Project DeliveredAtProject { get; set; }
        public DateTime DeliveryDate { get; set; }
        public DateTime DateAdded { get; set; }
        public string AddedBy { get; set; }
    }
}
