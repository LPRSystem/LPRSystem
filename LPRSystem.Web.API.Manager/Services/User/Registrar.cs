using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LPRSystem.Web.API.Manager.Services.User
{
    public static class Registrar
    {
        public static void Register(HostBuilderContext context, IServiceCollection services)
        {
            ArgumentNullException.ThrowIfNull(context);
            ArgumentNullException.ThrowIfNull(services);
        }
    }
}
