using DTOs.Pagination;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Utility
{
    public static class PagingHelper
    {
        public static PagedList<T> GetWithPaging<T>(
            this IQueryable<T> source,
            BasePagingSearchRequest requestParam)
        {
            var count = source.Count();

            requestParam.PageSize = requestParam.PageSize < 1 ? 1 : requestParam.PageSize;
            requestParam.Page = requestParam.Page < 1 ? 1 : requestParam.Page;

            var items =  source
                .Skip(requestParam.Page == 1 ? 0 : requestParam.PageSize * (requestParam.Page - 1)) // Paging
                .Take(requestParam.PageSize).ToList(); // Take only a number of items

            return new PagedList<T>(items, count, requestParam.Page, requestParam.PageSize);
        }
    }
}
