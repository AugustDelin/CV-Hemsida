using CVDataLayer;
using CVModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using CV_SITE.Repositories;


var builder = WebApplication.CreateBuilder(args);

// L�gger till MVC-kontroller och vyer till tj�nstcontainern

builder.Services.AddControllersWithViews();

// Konfigurerar databaskontexten f�r att anv�nda LazyLoading och SQL Server

builder.Services.AddDbContext<CVContext>(Options =>
    Options.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("CVContext")));

// Konfigurerar identitetssystemet f�r anv�ndarautentisering och -auktorisering

builder.Services.AddIdentity<Anv�ndare, IdentityRole>()
    .AddEntityFrameworkStores<CVContext>()
    .AddDefaultTokenProviders();

// Anpassar inst�llningar f�r identitetsoptioner, s�rskilt l�senordskrav

builder.Services.Configure<IdentityOptions>(options =>
{
    // Anger l�senordskrav
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 3;
    options.Password.RequiredUniqueChars = 1;
});

// Registrerar PersonRepository som en scoped service
builder.Services.AddScoped<PersonRepository>();

var app = builder.Build();

// Konfigurerar HTTP-f�rfr�gningspipelinen
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");    // Anv�nder en felhanterare i produktion
    app.UseHsts();   // Anv�nder HTTP Strict Transport Security
}

app.UseHttpsRedirection();  // Omdirigerar HTTP-f�rfr�gningar till HTTPS
app.UseStaticFiles(); // Till�ter att anv�nda statiska filer som bilder och CSS

app.UseRouting(); // Aktiverar routing

app.UseAuthorization();       // Aktiverar auktorisering

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();   // Startar applikationen
