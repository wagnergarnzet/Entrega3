using Fiap2025.Entrega3.Domain.Interfaces;
using Fiap2025.Entrega3.Infrastructure;
using Fiap2025.Entrega3.Infrastructure.Repositories;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();

// Configuração do RabbitMQConnection
builder.Services.AddSingleton(provider =>
{
    var hostname = "20.242.177.148";
    var username = "guest";
    var password = "guest";
    return new RabbitMQConnection(hostname, username, password);
});

// Configuração do IConfiguration
builder.Services.AddSingleton<IConfiguration>(provider =>
{
    return new ConfigurationBuilder()
        .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
        .AddEnvironmentVariables()
        .Build();
});

// Configuração do IContatoRepository
builder.Services.AddSingleton<IContatoRepository>(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    var rabbitMQConnection = provider.GetRequiredService<RabbitMQConnection>();

    var connectionString = configuration["DefaultConnection"];
    if (string.IsNullOrEmpty(connectionString))
    {
        throw new InvalidOperationException("Connection String incorreta");
    }

    return new ContatoRepository(rabbitMQConnection, configuration);
});

builder.Build().Run();