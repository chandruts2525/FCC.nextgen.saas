
using Microsoft.Extensions.DependencyInjection;
using OrganizationStructure.DataAccess.Sql.Data;
using Softura.EntityFrameworkCore.SqlServer;

namespace OrganizationStructure.DataAccess.Sql.DI
{
    public static class IACDataRegistration
    {
        public static IServiceCollection AddIACDatabase(this IServiceCollection services, string? connectionString)
        {
            services.AddSofturaSqlServerRepository();
            services.AddDatabase<OrganizationDatabaseContext>(connectionString);

            return services;
        }
    }
}
