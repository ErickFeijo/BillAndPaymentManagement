global using RegisterServiceAPI.Data;
global using Microsoft.EntityFrameworkCore;
using RegisterServiceAPI.Data.Repositories;
using RegisterServiceAPI.Data.Repositories.Interfaces;
using RegisterServiceAPI.Messaging.Interfaces;
using RegisterServiceAPI.Model;
using RegisterServiceAPI.Service;
using RegisterServiceAPI.Service.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseMySQL(GetMySQLConfig(builder));
});

builder.Services.AddSingleton((Func<IServiceProvider, IMessaging>)(provider =>
{
    RabbitMQConfig rabbitMQConfig = GetRabbitMQConfig(builder);
    return new RabbitMQService(rabbitMQConfig);
}));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<DbContext, DataContext>();
builder.Services.AddScoped(typeof(IBaseService<>), typeof(BaseService<>));
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<IBillService, BillService>();
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider
        .GetRequiredService<DataContext>();

    // Here is the migration executed
    dbContext.Database.Migrate();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

static string GetMySQLConfig(WebApplicationBuilder builder)
{
    var host = builder.Configuration["DBHOST"] ?? "localhost";
    var port = builder.Configuration["DBPORT"] ?? "3306";
    var password = builder.Configuration["MYSQL_PASSWORD"] ?? builder.Configuration.GetConnectionString("MYSQL_PASSWORD");
    var userid = builder.Configuration["MYSQL_USER"] ?? builder.Configuration.GetConnectionString("MYSQL_USER");
    var productsdb = builder.Configuration["MYSQL_DATABASE"] ?? builder.Configuration.GetConnectionString("MYSQL_DATABASE");

    string mySqlConnStr = $"server={host}; userid={userid};pwd={password};port={port};database={productsdb}";
    return mySqlConnStr;
}

static RabbitMQConfig GetRabbitMQConfig(WebApplicationBuilder builder)
{
    return new RabbitMQConfig()
    {
        HostName = builder.Configuration["RABBITMQ_Host"],
        Password = builder.Configuration["RABBITMQ_PASSWORD"],
        User = builder.Configuration["RABBITMQ_USER"]
    };
}