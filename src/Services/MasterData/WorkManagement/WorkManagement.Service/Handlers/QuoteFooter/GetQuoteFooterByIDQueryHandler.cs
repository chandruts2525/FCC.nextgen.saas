using WorkManagement.Domain.Model;
using WorkManagement.Service.Queries.QuoteFooter;
using WorkManagement.Service.ViewModel.QuoteFooter;
using WorkManagement.Service.ViewModel.QuoteFooters;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Softura.EntityFrameworkCore.Abstractions;
using OrganizationStructure.Domain.Model;

namespace WorkManagement.Service.Handlers.QuoteFooter
{
    public class GetQuoteFooterByIDQueryHandler : IRequestHandler<GetQuoteFooterByIDQuery, QuoteFootersByIdvm>
    {
        private readonly IRepository<Terms> _TermRepository;
        private readonly IRepository<BusinessEntityTerm> _BusinessEntityTermRepository;
        private readonly IRepository<BusinessEntity> _BusinessEntityRepository;
        private readonly IRepository<BusinessEntityType> _BusinessEntityTypeRepository;
        private readonly IRepository<TermModuleEntity> _TermModuleRepository;
        private readonly IRepository<Modules> _ModuleRepository;
        private readonly ILogger<GetQuoteFooterByIDQueryHandler> _logger;


        public GetQuoteFooterByIDQueryHandler(IRepository<Terms> TermRepository, IRepository<BusinessEntityTerm> BusinessEntityTermRepository,
            IRepository<TermModuleEntity> TermModuleRepository, IRepository<BusinessEntityType> businessEntityTypeRepository,
            IRepository<BusinessEntity> businessEntityRepository, IRepository<Modules> ModuleRepository,
            ILogger<GetQuoteFooterByIDQueryHandler> logger)
        {
            _TermRepository = TermRepository;
            _BusinessEntityTermRepository = BusinessEntityTermRepository;
            _TermModuleRepository = TermModuleRepository;
            _BusinessEntityTypeRepository = businessEntityTypeRepository;
            _BusinessEntityRepository = businessEntityRepository;
            _ModuleRepository = ModuleRepository;
            _logger = logger;
        }

        public async Task<QuoteFootersByIdvm> Handle(GetQuoteFooterByIDQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Start GetQuoteFooterByIDQueryHandler");
            try
            {
                QuoteFootersByIdvm quoteFooterByID = new QuoteFootersByIdvm();
                if (request.Id > 0)
                {
                    var TermResult = await _TermRepository.Get().Where(x => x.TermID == request.Id).FirstOrDefaultAsync(cancellationToken);
                    if (TermResult != null)
                    {
                        quoteFooterByID.QuoteFooterId = TermResult.TermID;
                        quoteFooterByID.QuoteFooterName = TermResult.TermName;
                        quoteFooterByID.Status = TermResult.IsActive;
                        quoteFooterByID.Description = TermResult.TermText;

                        var CompanyBusinessEntityId = await _BusinessEntityTypeRepository.Get().Where(x=>x.BusinessEntityTypeName == "Company").FirstOrDefaultAsync(cancellationToken);

                        quoteFooterByID.Company = await (from r in _BusinessEntityTermRepository.Get().Where(x => x.TermID == TermResult.TermID && !x.IsDeleted)
                                                   join s in _BusinessEntityRepository.Get() on r.BusinessEntityID equals s.BusinessEntityID
                                                   where CompanyBusinessEntityId!=null && s.BusinessEntityTypeId == CompanyBusinessEntityId.BusinessEntityTypeId
                                                   select new BusinessEntityVM
                                                   {
                                                       BusinessEntityId = s.BusinessEntityID,
                                                       BusinessEntityName = s.BusinessEntityName
                                                   }).ToListAsync(cancellationToken);


                        quoteFooterByID.Modules = await (from r in _TermModuleRepository.Get().Where(x => x.TermID == TermResult.TermID && !x.IsDeleted )
                                                  join s in _ModuleRepository.Get() on r.ModuleID equals s.ModuleID
                                                  select new ModulesVM
                                                  {
                                                      ModuleID = s.ModuleID,
                                                      ModuleName = s.ModuleName
                                                  }).ToListAsync(cancellationToken);
                    }
                }
                _logger.LogInformation($"END GetQuoteFooterByIDQueryHandler");
                return quoteFooterByID;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Controller.GetQuoteFooterByIDQueryHandler");
                throw;
            }
        }
    }
}
