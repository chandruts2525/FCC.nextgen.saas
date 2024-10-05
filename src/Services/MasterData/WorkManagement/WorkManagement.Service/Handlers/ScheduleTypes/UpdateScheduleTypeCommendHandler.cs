using FCC.Core.Constants;
using FCC.Core;
using WorkManagement.Domain.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Softura.EntityFrameworkCore.Abstractions;
using WorkManagement.Service.Commands.ScheduleType;
using WorkManagement.Service.ViewModel.ScheduleType;

namespace WorkManagement.Service.Handlers.ScheduleTypes
{
    public class UpdateScheduleTypeCommendHandler : IRequestHandler<UpdateScheduleTypeCommand, ScheduleTypeViewModel>
    {
        private readonly IRepository<ScheduleType> _scheduleTypeRepository;
        public UpdateScheduleTypeCommendHandler(IRepository<ScheduleType> scheduleTypeRepository)
        {
            _scheduleTypeRepository = scheduleTypeRepository;
        }

        public async Task<ScheduleTypeViewModel> Handle(UpdateScheduleTypeCommand request, CancellationToken cancellationToken)
        {
            if (request.scheduleTypeViewModel != null)
            {
                var scheduleType = await _scheduleTypeRepository.Get(a => !a.IsDeleted).ToListAsync(cancellationToken);
                if (scheduleType?.Count > 0 && scheduleType.Exists(p => p.ScheduleTypeName == request.scheduleTypeViewModel.ScheduleTypeName && p.ScheduleTypeId != request.scheduleTypeViewModel.ScheduleTypeId))
                {
                    throw new FCCException(ErrorMessage.NAME_ALREADY_EXIST_ERROR);
                }

                var ScheduleType = new ScheduleType
                {
                    ScheduleTypeId = request.scheduleTypeViewModel.ScheduleTypeId,
                    ScheduleTypeName = request.scheduleTypeViewModel.ScheduleTypeName,
                    ScheduleTypeCode = request.scheduleTypeViewModel.ScheduleTypeCode,
                    UnitComponentsID = request.scheduleTypeViewModel.UnitOrComponent,
                    Schedulable = request.scheduleTypeViewModel.Schedulable,
                    GroundBearingPresure = request.scheduleTypeViewModel.GBP,
                    IsActive = request.scheduleTypeViewModel.IsActive,
                    IsDeleted = request.scheduleTypeViewModel.IsDeleted,
                    IsLocked = request.scheduleTypeViewModel.IsLocked,
					ModifiedBy = "Administrator",
					ModifiedDateUTC = DateTime.UtcNow
				};
                var result = await _scheduleTypeRepository.UpdateAsync(ScheduleType,cancellationToken);


                if (result != null)
                    return new ScheduleTypeViewModel() { ScheduleTypeId = result.ScheduleTypeId, ScheduleTypeCode = ScheduleType.ScheduleTypeCode };
                else
                    throw new FCCException(ErrorMessage.SCHEDULETYPE_UPDATE_ERROR);

            }
            else
            {
                return new ScheduleTypeViewModel();
            }
        }


    }
}
