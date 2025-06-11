using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPRSystem.Web.API.Manager.Services.Location
{
    public static class Registrar
    { 
        public static void Register(HostBuilderContext context, IServiceCollection services) 
        { 
            ArgumentNullException.ThrowIfNull(context);
            ArgumentNullException.ThrowIfNull(services);

            //repository

            services.AddTransient<IGetLocationRepository, GetLocationRepository>();
            services.AddTransient<IProcessLocationRepository, ProcessLocationRepository>();
            services.AddTransient<IGetLocationByIdRepository, GetLocationByIdRepository>();

            //Manager
            services.AddTransient<IGetLocationManager, GetLocationManager>();
            services.AddTransient<IProcessLocationManager, ProcessLocationManager>();
            services.AddTransient<IGetLocationByIdManager, GetLocationByIdManager>();

            services.AddTransient<IRequestParser<LPRSystem.Web.API.Manager.Models.Location.Location>, LocationProcessRequestParser>();
            services.AddTransient<IRequestParser<LPRSystem.Web.API.Manager.Models.Location.GetLocationByIdRequest>, GetLocationByIdRequestParser>();

        }
    }
}
