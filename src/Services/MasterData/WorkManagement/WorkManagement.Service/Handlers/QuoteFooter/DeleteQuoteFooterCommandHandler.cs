using FCC.Core.Constants;
using FCC.Core;
using IAC.Service.Commands.QuoteFooter;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Softura.EntityFrameworkCore.Abstractions;
using WorkManagement.Domain.Model;

namespace IAC.Service.Handlers.QuoteFooter
{
    public class DeleteQuoteFooterCommandHandler : IRequestHandler<DeleteQuoteFooterCommand, int?>
    {
        private readonly IRepository<Terms> _termRepository;
        private readonly IRepository<TermType> _termTypeRepository;
        private readonly ILogger<DeleteQuoteFooterCommandHandler> _logger;


        public DeleteQuoteFooterCommandHandler(IRepository<Terms> termRepository, IRepository<TermType> termTypeRepository, ILogger<DeleteQuoteFooterCommandHandler> logger)
        {
            _termRepository = termRepository;
            _termTypeRepository = termTypeRepository;
            _logger = logger;
        }

        public async Task<int?> Handle(DeleteQuoteFooterCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var termType = await _termTypeRepository.Get().Where(p => p.TermTypeName == "Footer").FirstOrDefaultAsync(cancellationToken);
                if (termType != null)
                {
                    var quotefooterModel = await _termRepository.Get().Where(x => x.TermID == request.QuoteFooterId && x.TermTypeID == termType.TermTypeID).FirstOrDefaultAsync(cancellationToken);
                    if (quotefooterModel != null)
                    {
                        var QuoteFooter = new Terms()
                        {
                            TermID = quotefooterModel.TermID,
                            TermName = quotefooterModel.TermName,
                            IsDeleted = true,
                            IsActive = quotefooterModel.IsActive,
                            CreatedBy = quotefooterModel.CreatedBy,
                            CreatedDateUTC = quotefooterModel.CreatedDateUTC,
                            TermText = quotefooterModel.TermText,
                            TermTypeID = quotefooterModel.TermTypeID,
                            IsLocked = quotefooterModel.IsLocked,
                        };

                        _logger.LogInformation($"Start DeleteQuoteFooterCommandHandler");

                        var QuoteFooterUpdate = await _termRepository.UpdateAsync(QuoteFooter,cancellationToken);

                        _logger.LogInformation($"END DeleteQuoteFooterCommandHandler");

                        if (QuoteFooterUpdate.TermID == 0)
                            throw new FCCException(ErrorMessage.SCHEDULETYPE_SAVE_ERROR);

                        return QuoteFooterUpdate.TermID;
                    }
                    else
                        return -1;
                }
                return null;
            }
            catch (Exception)
            {
                _logger.LogError($"Error In QuoteFooterController.DeleteQuoteFooterCommandHandler");
                throw;
            }
        }
    }
}
