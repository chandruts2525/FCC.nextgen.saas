using FCC.Core.Constants;
using FCC.Core.ViewModel.GridFilter;
using IAC.Domain.Model;
using IAC.Domain.ViewModel.SecurityUser;
using IAC.Service.Queries.User;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OrganizationStructure.Domain.Model;
using Softura.EntityFrameworkCore.Abstractions;

namespace IAC.Service.Handlers.User
{
	public class GetSecurityUserListQueryHandler : IRequestHandler<GetSecurityUserListQuery, SecurityUserResponseVM>
	{
		private readonly IRepository<SecurityUser> _securityUserRepository;
		public readonly IRepository<SecurityUserBusinessEntity> _securityUserBusinessEntityRepository;
		public readonly IRepository<BusinessEntity> _businessEntityRepository;
		public readonly IRepository<BusinessEntityType> _businessEntityTypeRepository;
		private readonly ILogger<GetSecurityUserListQueryHandler> _logger;

		public GetSecurityUserListQueryHandler(
			IRepository<SecurityUser> securityUserRepository,
			IRepository<SecurityUserBusinessEntity> securityUserBusinessEntityRepository,
			IRepository<BusinessEntity> businessEntityRepository,
			IRepository<BusinessEntityType> businessEntityTypeRepository, ILogger<GetSecurityUserListQueryHandler> logger)
		{
			_securityUserRepository = securityUserRepository;
			_securityUserBusinessEntityRepository = securityUserBusinessEntityRepository;
			_businessEntityRepository = businessEntityRepository;
			_businessEntityTypeRepository = businessEntityTypeRepository;
			_logger = logger;
		}

