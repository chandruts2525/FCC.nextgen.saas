using Microsoft.EntityFrameworkCore;
using OrganizationStructure.Domain.Model;
using Softura.EntityFrameworkCore.Abstractions;
using WorkManagement.Domain.Model;
using WorkManagement.Domain.ViewModel.QuoteFooter;
using WorkManagement.Domain.ViewModel.UnitOfMeasure;

namespace WorkManagement.DataAccess.Sql.Data
{
	public class WorkManagementDatabaseContext : DatabaseContext
	{
		public WorkManagementDatabaseContext(DbContextOptions options) : base(options)
		{

		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UnitMeasure>(entity =>
            {
                entity.ToTable("UnitMeasure", "MasterDataManagement.WorkManagement");
            });
            modelBuilder.Entity<UnitMeasureType>(entity =>
            {
                entity.ToTable("UnitMeasureType", "MasterDataManagement.WorkManagement");
            });
            modelBuilder.Entity<Terms>(entity =>
            {
                entity.ToTable("Term", "MasterDataManagement.WorkManagement");
            });
           
            modelBuilder.Entity<TermType>(entity =>
            {
                entity.ToTable("TermType", "MasterDataManagement.WorkManagement");
            });

            modelBuilder.Entity<Modules>(entity =>
            {
                entity.ToTable("Module", "MasterDataManagement.WorkManagement");
            });

            modelBuilder.Entity<JobTypes>(entity =>
            {
                entity.ToTable("JobType", "MasterDataManagement.WorkManagement");
            });

            modelBuilder.Entity<EmployeeTypes>(entity =>
            {
                entity.ToTable("EmployeeTypes", "MasterDataManagement.WorkManagement");
            });
            
            modelBuilder.Entity<ScheduleType>(entity =>
            {
                entity.ToTable("ScheduleType", "MasterDataManagement.WorkManagement");
            });
            modelBuilder.Entity<ScheduleTypeComponents>(entity =>
            {
                entity.ToTable("ScheduleTypeComponents", "MasterDataManagement.WorkManagement");
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

		public DbSet<GetAllUnitOfMeasuresListResponseVM>? AllUnitOfMeasures { get; set; }
		public DbSet<GetAllQuoteFooter>? getAllQuoteFooters { get; set; }
	}
}