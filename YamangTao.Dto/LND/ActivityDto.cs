using System;
using YamangTao.Core.Common;

namespace YamangTao.Dto.LND
{
    public class ActivityDto : IIdentifyableEntity<string>
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public int? OrgUnitId { get; set; }
        public int ActivityTypeId { get; set; }
        public string ActivityType { get; set; } // (LND, Extension, External, Conference, University Event)
        public string Sponsor { get; set; }
        public string ProgramLeader { get; set; }
        public string Venue { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public string Duration { get; set; }  
        
        public string EntityId 
        { 
            get {
                    return Id;
                } 
        }

    }
}