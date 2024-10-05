using IAC.Domain.ViewModel.Role;
using IAC.Service.ViewModel.Role;
using MediatR;

namespace IAC.Service.Queries.Role
{
	public class SearchRoleQuery : IRequest<SearchRoleFilterResponseVM>
    {
        public SearchRoleVM searchRoleVM { get; set; }
        public SearchRoleQuery(SearchRoleVM _searchRole)
        {
			searchRoleVM = _searchRole;
        }
    }
}