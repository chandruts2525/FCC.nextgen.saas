using System.ComponentModel.DataAnnotations;

namespace IAC.Domain.ViewModel.Role
{
	public class SearchRoleFilterResponseVM
	{
		public SearchRoleFilterResponseVM()
		{
			searchRoleFilerResponse = new List<SearchRoleResponseVM>();
		}
		public int? Count { get; set; }
		public List<SearchRoleResponseVM> searchRoleFilerResponse { get; set; }
	}
}
