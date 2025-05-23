using Transfer.Domain.TransferAggregate;
using Transfer.Infrastructure;
using Transfer.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Transfer.Application.Transfers.Command.CreateTransfer;
using Transfer.Domain.SeedWork;
using MassTransit;
using Transfer.Application.Transfers.Interfaces;
using Transfer.Infrastructure.Messaging.Producers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(CreateTransferCommand).Assembly);
});

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IDbContextHandler, DbContextHandler>();
builder.Services.AddScoped<ITransferRepository, TransferRepository>();

builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });
    });
});
builder.Services.AddScoped<ITransferEventPublisher, TransferInitiatedEventPublisher>();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new()
    {
        Title = "Transfer.API",
        Version = "v1",
        Description = "wrok"
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options => { options.SwaggerEndpoint("/swagger/v1/swagger.json", "Transfer.API v1"); });
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();