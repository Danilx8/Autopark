namespace Autopark.Models
{
    public class PaginationFilter
    {
        public int Page { get; set; }
        public int Limit { get; set; }
        public PaginationFilter()
        {
            Page = 1;
            Limit = 10;
        }
        public PaginationFilter(int pageNumber, int pageSize)
        {
            Page = pageNumber < 1 ? 1 : pageNumber;
            Limit = pageSize < 1 ? 1 : pageSize;
        }
    }
}
