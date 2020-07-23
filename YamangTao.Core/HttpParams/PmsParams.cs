using System;

namespace YamangTao.Core.HttpParams
{
    public class PmsParams
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
        public string Keyword { get; set; } //Used for search
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public string Filter { get; set; } //Filtered field for searching using Keyword
        public string Filter1 { get; set; }//2nd Filtered field for searching using Keyword
        public string FilterByKey { get; set; } //Field holding a foreign key or a boolean field
        public int? KeyInt { get; set; }//Value for the filtered int jey
        public bool? KeyBool { get; set; }//Value for the filtered boolean key
        public string EmployeeId { get; set; }
    }
}