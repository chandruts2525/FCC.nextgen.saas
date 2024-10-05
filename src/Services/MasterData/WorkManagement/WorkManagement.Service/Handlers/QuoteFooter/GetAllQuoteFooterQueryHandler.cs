using FCC.Core.Constants;
using WorkManagement.Domain.ViewModel.QuoteFooter;
using WorkManagement.Service.Queries.QuoteFooter;
using WorkManagement.Service.ViewModel.QuoteFooters;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Globalization;
using Softura.EntityFrameworkCore.Abstractions;

namespace WorkManagement.Service.Handlers.QuoteFooter
{
	public class GetAllQuoteFooterQueryHandler : IRequestHandler<GetAllQuoteFooterQuery, GetAllQuoteFooterVM>
    {
        private readonly IRepository<GetAllQuoteFooter> _QuoteFooterRepository;
        private readonly ILogger<GetAllQuoteFooterQueryHandler> _logger;
        public GetAllQuoteFooterQueryHandler(IRepository<GetAllQuoteFooter> QuoteFooterRepository, ILogger<GetAllQuoteFooterQueryHandler> logger)
        {
            _QuoteFooterRepository = QuoteFooterRepository;
            _logger = logger;
        }

        async Task<GetAllQuoteFooterVM> IRequestHandler<GetAllQuoteFooterQuery, GetAllQuoteFooterVM>.Handle(GetAllQuoteFooterQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"START GetAllQuoteFooterQueryHandler");

                GetAllQuoteFooterVM getAllQuoteFooter = new ();

                var getall = await _QuoteFooterRepository.GetFromCommand("Select QuoteFooterID,QuoteFooterName,CompanyCount," +
                    "CompanyName,ModuleCount,ModuleName,Status,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate From " + DBConstants.VW_GETALL_QUOTEFOOTER).ToListAsync(cancellationToken);                

                if (request.gridFilterViewModel != null)
                {
                    foreach (var item in request.gridFilterViewModel)
                    {
                        if (!string.IsNullOrEmpty(item.Value))
                        {
                            switch (item.Field.ToLower())
                            {
                                case nameof(QuoteFooterFilter.quotefootername):
                                    getall = getall.Where(x => x.QuoteFooterName?.IndexOf(item.Value, StringComparison.InvariantCultureIgnoreCase) >= 0).ToList();
                                    break;
                                case nameof(QuoteFooterFilter.companycount):
                                    getall = getall.Where(x => x.CompanyName?.IndexOf(item.Value, StringComparison.InvariantCultureIgnoreCase) >= 0).ToList();
                                    break;
                                case nameof(QuoteFooterFilter.modulecount):
                                    getall = getall.Where(x => x.ModuleName?.IndexOf(item.Value, StringComparison.InvariantCultureIgnoreCase) >= 0).ToList();
                                    break;
                                case nameof(QuoteFooterFilter.status):
                                    if (item.Value.ToLower() == "active")
                                        getall = getall.Where(x => x.Status).ToList();
                                    else if (item.Value.ToLower() == "inactive")
                                        getall = getall.Where(x => !x.Status).ToList();
                                    break;
                                case nameof(QuoteFooterFilter.modifieddate):
                                    if (item.Operator == "gt")
                                        getall = getall.Where(x => x.ModifiedDate?.Date > Convert.ToDateTime(item.Value, new CultureInfo("en-US")).Date).ToList();
                                    else if (item.Operator == "lt")
                                        getall = getall.Where(x => x.ModifiedDate?.Date < Convert.ToDateTime(item.Value, new CultureInfo("en-US")).Date).ToList();
                                    else
                                        getall = getall.Where(x => x.ModifiedDate?.Date == Convert.ToDateTime(item.Value, new CultureInfo("en-US")).Date).ToList();
                                    break;
                                case nameof(QuoteFooterFilter.createddate):
                                    if (item.Operator == "gt")
                                        getall = getall.Where(x => x.CreatedDate.Date > Convert.ToDateTime(item.Value, new CultureInfo("en-US")).Date).ToList();
                                    else if (item.Operator == "lt")
                                        getall = getall.Where(x => x.CreatedDate.Date < Convert.ToDateTime(item.Value, new CultureInfo("en-US")).Date).ToList();
                                    else
                                        getall = getall.Where(x => x.CreatedDate.Date == Convert.ToDateTime(item.Value, new CultureInfo("en-US")).Date).ToList();
                                    break;
                                case nameof(QuoteFooterFilter.createdby):
                                    getall = getall.Where(x => x.CreatedBy?.IndexOf(item.Value, StringComparison.InvariantCultureIgnoreCase) >= 0).ToList();
                                    break;
                                case nameof(QuoteFooterFilter.modifiedby):
                                    getall = getall.Where(x => x.ModifiedBy?.IndexOf(item.Value, StringComparison.InvariantCultureIgnoreCase) >= 0).ToList();
                                    break;
                            }
                        }
                    }
                }

