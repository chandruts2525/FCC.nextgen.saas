﻿namespace OrganizationManagementService.Service.ViewModel.Region
{
    public class RegionRequestVM
    {
        public int? ParentBusinessEntityId { get; set; } 
        public int? RegionID { get; set; }
        public string? RegionName { get; set; }
        public bool RegionStatus { get; set; }
        public string? RegionDescription { get; set; }
        public string? RegionYardsList { get; set; } 

    }
}
