using YamangTao.Core.Common;

namespace YamangTao.Dto.LND
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