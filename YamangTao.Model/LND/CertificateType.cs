using YamangTao.Core.Common;

namespace YamangTao.Model.LND
{
    public class CertificateType : IIdentifyableEntity<string>
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