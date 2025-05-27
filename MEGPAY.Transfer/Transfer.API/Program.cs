using MassTransit;
using Transfer.Domain.TransferAggregate;
using Transfer.Infrastructure;
using Transfer.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Transfer.Application.Interfaces;
using Transfer.Application.Transfers.Command.CreateTransfer;
using Transfer.Domain.SeedWork;
using Transfer.Infrastructure.Consumers;
using Transfer.Infrastructure.Saga;
using Transfer.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(CreateTransferCommand).Assembly);
});

builder.Services.AddMassTransit(x =>
{
    builder.Services.AddScoped<IPublishEndpoint>(provider =>
    {
        var bus = provider.GetRequiredService<IBusControl>();
        return bus;
    });
    
    x.AddConsumer<TransferCompletedEventConsumer>();
    x.AddConsumer<TransferFailedEventConsumer>();
    x.AddConsumer<TransferStartedEventConsumer>();
    
    x.AddSagaStateMachine<TransferStateMachine, TransferState>()
        .EntityFrameworkRepository(cfg =>
        {
            cfg.ConcurrencyMode = ConcurrencyMode.Pessimistic;

            cfg.AddDbContext<DbContext, AppDbContext>((provider, options) =>
            {
                var configuration = provider.GetRequiredService<IConfiguration>();
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
            });
            cfg.UsePostgres();
        });

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("amqp://guest:guest@localhost:5672");

        cfg.ReceiveEndpoint("transfer-completed-event-queue", e =>
        {
            e.ConfigureConsumer<TransferCompletedEventConsumer>(context);
        });
        
        cfg.ReceiveEndpoint("transfer-failed-event-queue", e =>
        {
            e.ConfigureConsumer<TransferFailedEventConsumer>(context);
        });
        
        cfg.ReceiveEndpoint("transfer-started-event-queue", e =>
        {
            e.ConfigureConsumer<TransferStartedEventConsumer>(context);
        });
        cfg.ConfigureEndpoints(context);
    });
});

builder.Services.AddScoped<ITransferStatusService, TransferStatusService>();

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<AppDbContext>());
builder.Services.AddScoped<IDbContextHandler, DbContextHandler>();
builder.Services.AddScoped<ITransferRepository, TransferRepository>();

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