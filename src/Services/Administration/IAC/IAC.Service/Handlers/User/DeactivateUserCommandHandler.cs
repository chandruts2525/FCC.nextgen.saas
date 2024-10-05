using IAC.Domain.Model;
using IAC.Service.Commands.User;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Softura.EntityFrameworkCore.Abstractions;

namespace IAC.Service.Handlers.User
{
	public class DeactivateUserCommandHandler : IRequestHandler<DeactivateUserCommand, SecurityUser>
	{
		private readonly IRepository<SecurityUser> _securityUserRepository;

		private readonly ILogger<DeactivateUserCommandHandler> _logger;

		public DeactivateUserCommandHandler(IRepository<SecurityUser> securityUserRepository, ILogger<DeactivateUserCommandHandler> logger)
		{
			_securityUserRepository = securityUserRepository;
			_logger = logger;
		}

		public async Task<SecurityUser> Handle(DeactivateUserCommand request, CancellationToken cancellationToken)
		{
			var getuser = await _securityUserRepository.Get().Where(p => p.UserId == request.UserViewModel.UserId).FirstOrDefaultAsync(cancellationToken);
			if (getuser != null)
			{
				getuser.Status = request.UserViewModel.Status;
				getuser.IsActive = request.UserViewModel.Status;

				_logger.LogInformation("Start DeactivateUserCommandHandler, Parameters: UserId: {request.UserViewModel.UserId}", request.UserViewModel.UserId);

				var updatedUser = await _securityUserRepository.UpdateAsync(getuser, cancellationToken);

				if (updatedUser != null && updatedUser.IsActive)
					return new SecurityUser() { FirstName = "Successfully Activated User" };
				else if (updatedUser != null && !updatedUser.IsActive)
					return new SecurityUser() { FirstName = "Successfully Deactivated User" };
				else
					return new SecurityUser() { FirstName = "Activate User Failed" };
			}
			else
			{
				return new SecurityUser() { };
			}
		}
	}
}
