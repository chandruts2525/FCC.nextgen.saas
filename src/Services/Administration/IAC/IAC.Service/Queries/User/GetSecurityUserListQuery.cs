using FCC.Core.ViewModel.GridFilter;
using IAC.Domain.ViewModel.SecurityUser;
using IAC.Service.ViewModel;
using MediatR;

namespace IAC.Service.Queries.User
{
    public class GetSecurityUserListQuery : IRequest<SecurityUserResponseVM>
    {
        public List<ColumnFilter>? gridFilterViewModel { get; set; }
        public string? SearchFilter { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public string? OrderBy { get; set; }
        public string? SortOrderBy { get; set; }

        public GetSecurityUserListQuery(List<ColumnFilter>? _gridFilterViewModel, string? _searchFilter, int? _pageNumber, int? _pageSize, string? _orderBy, string? _sortOrderBy)
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
