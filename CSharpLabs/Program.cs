using System.Text.Json.Serialization;
using CSharpLabs.DB;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Postgres"))); // вместо "DefaultConnection"
// Добавление поддержки контроллеров
builder.Services.AddControllers();
builder.Services.AddControllers()
    .AddJsonOptions(options => {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });
var app = builder.Build();

// Включение маршрутизации
app.UseRouting();

// Маппинг контроллеров
app.MapControllers();

app.Run();