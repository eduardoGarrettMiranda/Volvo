using VOLVO.DAL;
using VOLVO.BLL;
using VOLVO.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IVehicleService, VehicleService>();

builder.Services.AddScoped<IVehicleFactory, VehicleFactory>();

builder.Services.AddScoped<IVehicleRepository>(provider =>
{
    var config = provider.GetRequiredService<IConfiguration>();
    var connectionString = config.GetConnectionString("DefaultConnection");
    var factory = provider.GetRequiredService<IVehicleFactory>();

    return new VehicleRepository(connectionString, factory);
});

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var repository = scope.ServiceProvider.GetRequiredService<IVehicleRepository>();
    repository.CreateDatabaseAndTable();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
