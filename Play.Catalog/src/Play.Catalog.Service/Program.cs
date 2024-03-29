using Play.Catalog.Service.Models;
using Play.Common.MassTransit;
using Play.Common.MongoDB;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// builder.Services.Configure<CatalogDatabaseSettings>(
//     builder.Configuration.GetSection(nameof(CatalogDatabaseSettings)));

// Inject MongoDB database as a singleton.
builder.Services.AddMongo().AddMongoRepository<Item>("items").AddMassTransitWithRabbitMQ();

// Configure MassTransit


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
