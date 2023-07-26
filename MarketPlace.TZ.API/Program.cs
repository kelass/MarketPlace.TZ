using MarketPlace.TZ.Data;
using MarketPlace.TZ.Services;
using MarketPlace.TZ.Services.Interfaces;
using MarketPlace.TZ.Services.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Configuration.AddJsonFile("Secrets.json");
string connect = builder.Configuration.GetConnectionString("PersonalConnection");
builder.Services.AddAutoMapper(typeof(UnitOfWork));
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connect, b => b.MigrationsAssembly("MarketPlace.TZ.Data")));

builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddScoped<IAuctionRepository, AuctionRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Version = "v1",
        Title = "Item MarketPlace - API",
    });

    string filePath = Path.Combine(AppContext.BaseDirectory, "MarketPlace.TZ.API.xml");
    c.IncludeXmlComments(filePath);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
