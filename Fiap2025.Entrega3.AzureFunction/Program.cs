using Fiap2025.Entrega3.Domain.Interfaces;
using Fiap2025.Entrega3.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices((context, services) =>
    {
        services.AddScoped<IContatoRepository, ContatoRepository>();

        // Add other services and configurations here
    })
    .Build();

host.Run();