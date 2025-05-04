using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LPRSystem.Web.API.Manager.Services.Role
{
    public static class Registrar
    {
        public static void Register(HostBuilderContext context, IServiceCollection services)
        {
            ArgumentNullException.ThrowIfNull(context);
            ArgumentNullException.ThrowIfNull(services);

            services.AddTransient<IGetRolesRepository, GetRolesRepository>();
            services.AddTransient<IGetRolesManager, GetRolesManager>();
        }
    }
}
