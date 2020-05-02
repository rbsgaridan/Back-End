namespace YamangTao.Core.HttpParams
{
    public class UserParams
    {
        private int _pageSize = 10;
        private const int MaxPageSize = 50;
        public int PageNumber { get; set; } = 1;
        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = (value > MaxPageSize) ? MaxPageSize : value; }
        }
        public string OrderBy { get; set; } = "knownAs";
        public string Keyword { get; set; }
        public string FilterBy { get; set; }
    }
}