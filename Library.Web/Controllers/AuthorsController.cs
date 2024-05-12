using Library.Web.Data; // Importa el espacio de nombres que contiene las clases relacionadas con el acceso a datos.
using Library.Web.Data.Entities; // Importa el espacio de nombres que contiene las entidades de datos.
using Library.Web.DTO;
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

        [HttpGet]
        // Acción para mostrar la lista de autores.
        public async Task<IActionResult> Index()
        {
            // Obtiene la lista de autores de forma asincrónica desde la base de datos.
            IEnumerable<Author> list = await _context.Authors.ToListAsync();

            // Devuelve una vista pasando la lista de autores como modelo.
            return View(list);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {

            return View();
        }

        //al crear un post en create lo creamos en el controller
        // Acción HTTP POST para crear un nuevo autor.
        [HttpPost]
        public async Task<IActionResult> Create(AuthorDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(dto);// Si los datos del modelo no son válidos, vuelve a la vista de creación.
                }
                // Crea un nuevo objeto Author utilizando los datos del DTO recibido.
                Author author = new Author
                {
                    Name = dto.Name,
                    Last_Name = dto.Last_Name
                };
                // Agrega el nuevo autor al contexto de datos.
                await _context.Authors.AddAsync(author);
                // Guarda los cambios en la base de datos.
                await _context.SaveChangesAsync();
                // Redirige al usuario a la acción Index después de crear exitosamente el autor.
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                return RedirectToAction(nameof(Index));
            }
            
        }
    }
}