using FCC.Core.Constants;
using FCC.Core;
using WorkManagement.Domain.Model;
using WorkManagement.Service.Commands.QuoteFooter;
using MediatR;
using Microsoft.Extensions.Logging;
using Softura.EntityFrameworkCore.Abstractions;
using Microsoft.IdentityModel.Tokens;
using WorkManagement.Service.ViewModel.QuoteFooter;
using OrganizationStructure.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace WorkManagement.Service.Handlers.QuoteFooter
{
    public class CreateQuoteFootersCommandHandler : IRequestHandler<CreateQuoteFootersCommand, QuoteFootersRequestVM>
    {
        private readonly IRepository<TermType> _termTypeRepository;
        private readonly IRepository<Terms> _termsRepository;
        private readonly IRepository<BusinessEntityTerm> _businessEntityTermRepository;
        private readonly IRepository<TermModuleEntity> _termModuleEntityRepository;
        private readonly ILogger<CreateQuoteFootersCommandHandler> _logger;
        public CreateQuoteFootersCommandHandler(IRepository<TermType> termTypeRepository,IRepository<Terms> termsRepository, IRepository<BusinessEntityTerm> businessEntityTermRepository,
            IRepository<TermModuleEntity> termModuleEntityRepository, ILogger<CreateQuoteFootersCommandHandler> logger)
        {
            _termTypeRepository = termTypeRepository;
            _termsRepository = termsRepository;
            _businessEntityTermRepository = businessEntityTermRepository;
            _termModuleEntityRepository = termModuleEntityRepository;
            _logger = logger;
        }

        public async Task<QuoteFootersRequestVM> Handle(CreateQuoteFootersCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var response = new QuoteFootersRequestVM();

                _logger.LogInformation($"Start CreateQuoteFootersCommandHandler");

                var TermTypeId = await _termTypeRepository.Get().Where(p => p.TermTypeName == "Footer").FirstOrDefaultAsync(cancellationToken);
                var term = new Terms()
                {
                    TermName = request.quoteFootersRequestVM.Name,
                    IsActive = request.quoteFootersRequestVM.Status,
                    TermText = request.quoteFootersRequestVM.Description,
                    TermTypeID = TermTypeId?.TermTypeID,
                    ModifiedBy = null,
                    ModifiedDateUTC = null
                };
                int Count = 0;
                if (TermTypeId != null)
                    Count = _termsRepository.Get().Where(t => t.TermName == request.quoteFootersRequestVM.Name && t.TermTypeID == TermTypeId.TermTypeID && !t.IsDeleted).Count();

                if (Count == 0)
                {
                    var createdterm = await _termsRepository.CreateAsync(term, cancellationToken);

                    if (createdterm.TermID == 0)
                        throw new FCCException(ErrorMessage.QUOTE_FOOTER_SAVE_ERROR);

                    if (request.quoteFootersRequestVM != null && !request.quoteFootersRequestVM.Companies.IsNullOrEmpty())
                    {
                        var mappedCompanies = request.quoteFootersRequestVM?.Companies?.Split(',');
                        if (mappedCompanies != null)
                        {
                            foreach (var mappedCompany in mappedCompanies)
                            {
                                if (!string.IsNullOrEmpty(mappedCompany))
                                {
                                    var BusinessEntityTerm = new BusinessEntityTerm
                                    {
                                        TermID = createdterm.TermID,
                                        BusinessEntityID = Convert.ToInt32(mappedCompany),
                                        ModifiedBy = null,
                                        ModifiedDateUTC = null
                                    };
                                    var businessEntityTerm = await _businessEntityTermRepository.CreateAsync(BusinessEntityTerm, cancellationToken);

                                    if (businessEntityTerm.BusinessEntityTermID == 0)
                                        throw new FCCException(ErrorMessage.QUOTE_FOOTER_SAVE_ERROR);
                                }
                            }
                        }
                    }

                    if (request.quoteFootersRequestVM != null && !request.quoteFootersRequestVM.Modules.IsNullOrEmpty())
                    {
                        var mappedModules = request.quoteFootersRequestVM.Modules?.Split(',');
                        if (mappedModules != null)
                        {
                            foreach (var mappedModule in mappedModules)
                            {
                                if (!string.IsNullOrEmpty(mappedModule))
                                {
                                    var TermModuleEntity = new TermModuleEntity
                                    {
                                        TermID = createdterm.TermID,
                                        ModuleID = Convert.ToInt32(mappedModule),
                                        ModifiedBy = null,
                                        ModifiedDateUTC = null
                                    };

                                    var termModuleEntity = await _termModuleEntityRepository.CreateAsync(TermModuleEntity, cancellationToken);
                                    if (termModuleEntity.TermModuleEntityID == 0)
                                        throw new FCCException(ErrorMessage.QUOTE_FOOTER_SAVE_ERROR);
                                }
                            }
                        }
                    }
                    response.QuoteFootersID = createdterm.TermID;
                }
                else
                    response.QuoteFootersID = Convert.ToInt32(ResponseEnum.NotExists);

                _logger.LogInformation($"END CreateQuoteFootersCommandHandler");
                return response;
            }
            catch (Exception)
            {
                _logger.LogError("Error in QuoteFooterController.CreateQuoteFootersCommandHandler");
                throw;
            }
        }
    }
}
