namespace BASEAPP.Models.DTOs
{
    public class PagedResultDto<T>
    {
        public List<T>? Items { get; set; }
        public int TotalItems { get; set; }
    }
}
