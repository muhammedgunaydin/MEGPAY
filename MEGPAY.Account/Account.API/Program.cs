using Account.Domain.AccountAggregate;
using Account.Infrastructure;
using Account.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Account.Application.Accounts.Commands.CreateAccount;
using Account.Domain.SeedWork;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediatR(cfg => { cfg.RegisterServicesFromAssembly(typeof(CreateAccountCommandHandler).Assembly); });

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IDbContextHandler, DbContextHandler>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new()
    {
        Title = "My API",
        Version = "v1",
        Description = "Bu API .NET 8 ile geliştirilmiştir."
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options => { options.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"); });
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();