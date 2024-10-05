using FCC.Core.Constants;
using FCC.Core;
using IAC.Domain.Model;
using MediatR;
using Microsoft.Extensions.Logging;
using Softura.EntityFrameworkCore.Abstractions;
using IAC.Service.Commands.User;
using IAC.Service.ViewModel.User;

namespace IAC.Service.Handlers.User
{
	public class CreateSecurityUserCommandHandler : IRequestHandler<CreateUserCommand, CreateSecurityUserViewModel>
	{
		private readonly IRepository<SecurityUser> _securityUserRepository;
		private readonly IRepository<SecurityUserBusinessEntity> _securityUserBusinessEntity;
		private readonly ILogger<CreateSecurityUserCommandHandler> _logger;
		public CreateSecurityUserCommandHandler(IRepository<SecurityUser> securityUserRepository, IRepository<SecurityUserBusinessEntity> securityUserBusinessEntity, ILogger<CreateSecurityUserCommandHandler> logger)
		{
			_securityUserRepository = securityUserRepository;
			_securityUserBusinessEntity = securityUserBusinessEntity;
			_logger = logger;
		}

		public async Task<CreateSecurityUserViewModel> Handle(CreateUserCommand request, CancellationToken cancellationToken)
		{
			var response = new CreateSecurityUserViewModel();

			_logger.LogInformation("Start CreateSecurityUserCommandHandler, Parameters: Email: {request.UserViewModel.LoginEmail}", request.UserViewModel.LoginEmail);

			if (request.UserViewModel.LoginEmail != "")
			{
				//Save the user information
				var user = new SecurityUser
				{
					LoginEmail = request.UserViewModel.LoginEmail,
					FirstName = request.UserViewModel.FirstName,
					LastName = request.UserViewModel.LastName,
					Status = request.UserViewModel.Status,
					RoleID = request.UserViewModel.RoleID,
					MaximumATOMDevices = request.UserViewModel.MaximumATOMDevices,
					IsActive = true
				};

				//Validate if the email already exists in database
				var getuser = _securityUserRepository.Get().Where(p => p.LoginEmail == request.UserViewModel.LoginEmail && !p.IsDeleted).FirstOrDefault();

				if (getuser == null)
				{
					var createdUser = await _securityUserRepository.CreateAsync(user, cancellationToken);

					//save security user in database

					if (createdUser.UserId == 0)
						throw new FCCException(ErrorMessage.USER_SAVE_ERROR);

					response = new CreateSecurityUserViewModel
					{
						UserId = createdUser.UserId,
						LoginEmail = createdUser.LoginEmail
					};


					//Now save the users business entity
					var userBusinessEntities = request.UserViewModel.BusinessEntityIDs?.Split(',');
					if (userBusinessEntities != null)
					{
						foreach (var userBusinessEntity in userBusinessEntities)
						{
							if (!string.IsNullOrEmpty(userBusinessEntity))
							{
								var businessEntity = new SecurityUserBusinessEntity
								{
									BusinessEntityId = Convert.ToInt32(userBusinessEntity),
									UserId = createdUser.UserId,
									IsActive = true
								};
								var createdUserBusinessEntity = await _securityUserBusinessEntity.CreateAsync(businessEntity, cancellationToken);

								if (createdUserBusinessEntity.UserBusinessEntityId == 0)
									throw new FCCException(ErrorMessage.USER_BUSINESS_ENTITY_SAVE_ERROR);
							}
						}
					}
				}
				else
					response.UserId = Convert.ToInt32(ResponseEnum.NotExists);

				_logger.LogInformation($"CreateSecurityUserCommandHandler End");

				return response;
			}
			else
			{
				return response;
			}
		}
	}
}
