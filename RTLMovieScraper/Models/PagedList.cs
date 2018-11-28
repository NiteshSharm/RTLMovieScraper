using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RTLMovieScraper.Models
{
    /// <summary>
    /// Generic PeginatedList.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagedList<T>
    {
        public PagedList(IQueryable<T> source, int? pageNumber, int? pageSize)
        {
            this.TotalItems = source.Count();
            this.PageNumber = pageNumber.HasValue ? pageNumber.Value : 1;
            this.PageSize = pageSize.HasValue ? pageSize.Value : 10;
            this.List = source
                            .Skip(this.PageSize * (this.PageNumber - 1))
                            .Take(this.PageSize)
                            .ToList();
        }

        public int TotalItems { get; }
        public int PageNumber { get; }
        public int PageSize { get; }
        public List<T> List { get; }
        public int TotalPages =>
              (int)Math.Ceiling(this.TotalItems / (double)this.PageSize);
        public bool HasPreviousPage => this.PageNumber > 1;
        public bool HasNextPage => this.PageNumber < this.TotalPages;
        public int NextPageNumber =>
               this.HasNextPage ? this.PageNumber + 1 : this.TotalPages;
        public int PreviousPageNumber =>
               this.HasPreviousPage ? this.PageNumber - 1 : 1;

    }
}
