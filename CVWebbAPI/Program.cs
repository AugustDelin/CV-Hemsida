using CVDataLayer;
using CVModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

var builder = WebApplication.CreateBuilder(args);

// L�gg till tj�nster i containern.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<CVContext>(options =>
    options.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("CVContext")));
builder.Services.AddIdentity<Anv�ndare, IdentityRole>()
    .AddEntityFrameworkStores<CVContext>()
    .AddDefaultTokenProviders();
builder.Services.Configure<IdentityOptions>(options => {
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 3;
    options.Password.RequiredUniqueChars = 1;
});

var app = builder.Build();

// Seed-databasen
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<CVContext>();
    SeedData(dbContext);
}

// Konfigurera HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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

void SeedData(CVContext context)
{
    // Ser till att databasen �r skapad
    context.Database.EnsureCreated();

    // Kontrollerar om det redan finns data i databasen
    if (!context.Users.Any())
    {
        var passwordHasher = new PasswordHasher<Anv�ndare>();

        // Skapa en ny anv�ndare
        var anvandare1 = new Anv�ndare
        {
            UserName = "anvandare1",
            NormalizedUserName = "ANVANDARE1",
            Email = "anvandare1@example.com",
            NormalizedEmail = "ANVANDARE1@EXAMPLE.COM",
            EmailConfirmed = true,
            PasswordHash = passwordHasher.HashPassword(null, "anvandare1sL�senord")
        };

        // L�gg till anv�ndaren i databasen
        context.Users.Add(anvandare1);
        context.SaveChanges(); // Spara anv�ndaren f�r att f� ett ID

        // Skapa ett nytt projekt
        var projekt1 = new Projekt
        {
            Titel = "Projekt 1",
            Beskrivning = "Beskrivning av Projekt 1",
            Anv�ndarId = anvandare1.Id
        };

        // L�gg till projektet i databasen
        context.Projekts.Add(projekt1);
        context.SaveChanges(); // Spara projektet f�r att f� ett ID

        // Skapa en relation mellan anv�ndaren och projektet
        var deltarProjekt1 = new DeltarProjekt
        {
            Deltagare = anvandare1.Id,
            Projekt = projekt1.Id
        };

        // L�gg till relationen i databasen
        context.PersonDeltarProjekt.Add(deltarProjekt1);
        context.SaveChanges(); // Spara alla �ndringar
    }
}

