using Library.Web.Data; // Importa el espacio de nombres que contiene la clase DataContext.
using Microsoft.EntityFrameworkCore; // Importa el espacio de nombres que contiene las clases relacionadas con Entity Framework Core.
using Microsoft.AspNetCore.Builder; // Importa el espacio de nombres que contiene las clases para configurar la aplicación ASP.NET Core.
using Microsoft.AspNetCore.Hosting; // Importa el espacio de nombres que contiene las clases relacionadas con el hospedaje de la aplicación ASP.NET Core.
using Microsoft.Extensions.DependencyInjection;
using Library.Web.Service; // Importa el espacio de nombres que contiene las clases para configurar los servicios de la aplicación ASP.NET Core.

var builder = WebApplication.CreateBuilder(args); // Crea un nuevo constructor de aplicaciones web.

// Agrega servicios al contenedor de dependencias.
builder.Services.AddControllersWithViews(); // Agrega servicios necesarios para admitir controladores y vistas MVC.

// Configura el contexto de datos.
builder.Services.AddDbContext<DataContext>(cfg =>
{
    cfg.UseSqlServer(builder.Configuration.GetConnectionString("MyConnection")); // Configura Entity Framework Core para usar SQL Server como proveedor de base de datos y obtiene la cadena de conexión desde la configuración de la aplicación.
});

builder.Services.AddScoped<IAuthorsService, AuthorsService>();

var app = builder.Build(); // Construye la aplicación.

// Configura el pipeline de solicitud HTTP.
if (!app.Environment.IsDevelopment()) // Verifica si la aplicación no se está ejecutando en un entorno de desarrollo.
{
    app.UseExceptionHandler("/Home/Error"); // Usa el manejador de excepciones para redirigir a la página de error en caso de excepciones no controladas.
    // El valor HSTS predeterminado es de 30 días. Puedes querer cambiar esto para escenarios de producción, ver https://aka.ms/aspnetcore-hsts.
    app.UseHsts(); // Configura HSTS (HTTP Strict Transport Security) para aplicar políticas de seguridad estrictas.
}

app.UseHttpsRedirection(); // Habilita la redirección HTTPS.
app.UseStaticFiles(); // Habilita el uso de archivos estáticos como CSS, JavaScript, imágenes, etc.

app.UseRouting(); // Habilita el enrutamiento de solicitudes.

app.UseAuthorization(); // Habilita la autorización en la aplicación.

app.MapControllerRoute( // Mapea una ruta de controlador predeterminada.
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"); // Configura la ruta predeterminada para que apunte al controlador "Home" y la acción "Index".

app.Run(); // Ejecuta la aplicación.