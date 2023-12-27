using Play.Catalog.Service.Models;
using Play.Catalog.Service.Services;
using MongoDB.Driver;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<CatalogDatabaseSettings>(
    builder.Configuration.GetSection(nameof(CatalogDatabaseSettings)));

// Inject MongoDB database as a singleton.
builder.Services.AddSingleton<IMongoDatabase>(serviceProvider =>
{
    var settings = serviceProvider.GetRequiredService<IOptions<CatalogDatabaseSettings>>().Value;

    return new MongoClient(settings.ConnectionString).GetDatabase(settings.DatabaseName);
});


builder.Services.AddSingleton<IRepository<Item>>(serviceProvider =>
{
    var settings = serviceProvider.GetRequiredService<IOptions<CatalogDatabaseSettings>>().Value;
    return new MongoRepository<Item>(serviceProvider.GetService<IMongoDatabase>(), settings.ItemsCollectionName);
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
