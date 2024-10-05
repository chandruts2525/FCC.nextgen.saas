namespace WorkManagement.Service.ViewModel.JobType
{
    public class GetAllJobTypeVM
    {
        public int Count { get; set; }
        public List<GetAllJobType>? JobTypes { get; set; }
    }

    public class GetAllJobType
    {
        public int JobTypeId { get; set; }
        public string? JobTypeCode { get; set; }
        public string? JobTypeName { get; set; }
        public bool isActive { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedDateUTC { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDateUTC { get; set; }
    }
}
