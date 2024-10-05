using Softura.EntityFrameworkCore.Abstractions;
using MediatR;
using FCC.Core.Constants;
using FCC.Core;
using Microsoft.EntityFrameworkCore;
using WorkManagement.Domain.Model;
using WorkManagement.Domain.ViewModel.UnitOfMeasure;
using WorkManagement.Service.Commands.UnitOfMeasure;
using DocumentFormat.OpenXml.Wordprocessing;

namespace WorkManagement.Service.Handlers.UnitOfMeasure
{
    public class CreateUnitOfMeasureCommandHandler : IRequestHandler<CreateUnitOfMeasureCommand, UnitOfMeasureResponseVM>
    {

        private readonly IRepository<UnitMeasure> _unitMeasureRepository;


        public CreateUnitOfMeasureCommandHandler(IRepository<UnitMeasure> unitMeasureRepository)
        {
            _unitMeasureRepository = unitMeasureRepository;
        }

        public async Task<UnitOfMeasureResponseVM> Handle(CreateUnitOfMeasureCommand request, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(request.UnitOfMeasureVM.UnitMeasureCode))
            {
                var getUoM = await _unitMeasureRepository.Get().ToListAsync(cancellationToken);

                if (getUoM?.Count > 0 && getUoM.Exists(p => p.UnitMeasureCode == request.UnitOfMeasureVM.UnitMeasureCode && !p.IsDeleted))
                {
                    throw new FCCException(ErrorMessage.CODE_ALREADY_EXIST_ERROR);
                }
                if (getUoM?.Count > 0 && getUoM.Exists(p => p.UnitMeasureDisplayValue == request.UnitOfMeasureVM.UnitMeasureName && !p.IsDeleted))
                {
                    throw new FCCException(ErrorMessage.NAME_ALREADY_EXIST_ERROR);
                }

                var uom = new UnitMeasure
                {
                    UnitMeasureCode = request.UnitOfMeasureVM.UnitMeasureCode,
                    UnitMeasureDisplayValue = request.UnitOfMeasureVM.UnitMeasureName,
                    UnitMeasureTypeId = request.UnitOfMeasureVM.UnitMeasureTypeId,
                    ConversionFactor = request.UnitOfMeasureVM.ConversionFactor,
                    IsActive = request.UnitOfMeasureVM.IsActive,                    
                    ModifiedBy = null,
                    ModifiedDateUTC = null
                };
                if (getUoM?.Count > 0 && getUoM.Exists(p => p.UnitMeasureCode == request.UnitOfMeasureVM.UnitMeasureCode && p.IsDeleted))
                {
                    uom.IsDeleted = request.UnitOfMeasureVM.IsDelete;
                    uom = await _unitMeasureRepository.UpdateAsync(uom, cancellationToken);
                }
                else
                {
                    uom = await _unitMeasureRepository.CreateAsync(uom, cancellationToken);
                }
                if (uom != null)
                    return new UnitOfMeasureResponseVM() { UnitMeasureCode = request.UnitOfMeasureVM.UnitMeasureCode };
                else
                    throw new FCCException(ErrorMessage.UOM_SAVE_ERROR);
            }
            else
            {
                return new UnitOfMeasureResponseVM();
            }
        }
    }
}
