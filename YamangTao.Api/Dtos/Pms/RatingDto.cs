namespace YamangTao.Api.Dtos.Pms
{
    public class RatingDto
    {
        // public uint id { get; set; }
        public long RatingMatrixId { get; set; }
        public sbyte Rate { get; set; }
        public string Description { get; set; }
    }
}