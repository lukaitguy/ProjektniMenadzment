using Microsoft.EntityFrameworkCore;
using ProjektniMenadzment.Data;
using ProjektniMenadzment.Repositories;
using ProjektniMenadzment.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<PMDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PMConnectionString")));
builder.Services.AddScoped<IProjektiRepository, ProjektiRepository>();
builder.Services.AddScoped<IZadaciRepository, ZadaciRepository>();
builder.Services.AddScoped<IZanroviRepository, ZanroviRepository>();
builder.Services.AddScoped<IKorisniciRepository, KorisniciRepository>();
builder.Services.AddScoped<IClanoviProjektaRepository, ClanoviProjektaRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
