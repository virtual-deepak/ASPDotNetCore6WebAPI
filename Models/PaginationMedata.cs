namespace DotNetCoreWebAPI.Models
{
    public class PaginationMetadata
    {
        public PaginationMetadata(
            int pageNumber,
            int pageSize,
            int totalRecords)
        {
            this.TotalRecords = totalRecords;
            this.CurrentPageNumber = pageNumber;
            this.CurrentPageSize = pageSize;
        }

        public int CurrentPageSize { get; set; }

        public int CurrentPageNumber { get; set; }

        public int TotalRecords { get; set; }
    }
}