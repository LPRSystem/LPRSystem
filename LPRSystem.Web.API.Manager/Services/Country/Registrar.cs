using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPRSystem.Web.API.Manager.Services.Country
{
   public static class Registrar
    {
        public static void Register(HostBuilderContext context, IServiceCollection services) 
        {
            ArgumentNullException.ThrowIfNull(context);
            ArgumentNullException.ThrowIfNull(services);

            //repository
            services.AddTransient<IGetCountryRepository, GetCountryRepository>();


            //Manager
            services.AddTransient<IGetCountryManager, GetCountryManager>();
        }
    }
}
