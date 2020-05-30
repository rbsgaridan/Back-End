namespace YamangTao.Core.HttpParams
{
    public interface IParams
    {
        int PageNumber { get; set; }
        int PageSize { get; set; }
        string Keyword { get; set; }
        string OrderBy {get; set;}
    }
}