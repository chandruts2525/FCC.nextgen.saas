using FCC.Core.Constants;
using IAC.Domain.ViewModel.Role;
using IAC.Service.Queries.Role;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Softura.EntityFrameworkCore.Abstractions;

namespace IAC.Service.Handlers.Role
{
	public class GetRoleByIdQueryHandler : IRequestHandler<GetRoleByIdQuery, List<GetRoleByIdResponseVM>>
    {
        private readonly IRepository<GetRoleByIdResponseVM> _iRepository;
        private readonly ILogger<GetRoleByIdQueryHandler> _logger;

        public GetRoleByIdQueryHandler(IRepository<GetRoleByIdResponseVM> iRepository, ILogger<GetRoleByIdQueryHandler> logger)
        {
           _iRepository = iRepository;
            _logger = logger;
        }

		async Task<List<GetRoleByIdResponseVM>> IRequestHandler<GetRoleByIdQuery, List<GetRoleByIdResponseVM>>.Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
		{


			if (request.getRoleByIdResponseVM.RoleId != 0)
			{
				_logger.LogInformation($"Start GetSecurityRoleByIdQueryHandler, Parameters: RoleID: {request.getRoleByIdResponseVM.RoleId}");

				var result =await _iRepository.GetFromCommand("SELECT * FROM " + DBConstants.VW_GET_ROLEBYROLEID).ToListAsync();

				result = result.Where(x => x.RoleId == request.getRoleByIdResponseVM.RoleId).ToList();

				_logger.LogInformation($"GetSecurityRoleByIdQueryHandler End");

                return result;
            }
            else
            {
                return new List<GetRoleByIdResponseVM>();
            }
        }
    }
}
