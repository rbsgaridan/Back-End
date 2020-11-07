using System;

namespace YamangTao.Dto.Rsp
{
    public class IdentificationDto
    {
        public int Id { get; set; }
        public int PdsId { get; set; }
        public string EmployeeId { get; set; }
        public string IdType { get; set; }
        public string Control { get; set; }
        public string IssuanceDatePlace { get; set; }
        public DateTime? DateIssued { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}