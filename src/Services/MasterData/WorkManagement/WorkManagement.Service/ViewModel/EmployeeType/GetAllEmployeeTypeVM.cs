namespace WorkManagement.Service.ViewModel.EmployeeType
{
    public class GetAllEmployeeTypeVM
    {
        public GetAllEmployeeTypeVM()
        {
            getAllEmployeeTypes = new List<GetAllEmployeeType>();
        }
        public int Count { get; set; }
        public List<GetAllEmployeeType>? getAllEmployeeTypes { get; set; }

    }

    public class GetAllEmployeeType
    {
        public int EmployeeTypeId { get; set; }
        public string? EmployeeTypeName { get; set; }
        public bool IsActive { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedDateUTC { get; set; }
        public DateTime? ModifiedDateUTC { get; set; }
        public string? ModifiedBy { get; set; }
    }
}
