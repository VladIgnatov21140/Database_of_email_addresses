using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Database_of_email_addresses.Models
{
    public class PageViewModel
    {
        public int RowsCount { get; private set; }
        public int PageNumber { get; private set; }
        public int TotalPages { get; private set; }
        public int PageSize { get; set; }

        public PageViewModel(int rowsCount, int pageNumber, int pageSize)
        {
            PageSize = pageSize;
            RowsCount = rowsCount;
            if (rowsCount > 100)
                RowsCount = 100;
            else if (rowsCount <= 3)
                RowsCount = 3;
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
