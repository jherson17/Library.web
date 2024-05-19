using Library.Web.Data; // Importa el espacio de nombres que contiene la clase DataContext.
using Microsoft.EntityFrameworkCore; // Importa el espacio de nombres que contiene las clases relacionadas con Entity Framework Core.
using Microsoft.AspNetCore.Builder; // Importa el espacio de nombres que contiene las clases para configurar la aplicaci�n ASP.NET Core.
using Microsoft.AspNetCore.Hosting; // Importa el espacio de nombres que contiene las clases relacionadas con el hospedaje de la aplicaci�n ASP.NET Core.
using Microsoft.Extensions.DependencyInjection;
using Library.Web.Service; // Importa el espacio de nombres que contiene las clases para configurar los servicios de la aplicaci�n ASP.NET Core.

var builder = WebApplication.CreateBuilder(args); // Crea un nuevo constructor de aplicaciones web.

// Agrega servicios al contenedor de dependencias.
builder.Services.AddControllersWithViews(); // Agrega servicios necesarios para admitir controladores y vistas MVC.

// Configura el contexto de datos.
builder.Services.AddDbContext<DataContext>(cfg =>
{
    cfg.UseSqlServer(builder.Configuration.GetConnectionString("MyConnection")); // Configura Entity Framework Core para usar SQL Server como proveedor de base de datos y obtiene la cadena de conexi�n desde la configuraci�n de la aplicaci�n.
});

builder.Services.AddScoped<IAuthorsService, AuthorsService>();

var app = builder.Build(); // Construye la aplicaci�n.

// Configura el pipeline de solicitud HTTP.
if (!app.Environment.IsDevelopment()) // Verifica si la aplicaci�n no se est� ejecutando en un entorno de desarrollo.
{
    app.UseExceptionHandler("/Home/Error"); // Usa el manejador de excepciones para redirigir a la p�gina de error en caso de excepciones no controladas.
    // El valor HSTS predeterminado es de 30 d�as. Puedes querer cambiar esto para escenarios de producci�n, ver https://aka.ms/aspnetcore-hsts.
    app.UseHsts(); // Configura HSTS (HTTP Strict Transport Security) para aplicar pol�ticas de seguridad estrictas.
}

app.UseHttpsRedirection(); // Habilita la redirecci�n HTTPS.
app.UseStaticFiles(); // Habilita el uso de archivos est�ticos como CSS, JavaScript, im�genes, etc.

app.UseRouting(); // Habilita el enrutamiento de solicitudes.

app.UseAuthorization(); // Habilita la autorizaci�n en la aplicaci�n.

app.MapControllerRoute( // Mapea una ruta de controlador predeterminada.
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"); // Configura la ruta predeterminada para que apunte al controlador "Home" y la acci�n "Index".

app.Run(); // Ejecuta la aplicaci�n.