		async Task<SecurityUserResponseVM> IRequestHandler<GetSecurityUserListQuery, SecurityUserResponseVM>.Handle(GetSecurityUserListQuery request, CancellationToken cancellationToken)
		{
			try
			{
				SecurityUserResponseVM response = new();
				response.SecurityUserResponse = await (from u in _securityUserRepository.Get(a => !a.IsDeleted)
													   join UBE in _securityUserBusinessEntityRepository.Get() on u.UserId equals UBE.UserId
													   join BE in _businessEntityRepository.Get() on UBE.BusinessEntityId equals BE.BusinessEntityID
													   join BET in _businessEntityTypeRepository.Get() on BE.BusinessEntityTypeId equals BET.BusinessEntityTypeId

                                                       where !UBE.IsDeleted && !u.IsDeleted && (BET.BusinessEntityTypeName!=null && BET.BusinessEntityTypeName.ToLower() == BuisnessEntityName.Employee.ToString().ToLower())
                                                       select new SecurityUserResponse
                                                       {
                                                           UserID = u.UserId,
                                                           LoginEmail = u.LoginEmail,
                                                           FirstName = u.FirstName,
                                                           LastName = u.LastName,
                                                           MaximumATOMDevices = u.MaximumATOMDevices,
                                                           Status = u.IsActive,
                                                           EmployeeName = BE.BusinessEntityName

													   }).ToListAsync(cancellationToken);


                if (response.SecurityUserResponse?.Count > 0)
                {
                    if (!string.IsNullOrEmpty(request.SearchFilter))
                    {
                        Predicate<SecurityUserResponse> search = x => x.FirstName?.IndexOf(request.SearchFilter, StringComparison.InvariantCultureIgnoreCase) >= 0 || x.LastName?.IndexOf(request.SearchFilter, StringComparison.InvariantCultureIgnoreCase) >= 0
                        || x.LoginEmail?.IndexOf(request.SearchFilter, StringComparison.InvariantCultureIgnoreCase) >= 0
                        || x.EmployeeName?.IndexOf(request.SearchFilter, StringComparison.InvariantCultureIgnoreCase) >= 0;

                        response.SecurityUserResponse = response.SecurityUserResponse?.FindAll(search);
                    }
                    if (request.gridFilterViewModel?.Count > 0 && response.SecurityUserResponse!=null)
                    {
                        response.SecurityUserResponse = filter(response.SecurityUserResponse, request.gridFilterViewModel);
                    }

					response.Count = response.SecurityUserResponse == null ? 0 : response.SecurityUserResponse.Count;

					int PageNumber = Convert.ToInt32(request.PageNumber != null && request.PageNumber != 0 ? request.PageNumber : 1);
					int PageSize = Convert.ToInt32(request.PageSize != null && request.PageSize != 0 ? request.PageSize : 10);

                    response.SecurityUserResponse = response.SecurityUserResponse?.Skip((PageNumber - 1) * PageSize).Take(PageSize).ToList();

					Func<SecurityUserResponse, object> orderByExpression;
					switch (request.OrderBy)
					{
						case "loginEmail":
							orderByExpression = s => s.LoginEmail ?? string.Empty;
							break;
						case "firstName":
							orderByExpression = s => s.FirstName ?? string.Empty;
							break;
						case "lastName":
							orderByExpression = s => s.LastName ?? string.Empty;
							break;
						case "status":
							orderByExpression = s => s.Status;
							break;
						case "maximumATOMDevices":
							orderByExpression = s => s.MaximumATOMDevices ?? 0;
							break;
						case "employeeName":
							orderByExpression = s => s.EmployeeName ?? string.Empty;
							break;
						default:
							orderByExpression = s => s.FirstName ?? string.Empty;
							break;
					}

                    if (request.SortOrderBy == "desc")
                        response.SecurityUserResponse = response.SecurityUserResponse?.OrderByDescending(orderByExpression).ToList();
                    else
                        response.SecurityUserResponse = response.SecurityUserResponse?.OrderBy(orderByExpression).ToList();
                }
                _logger.LogInformation($"SearchUserQueryHandler End");
                if (response.SecurityUserResponse != null)
                    return response;
                else
                    return new SecurityUserResponseVM();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in UserController.GetSecurityUserListQueryHandler");
                throw;
            }
        }
        private static List<SecurityUserResponse> filter(List<SecurityUserResponse> securityUser, List<ColumnFilter> filters)
        {
            foreach (var filter in filters)
            {
                if (!string.IsNullOrEmpty(filter.Value))
                {
                    switch (filter.Field)
                    {
                        case string x when x.ToLower().Contains("loginemail"):
                            securityUser = securityUser.Where(x => x.LoginEmail?.IndexOf(filter.Value, StringComparison.InvariantCultureIgnoreCase) >= 0).ToList();
                            break;
                        case string x when x.ToLower().Contains("firstname"):
                            securityUser = securityUser.Where(x => x.FirstName?.IndexOf(filter.Value, StringComparison.InvariantCultureIgnoreCase) >= 0).ToList();
                            break;
                        case string x when x.ToLower().Contains("lastname"):
                            securityUser = securityUser.Where(x => x.LastName?.IndexOf(filter.Value, StringComparison.InvariantCultureIgnoreCase) >= 0).ToList();
                            break;
                        case string x when x.ToLower().Contains("status"):
                            if (filter.Value.ToLower() == "active")
                                securityUser = securityUser.Where(x => x.Status).ToList();
                            else if (filter.Value.ToLower() == "inactive")
                                securityUser = securityUser.Where(x => !x.Status).ToList();
                            break;
                        case string x when x.ToLower().Contains("employeename"):
                            securityUser = securityUser.Where(x => x.EmployeeName?.IndexOf(filter.Value, StringComparison.InvariantCultureIgnoreCase) >= 0).ToList();
                            break;
                        case string x when x.ToLower().Contains("maximumatomdevices"):
                            var maximumatomdevices = int.Parse(filter.Value.Trim().ToLower());
                            if (maximumatomdevices >= 0)
                                securityUser = securityUser.Where(x => x.MaximumATOMDevices == maximumatomdevices).ToList();
                            break;

						default:
							break;
					}
				}
			}
			return securityUser;
		}
	}
}
