using Fiap2025.Entrega3.Application.Handlers;
using Fiap2025.Entrega3.Domain.Interfaces;
using Fiap2025.Entrega3.Infrastructure;
using Fiap2025.Entrega3.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSingleton(sp => new RabbitMQConnection("localhost", "guest", "guest"));
builder.Services.AddScoped<IContatoRepository, ContatoRepository>();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(AddContatoHandler).Assembly)); ;

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
