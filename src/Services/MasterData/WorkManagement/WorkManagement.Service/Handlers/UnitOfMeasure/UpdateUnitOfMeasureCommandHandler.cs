using FCC.Core.Constants;
using FCC.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Softura.EntityFrameworkCore.Abstractions;
using System.Data;
using WorkManagement.Domain.Model;
using WorkManagement.Domain.ViewModel.UnitOfMeasure;
using WorkManagement.Service.Commands.UnitOfMeasure;

namespace WorkManagement.Service.Handlers.UnitOfMeasure
{
    public class UpdateUnitOfMeasureCommandHandler : IRequestHandler<UpdateUnitOfMeasureCommand, UnitOfMeasureResponseVM>
    {
        private readonly IRepository<UnitMeasure> _unitMeasureRepository;

        public UpdateUnitOfMeasureCommandHandler(IRepository<UnitMeasure> unitMeasureRepository)
        {
            _unitMeasureRepository = unitMeasureRepository;
        }

        public async Task<UnitOfMeasureResponseVM> Handle(UpdateUnitOfMeasureCommand request, CancellationToken cancellationToken)
        {
            if (request.UnitOfMeasureVM.UnitMeasureCode != "")
            {
                var getUoM = await _unitMeasureRepository.Get(a => !a.IsDeleted).ToListAsync(cancellationToken);

                if (getUoM?.Count > 0 && getUoM.Exists(p => p.UnitMeasureDisplayValue == request.UnitOfMeasureVM.UnitMeasureName && p.UnitMeasureCode != request.UnitOfMeasureVM.UnitMeasureCode && p.UnitMeasureId==request.UnitOfMeasureVM.UnitMeasureId))
                {
                    throw new FCCException(ErrorMessage.NAME_ALREADY_EXIST_ERROR);
                }
                var uom = new UnitMeasure
                {
                    UnitMeasureId = request.UnitOfMeasureVM.UnitMeasureId,
                    UnitMeasureCode = request.UnitOfMeasureVM.UnitMeasureCode,
                    UnitMeasureDisplayValue = request.UnitOfMeasureVM.UnitMeasureName,
                    UnitMeasureTypeId = request.UnitOfMeasureVM.UnitMeasureTypeId,
                    ConversionFactor = request.UnitOfMeasureVM.ConversionFactor,
                    IsActive = request.UnitOfMeasureVM.IsActive,
                    IsDeleted = request.UnitOfMeasureVM.IsDelete,
					ModifiedDateUTC = DateTime.UtcNow
				};

                var updatedUoM = await _unitMeasureRepository.UpdateAsync(uom, cancellationToken);


                if (updatedUoM != null)
                    return new UnitOfMeasureResponseVM() { UnitMeasureCode = updatedUoM.UnitMeasureCode };
                else
                    throw new FCCException(ErrorMessage.UOM_UPDATE_ERROR);
            }
            else
            {
                return new UnitOfMeasureResponseVM();
            }
        }
    }
}
