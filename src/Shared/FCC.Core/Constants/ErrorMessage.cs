using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCC.Core.Constants
{
	public static class ErrorMessage
	{
		#region GlobalErrorMessage
		public const string NO_PARAMETERS = "Error Occurred. No Input Parameters";
		public const string INVALID_PARAMETERS = "Error Occurred.Invalid Parameters";
		public const string NONEDITABLE = "You Cannot able to edit the Administrator";
		public const string DATABASE_ERROR = "ErrorOccured in Database - Please contact system administrator";
		public const string INTERNAL_SERVER_ERROR = "Error Occurred. Please contact system administrator";
		public const string DATABASE_DOWN = "Error Occurred. Please contact system administrator";
		public const string UNKNOWN_ERROR = "Error Occurred. Please contact system administrator";
		#endregion GlobalErrorMessage

		#region Role
		public const string ROLE_SAVE_ERROR = "Failed to save Role";
		public const string ROLE_UPDATE_ERROR = "Failed to update Role";
		public const string ROLE_ATTACHMENT_SAVE_ERROR = "Failed to Save Role Attachment";
		public const string ROLE_ATTACHMENT_UPDATE_ERROR = "Failed to update Role Attachment";
		public const string ROLE_ATTACHMENT_DELETE_ERROR = "Failed to delete Role Attachment";
		public const string ROLE_USER_SAVE_ERROR = "Failed to save User Role";
		public const string ROLE_USER_UPDATE_ERROR = "Failed to update User Role";
		public const string ROLE_ALREADY_EXIST_ERROR = "Role Name Already Exists";
		#endregion Role

		#region User
		public const string USER_SAVE_ERROR = "Failed to save User";
		public const string USER_UPDATE_ERROR = "Failed to update User";
		public const string USER_BUSINESS_ENTITY_SAVE_ERROR = "Failed to Save User Business Entity";
		public const string USER_ALREADY_EXIST_ERROR_1010 = "User Already Exists";
		#endregion User

		#region UOM
		public const string UOM_SAVE_ERROR = "Failed to Save Unit of Measure";
		public const string UOM_UPDATE_ERROR = "Failed to Update Unit of Measure";
		#endregion UOM

		#region ScheduleType
		public const string SCHEDULETYPE_SAVE_ERROR = "Failed to Save Schedule Type";
		public const string SCHEDULETYPE_UPDATE_ERROR = "Failed to Update Schedule Type";
		#endregion ScheduleType

		#region JobType
		public const string JOBTYPE_SAVE_ERROR = "Failed to save JobType";
		public const string JOBTYPE_UPDATE_ERROR = "Failed to update JobType";
		public const string JOBTYPE_ALREADY_EXIST_ERROR = "JobType Name/Code Already Exists";
		#endregion JobType

		#region Employee
		public const string EMPLOYEETYPE_SAVE_ERROR = "Failed to save EmployeeType";
		public const string EMPLOYEETYPE_UPDATE_ERROR = "Failed to update EmployeeType";
		public const string EMPLOYEETYPE_ALREADY_EXIST_ERROR = "EmployeeTypeName Already Exists";
		#endregion Employee

		#region QuoteFooter
		public const string QUOTE_FOOTER_ALREADY_EXIST_ERROR = "Quote Footer Already Exists";
		public const string QUOTE_FOOTER_SAVE_ERROR = "Failed to Save Quote Footer";
		public const string QUOTEFOOTERS_UPDATE_ERROR = "Failed to update QuoteFooters";
		#endregion QuoteFooter

		#region CommonErrorMessage
		public const string CODE_ALREADY_EXIST_ERROR = "Code already used.Please enter a different code.";
        public const string NAME_ALREADY_EXIST_ERROR = "Name already used.Please enter a different name.";
        #endregion CommonErrorMessage

        #region Organization
        public const string ORGANIZATION_SAVE_ERROR = "Failed to save Organization";
        public const string BUSINESSENTITY_SAVE_ERROR = "Failed to save BusinessEntity";
		public const string ORGANIZATION_ALREADY_EXISTS = "Organization Already Exists";
        public const string BUSINESSENTITY_ALREADY_EXISTS = "BusinessEntity Already Exists";
        public const string NOT_EXISTS = "Record Not Exists";
        public const string ORGANIZATION_UPDATE_ERROR = "Failed to Update Organization";
        public const string BUSINESSENTITY_UPDATE_ERROR = "Failed to Update BusinessEntity";
        public const string BUSINESSENTITYHIERARCHY_UPDATE_ERROR = "Failed to Update BusinessEntityHierarchy";
        public const string BUSINESSENTITYHIERARCHY_SAVE_ERROR = "Failed to Save BusinessEntityHierarchy";
        #endregion

        #region Region
        public const string REGION_SAVE_ERROR = "Failed to save Region";
        #endregion

        #region Yard
        public const string YARD_SAVE_ERROR = "Failed to save Yard";
        public const string YARD_ALREADY_EXIST_ERROR = "Yard Name/Code Already Exists";
        public const string YARD_UPDATE_ERROR = "Failed to update Yard";

        #endregion Yard

        #region BusinessEntity
        public const string BUSINESSENTITYGENERALLEDGER_SAVE_ERROR = "Failed to save BusinessEntityGeneralLedger";
        public const string BUSINESSENTITYGENERALLEDGER_UPDATE_ERROR = "Failed to save BusinessEntityGeneralLedger";
        public const string BUSINESSENTITYADDRESS_SAVE_ERROR = "Failed to save BusinessEntityAddress";
        public const string BUSINESSENTITYADDRESS_UPDATE_ERROR = "Failed to save BusinessEntityAddress";
        public const string BUSINESSENTITYPHONE_SAVE_ERROR = "Failed to save BusinessEntityPhone";
        public const string BUSINESSENTITYPHONE_UPDATE_ERROR = "Failed to update BusinessEntityPhone";
        public const string BUSINESSENTITYTERM_SAVE_ERROR = "Failed to save BusinessEntityTerm";
        public const string BUSINESSENTITYTERM_UPDATE_ERROR = "Failed to update BusinessEntityTerm";
        public const string BUSINESSENTITYCOUNTER_SAVE_ERROR = "Failed to save BusinessEntityCounter";
        public const string BUSINESSENTITYCOUNTER_UPDATE_ERROR = "Failed to update BusinessEntityCounter";
        #endregion BusinessEntity

        #region Address
        public const string ADDRESS_SAVE_ERROR = "Failed to save Address";
        public const string ADDRESS_UPDATE_ERROR = "Failed to update Address";
        #endregion Address

        #region Term
        public const string TERM_SAVE_ERROR = "Failed to save Term";
        public const string TERM_UPDATE_ERROR = "Failed to update Term";
        #endregion Term
        #region Company
        public const string COMPANY_ALREADY_EXIST_ERROR = "Company Name Already Exists";
        public const string Company_SAVE_ERROR = "Failed to save Company";


        #endregion Company
        #region Regions
        public const string REGION_UPDATE_ERROR = "Failed to Update Region";
        public const string REGION_NOT_EXISTS = "Region Name Already Exists";
		public const string REGIONID_NOT_EXISTS = "RegionID NOT Exists";
        #endregion
    }
}

