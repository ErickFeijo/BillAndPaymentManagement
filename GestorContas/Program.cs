global using GestorContas.Data;
global using Microsoft.EntityFrameworkCore;
using GestorContas.Data.Repositories;
using GestorContas.Data.Repositories.Interfaces;
using GestorContas.Service;
using GestorContas.Service.Interfaces;
using System;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseMySQL(BuildConnectionString(builder));
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<DbContext, DataContext>();
builder.Services.AddScoped(typeof(IBaseService<>), typeof(BaseService<>));
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

static string BuildConnectionString(WebApplicationBuilder builder)
{
    var host = builder.Configuration["DBHOST"] ?? "localhost";
    var port = builder.Configuration["DBPORT"] ?? "3306";
    var password = builder.Configuration["MYSQL_PASSWORD"] ?? builder.Configuration.GetConnectionString("MYSQL_PASSWORD");
    var userid = builder.Configuration["MYSQL_USER"] ?? builder.Configuration.GetConnectionString("MYSQL_USER");
    var productsdb = builder.Configuration["MYSQL_DATABASE"] ?? builder.Configuration.GetConnectionString("MYSQL_DATABASE");

    string mySqlConnStr = $"server={host}; userid={userid};pwd={password};port={port};database={productsdb}";
    return mySqlConnStr;
}