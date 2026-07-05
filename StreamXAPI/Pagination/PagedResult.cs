namespace StreamXAPI.Pagination
{
    public class PagedResult<T>
    {
        public List<T> Items { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;
        //public PagedResult(List<T> items, int TotalPages, int TotalRecords, int CurrentPage, int PageSize)
        //{
        //    Items = items;
        //    this.TotalPages = TotalPages;
        //    this.TotalRecords = TotalRecords;
        //    this.CurrentPage = CurrentPage;
        //    this.PageSize = PageSize;
        //}
    }
}
