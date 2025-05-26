using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LPRSystem.Web.API.Manager.Services.Organization
{
    public static class Registrar
    {
        public static void Register(HostBuilderContext context, IServiceCollection services)
        {
            ArgumentNullException.ThrowIfNull(context);
            ArgumentNullException.ThrowIfNull(services);

            //repository
            services.AddTransient<IGetOrganizationsRepository, GetOrganizationsRepository>();
            services.AddTransient<IProcessOrganizationRepository, ProcessOrganizationRepository>();

            //managers
            services.AddTransient<IGetOrganizationsManager, GetOrganizationsDataManager>();
            services.AddTransient<IProcessOrganizationManager, ProcessOrganizationDataManager>();

            //parser
            //services.AddTransient<IRequestParser<GetRoleByIdRequest>, GetRoleByIdRequestParser>();
        }
    }
}
