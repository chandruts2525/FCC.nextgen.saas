using FCC.Data;
using System.ComponentModel.DataAnnotations;

namespace WorkManagement.Domain.Model
{
    public class EmployeeTypes : MasterBaseEntity
    {
        [Key]
        public int EmployeeTypeId { get; set; }
        public string? EmployeeTypeName { get; set; }
    }
}
