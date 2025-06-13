using LPRSystem.Web.API.Manager.Services.Location;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPRSystem.Web.API.Manager.Services.City
{
   public static class Registrar
    {
        public static void Register(HostBuilderContext context, IServiceCollection services)
        {
            ArgumentNullException.ThrowIfNull(context);
            ArgumentNullException.ThrowIfNull(services);

            //repository
            services.AddTransient<IGetCityRepository, GetCityRepository>();
            services.AddTransient<IGetCityByIdRepository,GetCityByIdRepository>();
            services.AddTransient <IProcessCityRepository, ProcessCityRepository>();


            //Manager

            services.AddTransient<IGetCityManager, GetCityManager>(); 
            services.AddTransient<IGetCityByIdManager, GetCityByIdManager>();
            services.AddTransient<IProcessCityManager, ProcessCityManager>();

            services.AddTransient<IRequestParser<LPRSystem.Web.API.Manager.Models.City.City>, CityProcessRequestParser>();
            services.AddTransient<IRequestParser<LPRSystem.Web.API.Manager.Models.City.GetCityByIdRequest>, GetCityByIdRequestParser>();


        }
    }
}
