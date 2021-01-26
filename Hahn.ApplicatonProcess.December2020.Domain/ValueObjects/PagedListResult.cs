using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hahn.ApplicationProcess.December2020.Domain.ValueObjects
{
    public class PagedListResult<T>: IPagedListResult<T>
    {
        public List<T> Result { get; set; }

        public int PageSize { get; set; }

        public int PageNumber { get; set; }

        public int TotalCount { get; set; }

        public IPagedListResult<T> Create(IEnumerable<T> input,int totalCount, int pageSize = 20)
        {
            TotalCount = totalCount;
            PageSize = pageSize;
            Result = input.ToList();
            return this;
        }

        public IPagedListResult<T> Create(IEnumerable<T> input)
        {
            this.Result = input.ToList();
            return this;
        }
    }
}
