using ApplicationLayersDemo.Components;
using BusinessLogic;
using Data;
using Microsoft.EntityFrameworkCore;
using Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<GadgetDbContext>(opt => opt.UseInMemoryDatabase("GadgetDbContext"));

builder.Services.AddScoped<IGadgetBusinessLogic, GadgetBusinessLogic>();
builder.Services.AddScoped<IWidgetBusinessLogic, WidgetBusinessLogic>();
builder.Services.AddScoped<IGadgetDataAccess, GadgetDataAccess>();
builder.Services.AddScoped<IWidgetDataAccess, WidgetAPIInvoker>();

builder.Services.AddHttpClient();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
