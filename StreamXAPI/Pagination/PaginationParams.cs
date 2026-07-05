namespace StreamXAPI.Pagination
{
    public class PaginationParams
    {
        public int PageNumber { get; set; }
        
        private const int MaxPageSize = 25;

        private int _pageSize = 10;

        public int PageSize
        {
            get { return _pageSize; }
            set
            {
                if (value > MaxPageSize)
                {
                    _pageSize = MaxPageSize;
                }
                else
                {
                    _pageSize = value;
                }
            }
        }
    }
}
