using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Services = LPRSystem.Web.API.Manager.Services; // Adjusted to point to your Services namespace

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices((context, services) =>
    {
        // Add Application Insights telemetry
        services.AddApplicationInsightsTelemetryWorkerService();

        // Register your services from the LPRSystem.Web.API.Manager.Services namespace
        Services.Registrar.Register(context, services);

        // Configure CORS
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
                builder.WithOrigins("http://localhost:4200") // Allow requests from this origin
                       .AllowAnyMethod() // Allow any HTTP method
                       .AllowAnyHeader() // Allow any HTTP header
            );
        });
    })
    .Build();

// Run the host
host.Run();
