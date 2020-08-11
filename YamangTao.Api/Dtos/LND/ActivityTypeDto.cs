using System.Collections.Generic;
using YamangTao.Core.Common;

namespace YamangTao.Api.Dtos.LND
{
    public class ActivityTypeDto : IIdentifyableEntity<int>
    {
        public int Id { get; set; }
        public string ShortName { get; set; }
        public string Description { get; set; }
        public IEnumerable<ActivityDto> Activities { get; set; }
        public int EntityId 
        { 
            get {
                    return Id;
                } 
        }

    }
}