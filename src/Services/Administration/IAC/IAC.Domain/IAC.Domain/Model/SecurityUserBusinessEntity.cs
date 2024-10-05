using FCC.Data;
using System.ComponentModel.DataAnnotations;

namespace IAC.Domain.Model
{
	public class SecurityUserBusinessEntity : BaseEntity
	{
        [Key]
		public int UserBusinessEntityId { get; set; }
		public int BusinessEntityId { get; set; }
		public int UserId { get; set; }
	} 
}
