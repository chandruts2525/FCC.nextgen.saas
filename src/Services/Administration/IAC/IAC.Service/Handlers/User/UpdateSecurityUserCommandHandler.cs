using FCC.Core;
using FCC.Core.Constants;
using IAC.Domain.Model;
using IAC.Service.Commands.User;
using IAC.Service.ViewModel.User;
using MediatR;
using Microsoft.Extensions.Logging;
using Softura.EntityFrameworkCore.Abstractions;

namespace IAC.Service.Handlers.User
{
	public class UpdateSecurityUserCommandHandler : IRequestHandler<UpdateUserCommand, UpdateSecurityUserViewModel>
	{
		private readonly IRepository<SecurityUser> _securityUserRepository;
		private readonly IRepository<SecurityUserBusinessEntity> _securityUserBusinessEntity;

		private readonly ILogger<UpdateSecurityUserCommandHandler> _logger;

		public UpdateSecurityUserCommandHandler(IRepository<SecurityUser> securityUserRepository, IRepository<SecurityUserBusinessEntity> securityUserBusinessEntity, ILogger<UpdateSecurityUserCommandHandler> logger)
		{
			_securityUserRepository = securityUserRepository;
			_securityUserBusinessEntity = securityUserBusinessEntity;
			_logger = logger;
		}

		public async Task<UpdateSecurityUserViewModel> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
		{
			var response = new UpdateSecurityUserViewModel();

			_logger.LogInformation("Start UpdateSecurityUserCommandHandler, Parameters: UserId: {request.UserViewModel.UserId}", request.UserViewModel.UserId);

			if (request.UserViewModel.LoginEmail != "" && request.UserViewModel.UserId != 0)
			{

				//update the user information
				var users = new SecurityUser
				{
					UserId = request.UserViewModel.UserId,
					LoginEmail = request.UserViewModel.LoginEmail,
					FirstName = request.UserViewModel.FirstName,
					LastName = request.UserViewModel.LastName,
					RoleID = request.UserViewModel.RoleID,
					Status = request.UserViewModel.Status,
					MaximumATOMDevices = request.UserViewModel.MaximumATOMDevices,
					ModifiedBy = "DheshKumar",
					ModifiedDate = DateTime.UtcNow,
					IsActive = true
				};

				var getuser = _securityUserRepository.Get().Where(p => p.UserId != request.UserViewModel.UserId && p.LoginEmail == request.UserViewModel.LoginEmail).FirstOrDefault();

				if (getuser == null)
				{
					var updatedUser = await _securityUserRepository.UpdateAsync(users, cancellationToken);

					if (updatedUser.UserId == 0)
						throw new FCCException(ErrorMessage.USER_UPDATE_ERROR);

					//update security role in database

					response = new UpdateSecurityUserViewModel
					{
						UserId = updatedUser.UserId,
						LoginEmail = updatedUser.LoginEmail
					};

					//Now save the mapped users
					var userBusinessEntities = request.UserViewModel.NewBusinessEntityIDs?.Split(',');
					if (userBusinessEntities != null)
					{
						foreach (var userBusinessEntity in userBusinessEntities)
						{
							if (!string.IsNullOrEmpty(userBusinessEntity))
							{
								var getuserbusinessentity = _securityUserBusinessEntity.Get().Where(p => p.UserId == response.UserId && p.BusinessEntityId == Convert.ToInt64(userBusinessEntity) && !p.IsDeleted).FirstOrDefault();
								if (getuserbusinessentity == null)
								{
									var businessEntity = new SecurityUserBusinessEntity
									{
										BusinessEntityId = Convert.ToInt32(userBusinessEntity),
										UserId = updatedUser.UserId,
										IsActive = true
									};
									var createdUserBusinessEntity = await _securityUserBusinessEntity.CreateAsync(businessEntity, cancellationToken);

									if (createdUserBusinessEntity.UserBusinessEntityId == 0)
										throw new FCCException(ErrorMessage.USER_BUSINESS_ENTITY_SAVE_ERROR);
								}
							}
						}
					}

					//Now update the mapped users
					var oldUserBusinessEntities = request.UserViewModel.OldBusinessEntityIDs?.Split(',');
					if (oldUserBusinessEntities != null)
					{
						foreach (var oldUserBusinessEntity in oldUserBusinessEntities)
						{
							if (!string.IsNullOrEmpty(oldUserBusinessEntity))
							{
								var getuserbusinessentity = _securityUserBusinessEntity.Get().Where(p => p.UserId == response.UserId && p.BusinessEntityId == Convert.ToInt64(oldUserBusinessEntity) && !p.IsDeleted).FirstOrDefault();
								if (getuserbusinessentity != null)
								{
									var securityUserRole = new SecurityUserBusinessEntity
									{
										UserBusinessEntityId = Convert.ToInt32(getuserbusinessentity.UserBusinessEntityId),
										UserId = updatedUser.UserId,
										BusinessEntityId = Convert.ToInt32(oldUserBusinessEntity),
										ModifiedBy = "DheshKumar",
										ModifiedDate = DateTime.UtcNow,
										IsDeleted = true,
										IsActive = false,
									};

									var createdUserRole = await _securityUserBusinessEntity.UpdateAsync(securityUserRole, cancellationToken);
									if (createdUserRole.UserBusinessEntityId == 0)
										throw new FCCException(ErrorMessage.ROLE_USER_UPDATE_ERROR);
								}
							}
						}
					}
				}
				else
					response.UserId = Convert.ToInt32(ResponseEnum.NotExists);

				_logger.LogInformation($"UpdateSecurityUserCommandHandler End");

				return response;
			}
			else
			{
				return new UpdateSecurityUserViewModel();
			}
		}
	}
}
