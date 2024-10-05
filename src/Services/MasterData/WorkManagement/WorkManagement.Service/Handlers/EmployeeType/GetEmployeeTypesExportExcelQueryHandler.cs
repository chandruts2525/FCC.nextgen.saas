using WorkManagement.Domain.Model;
using WorkManagement.Service.Queries.EmployeeType;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Softura.EntityFrameworkCore.Abstractions;

namespace WorkManagement.Service.Handlers.EmployeeType
{
    public class GetEmployeeTypesExportExcelQueryHandler : IRequestHandler<GetEmployeeTypesExportExcelQuery, List<EmployeeTypes>>
    {
        private readonly IRepository<EmployeeTypes> _EmployeeRepository;
        private readonly ILogger<GetEmployeeTypesExportExcelQueryHandler> _logger;
        public GetEmployeeTypesExportExcelQueryHandler(IRepository<EmployeeTypes> EmployeeRepository, ILogger<GetEmployeeTypesExportExcelQueryHandler> logger)
        {
            _EmployeeRepository = EmployeeRepository;
            _logger = logger;
        }
        async Task<List<EmployeeTypes>> IRequestHandler<GetEmployeeTypesExportExcelQuery, List<EmployeeTypes>>.Handle(GetEmployeeTypesExportExcelQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Start GetEmployeeTypesExportExcelQueryHandler");
            var result = await _EmployeeRepository.Get().Where(x => !x.IsDeleted).ToListAsync(cancellationToken);
            _logger.LogInformation($"END GetEmployeeTypesExportExcelQueryHandler");
            if (result != null)
                return result;
            else
                return new List<EmployeeTypes>();
        }
    }
}
