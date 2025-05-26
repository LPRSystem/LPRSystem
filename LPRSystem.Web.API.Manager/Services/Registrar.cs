using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LPRSystem.Web.API.Manager.Services
{
    public static class Registrar
    {
        public static void Register(HostBuilderContext context, IServiceCollection services)
        {
            ArgumentNullException.ThrowIfNull(context);
            ArgumentNullException.ThrowIfNull(services);


            Role.Registrar.Register(context, services);
            User.Registrar.Register(context, services);
            Organization.Registrar.Register(context, services);
        }
    }
}
