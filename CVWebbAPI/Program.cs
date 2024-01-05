var builder = WebApplication.CreateBuilder(args);

// Lägger till MVC-kontroller och vyer till tjänstcontainern.
// Detta är nödvändigt för att hantera HTTP-förfrågningar och svar.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Konfigurerar HTTP-förfrågningspipelinen
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");  // Använder en felhanterare i icke-utvecklingsmiljöer

    app.UseHsts(); // Använder HTTP Strict Transport Security i icke-utvecklingsmiljöer
}

app.UseHttpsRedirection(); // Omdirigerar HTTP-förfrågningar till HTTPS
app.UseStaticFiles();  // Tillåter att använda statiska filer som bilder och CSS

app.UseRouting(); // Aktiverar routing för att kunna mappa förfrågningar till kontroller.

app.UseAuthorization(); // Aktiverar auktorisering

// Kommenterad kod. Om du vill definiera en standardroute för MVC-kontroller, avkommentera och anpassa.
//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run(); // Startar applikationen
