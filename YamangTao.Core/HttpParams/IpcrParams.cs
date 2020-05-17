namespace YamangTao.Core.HttpParams
{
    public class IpcrParams
    {
        private int _pageSize = 10;
        private const int MaxPageSize = 50;
        public int PageNumber { get; set; } = 1;
        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = (value > MaxPageSize) ? MaxPageSize : value; }
        }
        public string OrderBy { get; set; }
        public string Keyword { get; set; }
        public string RateeId { get; set; }
        public string CompiledById { get; set; }
        public string ReviewedById { get; set; }
        public string ApprovedById { get; set; }
        public int JobPositionId { get; set; }
        public int OrgUnitId { get; set; }
        
    }
}