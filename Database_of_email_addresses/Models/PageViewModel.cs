using System;

namespace Database_of_email_addresses.Models
{
    public class PageViewModel
    {
        public int RowsCount { get; set; }
        public int PageNumber { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }

        public PageViewModel(int rowsCount, int pageNumber, int pageSize)
        {
            PageSize = pageSize;
            RowsCount = rowsCount;
            PageNumber = pageNumber;
            TotalPages = (int)Math.Ceiling(RowsCount / (double)pageSize);
        }

        public bool HasPreviousPage
        {
            get
            {
                return (PageNumber > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (PageNumber < TotalPages);
            }
        }
    }
}
