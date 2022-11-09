using FluentAssertions.Common;
using Inventory.Core.Web.Api.Brokers.Loggings;
using Inventory.Core.Web.Api.Brokers.Storages;
using Inventory.Core.Web.Api.Services.Foundations.Products;
using Newtonsoft.Json;
using JsonStringEnumConverter = Newtonsoft.Json.Converters.StringEnumConverter;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddMvc().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    options.SerializerSettings.Converters.Add(new JsonStringEnumConverter());
    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
});

builder.Services.AddDbContext<StorageBroker>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//Add Brokers
builder.Services.AddScoped<IStorageBroker, StorageBroker>();
builder.Services.AddTransient<ILoggingBroker, LoggingBroker>();

//Add FoundationServices
builder.Services.AddTransient<IProductService, ProductService>();

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
