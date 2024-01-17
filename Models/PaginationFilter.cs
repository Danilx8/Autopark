namespace Autopark.Models
{
    public class PaginationFilter
    {
        public int PageNum { get; set; }
        public int Limit { get; set; }
        public PaginationFilter()
        {
            PageNum = 1;
            Limit = 10;
        }
        public PaginationFilter(int pageNumber, int pageSize)
        {
            PageNum = pageNumber < 1 ? 1 : pageNumber;
            Limit = pageSize < 1 ? 1 : pageSize;
        }
    }
}
