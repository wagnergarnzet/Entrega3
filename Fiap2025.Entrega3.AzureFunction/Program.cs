using Fiap2025.Entrega3.Domain.Interfaces;
using Fiap2025.Entrega3.Infrastructure;
using Fiap2025.Entrega3.Infrastructure.Repositories;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();
builder.Services.AddSingleton<RabbitMQConnection>(provider =>
{
    var hostname = "localhost";
    var username = "guest";
    var password = "guest";
    return new RabbitMQConnection(hostname, username, password);
});


var configuration = new ConfigurationBuilder()
    .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables()
    .Build();

var rabbitMQConnection = builder.Services.BuildServiceProvider().GetService<RabbitMQConnection>();

var connectionString = configuration.GetConnectionString("DefaultConnection");

builder.Services.AddSingleton<IContatoRepository>(provider => new ContatoRepository(rabbitMQConnection, configuration));
                    

// Application Insights isn't enabled by default. See https://aka.ms/AAt8mw4.
// builder.Services
//     .AddApplicationInsightsTelemetryWorkerService()
//     .ConfigureFunctionsApplicationInsights();

builder.Build().Run();
