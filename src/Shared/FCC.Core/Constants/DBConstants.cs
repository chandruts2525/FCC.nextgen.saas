using System.ComponentModel;

namespace FCC.Core.Constants
{
	public static class DBConstants
	{
		public const string VW_SEARCH_ROLE = "VW_RoleInformation";
		public const string VW_GET_ROLEBYROLEID = "VW_GetRoleByRoleId";
		public const string VW_GETALL_UNITOFMEASURE = "VW_GetAllUnitOfMeasure";
		public const string VW_GETALL_QUOTEFOOTER = "VW_GetAllQuoteFooters";
		public const string VW_GETALL_USERLISTFORROLE = "VW_UserListForRole";
	}

	public enum RoleFilter
	{
		[Description("RoleName")]
		roleName,
		[Description("AssignedUser")]
		assignedUser,
	}

	public enum UOMFilter
	{
		[Description("UnitMeasureCode")]
		unitMeasureCode,
		[Description("UnitMeasureDisplayValue")]
		unitMeasureDisplayValue,
		[Description("UnitMeasureTypeDescription")]
		unitMeasureTypeDescription,
		[Description("ConversionFactor")]
		conversionFactor,
		[Description("IsActive")]
		isActive,
		[Description("CreatedBy")]
		createdBy,
		[Description("CreatedDateUTC")]
		createdDateUTC,
		[Description("ModifiedBy")]
		modifiedBy,
		[Description("ModifiedDateUTC")]
		modifiedDateUTC,
	}

	public enum QuoteFooterFilter
	{
        [Description("QuoteFooterName")]
        quotefootername,
        [Description("Company Count")]
        companycount,
        [Description("Module Count")]
        modulecount,
        [Description("Status")]
        status,
        [Description("Modified Date")]
        modifieddate,
        [Description("Created Date")]
        createddate,
        [Description("Created By")]
        createdby,
        [Description("Modified By")]
        modifiedby
    }
}
