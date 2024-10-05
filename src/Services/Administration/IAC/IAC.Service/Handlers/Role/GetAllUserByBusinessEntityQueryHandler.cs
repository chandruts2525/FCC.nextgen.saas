using FCC.Core.Constants;
using IAC.Domain.ViewModel.Role;
using IAC.Service.Queries.Role;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Softura.EntityFrameworkCore.Abstractions;

namespace IAC.Service.Handlers.Role
{
	public class GetAllUserByBusinessEntityQueryHandler : IRequestHandler<GetAllUserByBusinessEntityQuery, List<GetAllUserListFilterResponseVM>>
	{
		private readonly IRepository<GetAllUserByBusinessEntityResponseVM> _iRepository;
		private readonly ILogger<GetAllUserByBusinessEntityQueryHandler> _logger;
		public GetAllUserByBusinessEntityQueryHandler(IRepository<GetAllUserByBusinessEntityResponseVM> iRepository,
			ILogger<GetAllUserByBusinessEntityQueryHandler> logger)
		{
			_iRepository = iRepository;
			_logger = logger;
		}
		async Task<List<GetAllUserListFilterResponseVM>> IRequestHandler<GetAllUserByBusinessEntityQuery, List<GetAllUserListFilterResponseVM>>.Handle(GetAllUserByBusinessEntityQuery request, CancellationToken cancellationToken)
		{
			_logger.LogInformation("Start GetAllUserResponseViewModel, Parameters: RoleID: {request.getAllUserByBusinessEntityVM.RoleId}", request.getAllUserByBusinessEntityVM.RoleId);

			var result = await _iRepository.GetFromCommand("SELECT * FROM " + DBConstants.VW_GETALL_USERLISTFORROLE).ToListAsync();

			Predicate<GetAllUserByBusinessEntityResponseVM> userbusinessentity;

			if (request.getAllUserByBusinessEntityVM.RoleId != 0)
			{
				var getuser = result.Where(x => x.RoleId == request.getAllUserByBusinessEntityVM.RoleId).ToList();
				result = result.Where(a => !getuser.Any(b => b.UserId == a.UserId)).ToList();
			}

			if (request.getAllUserByBusinessEntityVM.BusinessEntityTypeIds != "")
			{
				userbusinessentity = x => x.BusinessEntityTypeId.ToString()!.Contains(request.getAllUserByBusinessEntityVM.BusinessEntityTypeIds!.ToString());
				result = result.FindAll(userbusinessentity).ToList();
			}

			if (request.getAllUserByBusinessEntityVM.BusinessEntityIds != "")
			{
				var ids = request.getAllUserByBusinessEntityVM.BusinessEntityIds?.Split(',');
				if(ids!=null)
				{
                    result = result.Where(x => ids.Contains(x.BusinessEntityId.ToString())).ToList();
                }
			}

			List<GetAllUserListFilterResponseVM> getAllUserListFilterResponseVM;

			getAllUserListFilterResponseVM = result.Select(x => new GetAllUserListFilterResponseVM()
			{
				UserId = x.UserId,
				UserName = x.UserName,
				UserRole = x.UserRole,
				IsActive = x.IsActive,
			}).DistinctBy(s => s.UserId).ToList();

			_logger.LogInformation($"GetAllUserByBusinessEntityQueryHandler End");

            return getAllUserListFilterResponseVM;
        }
	}
}
