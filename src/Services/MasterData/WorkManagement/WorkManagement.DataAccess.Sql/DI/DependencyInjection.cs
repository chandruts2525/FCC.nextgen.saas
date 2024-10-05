using Microsoft.Extensions.DependencyInjection;
using Softura.EntityFrameworkCore.SqlServer;
using WorkManagement.DataAccess.Sql.Data;

namespace WorkManagement.DataAccess.Sql.DI
{
	public static class IACDataRegistration
	{
		public static IServiceCollection AddIACDatabase(this IServiceCollection services, string? connectionString)
		{
			services.AddSofturaSqlServerRepository();
			services.AddDatabase<WorkManagementDatabaseContext>(connectionString);

			return services;
		}
	}
}
