using System;

namespace YamangTao.Core.HttpParams
{
    public class PdsParams
    {
        private int _pageSize = 10;
        private const int MaxPageSize = 50;
        public int PageNumber { get; set; }
        public int PageSize 
        { 
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;  
        }
        public string Keyword { get; set; }
        public string OrderBy { get; set; }
        public int? PdsId { get; set; }
        public string EmployeeId { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
    }
}
