using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Pagination
{
    public abstract class BasePagingSearchRequest
    {
        //define some constant values
        private const int MaxPageSize = 50;
        private const int DefaultPage = 1;
        private const int DefaultPageSize = 50;
        private const string DefaultSort = "id_asc";

        private int _page = DefaultPage;
        //get or set the current page number
        public int Page { get; set; } = DefaultPage;

        private int _pageSize = DefaultPageSize;
        //get or set the size of current page
        [Range(Int32.MinValue,50, ErrorMessage = "Input page size is over the safe limitation of 50")]
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value > MaxPageSize ? MaxPageSize : value;
        }
    }
}
