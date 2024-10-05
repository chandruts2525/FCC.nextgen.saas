using FCC.Core;
using FCC.Core.Constants;
using IAC.Domain.Model;
using IAC.Service.Commands.Role;
using IAC.Service.ViewModel.Role;
using MediatR;
using Microsoft.Extensions.Logging;
using Softura.EntityFrameworkCore.Abstractions;
using System.Data;

namespace IAC.Service.Handlers.Role
{
	public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, CreateRoleVM>
	{
		private readonly IRepository<SecurityRole> _securityRoleRepository;
		private readonly IRepository<SecurityUserRole> _securityUserRoleRepository;
		private readonly IRepository<SecurityRoleAttachment> _securityRoleAttachmentRepository;

		private readonly ILogger<CreateRoleCommandHandler> _logger;
		public CreateRoleCommandHandler(IRepository<SecurityRole> securityRoleRepository, IRepository<SecurityUserRole> securityUserRoleRepository,
			IRepository<SecurityRoleAttachment> securityRoleAttachmentRepository, ILogger<CreateRoleCommandHandler> logger)
		{
			_securityRoleRepository = securityRoleRepository;
			_securityUserRoleRepository = securityUserRoleRepository;
			_securityRoleAttachmentRepository = securityRoleAttachmentRepository;
			_logger = logger;
		}

		public async Task<CreateRoleVM> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
		{
			var response = new CreateRoleVM();

			_logger.LogInformation("Start CreateSecurityRoleCommandHandler, Parameters: RoleID: {request.createRoleVM.RoleId}", request.createRoleVM.RoleId);

			if (request.createRoleVM.RoleName != "")
			{
				//Save the role information

				var role = new SecurityRole
				{
					RoleName = request.createRoleVM.RoleName,
					IsActive = true,
				};

				//Validate if the name already exists in database
				var getrole = _securityRoleRepository.Get().Where(p => p.RoleName == request.createRoleVM.RoleName).FirstOrDefault();

				if (getrole == null)
				{
					var createdRole = await _securityRoleRepository.CreateAsync(role, cancellationToken);

					//save security role in database

					if (createdRole.RoleId == 0)
						throw new FCCException(ErrorMessage.ROLE_SAVE_ERROR);

					response = new CreateRoleVM
					{
						RoleId = createdRole.RoleId,
						RoleName = createdRole.RoleName
					};

				//Now save the mapped users
				var mappedUsers = request.createRoleVM.MappedUser?.Split(',');
				if (mappedUsers != null)
				{
					foreach (var mappedUser in mappedUsers)
					{
						if (!string.IsNullOrEmpty(mappedUser))
						{
							var securityUserRole = new SecurityUserRole
							{
								RoleId = createdRole.RoleId,
								UserId = Convert.ToInt32(mappedUser),
								IsActive = true
							};

							var createdUserRole = await _securityUserRoleRepository.CreateAsync(securityUserRole, cancellationToken);
							if (createdUserRole.UserRoleId == 0)
								throw new FCCException(ErrorMessage.ROLE_USER_SAVE_ERROR);
						}
					}
				}

				if (request.createRoleVM.Attachments != null)
				{
					//Save Attachments
					foreach (var attachment in request.createRoleVM.Attachments)
					{
						var securityRoleAttachment = new SecurityRoleAttachment
						{
							RoleId = createdRole.RoleId,
							Url = attachment.FileURI,
							FileName = attachment.FileName,
							IsActive = true
						};

						var createdSecurityRoleAttachment = await _securityRoleAttachmentRepository.CreateAsync(securityRoleAttachment, cancellationToken);

						if (createdSecurityRoleAttachment.RoleUserMappingID == 0)
							throw new FCCException(ErrorMessage.ROLE_ATTACHMENT_SAVE_ERROR);

					}
				}

				}
				else
					response.RoleId = Convert.ToInt32(ResponseEnum.NotExists);
				
				_logger.LogInformation($"CreateSecurityRoleCommandHandler End");
				return response;
			}
			else
			{
				return response;
			}
		}
	}
}
