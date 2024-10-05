using WorkManagement.Domain.Model;
using WorkManagement.Service.Queries.UnitOfMeasure;
using WorkManagement.Service.ViewModel;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Softura.EntityFrameworkCore.Abstractions;

namespace WorkManagement.Service.Handlers.UnitOfMeasure
{
    public class GetAllUnitofMeasureTypesQueryHandler : IRequestHandler<GetAllUnitofMeasureTypesQuery, List<UnitMeasureType>>
    {
        private readonly IRepository<UnitMeasureType> _unitMeasureTypeRepository;
        public GetAllUnitofMeasureTypesQueryHandler(IRepository<UnitMeasureType> unitMeasureTypeRepository)
        {
            _unitMeasureTypeRepository = unitMeasureTypeRepository;
        }
        public async Task<List<UnitMeasureType>> Handle(GetAllUnitofMeasureTypesQuery request, CancellationToken cancellationToken)
        {
            return await _unitMeasureTypeRepository.Get().ToListAsync(cancellationToken);
        }

    }
}
