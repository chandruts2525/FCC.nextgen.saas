using FCC.Core.ViewModel.GridFilter;
using WorkManagement.Domain.ViewModel.UnitOfMeasure;
using MediatR;

namespace WorkManagement.Service.Queries.UnitOfMeasure
{
    public class GetAllUnitOfMeasuresQuery : IRequest<GetAllUnitOfMeasuresListFilterResponseVM>
    {
        public GridFilter GridFilter { get; set; }
        public GetAllUnitOfMeasuresQuery(GridFilter gridFilter)
        {
            GridFilter = gridFilter;
        }
    }

}
