using MediatR;
using OrganizationManagementService.Service.ViewModel.Region;

namespace OrganizationManagementService.Service.Commands.Region
{
    public class UpdateRegionCommand:IRequest<RegionRequestVM>
    {
        public RegionRequestVM Region { get; set; }
        public UpdateRegionCommand(RegionRequestVM _region) { 
            Region = _region;
        }
    }
}
