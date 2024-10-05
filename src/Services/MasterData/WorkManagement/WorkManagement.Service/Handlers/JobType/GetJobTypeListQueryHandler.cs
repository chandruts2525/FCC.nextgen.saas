using WorkManagement.Domain.Model;
using WorkManagement.Service.Queries.JobType;
using WorkManagement.Service.ViewModel.JobType;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Globalization;
using Softura.EntityFrameworkCore.Abstractions;

namespace WorkManagement.Service.Handlers.JobType
{
    public class GetJobTypeListQueryHandler : IRequestHandler<GetJobTypeListQuery, GetAllJobTypeVM>
    {
        private readonly IRepository<JobTypes> _JobTypeRepository;
        private readonly ILogger<GetJobTypeListQueryHandler> _logger;
        public GetJobTypeListQueryHandler(IRepository<JobTypes> jobTypeRepository, ILogger<GetJobTypeListQueryHandler> logger)
        {
            _JobTypeRepository = jobTypeRepository;
            _logger = logger;
        }
        async Task<GetAllJobTypeVM> IRequestHandler<GetJobTypeListQuery, GetAllJobTypeVM>.Handle(GetJobTypeListQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Start GetJobTypeListQueryHandler");
            try
            {
                GetAllJobTypeVM getAllJobType = new GetAllJobTypeVM();
                var getall = await _JobTypeRepository.Get().Where(x => !x.IsDeleted).ToListAsync(cancellationToken);
                if (request.gridFilterViewModel != null)
                {
                    foreach (var item in request.gridFilterViewModel)
                    {
                        if (!string.IsNullOrWhiteSpace(item?.Value))
                        {
                            switch (item?.Field?.ToLower())
                            {
                                case "jobtypecode":
                                    getall = getall.Where(x => x.JobTypeCode != null && x.JobTypeCode.Contains(item.Value, StringComparison.InvariantCultureIgnoreCase)).ToList();
                                    break;
                                case "jobtypename":
                                    getall = getall.Where(x => x.JobTypeName != null && x.JobTypeName.Contains(item.Value, StringComparison.InvariantCultureIgnoreCase)).ToList();
                                    break;
                                case "isactive":
                                    if (item.Value.ToLower() == "active")
                                        getall = getall.Where(x => x.IsActive).ToList();
                                    else if (item.Value.ToLower() == "inactive")
                                        getall = getall.Where(x => !x.IsActive).ToList();
                                    break;
                                case "modifieddateutc":
                                    if (item.Operator == "gt")
                                        getall = getall.Where(x => x.ModifiedDateUTC?.Date > Convert.ToDateTime(item.Value, new CultureInfo("en-US")).Date).ToList();
                                    else if (item.Operator == "lt")
                                        getall = getall.Where(x => x.ModifiedDateUTC?.Date < Convert.ToDateTime(item.Value, new CultureInfo("en-US")).Date).ToList();
                                    else
                                        getall = getall.Where(x => x.ModifiedDateUTC?.Date == Convert.ToDateTime(item.Value, new CultureInfo("en-US")).Date).ToList();
                                    break;
                                case "createddateutc":
                                    if (item.Operator == "gt")
                                        getall = getall.Where(x => x.CreatedDateUTC.Date > Convert.ToDateTime(item.Value, new CultureInfo("en-US")).Date).ToList();
                                    else if (item.Operator == "lt")
                                        getall = getall.Where(x => x.CreatedDateUTC.Date < Convert.ToDateTime(item.Value, new CultureInfo("en-US")).Date).ToList();
                                    else
                                        getall = getall.Where(x => x.CreatedDateUTC.Date == Convert.ToDateTime(item.Value, new CultureInfo("en-US")).Date).ToList();
                                    break;
                                case "createdby":
                                    getall = getall.Where(x => x.CreatedBy.Contains(item.Value, StringComparison.InvariantCultureIgnoreCase)).ToList();
                                    break;
                                case "modifiedby":
                                    getall = getall.Where(x => x.ModifiedBy?.IndexOf(item.Value, StringComparison.InvariantCultureIgnoreCase) >= 0).ToList();
                                    break;
                            }
                        }
                    }
                }

                if (!string.IsNullOrEmpty(request.SearchFilter))
                {
                    Predicate<JobTypes> search = x => (x.JobTypeName != null && x.JobTypeName.Contains(request.SearchFilter, StringComparison.InvariantCultureIgnoreCase)) || (x.JobTypeCode != null && x.JobTypeCode.Contains(request.SearchFilter, StringComparison.InvariantCultureIgnoreCase)) ||
                    (x.IsActive ? "Active" : "Inactive").Contains(request.SearchFilter, StringComparison.InvariantCultureIgnoreCase);

                    getall = getall.FindAll(search);
                }

                getAllJobType.Count = getall.Count;

                Func<JobTypes, object> orderByExpression;
                switch (request.OrderBy?.ToLower())
                {
                    case "jobtypecode":
                        orderByExpression = s => s.JobTypeCode ?? string.Empty;
                        break;
                    case "jobtypename":
                        orderByExpression = s => s.JobTypeName ?? string.Empty;
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
                        orderByExpression = s => s.JobTypeCode ?? string.Empty;
                        break;
                }
                if (request.SortOrderBy?.ToLower() == "desc")
                    getall = getall.OrderByDescending(orderByExpression).ToList();
                else
                    getall = getall.OrderBy(orderByExpression).ToList();

                int PageNumber = Convert.ToInt32((request.PageNumber != null && request.PageNumber != 0) ? request.PageNumber : 1);
                int PageSize = Convert.ToInt32((request.PageSize != null && request.PageSize != 0) ? request.PageSize : 10);

                getall = getall.Skip((PageNumber - 1) * PageSize).Take(PageSize).ToList();

                getAllJobType.JobTypes = getall.Select(x => new GetAllJobType
                {
                    JobTypeId = x.JobTypeId,
                    JobTypeCode = x.JobTypeCode,
                    JobTypeName = x.JobTypeName,
                    isActive = x.IsActive,
                    CreatedBy = x.CreatedBy,
                    CreatedDateUTC = x.CreatedDateUTC,
                    ModifiedBy = x.ModifiedBy,
                    ModifiedDateUTC = x.ModifiedDateUTC                    
                }).ToList();
                
                _logger.LogInformation($"END GetJobTypeListQueryHandler");
                return getAllJobType;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in JobController.GetJobTypeListQueryHandler");
                throw;
            }

        }
    }
}
