using Microsoft.EntityFrameworkCore;
using OrganizationManagementService.Domain.Model;
using OrganizationStructure.Domain.Model;
using Softura.EntityFrameworkCore.Abstractions;
using WorkManagement.Domain.Model;

namespace OrganizationStructure.DataAccess.Sql.Data
{
    public class OrganizationDatabaseContext : DatabaseContext
    {
        public OrganizationDatabaseContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Organization>(entity =>
            {
                entity.ToTable("Organization", "MasterDataManagement.OrganizationStructure");
            });
            modelBuilder.Entity<Yard>(entity =>
            {
                entity.ToTable("Yard", "MasterDataManagement.OrganizationStructure");
            });
            modelBuilder.Entity<BusinessEntity>(entity =>
            {
                entity.ToTable("BusinessEntity", "MasterDataManagement.OrganizationStructure");
            });
            modelBuilder.Entity<BusinessEntityType>(entity =>
            {
                entity.ToTable("BusinessEntityType", "MasterDataManagement.OrganizationStructure");
            });
            modelBuilder.Entity<BusinessEntityHierarchy>(entity =>
            {
                entity.ToTable("BusinessEntityHierarchy", "MasterDataManagement.OrganizationStructure");
            }); 
            modelBuilder.Entity<Address>(entity =>
            {
                entity.ToTable("Address", "MasterDataManagement.Core");
            });
            modelBuilder.Entity<AddressType>(entity =>
            {
                entity.ToTable("AddressType", "MasterDataManagement.Core");
            });
            modelBuilder.Entity<BusinessEntityAddress>(entity =>
            {
                entity.ToTable("BusinessEntityAddress", "MasterDataManagement.Core");
            });
            modelBuilder.Entity<BusinessEntityAddress>().HasKey(be => new { be.BusinessEntityId, be.AddressId });

            modelBuilder.Entity<BusinessEntityPhone>(entity =>
            {
                entity.ToTable("BusinessEntityPhone", "MasterDataManagement.Core");
            });
            modelBuilder.Entity<BusinessEntityPhone>().HasKey(be => new { be.BusinessEntityId, be.PhoneNumberTypeId });
            modelBuilder.Entity<ContactType>(entity =>
            {
                entity.ToTable("ContactType", "MasterDataManagement.Core");
            });
            modelBuilder.Entity<StateProvince>(entity =>
            {
                entity.ToTable("StateProvince", "MasterDataManagement.Core");
            });
            modelBuilder.Entity<BusinessEntityCategory>(entity =>
            {
                entity.ToTable("BusinessEntityCategory", "MasterDataManagement.OrganizationStructure");
            });
             
            modelBuilder.Entity<BusinessEntityContact>(entity =>
            {
                entity.ToTable("BusinessEntityContact", "MasterDataManagement.OrganizationStructure");
            });
            modelBuilder.Entity<BusinessEntityCounter>(entity =>
            {
                entity.ToTable("BusinessEntityCounter", "MasterDataManagement.OrganizationStructure");
            }); 
            modelBuilder.Entity<BusinessEntityIntegration>(entity =>
            {
                entity.ToTable("BusinessEntityIntegration", "MasterDataManagement.OrganizationStructure");
            });
            modelBuilder.Entity<BusinessEntityTerm>(entity =>
            {
                entity.ToTable("BusinessEntityTerm", "MasterDataManagement.OrganizationStructure");
            });
            modelBuilder.Entity<Company>(entity =>
            {
                entity.ToTable("Company", "MasterDataManagement.OrganizationStructure");
            });
            modelBuilder.Entity<CompanyDomain>(entity =>
            {
                entity.ToTable("CompanyDomain", "MasterDataManagement.OrganizationStructure");
            });
            modelBuilder.Entity<CounterCategory>(entity =>
            {
                entity.ToTable("CounterCategory", "MasterDataManagement.OrganizationStructure");
            });
            modelBuilder.Entity<ExternalIntegrationMetaData>(entity =>
            {
                entity.ToTable("ExternalIntegrationMetaData", "MasterDataManagement.OrganizationStructure");
            });
            modelBuilder.Entity<ExternalIntegrationVendor>(entity =>
            {
                entity.ToTable("ExternalIntegrationVendor", "MasterDataManagement.OrganizationStructure");
            });
            modelBuilder.Entity<PhoneNumberType>(entity =>
            {
                entity.ToTable("PhoneNumberType", "MasterDataManagement.Core");
            });
            modelBuilder.Entity<GeneralLedger>(entity =>
            {
                entity.ToTable("GeneralLedger", "Operations.FinancialManagement");
            });
            modelBuilder.Entity<BusinessEntityGeneralLedger>(entity =>
            {
                entity.ToTable("BusinessEntityGeneralLedger", "MasterDataManagement.OrganizationStructure");
            });  
            modelBuilder.Entity<Terms>(entity =>
            {
                entity.ToTable("Term", "MasterDataManagement.WorkManagement");
            });
            modelBuilder.Entity<BusinessEntityCounter>(entity =>
            {
                entity.ToTable("BusinessEntityCounter", "MasterDataManagement.OrganizationStructure");
            });
            modelBuilder.Entity<BusinessEntityCounter>().HasKey(be => new { be.BusinessEntityId, be.CounterCategoryId });
            modelBuilder.Entity<Country>(entity =>
            {
                entity.ToTable("Country", "MasterDataManagement.Core");
            });
            modelBuilder.Entity<MeasurementType>(entity =>
            {
                entity.ToTable("MeasurementType", "MasterDataManagement.Core");
            });
            modelBuilder.Entity<Currency>(entity =>
            {
                entity.ToTable("Currency", "MasterDataManagement.Core");
            });
            modelBuilder.Entity<Language>(entity =>
            {
                entity.ToTable("Language", "MasterDataManagement.Core");
            }); 
        }
    }
}
