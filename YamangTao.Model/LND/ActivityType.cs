using System.Collections.Generic;
using YamangTao.Core.Common;

namespace YamangTao.Model.LND
{
    public class ActivityType : IIdentifyableEntity<int>
    {
        public int Id { get; set; }
        public string ShortName { get; set; }
        public string Description { get; set; }
        public IEnumerable<Activity> Activities { get; set; }
        public int EntityId 
        { 
            get {
                    return Id;
                } 
        }

    }
}