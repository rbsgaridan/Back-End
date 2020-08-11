using YamangTao.Core.Common;

namespace YamangTao.Api.Dtos.LND
{
    public class CertificateTypeDto : IIdentifyableEntity<string>
    {
        public string Id{ get; set; }
        public string Name { get; set; }
        public string EntityId 
        { 
            get {
                    return Id;
                } 
        }

    }
}