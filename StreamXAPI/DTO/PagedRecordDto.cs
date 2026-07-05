namespace StreamXAPI.DTO
{
    public class PagedRecordDto<T>
    {
        public int TotalRecords { get; set; }
        public List<T> Items { get; set; }
    }
}
