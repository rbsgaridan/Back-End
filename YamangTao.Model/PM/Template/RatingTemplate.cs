using YamangTao.Core.Common;

namespace YamangTao.Model.PM.Template
{
    public class RatingTemplate : IIdentifyableEntity<long>
    {
        public long Id { get; set; }
        public int RatingMatrixId { get; set; }
        public RatingMatrixTemplate Matrix { get; set; }
        public sbyte Rate { get; set; }
        public string Description { get; set; }

        public long EntityId => Id;
    }
}