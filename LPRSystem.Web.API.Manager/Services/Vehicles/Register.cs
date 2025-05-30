using LPRSystem.Web.API.Manager.Models.Vehicles;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPRSystem.Web.API.Manager.Services.Vehicles
{
    public static class Register
    {
        public static void Registrer(HostBuilderContext context ,IServiceCollection services) 
        {
            ArgumentNullException.ThrowIfNull(context);
            ArgumentNullException.ThrowIfNull(services);

            //repository
            services.AddTransient<IGetVehiclesRepository, GetVehiclesRepository>();
            services.AddTransient<IGetVehiclesByIdRepository,GetVehiclesByIdRepository>();
            services.AddTransient<IVehiclesProcessRepository,VehiclesProcessRepository>();

            //managers
            services.AddTransient<IGetVehiclesManager, GetVehiclesManager>();
            services.AddTransient<IGetVehiclesByIdRepository, GetVehiclesByIdRepository>();
            services.AddTransient<IVehiclesProcessRepository, VehiclesProcessRepository>();

            //parser
            services.AddTransient<IRequestParser<GetVehiclesByIdRequest>, GetVehiclesByIdRequestParser>();
        }
    }
}
