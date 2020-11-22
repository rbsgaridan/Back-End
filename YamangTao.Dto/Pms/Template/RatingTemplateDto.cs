using YamangTao.Core.Common;

namespace YamangTao.Dto.Pms.Template
{
    public class RatingTemplateDto
    {
        public long Id { get; set; }
        public int RatingMatrixId { get; set; }
        public sbyte Rate { get; set; }
        public string Description { get; set; }

    }
}