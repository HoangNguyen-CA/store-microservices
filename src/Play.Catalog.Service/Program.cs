using Play.Catalog.Service.Models;
using Play.Catalog.Service.Services;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<CatalogDatabaseSettings>(
    builder.Configuration.GetSection(nameof(CatalogDatabaseSettings)));

// Inject MongoDB database as a singleton.
builder.Services.AddSingleton<IMongoClient>(serviceProvider =>
{
    var settings = builder.Configuration.GetSection(nameof(CatalogDatabaseSettings)).Get<CatalogDatabaseSettings>();
    return new MongoClient(settings.ConnectionString);
});

builder.Services.AddSingleton<IItemsService, ItemsService>();

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
