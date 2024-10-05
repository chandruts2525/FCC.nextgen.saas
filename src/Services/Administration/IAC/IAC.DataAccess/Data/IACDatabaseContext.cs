using Microsoft.EntityFrameworkCore;
using Softura.EntityFrameworkCore.Abstractions;
using IAC.Domain.Model;
using IAC.Domain.ViewModel.Role;
using IAC.Domain.ViewModel.SecurityUser;
using OrganizationStructure.Domain.Model;

namespace IAC.DataAccess.Sql.Data
{
    public class IACDatabaseContext : DatabaseContext
    {
        public IACDatabaseContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SecurityRole>(entity =>
            {
                entity.ToTable("SecurityRole", "Administration.IdentityAndAccessManagement");
            });

            modelBuilder.Entity<SecurityUserRole>(entity =>
            {
                entity.ToTable("SecurityUserRole", "Administration.IdentityAndAccessManagement");
            });

            modelBuilder.Entity<SecurityRoleAttachment>(entity =>
            {
                entity.ToTable("SecurityRoleAttachment", "Administration.IdentityAndAccessManagement");
            });

            modelBuilder.Entity<SecurityUser>(entity =>
            {
                entity.ToTable("SecurityUser", "Administration.IdentityAndAccessManagement");
            });

            modelBuilder.Entity<SecurityUserBusinessEntity>(entity =>
            {
                entity.ToTable("SecurityUserBusinessEntity", "Administration.IdentityAndAccessManagement");
            });

            modelBuilder.Entity<BusinessEntity>(entity =>
            {
                entity.ToTable("BusinessEntity", "MasterDataManagement.OrganizationStructure");
            });

            modelBuilder.Entity<BusinessEntityType>(entity =>
            {
                entity.ToTable("BusinessEntityType", "MasterDataManagement.OrganizationStructure");
            });

            modelBuilder.Entity<TermModuleEntity>(entity =>
            {
                entity.ToTable("TermModuleEntity", "MasterDataManagement.OrganizationStructure");
            });
            modelBuilder.Entity<BusinessEntityTerm>(entity =>
            {
                entity.ToTable("BusinessEntityTerm", "MasterDataManagement.OrganizationStructure");
            });
        }

        public DbSet<SearchRoleResponseVM>? SearchRoles { get; set; }
        public DbSet<GetRoleByIdResponseVM>? GetRoleById { get; set; }
        public DbSet<GetRoleAttachmentResponseVM>? GetRoleAttachments { get; set; }
        public DbSet<GetAllUserByBusinessEntityResponseVM>? GetAllUserByBusinessEntity { get; set; }
        public DbSet<SecurityUserResponseVM>? SearchSecurityUser { get; set; }
    }
}