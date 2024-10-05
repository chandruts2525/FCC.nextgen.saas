using FCC.Core.Constants;
using FCC.Core;
using WorkManagement.Domain.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Softura.EntityFrameworkCore.Abstractions;
using WorkManagement.Service.Commands.ScheduleType;
using WorkManagement.Service.ViewModel.ScheduleType;
using Microsoft.Extensions.Logging;
using IAC.Service.Handlers.User;

namespace WorkManagement.Service.Handlers.ScheduleTypes
{
	public class CreateScheduleTypeCommendHandler : IRequestHandler<AddScheduleTypeCommand, ScheduleTypeViewModel>
    {
        private readonly IRepository<ScheduleType> _scheduleTypeRepository;
        private readonly ILogger<GetSecurityUserListQueryHandler> _logger;
        public CreateScheduleTypeCommendHandler(IRepository<ScheduleType> scheduleTypeRepository, ILogger<GetSecurityUserListQueryHandler> logger)
        {
            _scheduleTypeRepository = scheduleTypeRepository;
            _logger = logger;
        }

        public async Task<ScheduleTypeViewModel> Handle(AddScheduleTypeCommand request, CancellationToken cancellationToken)
        {
            try
            {

                if (request.scheduleTypeViewModel != null)
                {
                    var scheduleType = await _scheduleTypeRepository.Get(a => !a.IsDeleted).ToListAsync(cancellationToken);
                    if (scheduleType?.Count > 0 && scheduleType.Exists(p => p.ScheduleTypeCode == request.scheduleTypeViewModel.ScheduleTypeCode))
                    {
                        throw new FCCException(ErrorMessage.CODE_ALREADY_EXIST_ERROR);
                    }
                    if (scheduleType?.Count > 0 && scheduleType.Exists(p => p.ScheduleTypeName == request.scheduleTypeViewModel.ScheduleTypeName))
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
                        ModifiedBy = null,
                        ModifiedDateUTC = null
                    };
                    var result = await _scheduleTypeRepository.CreateAsync(ScheduleType, cancellationToken);


                    if (result != null)
                        return new ScheduleTypeViewModel() { ScheduleTypeId = result.ScheduleTypeId, ScheduleTypeCode = ScheduleType.ScheduleTypeCode };
                    else
                        throw new FCCException(ErrorMessage.SCHEDULETYPE_SAVE_ERROR);

                }
                else
                {
                    return new ScheduleTypeViewModel();
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ScheduleTypeController.CreateScheduleTypeCommendHandler");
                throw;
            }
        }


    }
}
