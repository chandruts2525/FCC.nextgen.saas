using WorkManagement.Domain.Model;
using WorkManagement.Domain.ViewModel.ScheduleTypes;
using WorkManagement.Service.Queries.SchdeuleType;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Softura.EntityFrameworkCore.Abstractions;
using System.Globalization;
using FCC.Core.ViewModel.GridFilter;

namespace WorkManagement.Service.Handlers
{
	public class GetScheduleTypeListQueryHandler : IRequestHandler<GetScheduleTypeListQuery, ScheduleTypeResponseVM>
    {
        private readonly IRepository<ScheduleType> _iRepository;
        private readonly IRepository<ScheduleTypeComponents> _scheduleTypeComponentsRepository;
        private readonly ILogger<GetScheduleTypeListQueryHandler> _logger;
        public GetScheduleTypeListQueryHandler(IRepository<ScheduleType> iRepository, IRepository<ScheduleTypeComponents> scheduleTypeComponentsRepository,ILogger<GetScheduleTypeListQueryHandler> logger)
        {
            _iRepository = iRepository;
            _logger = logger;
            _scheduleTypeComponentsRepository = scheduleTypeComponentsRepository;
        }
        async Task<ScheduleTypeResponseVM> IRequestHandler<GetScheduleTypeListQuery, ScheduleTypeResponseVM>.Handle(GetScheduleTypeListQuery request, CancellationToken cancellationToken)
        {
            try
            {
                ScheduleTypeResponseVM response = new();
                var result = await _iRepository.Get(a => !a.IsDeleted).ToListAsync(cancellationToken); 
                var getunitorcomponent = await _scheduleTypeComponentsRepository.Get().ToListAsync(cancellationToken);
                if (result?.Count > 0)
                {
                    response.ScheduleTypes = result.Select(x => new GetScheduleTypes
                    {
                        ScheduleTypeCode = x.ScheduleTypeCode,
                        ScheduleTypeName = x.ScheduleTypeName,
                        UnitOrComponent = getunitorcomponent?.Find(a => a.UnitComponentsID == x.UnitComponentsID)?.UnitOrComponent,
                        Schedulable = x.Schedulable,
                        GBP = x.GroundBearingPresure,
                        ScheduleTypeId = x.ScheduleTypeId,
                        CreatedBy = x.CreatedBy,
                        CreatedDateUTC = x.CreatedDateUTC,
                        ModifiedBy = x.ModifiedBy,
                        ModifiedDateUTC = x.ModifiedDateUTC,
                        IsActive = x.IsActive,
                    }).ToList();
                }
                if(response.ScheduleTypes?.Count > 0)
                {
                    if (!string.IsNullOrEmpty(request.SearchFilter))
                    {
                        Predicate<GetScheduleTypes> search = x => ((x.ScheduleTypeName?.IndexOf(request.SearchFilter, StringComparison.InvariantCultureIgnoreCase) >= 0) || (x.ScheduleTypeCode?.IndexOf(request.SearchFilter, StringComparison.InvariantCultureIgnoreCase) >= 0) ||
                        (x.UnitOrComponent?.IndexOf(request.SearchFilter, StringComparison.InvariantCultureIgnoreCase) >= 0));

                        response.ScheduleTypes = response.ScheduleTypes?.FindAll(search);
                    }
                    if (request.gridFilterViewModel?.Count > 0 && response.ScheduleTypes?.Count > 0)
                    {
                        response.ScheduleTypes = filter(response.ScheduleTypes, request.gridFilterViewModel);
                    }

                    response.Count = response.ScheduleTypes == null ? 0 : response.ScheduleTypes.Count;

                    int PageNumber = Convert.ToInt32((request.PageNumber != null && request.PageNumber != 0) ? request.PageNumber : 1);
                    int PageSize = Convert.ToInt32((request.PageSize != null && request.PageSize != 0) ? request.PageSize : 10);

                    response.ScheduleTypes = response.ScheduleTypes?.Skip((PageNumber - 1) * PageSize).Take(PageSize).ToList();


                    Func<GetScheduleTypes, object> orderByExpression;
                    switch (request.OrderBy)
                    {
                        case "scheduleTypeCode":
                            orderByExpression = s => s.ScheduleTypeCode ?? string.Empty;
                            break;
                        case "scheduleTypeName":
                            orderByExpression = s => s.ScheduleTypeName ?? string.Empty;
                            break;
                        case "unitOrComponent":
                            orderByExpression = s => s.UnitOrComponent ?? string.Empty;
                            break;
                        case "isActive":
                            orderByExpression = s => s.IsActive;
                            break;
                        case "createdBy":
                            orderByExpression = s => s.CreatedBy ?? string.Empty;
                            break;
                        case "createdDate":
                            orderByExpression = s => s.CreatedDateUTC;
                            break;
                        case "modifiedBy":
                            orderByExpression = s => s.ModifiedBy ?? string.Empty;
                            break;
                        case "modifiedDate":
                            orderByExpression = s => s.ModifiedDateUTC ?? DateTime.MinValue;
                            break;
                        default:
                            orderByExpression = s => s.ScheduleTypeCode ?? string.Empty;
                            break;
                    }

                    if (request.SortOrderBy == "desc")
                        response.ScheduleTypes = response.ScheduleTypes?.OrderByDescending(orderByExpression).ToList();
                    else
                        response.ScheduleTypes = response.ScheduleTypes?.OrderBy(orderByExpression).ToList();

                } 
                _logger.LogInformation($"SearchRoleQueryHandler End");

                

                if (response.ScheduleTypes != null)
                    return response;
                else
                    return new ScheduleTypeResponseVM();



            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in JobController.GetJobTypeListQueryHandler");
                throw;
            }

        }
        private static List<GetScheduleTypes> filter(List<GetScheduleTypes> scheduleType, List<ColumnFilter> filters)
        {
            foreach (var filter in filters)
            {
                if (!string.IsNullOrEmpty(filter.Value))
                { 
                    switch (filter.Field)
                    {
                        case string x when x.ToLower().Contains("scheduletypename"):
                            scheduleType = scheduleType.Where(x => x.ScheduleTypeName?.IndexOf(filter.Value, StringComparison.InvariantCultureIgnoreCase) >= 0).ToList();
                            break;
                        case string x when x.ToLower().Contains("scheduletypecode"):
                            scheduleType = scheduleType.Where(x => x.ScheduleTypeCode?.IndexOf(filter.Value, StringComparison.InvariantCultureIgnoreCase) >= 0).ToList();
                            break;
                        case string x when x.ToLower().Contains("isactive"):
                            if (filter.Value.ToLower() == "active")
                                scheduleType = scheduleType.Where(x => x.IsActive).ToList();
                            else if (filter.Value.ToLower() == "inactive")
                                scheduleType = scheduleType.Where(x => !x.IsActive).ToList();
                            break;
                        case string x when x.ToLower().Contains("modifieddate"):
                            if (filter.Operator == "gt")
                                scheduleType = scheduleType.Where(x => x.ModifiedDateUTC?.Date > Convert.ToDateTime(filter.Value, new CultureInfo("en-US")).Date).ToList();
                            else if (filter.Operator == "lt")
                                scheduleType = scheduleType.Where(x => x.ModifiedDateUTC?.Date < Convert.ToDateTime(filter.Value, new CultureInfo("en-US")).Date).ToList();
                            else
                                scheduleType = scheduleType.Where(x => x.ModifiedDateUTC?.Date == Convert.ToDateTime(filter.Value, new CultureInfo("en-US")).Date).ToList();
                            break;
                        case string x when x.ToLower().Contains("createddate"):
                            if (filter.Operator == "gt")
                                scheduleType = scheduleType.Where(x => x.CreatedDateUTC.Date > Convert.ToDateTime(filter.Value, new CultureInfo("en-US")).Date).ToList();
                            else if (filter.Operator == "lt")
                                scheduleType = scheduleType.Where(x => x.CreatedDateUTC.Date < Convert.ToDateTime(filter.Value, new CultureInfo("en-US")).Date).ToList();
                            else
                                scheduleType = scheduleType.Where(x => x.CreatedDateUTC.Date == Convert.ToDateTime(filter.Value, new CultureInfo("en-US")).Date).ToList();
                            break;
                        case string x when x.ToLower().Contains("createdby"):
                            scheduleType = scheduleType.Where(x => x.CreatedBy?.IndexOf(filter.Value, StringComparison.InvariantCultureIgnoreCase) >= 0).ToList();
                            break;
                        case string x when x.ToLower().Contains("modifiedby"):
                            scheduleType = scheduleType.Where(x => x.ModifiedBy?.IndexOf(filter.Value, StringComparison.InvariantCultureIgnoreCase) >= 0).ToList();
                            break;

                        default:
                            break;
                    }
                }
            }

            return scheduleType;
        }
    }
}
