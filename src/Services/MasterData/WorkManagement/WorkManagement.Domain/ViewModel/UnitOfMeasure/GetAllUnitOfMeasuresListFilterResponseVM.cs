using System.ComponentModel.DataAnnotations;

namespace WorkManagement.Domain.ViewModel.UnitOfMeasure
{
	public class GetAllUnitOfMeasuresListFilterResponseVM
	{
		public int Count { get; set; }
		public List<GetAllUnitOfMeasuresListResponseVM>? getAllUnitOfMeasuresListFilter { get; set; }
	}
}
