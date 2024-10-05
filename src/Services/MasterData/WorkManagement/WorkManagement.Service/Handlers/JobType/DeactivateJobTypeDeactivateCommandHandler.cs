using WorkManagement.Domain.Model;
using WorkManagement.Service.Commands;
using WorkManagement.Service.Commands.JobType;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Softura.EntityFrameworkCore.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkManagement.Service.Handlers.JobType
{
    public class DeactivateJobTypeDeactivateCommandHandler : IRequestHandler<DeactivateJobTypeDeactivateCommand, JobTypes>
    {
        private readonly IRepository<JobTypes> _jobTypes;

        private readonly ILogger<DeactivateJobTypeDeactivateCommandHandler> _logger;

        public DeactivateJobTypeDeactivateCommandHandler(IRepository<JobTypes> jobTypes, ILogger<DeactivateJobTypeDeactivateCommandHandler> logger)
        {
            _jobTypes = jobTypes;
            _logger = logger;
        }

        public async Task<JobTypes> Handle(DeactivateJobTypeDeactivateCommand request, CancellationToken cancellationToken)
        {
            var getjobtype = await _jobTypes.Get().Where(x => x.JobTypeId == request._jobTypes.JobTypeId).FirstOrDefaultAsync(cancellationToken);
            if (getjobtype != null)
            {
                getjobtype.IsActive = request._jobTypes.IsActive;
                _logger.LogInformation("Start DeactivateJobTypeDeactivateCommandHandler, Parameters: JobTypeId: {request._jobTypes.JobTypeId}", request._jobTypes.JobTypeId);
                var updatejobtype = await _jobTypes.UpdateAsync(getjobtype,cancellationToken);
               
                if (updatejobtype != null && !updatejobtype.IsActive)
                    return new JobTypes() { JobTypeName = "Successfully Deactivated JobType" };

                else if (updatejobtype != null && updatejobtype.IsActive)
                    return new JobTypes() { JobTypeName = "Successfully Activated JobType" };
                else
                    return new JobTypes() { JobTypeName = "Activate JobType Failed" };
            }
            else
            {
                return new JobTypes() { };
            }

            

        }
    }
}
