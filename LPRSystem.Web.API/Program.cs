using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Services = LPRSystem.Web.API.Manager.Services;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices((context, services) =>
    {

        services.AddApplicationInsightsTelemetryWorkerService();

        Services.Registrar.Register(context, services);

        services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
                builder.WithOrigins("http://localhost:4200")
                       .AllowAnyMethod()
                       .AllowAnyHeader()
            );
        });
    })
    .Build();

host.Run();