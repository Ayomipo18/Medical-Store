using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Shared.Helpers
{
    public class PagedList<T> : List<T>
    {
        public int CurrentPage { get; private set; }
        public int TotalPages { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }
        public bool HasPrevious => (CurrentPage > 1);
        public bool HasNext => (CurrentPage < TotalPages);

        public PagedList(List<T> items, int count, int pageNumber, int pageSize)
        {
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            AddRange(items);
        }

        public static async Task<PagedList<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize, string sort = null)
        {
            var count = await source.CountAsync();
            if (pageSize == 0)
            {
                pageSize = count;
            }

            if (!string.IsNullOrEmpty(sort))
            {
                var sortedData = SortHelper<T>.OrderByDynamic(source, sort);

                var items = await sortedData.Skip(((pageNumber - 1) * pageSize)).Take(pageSize).ToListAsync();
                return new PagedList<T>(items, count, pageNumber, pageSize);
            }
            else
            {
                var items = await source.Skip(((pageNumber - 1) * pageSize)).Take(pageSize).ToListAsync();
                return new PagedList<T>(items, count, pageNumber, pageSize);
            }
        }
        public static PagedList<T> Create(IQueryable<T> source, int pageNumber, int pageSize, string sort = null)
        {
            var count = source.Count();
            if (pageSize == 0)
            {
                pageSize = count;
            }

            if (!string.IsNullOrEmpty(sort))
            {
                var sortedData = SortHelper<T>.OrderByDynamic(source, sort);

                var items = sortedData.Skip(((pageNumber - 1) * pageSize)).Take(pageSize).ToList();
                return new PagedList<T>(items, count, pageNumber, pageSize);
            }
            else
            {
                var items = source.Skip(((pageNumber - 1) * pageSize)).Take(pageSize).ToList();
                return new PagedList<T>(items, count, pageNumber, pageSize);
            }
        }
    }
    public enum ResourceUriType
    {
        PreviousPage,
        NextPage,
        CurrentPage
    }
}
