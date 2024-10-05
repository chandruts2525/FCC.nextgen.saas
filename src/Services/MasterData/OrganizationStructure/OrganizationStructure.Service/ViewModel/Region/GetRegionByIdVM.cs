
namespace OrganizationManagementService.Service.ViewModel.Region
{
    public class GetRegionByIdVM
    {
        public int RegionID { get; set; }
        public string? RegionName { get; set; }
        public bool RegionStatus { get; set; }
        public string? RegionDescription { get; set; }
        public List<BusinessEntityVM>? Yards { get; set; }
    }
}
