using WorkManagement.Service.ViewModel;
using WorkManagement.Service.ViewModel.JobType;
using MediatR;
using FCC.Core.ViewModel.GridFilter;

namespace WorkManagement.Service.Queries.JobType
{
    public class GetJobTypeListQuery : IRequest<GetAllJobTypeVM>
    {
        public List<ColumnFilter>? gridFilterViewModel { get; set; }
        public string? SearchFilter { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public string? OrderBy { get; set; }
        public string? SortOrderBy { get; set; }

        public GetJobTypeListQuery(List<ColumnFilter>? _gridFilterViewModel, string? _searchFilter, int? _pageNumber, int? _pageSize, string? _orderBy, string? _sortOrderBy)
        {
            gridFilterViewModel = _gridFilterViewModel;
            SearchFilter = _searchFilter;
            PageNumber = _pageNumber;
            PageSize = _pageSize;
            OrderBy = _orderBy;
            SortOrderBy = _sortOrderBy;
        }
    }
}
