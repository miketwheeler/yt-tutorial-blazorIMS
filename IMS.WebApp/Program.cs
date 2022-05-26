using IMS.Plugins.InMemory;
using IMS.UseCases.Inventories;
using IMS.UseCases.PluginInterfaces;
using IMS.WebApp.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();

// Lifetime management methods - AddSingleton, AddTransient, AddScoped (Blazor-server) => used to tell WHEN the instance of the proceeding class is to be disposed
builder.Services.AddSingleton<IInventoryRepository, InventoryRepository>(); // creates adhoc, and stores within application - as long as appln running (same instace (*copy) will be used over and over)
builder.Services.AddTransient<IViewInventoriesByNameUseCase, ViewInventoriesByNameUseCase>(); // doesn't store anywhere, created adhoc
builder.Services.AddScoped<IViewInventoriesByNameUseCase, ViewInventoriesByNameUseCase>(); // instance of that class is scoped lifetime - created, and stored as long as there is an established SignalR connection - hybrid of the above 2
// {{ in Blazor Server:: AddScoped and AddSingleton ARE THE SAME because of the architecture relying on the SigR comms channel }}



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
