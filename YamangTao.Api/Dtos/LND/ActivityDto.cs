using YamangTao.Core.Common;

namespace YamangTao.Api.Dtos.LND
{
    public class ActivityDto : IIdentifyableEntity<string>
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public int? OrgUnitId { get; set; }
        public int ActivityTypeId { get; set; }
        public ActivityTypeDto ActivityType { get; set; } // (LND, Extension, External, Conference, University Event )
        public string Sponsor { get; set; }
        public string ProgramLeader { get; set; }
        public string Venue { get; set; }
        
        public string EntityId 
        { 
            get {
                    return Id;
                } 
        }

    }
}