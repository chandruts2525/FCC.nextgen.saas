using FCC.Core.Constants;
using FCC.Core;
using WorkManagement.Domain.Model;
using WorkManagement.Service.Commands.ScheduleType;
using WorkManagement.Service.ViewModel.ScheduleType;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Softura.EntityFrameworkCore.Abstractions;
using MediatR;

namespace WorkManagement.Service.Handlers.ScheduleTypes
{
    public class ScheduleTypeDeactivateCommandHandler:IRequestHandler<ScheduleTypeDeactivateCommand,ScheduleTypeViewModel>
    {
        private readonly IRepository<ScheduleType> _scheduleTypeRepository;
        private readonly ILogger<ScheduleTypeDeactivateCommandHandler> _logger;

        public ScheduleTypeDeactivateCommandHandler(IRepository<ScheduleType> scheduleTypeRepository, ILogger<ScheduleTypeDeactivateCommandHandler> logger) { 
            _scheduleTypeRepository = scheduleTypeRepository;
            _logger = logger;
        }

        public async Task<ScheduleTypeViewModel> Handle(ScheduleTypeDeactivateCommand request, CancellationToken cancellationToken)
        {
            ScheduleTypeViewModel scheduleTypeViewModel = new();
            try
            {
                var getScheduletype = await _scheduleTypeRepository.Get().Where(p => p.ScheduleTypeId == request.scheduleTypeId).FirstOrDefaultAsync(cancellationToken);
                if (getScheduletype != null)
                {
                    var updateScheduleType = new ScheduleType
                    {
                        ScheduleTypeId = getScheduletype.ScheduleTypeId,
                        ScheduleTypeName = getScheduletype.ScheduleTypeName,
                        ScheduleTypeCode = getScheduletype.ScheduleTypeCode,
                        IsActive = request.status,
                        IsDeleted = getScheduletype.IsDeleted,
                        IsLocked = getScheduletype.IsLocked,
                        CreatedBy = getScheduletype.CreatedBy,
                        CreatedDateUTC = getScheduletype.CreatedDateUTC,
                        UnitComponentsID = getScheduletype.UnitComponentsID,
                        Schedulable = getScheduletype.Schedulable,
                        GroundBearingPresure = getScheduletype.GroundBearingPresure
                    };
                   _logger.LogInformation("Start ScheduleTypeDeactivateCommandHandler, Parameters: UserId: {request.scheduleTypeId}", request.scheduleTypeId);

                    var ScheduleTypeUpdate = await _scheduleTypeRepository.UpdateAsync(updateScheduleType,cancellationToken);

                    _logger.LogInformation($"END ScheduleTypeDeactivateCommandHandler");

                    if (ScheduleTypeUpdate.ScheduleTypeId == 0)
                        throw new FCCException(ErrorMessage.SCHEDULETYPE_SAVE_ERROR);

                    scheduleTypeViewModel.ScheduleTypeId = ScheduleTypeUpdate.ScheduleTypeId;
                }
                else
                    scheduleTypeViewModel.ScheduleTypeId = -1;

                return scheduleTypeViewModel;
            }
            catch (Exception)
            {
                _logger.LogError($"Error in ScheduleTypeController.ScheduleTypeDeactivateCommandHandler");
                throw;
            }
        }
    }
}
