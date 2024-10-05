using IAC.Domain.ViewModel.SecurityUser;
using MediatR;

namespace IAC.Service.Queries.User
{
    public class GetBusinessEntityQuery : IRequest<List<BusinessEntityResponseModel>>
    {
        public BusinessEntityResponseModel BusinessViewModel { get; set; }
        public GetBusinessEntityQuery(BusinessEntityResponseModel businessViewModel)
        {
            BusinessViewModel = businessViewModel;
        }
    }
}