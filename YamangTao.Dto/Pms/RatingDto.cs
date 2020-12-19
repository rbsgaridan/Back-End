namespace YamangTao.Dto.Pms
{
    public class RatingDto
    {
       public long Id { get; set; }
        public long RatingMatrixId { get; set; }
        public sbyte Rate { get; set; }
        public string Description { get; set; }
    }
}