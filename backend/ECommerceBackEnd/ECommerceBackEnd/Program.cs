using Catalog.Settings;
using ECommerceBackEnd.Repositories;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
IConfiguration configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build();

builder.Services.AddSingleton<IMongoClient>(serviceProvider =>
{
    var settings = configuration.GetSection(nameof(MongoDbSettings)).Get<MongoDbSettings>();
    return new MongoClient(settings.ConnectionString);
});
builder.Services.AddSingleton<IProductsRepository, ProductsRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
IWebHostEnvironment environment = app.Environment;



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
