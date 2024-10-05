using IAC.DataAccess.Sql.Data;
using Microsoft.Extensions.DependencyInjection;
using Softura.EntityFrameworkCore.SqlServer;

namespace IAC.DataAccess.Sql.DI
{
	public static class IacDataRegistration
	{
		public static IServiceCollection AddIACDatabase(this IServiceCollection services, string? connectionString)
		{
			services.AddSofturaSqlServerRepository();
			services.AddDatabase<IACDatabaseContext>(connectionString);

			return services;
		}
	}
}
