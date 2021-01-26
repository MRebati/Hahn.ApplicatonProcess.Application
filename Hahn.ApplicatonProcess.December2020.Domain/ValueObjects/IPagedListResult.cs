using System.Collections.Generic;

namespace Hahn.ApplicationProcess.December2020.Domain.ValueObjects
{
    public interface IPagedListResult<T>
    {
        List<T> Result { get; set; }

        int PageSize { get; set; }

        int PageNumber { get; set; }

        int TotalCount { get; set; }

        IPagedListResult<T> Create(IEnumerable<T> input);

        IPagedListResult<T> Create(IEnumerable<T> input,int totalCount, int pageSize);
    }
}