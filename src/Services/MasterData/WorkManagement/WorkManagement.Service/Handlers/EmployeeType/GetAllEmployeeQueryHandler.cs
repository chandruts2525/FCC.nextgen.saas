using WorkManagement.Domain.Model;
using WorkManagement.Service.Queries.EmployeeType;
using WorkManagement.Service.ViewModel.EmployeeType;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Globalization;
using Softura.EntityFrameworkCore.Abstractions;

namespace WorkManagement.Service.Handlers.EmployeeType
{
    public class GetAllEmployeeQueryHandler : IRequestHandler<GetAllEmployeeQuery, GetAllEmployeeTypeVM>
    {
        private readonly IRepository<EmployeeTypes> _EmployeeRepository;
        private readonly ILogger<GetAllEmployeeQueryHandler> _logger;
        public GetAllEmployeeQueryHandler(ILogger<GetAllEmployeeQueryHandler> logger,IRepository<EmployeeTypes> EmployeeRepository)
        {
            _logger = logger;
            _EmployeeRepository = EmployeeRepository;
        }
        async Task<GetAllEmployeeTypeVM> IRequestHandler<GetAllEmployeeQuery, GetAllEmployeeTypeVM>.Handle(GetAllEmployeeQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Start GetAllEmployeeQueryHandler");
            try
            {
                GetAllEmployeeTypeVM GetAllEmployee = new GetAllEmployeeTypeVM();

                var getall = await _EmployeeRepository.Get().Where(x => !x.IsDeleted).ToListAsync(cancellationToken);

                if(request.gridFilterViewModel != null)
                {
                    foreach (var item in request.gridFilterViewModel)
                    {
                        if (!string.IsNullOrWhiteSpace(item.Value))
                        {
                            switch (item.Field?.ToLower())
                            {
                                case "employeetypename":
                                    getall = getall.Where(x => x.EmployeeTypeName?.IndexOf(item.Value, StringComparison.InvariantCultureIgnoreCase) >= 0).ToList();
                                    break;
                                case "isactive":
                                    if (item.Value.ToLower() == "active")
                                        getall = getall.Where(x => x.IsActive).ToList();
                                    else if (item.Value.ToLower() == "inactive")
                                        getall = getall.Where(x => !x.IsActive).ToList();
                                    break;
                                case "modifieddateutc":
                                    if (!string.IsNullOrEmpty(item.Operator) && item.Operator == "gt")
                                        getall = getall.Where(x => x.ModifiedDateUTC?.Date > Convert.ToDateTime(item.Value, new CultureInfo("en-US")).Date).ToList();
                                    else if (!string.IsNullOrEmpty(item.Operator) && item.Operator == "lt")
                                        getall = getall.Where(x => x.ModifiedDateUTC?.Date < Convert.ToDateTime(item.Value, new CultureInfo("en-US")).Date).ToList();
                                    else
                                        getall = getall.Where(x => x.ModifiedDateUTC?.Date == Convert.ToDateTime(item.Value, new CultureInfo("en-US")).Date).ToList();
                                    break;
                                case "createddateutc":
                                    if (!string.IsNullOrEmpty(item.Operator) && item.Operator == "gt")
                                        getall = getall.Where(x => x.CreatedDateUTC.Date > Convert.ToDateTime(item.Value, new CultureInfo("en-US")).Date).ToList();
                                    else if (!string.IsNullOrEmpty(item.Operator) &&item.Operator == "lt")
                                        getall = getall.Where(x => x.CreatedDateUTC.Date < Convert.ToDateTime(item.Value, new CultureInfo("en-US")).Date).ToList();
                                    else
                                        getall = getall.Where(x => x.CreatedDateUTC.Date == Convert.ToDateTime(item.Value, new CultureInfo("en-US")).Date).ToList();
                                    break;
                                case "createdby":
                                    getall = getall.Where(x => x.CreatedBy.IndexOf(item.Value, StringComparison.InvariantCultureIgnoreCase) >= 0).ToList();
                                    break;
                                case "modifiedby":
                                    getall = getall.Where(x => x.ModifiedBy?.IndexOf(item.Value, StringComparison.InvariantCultureIgnoreCase) >= 0).ToList();
                                    break;
                            }
                        }
                    }
                }

                if(!string.IsNullOrEmpty(request.SearchFilter))
                {
                    Predicate<EmployeeTypes> search = x => ((x.EmployeeTypeName?.IndexOf(request.SearchFilter, StringComparison.InvariantCultureIgnoreCase) >= 0) ||
                    (x.IsActive ? "Active" : "Inactive").Contains(request.SearchFilter, StringComparison.InvariantCultureIgnoreCase));

                    getall = getall.FindAll(search);
                }

                GetAllEmployee.Count = getall.Count;

                Func<EmployeeTypes, object> orderByExpression;
                switch (request.OrderBy?.ToLower())
                {
                    case "employeetypename":
                        orderByExpression = s => s.EmployeeTypeName ?? string.Empty;
                        break;
                    case "isactive":
                        orderByExpression = s => s.IsActive;
                        break;
                    case "modifieddateutc":
                        orderByExpression = s => s.ModifiedDateUTC ?? DateTime.MinValue;
                        break;
                    case "modifiedby":
                        orderByExpression = s => s.ModifiedBy ?? string.Empty;
                        break;
                    case "createddateutc":
                        orderByExpression = s => s.CreatedDateUTC;
                        break;
                    case "createdby":
                        orderByExpression = s => s.CreatedBy;
                        break;
                    default:
                        orderByExpression = s => s.EmployeeTypeName ?? string.Empty;
                        break;
                }

                if (request.SortOrderBy != null && request.SortOrderBy == "desc")
                    getall = getall.OrderByDescending(orderByExpression).ToList();
                else
                    getall = getall.OrderBy(orderByExpression).ToList();

                int PageNumber = (request.PageNumber != null && request.PageNumber != 0) ? (int)request.PageNumber: 1;
                int PageSize = (request.PageSize != null && request.PageSize != 0) ? (int)request.PageSize: 10;

                getall = getall.Skip((PageNumber - 1) * PageSize).Take(PageSize).ToList();

                GetAllEmployee.getAllEmployeeTypes = getall.Select(x => new GetAllEmployeeType()
                {
                    EmployeeTypeId = x.EmployeeTypeId,
                    EmployeeTypeName = x.EmployeeTypeName,
                    IsActive = x.IsActive,
                    CreatedBy = x.CreatedBy,
                    CreatedDateUTC = x.CreatedDateUTC,
                    ModifiedBy = x.ModifiedBy,
                    ModifiedDateUTC = x.ModifiedDateUTC
                }).ToList();

                _logger.LogInformation($"END GetAllEmployeeQueryHandler");

                return GetAllEmployee;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in EmployeeController.GetAllEmployeeQueryHandler");
                throw;
            }
        }
    }
}
