var builder = WebApplication.CreateBuilder(args);

// L�gger till MVC-kontroller och vyer till tj�nstcontainern.
// Detta �r n�dv�ndigt f�r att hantera HTTP-f�rfr�gningar och svar.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Konfigurerar HTTP-f�rfr�gningspipelinen
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");  // Anv�nder en felhanterare i icke-utvecklingsmilj�er

    app.UseHsts(); // Anv�nder HTTP Strict Transport Security i icke-utvecklingsmilj�er
}

app.UseHttpsRedirection(); // Omdirigerar HTTP-f�rfr�gningar till HTTPS
app.UseStaticFiles();  // Till�ter att anv�nda statiska filer som bilder och CSS

app.UseRouting(); // Aktiverar routing f�r att kunna mappa f�rfr�gningar till kontroller.

app.UseAuthorization(); // Aktiverar auktorisering

// Kommenterad kod. Om du vill definiera en standardroute f�r MVC-kontroller, avkommentera och anpassa.
//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run(); // Startar applikationen
