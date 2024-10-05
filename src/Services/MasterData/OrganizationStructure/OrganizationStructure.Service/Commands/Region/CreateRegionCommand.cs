using MediatR;
using OrganizationManagementService.Domain.Model;
using OrganizationManagementService.Service.ViewModel.Region;
using OrganizationStructure.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationManagementService.Service.Commands.Region
{
    public class CreateRegionCommand : IRequest<BusinessEntity>
    {
        public RegionRequestVM RegionRequestVM { get; set; }
        public CreateRegionCommand(RegionRequestVM _regionRequestVM)
        {
            RegionRequestVM = _regionRequestVM;
        }
    }
}