                if (!string.IsNullOrEmpty(request.SearchFilter))
                {
                    Predicate<GetAllQuoteFooter> search = x => ((x.QuoteFooterName?.IndexOf(request.SearchFilter, StringComparison.InvariantCultureIgnoreCase) >= 0) ||
                    (x.CompanyName?.IndexOf(request.SearchFilter, StringComparison.InvariantCultureIgnoreCase) >= 0) || (x.ModuleName?.IndexOf(request.SearchFilter, StringComparison.InvariantCultureIgnoreCase) >= 0));

                    getall = getall.FindAll(search);
                }

                getAllQuoteFooter.Count = getall.Count;

                Func<GetAllQuoteFooter, object> orderByExpression;
                switch (request.OrderBy?.ToLower())
                {
                    case nameof(QuoteFooterFilter.quotefootername):
                        orderByExpression = s => s.QuoteFooterName ?? string.Empty;
                        break;
                    case nameof(QuoteFooterFilter.status):
                        orderByExpression = s => s.Status;
                        break;
                    case nameof(QuoteFooterFilter.modifieddate):
                        orderByExpression = s => s.ModifiedDate ?? DateTime.MinValue;
                        break;
                    case nameof(QuoteFooterFilter.modifiedby):
                        orderByExpression = s => s.ModifiedBy ?? string.Empty;
                        break;
                    case nameof(QuoteFooterFilter.createddate):
                        orderByExpression = s => s.CreatedDate;
                        break;
                    case nameof(QuoteFooterFilter.createdby):
                        orderByExpression = s => s.CreatedBy ?? string.Empty;
                        break;
                    case nameof(QuoteFooterFilter.companycount):
                        orderByExpression = s => s.CompanyCount;
                        break;
                    case nameof(QuoteFooterFilter.modulecount):
                        orderByExpression = s => s.ModuleCount;
                        break;
                    default:
                        orderByExpression = s => s.QuoteFooterName ?? string.Empty;
                        break;
                }

                if (request.SortOrderBy?.ToLower() == "desc")
                    getall = getall.OrderByDescending(orderByExpression).ToList();
                else
                    getall = getall.OrderBy(orderByExpression).ToList();

                int PageNumber = Convert.ToInt32((request.PageNumber != null && request.PageNumber != 0) ? request.PageNumber : 1);
                int PageSize = Convert.ToInt32((request.PageSize != null && request.PageSize != 0) ? request.PageSize : 10);

                getall = getall.Skip((PageNumber - 1) * PageSize).Take(PageSize).ToList();

                getAllQuoteFooter.QuoteFooters = getall;

                _logger.LogInformation($"END GetAllQuoteFooterQueryHandler");

                return getAllQuoteFooter;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in QuoteFooterController.GetAllQuoteFooterQueryHandler");
                throw;
            }

        }
    }
}
