using FCC.Core;
using FCC.Core.Constants;
using WorkManagement.Domain.Model;
using WorkManagement.Service.Commands.QuoteFooter;
using WorkManagement.Service.ViewModel.QuoteFooter;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Softura.EntityFrameworkCore.Abstractions;
using OrganizationStructure.Domain.Model;

namespace WorkManagement.Service.Handlers.QuoteFooter
{
    public class UpdateQuoteFootersCommandHandler : IRequestHandler<UpdateQuoteFootersCommand, QuoteFootersRequestVM>
    {
        private readonly IRepository<Terms> _termRepository;
        private readonly IRepository<TermType> _termTypeRepository;
        private readonly IRepository<BusinessEntityTerm> _businessEntityTermRepository;
        private readonly IRepository<TermModuleEntity> _termModuleEntityRepository;
        private readonly ILogger<UpdateQuoteFootersCommandHandler> _logger;

        public UpdateQuoteFootersCommandHandler(IRepository<Terms> TermRepository, IRepository<TermType> TermTypeRepository,
            IRepository<BusinessEntityTerm> BusinessEntityTermRepository, IRepository<TermModuleEntity> TermModuleEntityRepository,
            ILogger<UpdateQuoteFootersCommandHandler> logger)
        {
            _termRepository = TermRepository;
            _termTypeRepository = TermTypeRepository;
            _businessEntityTermRepository = BusinessEntityTermRepository;
            _termModuleEntityRepository = TermModuleEntityRepository;
            _logger = logger;
        }

        public async Task<QuoteFootersRequestVM> Handle(UpdateQuoteFootersCommand request, CancellationToken cancellationToken)
        {
            try
            {
               
                    _logger.LogInformation($"Start  UpdateQuoteFootersCommandHandler");

                QuoteFootersRequestVM quoteFooters = new();

                var TermType = await _termTypeRepository.Get().Where(p => p.TermTypeName == "Footer").FirstOrDefaultAsync(cancellationToken);
                var Terms = new Terms()
                {
                    TermID = Convert.ToInt32(request._QuoteFootersRequest.QuoteFootersID),
                    TermName = request._QuoteFootersRequest.Name,
                    TermText = request._QuoteFootersRequest.Description,
                    IsActive = request._QuoteFootersRequest.Status,
                    TermTypeID = TermType?.TermTypeID,
                    ModifiedBy = "Administrator",
					ModifiedDateUTC = DateTime.UtcNow
				};
                var Count =  _termRepository.Get().Where(t => t.TermName == Terms.TermName && t.TermTypeID == TermType.TermTypeID && t.TermID != Terms.TermID && !t.IsDeleted).Count();

                if (Count == 0)
                {
                    var updateTermType = await _termRepository.UpdateAsync(Terms,cancellationToken);
                    if (updateTermType.TermID == 0)
                        throw new FCCException(ErrorMessage.QUOTEFOOTERS_UPDATE_ERROR);

                    if (!request._QuoteFootersRequest.Companies.IsNullOrEmpty())
                    {
                        var businessEntity = await _businessEntityTermRepository.Get().Where(x => x.TermID == updateTermType.TermID && !x.IsDeleted).ToListAsync(cancellationToken);
                        var selectedCompanies = request._QuoteFootersRequest.Companies?.Split(',').ToList();

                        var updateBusinessEntityTermId = businessEntity.Where(x => selectedCompanies != null && !selectedCompanies.Any(y => x.BusinessEntityID.ToString().Equals(y))).ToList();
                        var insertBusinessentityTermId = selectedCompanies?.Where(x => !businessEntity.Any(y => x.Equals(y.BusinessEntityID.ToString()))).ToList();

                        if (updateBusinessEntityTermId.Count > 0)
                        {
                            var UpdateBusinessEntityList = updateBusinessEntityTermId.Select(x => new BusinessEntityTerm
                            {
                                BusinessEntityTermID = x.BusinessEntityTermID,
                                BusinessEntityID = x.BusinessEntityID,
                                TermID = x.TermID,
                                IsActive = false,
                                IsDeleted = true,
                                CreatedBy = x.CreatedBy,
                                CreatedDateUTC = x.CreatedDateUTC
                            }).ToList();

                            await _businessEntityTermRepository.UpdateAsync(UpdateBusinessEntityList,cancellationToken);
                        }

                        if (insertBusinessentityTermId.Count > 0)
                        {
                            var insertBusinessEntityList = insertBusinessentityTermId.Select(x => new BusinessEntityTerm
                            {
                                BusinessEntityID = Convert.ToInt32(x),
                                TermID = updateTermType.TermID,
                            }).ToList();

                            await _businessEntityTermRepository.CreateAsync(insertBusinessEntityList,cancellationToken);
                        }
                    }

                    if (!request._QuoteFootersRequest.Modules.IsNullOrEmpty())
                    {
                        var moduleTerm = await _termModuleEntityRepository.Get().Where(x => x.TermID == updateTermType.TermID && !x.IsDeleted).ToListAsync(cancellationToken);
                        var selectedModules = request._QuoteFootersRequest.Modules?.Split(',').ToList();

                        var updateModuleTermId = moduleTerm.Where(x => selectedModules!=null && !selectedModules.Any(y => x.ModuleID.ToString().Equals(y))).ToList();
                        var insertModuleTermId = selectedModules?.Where(x => !moduleTerm.Any(y => x.Equals(y.ModuleID.ToString()))).ToList();

                        if (updateModuleTermId.Count > 0)
                        {
                            var UpdateModuleTermList = updateModuleTermId.Select(x => new TermModuleEntity
                            {
                                TermModuleEntityID = x.TermModuleEntityID,
                                ModuleID = x.ModuleID,
                                TermID = x.TermID,
                                IsActive = false,
                                IsDeleted = true,
                                CreatedBy = x.CreatedBy,
                                CreatedDateUTC = x.CreatedDateUTC
                            }).ToList();

                            await _termModuleEntityRepository.UpdateAsync(UpdateModuleTermList,cancellationToken);
                        }

                        if (insertModuleTermId.Count > 0)
                        {
                            var insertModuleTermList = insertModuleTermId.Select(x => new TermModuleEntity
                            {
                                ModuleID = Convert.ToInt32(x),
                                TermID = updateTermType.TermID,
                            }).ToList();

                            await _termModuleEntityRepository.CreateAsync(insertModuleTermList,cancellationToken);
                        }
                    }
                    quoteFooters.QuoteFootersID = updateTermType.TermID;
                }
                else
                    quoteFooters.QuoteFootersID = Convert.ToInt32(ResponseEnum.NotExists);

                _logger.LogInformation($"END UpdateQuoteFootersCommandHandler");
                return quoteFooters;
            }
            catch (Exception)
            {
                _logger.LogError("Error in QuoteFooterController.UpdateQuoteFootersCommandHandler");
                throw;
            }
        }
    }
}
