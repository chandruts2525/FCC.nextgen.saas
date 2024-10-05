using FCC.Core.Constants;
using FCC.Core;
using IAC.Domain.Model;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Data;
using Softura.EntityFrameworkCore.Abstractions;
using IAC.Service.ViewModel.Role;
using IAC.Service.Commands.Role;

namespace IAC.Service.Handlers.Role
{
    public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, UpdateRoleVM>
    {
        private readonly IRepository<SecurityRole> _securityRoleRepository;
        private readonly IRepository<SecurityUserRole> _securityUserRoleRepository;
        private readonly IRepository<SecurityRoleAttachment> _securityRoleAttachmentRepository;

        private readonly ILogger<UpdateRoleCommandHandler> _logger;
        public UpdateRoleCommandHandler(IRepository<SecurityRole> securityRoleRepository, IRepository<SecurityUserRole> securityUserRoleRepository,
            IRepository<SecurityRoleAttachment> securityRoleAttachmentRepository, ILogger<UpdateRoleCommandHandler> logger)
        {
            _securityRoleRepository = securityRoleRepository;
            _securityUserRoleRepository = securityUserRoleRepository;
            _securityRoleAttachmentRepository = securityRoleAttachmentRepository;
            _logger = logger;
        }

        public async Task<UpdateRoleVM> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            var response = new UpdateRoleVM();

            if (request.updateRoleVM.RoleName != "" && request.updateRoleVM.RoleId != 0)
            {
                //update the role information
                var role = new SecurityRole
                {
                    RoleId = Convert.ToInt32(request.updateRoleVM.RoleId),
                    RoleName = request.updateRoleVM.RoleName,
                    IsActive = true,
					ModifiedBy = "Administrator",
					ModifiedDate = DateTime.UtcNow
				};

                _logger.LogInformation("Start UpdateSecurityRoleCommandHandler, Parameters: RoleId: {request.updateRoleVM.RoleId}", request.updateRoleVM.RoleId);

				var getrole = _securityRoleRepository.Get().Where(p => p.RoleId != request.updateRoleVM.RoleId && p.RoleName == request.updateRoleVM.RoleName).FirstOrDefault();

                if (getrole == null)
                {
                    var updatedRole = await _securityRoleRepository.UpdateAsync(role, cancellationToken);

                    //update security role in database

                    if (updatedRole.RoleId == 0)
                        throw new FCCException(ErrorMessage.ROLE_UPDATE_ERROR);

                    response = new UpdateRoleVM
                    {
                        RoleId = updatedRole.RoleId,
                        RoleName = updatedRole.RoleName
                    };

                    //Now save the mapped users
                    var mappedUsers = request.updateRoleVM.MappedUser?.Split(',');
                    if (mappedUsers != null)
                    {
                        foreach (var mappedUser in mappedUsers)
                        {
                            if (!string.IsNullOrEmpty(mappedUser))
                            {
                                var getuserrole = _securityUserRoleRepository.Get().Where(p => p.RoleId == response.RoleId && p.UserId == Convert.ToInt64(mappedUser) && p.IsActive && !p.IsDeleted).FirstOrDefault();
                                if (getuserrole == null)
                                {
                                    var securityUserRole = new SecurityUserRole
                                    {
                                        RoleId = updatedRole.RoleId,
                                        UserId = Convert.ToInt32(mappedUser),
                                        IsActive = true
                                    };
                                    var createdUserRole = await _securityUserRoleRepository.CreateAsync(securityUserRole, cancellationToken);
                                    if (createdUserRole.UserRoleId == 0)
                                        throw new FCCException(ErrorMessage.ROLE_USER_UPDATE_ERROR);
                                }
                            }
                        }
                    }
                    //Now update the mapped users
                    var unmappedUsers = request.updateRoleVM.UnMappedUser?.Split(',');
                    if (unmappedUsers != null)
                    {
                        foreach (var unmappedUser in unmappedUsers)
                        {
                            if (!string.IsNullOrEmpty(unmappedUser))
                            {
                                var getuserrole = _securityUserRoleRepository.Get().Where(p => p.RoleId == response.RoleId && p.UserId == Convert.ToInt64(unmappedUser) && !p.IsDeleted).FirstOrDefault();
                                if (getuserrole != null)
                                {
                                    var securityUserRole = new SecurityUserRole
                                    {
                                        UserRoleId = Convert.ToInt32(getuserrole?.UserRoleId),
                                        RoleId = updatedRole.RoleId,
                                        UserId = Convert.ToInt32(unmappedUser),
                                        IsDeleted = true,
                                        IsActive = false,
										ModifiedBy = "Administrator",
										ModifiedDate = DateTime.UtcNow
									};
                                    var createdUserRole = await _securityUserRoleRepository.UpdateAsync(securityUserRole, cancellationToken);
                                    if (createdUserRole.UserRoleId == 0)
                                        throw new FCCException(ErrorMessage.ROLE_USER_UPDATE_ERROR);
                                }
                            }
                        }
                    }
                    if (request.updateRoleVM.Attachments != null)
                    {
                        //var getroleattachment = _securityRoleAttachmentRepository.Get().Where(p => p.RoleId == response.RoleId).FirstOrDefault();

                        //Update Attachments
                        foreach (var attachment in request.updateRoleVM.Attachments)
                        {
                            var securityRoleAttachment = new SecurityRoleAttachment
                            {
                                //RoleUserMappingID = Convert.ToInt32(getroleattachment?.RoleUserMappingID),
                                RoleId = updatedRole.RoleId,
                                Url = attachment.FileURI,
                                FileName = attachment.FileName,
                                IsActive = true,
								ModifiedBy = "Administrator",
								ModifiedDate = DateTime.UtcNow
							};
                            var createdSecurityRoleAttachment = await _securityRoleAttachmentRepository.CreateAsync(securityRoleAttachment, cancellationToken);
                            if (createdSecurityRoleAttachment.RoleUserMappingID == 0)
                                throw new FCCException(ErrorMessage.ROLE_ATTACHMENT_UPDATE_ERROR);
                        }
                    }
                }
                else
					response.RoleId = Convert.ToInt32(ResponseEnum.NotExists);

				_logger.LogInformation($"UpdateSecurityRoleCommandHandler End");
                return response;
            }
            else
            {
                return response;
            }
        }
    }
}
