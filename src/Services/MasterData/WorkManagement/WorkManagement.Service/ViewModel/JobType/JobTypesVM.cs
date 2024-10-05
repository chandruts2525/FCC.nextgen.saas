namespace WorkManagement.Service.ViewModel.JobType
{
	public class JobTypesVM
    {
        public int JobTypeId { get; set; }
        public string? JobTypeCode { get; set; }
        public string? JobTypeName { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
