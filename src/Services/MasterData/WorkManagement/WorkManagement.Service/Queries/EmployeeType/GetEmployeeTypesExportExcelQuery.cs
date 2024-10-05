using WorkManagement.Domain.Model;
using MediatR;


namespace WorkManagement.Service.Queries.EmployeeType
{
    public class GetEmployeeTypesExportExcelQuery : IRequest<List<EmployeeTypes>>
    {

    }
}
