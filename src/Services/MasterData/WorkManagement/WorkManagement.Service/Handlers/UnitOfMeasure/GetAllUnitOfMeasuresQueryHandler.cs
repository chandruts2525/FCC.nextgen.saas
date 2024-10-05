using FCC.Core.Constants;
using WorkManagement.Domain.ViewModel.UnitOfMeasure;
using WorkManagement.Service.Queries.UnitOfMeasure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Softura.EntityFrameworkCore.Abstractions;
using System.Globalization;

namespace WorkManagement.Service.Handlers.UnitOfMeasure
{
	public class GetAllUnitOfMeasuresQueryHandler : IRequestHandler<GetAllUnitOfMeasuresQuery, GetAllUnitOfMeasuresListFilterResponseVM>
	{
		private readonly IRepository<GetAllUnitOfMeasuresListResponseVM> _iRepository;
		private readonly ILogger<GetAllUnitOfMeasuresQueryHandler> _logger;
		public GetAllUnitOfMeasuresQueryHandler(IRepository<GetAllUnitOfMeasuresListResponseVM> iRepository, ILogger<GetAllUnitOfMeasuresQueryHandler> logger)
		{
			_iRepository = iRepository;
			_logger = logger;
		}
		async Task<GetAllUnitOfMeasuresListFilterResponseVM> IRequestHandler<GetAllUnitOfMeasuresQuery, GetAllUnitOfMeasuresListFilterResponseVM>.Handle(GetAllUnitOfMeasuresQuery request, CancellationToken cancellationToken)
		{
			try
			{
				_logger.LogInformation($"Start GetAllUnitOfMeasuresQueryHandler");

				GetAllUnitOfMeasuresListFilterResponseVM getAllUnitOfMeasuresListResponseVM = new GetAllUnitOfMeasuresListFilterResponseVM();

				var getall = await _iRepository.GetFromCommand("SELECT * FROM " + DBConstants.VW_GETALL_UNITOFMEASURE).ToListAsync(cancellationToken);

				if (request.GridFilter.ColumnFilters != null)
				{
					foreach (var cf in request.GridFilter.ColumnFilters)
					{
						if (!string.IsNullOrWhiteSpace(cf.Value))
						{
							switch (cf.Field)
							{
								case nameof(UOMFilter.unitMeasureCode):
									getall = getall.Where(x => x.UnitMeasureCode?.IndexOf(cf.Value, StringComparison.InvariantCultureIgnoreCase) >= 0).ToList();
									break;
								case nameof(UOMFilter.unitMeasureDisplayValue):
									getall = getall.Where(x => x.UnitMeasureDisplayValue?.IndexOf(cf.Value, StringComparison.InvariantCultureIgnoreCase) >= 0).ToList();
									break;
								case nameof(UOMFilter.unitMeasureTypeDescription):
									getall = getall.Where(x => x.UnitMeasureTypeDescription?.IndexOf(cf.Value, StringComparison.InvariantCultureIgnoreCase) >= 0).ToList();
									break;
								case nameof(UOMFilter.conversionFactor):
									getall = getall.Where(x => x.ConversionFactor?.ToString().IndexOf(cf.Value, StringComparison.InvariantCultureIgnoreCase) >= 0).ToList();
									break;
								case nameof(UOMFilter.isActive):
									if (cf.Value.ToLower() == "active")
										getall = getall.Where(x => Convert.ToString(x.IsActive!).Equals("Active")).ToList();
									else if (cf.Value.ToLower() == "inactive")
										getall = getall.Where(x => Convert.ToString(x.IsActive!).Equals("Inactive")).ToList();
                                    break;
								case nameof(UOMFilter.createdBy):
									getall = getall.Where(x => x.CreatedBy?.IndexOf(cf.Value, StringComparison.InvariantCultureIgnoreCase) >= 0).ToList();
									break;
								case nameof(UOMFilter.createdDateUTC):
									if (cf.Operator == "gt")
										getall = getall.Where(x => Convert.ToDateTime(x.CreatedDateUTC, new CultureInfo("en-US")).Date > Convert.ToDateTime(cf.Value, new CultureInfo("en-US")).Date).ToList();
									else if (cf.Operator == "lt")
										getall = getall.Where(x => Convert.ToDateTime(x.CreatedDateUTC, new CultureInfo("en-US")).Date < Convert.ToDateTime(cf.Value, new CultureInfo("en-US")).Date).ToList();
									else
										getall = getall.Where(x => Convert.ToDateTime(x.CreatedDateUTC, new CultureInfo("en-US")).Date == Convert.ToDateTime(cf.Value, new CultureInfo("en-US")).Date).ToList();
									break;
								case nameof(UOMFilter.modifiedBy):
									getall = getall.Where(x => x.ModifiedBy?.IndexOf(cf.Value, StringComparison.InvariantCultureIgnoreCase) >= 0).ToList();
									break;
								case nameof(UOMFilter.modifiedDateUTC):
									if (cf.Operator == "gt")
										getall = getall.Where(x => Convert.ToDateTime(x.ModifiedDateUTC, new CultureInfo("en-US")).Date > Convert.ToDateTime(cf.Value, new CultureInfo("en-US")).Date).ToList();
									else if (cf.Operator == "lt")
										getall = getall.Where(x => Convert.ToDateTime(x.ModifiedDateUTC, new CultureInfo("en-US")).Date < Convert.ToDateTime(cf.Value, new CultureInfo("en-US")).Date).ToList();
									else
										getall = getall.Where(x => Convert.ToDateTime(x.ModifiedDateUTC, new CultureInfo("en-US")).Date == Convert.ToDateTime(cf.Value, new CultureInfo("en-US")).Date).ToList();
									break;
							}
						}
					}
				}
				if (request?.GridFilter != null && !string.IsNullOrEmpty(request.GridFilter.GlobalSearch))
				{
					Predicate<GetAllUnitOfMeasuresListResponseVM> search = x => ((x.UnitMeasureCode.IndexOf(request.GridFilter.GlobalSearch, StringComparison.InvariantCultureIgnoreCase) >= 0) || (x.UnitMeasureDisplayValue.IndexOf(request.GridFilter.GlobalSearch, StringComparison.InvariantCultureIgnoreCase) >= 0) ||
					(x.UnitMeasureTypeDescription?.IndexOf(request.GridFilter.GlobalSearch, StringComparison.InvariantCultureIgnoreCase) >= 0));

					getall = getall.FindAll(search);
				}

				getAllUnitOfMeasuresListResponseVM.Count = getall.Count;

				Func<GetAllUnitOfMeasuresListResponseVM, object> orderByExpression;
				switch (request?.GridFilter.SortBy)
				{
					case nameof(UOMFilter.unitMeasureCode):
						orderByExpression = s => s.UnitMeasureCode ?? string.Empty;
						break;
					case nameof(UOMFilter.unitMeasureDisplayValue):
						orderByExpression = s => s.UnitMeasureDisplayValue ?? string.Empty;
						break;
					case nameof(UOMFilter.unitMeasureTypeDescription):
						orderByExpression = s => s.UnitMeasureTypeDescription ?? string.Empty;
						break;
					case nameof(UOMFilter.conversionFactor):
						orderByExpression = s => s.ConversionFactor ?? 0;
						break;
					case nameof(UOMFilter.isActive):
						orderByExpression = s => s.IsActive ?? string.Empty;
						break;
					case nameof(UOMFilter.createdBy):
						orderByExpression = s => s.CreatedBy ?? string.Empty;
						break;
					case nameof(UOMFilter.createdDateUTC):
						orderByExpression = s => s.CreatedDateUTC ?? string.Empty;
						break;
					case nameof(UOMFilter.modifiedBy):
						orderByExpression = s => s.ModifiedBy ?? string.Empty;
						break;
					case nameof(UOMFilter.modifiedDateUTC):
						orderByExpression = s => s.ModifiedDateUTC ?? string.Empty;
						break;
					default:
						orderByExpression = s => s.UnitMeasureCode ?? string.Empty;
						break;
				}
				if (request?.GridFilter.SortOrder == "desc")
					getall = getall.OrderByDescending(orderByExpression).ToList();
				else
					getall = getall.OrderBy(orderByExpression).ToList();

				int PageNumber = Convert.ToInt32((request?.GridFilter.PageNumber != null && request?.GridFilter.PageNumber != 0) ? request?.GridFilter.PageNumber : 1);
				int PageSize = Convert.ToInt32((request?.GridFilter.PageSize != null && request?.GridFilter.PageSize != 0) ? request?.GridFilter.PageSize : 10);

				getall = getall.Skip((PageNumber - 1) * PageSize).Take(PageSize).ToList();

				getAllUnitOfMeasuresListResponseVM.getAllUnitOfMeasuresListFilter = getall.Select(x => new GetAllUnitOfMeasuresListResponseVM
				{
					UnitMeasureId = x.UnitMeasureId,
					UnitMeasureTypeId = x.UnitMeasureTypeId,
					UnitMeasureCode = x.UnitMeasureCode,
					UnitMeasureDisplayValue = x.UnitMeasureDisplayValue,
					UnitMeasureTypeDescription = x.UnitMeasureTypeDescription,
					ConversionFactor = x.ConversionFactor,
					IsActive = x.IsActive,
					CreatedBy = x.CreatedBy,
					CreatedDateUTC = x.CreatedDateUTC,
					ModifiedBy = x.ModifiedBy,
					ModifiedDateUTC = x.ModifiedDateUTC
				}).ToList();

				_logger.LogInformation($"END GetJobTypeListQueryHandler");
				return getAllUnitOfMeasuresListResponseVM;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error in UserController.GetSecurityUserListQueryHandler");
				throw;
			}
		}
	}
}


