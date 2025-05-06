using LPRSystem.Web.API.Manager.Models.Role;
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

            //repository
            services.AddTransient<IGetRolesRepository, GetRolesRepository>();
            services.AddTransient<IGetRoleByIdRepository, GetRoleByIdRepository>();
            services.AddTransient<IRoleProcessRepository, RoleProcessRepository>();

            //managers
            services.AddTransient<IGetRolesManager, GetRolesManager>();
            services.AddTransient<IGetRoleByIdManager, GetRoleByIdManager>();
            services.AddTransient<IRoleProcessManager, RoleProcessManager>();

            //parser
            services.AddTransient<IRequestParser<GetRoleByIdRequest>, GetRoleByIdRequestParser>();
        }
    }
}
