using CVDataLayer;
using CVModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using CV_SITE.Repositories;


var builder = WebApplication.CreateBuilder(args);

// Lägger till MVC-kontroller och vyer till tjänstcontainern

builder.Services.AddControllersWithViews();

// Konfigurerar databaskontexten för att använda LazyLoading och SQL Server

builder.Services.AddDbContext<CVContext>(Options =>
    Options.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("CVContext")));

// Konfigurerar identitetssystemet för användarautentisering och -auktorisering

builder.Services.AddIdentity<Användare, IdentityRole>()
    .AddEntityFrameworkStores<CVContext>()
    .AddDefaultTokenProviders();

// Anpassar inställningar för identitetsoptioner, särskilt lösenordskrav

builder.Services.Configure<IdentityOptions>(options =>
{
    // Anger lösenordskrav
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

// Konfigurerar HTTP-förfrågningspipelinen
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");    // Använder en felhanterare i produktion
    app.UseHsts();   // Använder HTTP Strict Transport Security
}

app.UseHttpsRedirection();  // Omdirigerar HTTP-förfrågningar till HTTPS
app.UseStaticFiles(); // Tillåter att använda statiska filer som bilder och CSS

app.UseRouting(); // Aktiverar routing

app.UseAuthorization();       // Aktiverar auktorisering

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();   // Startar applikationen
