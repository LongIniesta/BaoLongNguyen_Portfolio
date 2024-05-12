using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Pagination
{
    public class PagedList<T>
    {
        public int CurrentPage { get; set; }
        public int Size { get; set; }
        public int TotalResult { get; set; }
        public int TotalPages { get; set; }
        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;
        public List<T> Data { get; set; }
        public PagedList(List<T> items, int count, int pageNumber, int pageSize) 
        {
            TotalResult = count;
            Size = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            Data = new List<T>();
            Data.AddRange(items);
        }
    }
}
