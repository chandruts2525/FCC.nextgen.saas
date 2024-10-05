using IAC.Domain.Model;
using IAC.Domain.ViewModel.Role;
using IAC.Service.Queries.Role;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Softura.EntityFrameworkCore.Abstractions;
using System.Data;

namespace IAC.Service.Handlers.Role
{
	public class GetRoleAttachmentQueryHandler : IRequestHandler<GetRoleAttachmentQuery, List<GetRoleAttachmentResponseVM>>
    {
        private readonly IRepository<SecurityRoleAttachment> _isecurityRoleAttachmentRepository;
        private readonly ILogger<GetRoleAttachmentQueryHandler> _logger;

        public GetRoleAttachmentQueryHandler(IRepository<SecurityRoleAttachment> isecurityRoleAttachmentRepository, ILogger<GetRoleAttachmentQueryHandler> Logger)
        {
            _isecurityRoleAttachmentRepository = isecurityRoleAttachmentRepository;
            _logger = Logger;
        }

        async Task<List<GetRoleAttachmentResponseVM>> IRequestHandler<GetRoleAttachmentQuery, List<GetRoleAttachmentResponseVM>>.Handle(GetRoleAttachmentQuery request, CancellationToken cancellationToken)
        {
            if (request.RoleId != 0)
            {
                _logger.LogInformation("Start GetSecurityRoleByIdQueryHandler, Parameters: RoleID: {request.RoleId}", request.RoleId);

                var result = await (from ra in _isecurityRoleAttachmentRepository.Get(x => x.RoleId == request.RoleId)
                                    where !ra.IsDeleted
                                    select new GetRoleAttachmentResponseVM
									{
                                        RoleAttachmentId = ra.RoleUserMappingID,
                                        RoleId = ra.RoleId,
                                        FileURI = ra.Url,
                                        FileName = ra.FileName,
                                        Createdby = ra.CreatedBy,
                                    }).ToListAsync(cancellationToken);

                _logger.LogInformation($"GetSecurityRoleByIdQueryHandler End");

                if (result != null)
                    return result;
                else
                    return new List<GetRoleAttachmentResponseVM>();
            }
            else
            {
                return new List<GetRoleAttachmentResponseVM>();
            }
        }
    }
}
