namespace StreamXAPI.Pagination
{
    public class MovieQueryParams : PaginationParams
    {
        public string? Genre { get; set; }
        public string? Actor { get; set; }
        public string? SortBy { get; set; }
        public string? Search { get; set; }
    }
}