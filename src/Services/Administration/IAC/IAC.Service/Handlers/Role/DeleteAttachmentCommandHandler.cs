using FCC.Core.Constants;
using FCC.Core;
using IAC.Domain.Model;
using MediatR;
using Microsoft.Extensions.Logging;
using Softura.EntityFrameworkCore.Abstractions;
using IAC.Service.Commands.Role;
using Softura.Azure.Storage.Blobs.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace IAC.Service.Handlers.Role
{
    public class DeleteAttachmentCommandHandler : IRequestHandler<DeleteAttachmentCommand, bool>
    {
        private readonly IRepository<SecurityRoleAttachment> _securityRoleAttachmentRepository;

        private readonly IBlobStorage _blobStorage;
        private const string pathSeperator = "/";
        private readonly ILogger<DeleteAttachmentCommandHandler> _logger;

        public DeleteAttachmentCommandHandler(ILogger<DeleteAttachmentCommandHandler> logger, IBlobStorage blobStorage, IRepository<SecurityRoleAttachment> securityRoleAttachmentRepository)
        {
            _logger = logger;
            _blobStorage = blobStorage;
            _securityRoleAttachmentRepository = securityRoleAttachmentRepository;
        }

		async Task<bool> IRequestHandler<DeleteAttachmentCommand, bool>.Handle(DeleteAttachmentCommand request, CancellationToken cancellationToken)
		{
			if (request.RoleAttachmentId != null && request.RoleAttachmentId != 0)
			{
				_logger.LogInformation("Start DeleteAttachmentCommandHandler, Parameters: RoleAttachmentId: {request.RoleAttachmentId}", request.RoleAttachmentId);
				var getRoleAttachment = _securityRoleAttachmentRepository.Get().Where(p => p.RoleUserMappingID == request.RoleAttachmentId && p.Url == request.FileURI).FirstOrDefault();
				if (getRoleAttachment != null)
				{
					//update the role attachment information
					var roleattachment = new SecurityRoleAttachment
					{
                        RoleUserMappingID = Convert.ToInt32(request.RoleAttachmentId),
						Url = request.FileURI,
						RoleId = getRoleAttachment.RoleId,
						FileName = getRoleAttachment.FileName,
						IsDeleted = true,
						ModifiedBy = "DheshKumar",
						ModifiedDate = DateTime.UtcNow,
						IsActive = false
					};

                    var deleteRoleattachment = await _securityRoleAttachmentRepository.UpdateAsync(roleattachment, cancellationToken);

                    _logger.LogInformation($"DeleteAttachmentCommandHandler End");

					if (deleteRoleattachment.RoleUserMappingID == 0)
						throw new FCCException(ErrorMessage.ROLE_ATTACHMENT_DELETE_ERROR);
					else
					{
							var filename = $"{request.ContainerFolderName}{pathSeperator}{Path.GetFileName(request.FileURI)}";
							if (await _blobStorage.ExistsAsync(filename, request.ContainerName, cancellationToken))
							{
								await _blobStorage.DeleteAsync(request.FileURI);
								return true;
							}
							return false;
					} 
				}
				return false;
			}
			else
			{
				var filename = $"{request.ContainerFolderName}{pathSeperator}{Path.GetFileName(request.FileURI)}";
				if (await _blobStorage.ExistsAsync(filename, request.ContainerName, cancellationToken))
				{
					await _blobStorage.DeleteAsync(request.FileURI);
					return true;
				}
				return false;
			}
		}
	}
}
