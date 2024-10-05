using WorkManagement.Domain.Model;
using WorkManagement.Service.Queries.SchdeuleType;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Softura.EntityFrameworkCore.Abstractions;
namespace WorkManagement.Service.Handlers
{
    public class GetUnitorComponentQueryHandler : IRequestHandler<GetUnitorComponentQuery, List<ScheduleTypeComponents>>
    {
        private readonly IRepository<ScheduleTypeComponents> _iRepository;
        public GetUnitorComponentQueryHandler(IRepository<ScheduleTypeComponents> iRepository)
        {
            _iRepository = iRepository;
        }
        async Task<List<ScheduleTypeComponents>> IRequestHandler<GetUnitorComponentQuery, List<ScheduleTypeComponents>>.Handle(GetUnitorComponentQuery request, CancellationToken cancellationToken)
        {
            var result = await _iRepository.Get().ToListAsync(cancellationToken);
            if (result != null)
                return result;
            else
                return new List<ScheduleTypeComponents>();
        }
    }
}