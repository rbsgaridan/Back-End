using System;
using YamangTao.Core.Common;

namespace YamangTao.Dto.LND
{
    public class CertificateDto : IIdentifyableEntity<string>
    {
        public string Id { get; set; }
        public string ActivityId { get; set; }
        public string EmployeeId { get; set; }
        public string Name { get; set; }
        public string Suffix { get; set; }
        public string Sex { get; set; }
        public string Role { get; set; }
        public string Topic { get; set; }
        public string EventTitle { get; set; }
         public string EventDuration { get; set; }
        public DateTime? CertificateDate { get; set; }
        public string CertificateTypeId { get; set; }
        public string CertificateType { get; set; }
        public ActivityDto TheActivity { get; set; }
        public string EntityId 
        { 
            get {
                    return Id;
                } 
        }

    }
}