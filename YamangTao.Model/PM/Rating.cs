namespace YamangTao.Model.PM
{
    public class Rating
    {
        public uint id { get; set; }
        public uint RatingMatrixId { get; set; }
        public RatingMatrix Matrix { get; set; }
        public sbyte Rate { get; set; }
        public string Desciption { get; set; }
    }
}