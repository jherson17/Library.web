using Library.Web.Data; // Importa el espacio de nombres que contiene las clases relacionadas con el acceso a datos.
using Library.Web.Data.Entities; // Importa el espacio de nombres que contiene las entidades de datos.
using Microsoft.AspNetCore.Mvc; // Importa el espacio de nombres que contiene las clases relacionadas con ASP.NET Core MVC.
using Microsoft.EntityFrameworkCore; // Importa el espacio de nombres que contiene las clases relacionadas con Entity Framework Core.

namespace Library.Web.Controllers
{
    // Controlador para gestionar las operaciones relacionadas con los autores.
    public class AuthorsController : Controller
    {
        private readonly DataContext _context; // Declara un campo para almacenar el contexto de datos.

        // Constructor que recibe el contexto de datos mediante inyección de dependencias.
        public AuthorsController(DataContext context)
        {
            _context = context; // Asigna el contexto de datos recibido al campo privado.
        }

        // Acción para mostrar la lista de autores.
        public async Task<IActionResult> Index()
        {
            // Obtiene la lista de autores de forma asincrónica desde la base de datos.
            IEnumerable<Author> list = await _context.Authors.ToListAsync();

            // Devuelve una vista pasando la lista de autores como modelo.
            return View(list);
        }
    }
}