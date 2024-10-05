using FCC.Core.Constants;
using IAC.Domain.ViewModel.Role;
using IAC.Service.Queries.Role;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Softura.EntityFrameworkCore.Abstractions;

namespace IAC.Service.Handlers.Role
{
    public class SearchRoleQueryHandler : IRequestHandler<SearchRoleQuery, SearchRoleFilterResponseVM>
    {
        private readonly IRepository<SearchRoleResponseVM> _iRepository;
        private readonly ILogger<SearchRoleQueryHandler> _logger;
        public SearchRoleQueryHandler(IRepository<SearchRoleResponseVM> iRepository, ILogger<SearchRoleQueryHandler> logger)
        {
            _iRepository = iRepository;
            _logger = logger;
        }
        async Task<SearchRoleFilterResponseVM> IRequestHandler<SearchRoleQuery, SearchRoleFilterResponseVM>.Handle(SearchRoleQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Start SearchRoleQueryHandler, Parameters: RoleName: {request.searchRoleVM.RoleName}", request.searchRoleVM.RoleName);

            var result = await _iRepository.GetFromCommand("SELECT * FROM " + DBConstants.VW_SEARCH_ROLE).ToListAsync(cancellationToken);

            Predicate<SearchRoleResponseVM> role;

            if (request.searchRoleVM.RoleName != "")
            {
                role = x => request.searchRoleVM.RoleName!=null && x.RoleName?.IndexOf(request.searchRoleVM.RoleName, StringComparison.InvariantCultureIgnoreCase) >= 0;
                result = result.FindAll(role).ToList();
            }
            else if (request.searchRoleVM.UserCount != 0)
            {
                role = x => x.AssignedUser.ToString()!.Contains(request.searchRoleVM.UserCount.ToString()!);
                result = result.FindAll(role).ToList();
            }

            Func<SearchRoleResponseVM, object> orderByExpression;
            switch (request.searchRoleVM.OrderBy)
            {
                case nameof(RoleFilter.roleName):
                    orderByExpression = s => s.RoleName ?? string.Empty;
                    break;
                case nameof(RoleFilter.assignedUser):
                    orderByExpression = s => s.AssignedUser ?? 0;
                    break;
                default:
                    orderByExpression = s => s.RoleName ?? string.Empty;
                    break;
            }

            if (request.searchRoleVM.SortOrder == "desc")
                result = result.OrderByDescending(orderByExpression).ToList();
            else
                result = result.OrderBy(orderByExpression).ToList();

            SearchRoleFilterResponseVM searchRoleResponse = new SearchRoleFilterResponseVM();
            searchRoleResponse.Count = result.Count;

            if (request.searchRoleVM.PageNumber != 0 || request.searchRoleVM.PageSize != 0)
                result = result.Skip((request.searchRoleVM.PageNumber - 1) * request.searchRoleVM.PageSize)
                        .Take(request.searchRoleVM.PageSize).ToList();

            searchRoleResponse.searchRoleFilerResponse = result.Select(x => new SearchRoleResponseVM()
            {
                RoleId = x.RoleId,
                RoleName = x.RoleName,
                CreatedBy = x.CreatedBy,
                IsActive = x.IsActive,
                AssignedUser = x.AssignedUser,
            }).ToList();
             
            return searchRoleResponse;
        }
    }
}
