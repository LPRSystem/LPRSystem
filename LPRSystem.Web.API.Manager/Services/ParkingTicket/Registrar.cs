using LPRSystem.Web.API.Manager.Models.ParkingTicket;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LPRSystem.Web.API.Manager.Services.ParkingTicket
{
    public static class Registrar
    {
        public static void Register(HostBuilderContext context, IServiceCollection services)
        {
            ArgumentNullException.ThrowIfNull(context);
            ArgumentNullException.ThrowIfNull(services);

            //Manager
            services.AddTransient<IGetParkingTicketsManager, GetParkingTicketsManager>();
            services.AddTransient<IGetAllParkingTicketsByATMManager, GetAllParkingTicketsByATMManager>();
            services.AddTransient<IGetAllParkingTicketByATMIdParkedOnManager,GetAllParkingTicketByATMIdParkedOnManager>();

            //Repository
            services.AddTransient<IGetParkingTicketsRepository, GetParkingTicketsRepository>();
            services.AddTransient<IGetAllParkingTicketsByATMRepository, GetAllParkingTicketsByATMRepository>();
            services.AddTransient<IGetAllParkingTicketByATMIdParkedOnRepository, GetAllParkingTicketByATMIdParkedOnRepository>();




            services.AddTransient<IRequestParser<LPRSystem.Web.API.Manager.Models.ParkingTicket.ParkingTicket>, ParkingTicketProcessRequestParser>();
            services.AddTransient<IRequestParser<LPRSystem.Web.API.Manager.Models.ParkingTicket.GetParkingTicketByATMIdRequest>, GetParkingTicketByIATMdRequestParser>();
            services.AddTransient<IRequestParser<LPRSystem.Web.API.Manager.Models.ParkingTicket.GetAllParkingTicketByATMIdParkedOnRequest>, GetParkingTicketByIATMdParkedOnRequestParser>();





        }
    }
}
