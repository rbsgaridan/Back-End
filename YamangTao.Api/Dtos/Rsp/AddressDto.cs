namespace YamangTao.Api.Dtos.Rsp
{
    public class AddressDto
    {
        public int Id { get; set; }
        public int PdsId { get; set; }
        public string EmployeeId { get; set; }
        public string Description { get; set; }
        public string Block { get; set; }
        public string Street { get; set; }
        public string Purok { get; set; }
        public string Barangay { get; set; }
        public string Municipality { get; set; }
        public string Province { get; set; }
    }
}