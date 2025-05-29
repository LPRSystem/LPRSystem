using LPRSystem.Web.API.Manager.Models.User;
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
            services.AddTransient<IGetUsersRepository, GetUsersRepository>();
            services.AddTransient<IGetUserByIdRepository, GetUserByIdRepository>();
            services.AddTransient<IProgressUserRepository, ProgressUserRepository>();

            services.AddTransient<IGetUserByIdManager, GetUserByIdManager>();
            services.AddTransient<IGetUsersManager, GetUsersManager>();
            services.AddTransient<IProgressUserDataManager, ProgressUserDataManager>();
            services.AddTransient<IRequestParser<GetUserByIdRequest>, GetUserByIdRequestParser>();
            
        }
    }
}
