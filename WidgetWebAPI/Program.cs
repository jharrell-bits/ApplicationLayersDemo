using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Converters;
using WidgetWebAPI.BusinessLogic;
using WidgetWebAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<WidgetDbContext>(opt => opt.UseInMemoryDatabase("WidgetDbContext"));

builder.Services.AddScoped<IWidgetDataAccess, WidgetDataAccess>();
builder.Services.AddScoped<IWidgetBusinessLogic, WidgetBusinessLogic>();

builder.Services.AddControllers()
    .AddNewtonsoftJson(options => options.SerializerSettings.Converters.Add(new StringEnumConverter()));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGenNewtonsoftSupport();

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
