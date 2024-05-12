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
        //click derecho - añador vista (debe tener el mismo nombre)
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

        [HttpGet]
        /*[FromRoute] int id indica que el parámetro id del método Edit del controlador AuthorsController se extraerá de la ruta de la URL.*/
        public async Task<IActionResult> Edit([FromRoute] int id)
        {
            try
            {
                // Busca el autor en la base de datos con el id proporcionado.
                Author autor = await _context.Authors.FirstOrDefaultAsync(a => a.Id == id);

                if (autor is null) // Verifica si el autor no se encontró en la base de datos.
                {
                    return RedirectToAction(nameof(Index));
                }
                // Crea un objeto AuthorDTO que contiene los datos del autor para ser mostrados en la vista.
                AuthorDTO dto = new AuthorDTO
                {
                    Id = id,
                    Name = autor.Name,
                    Last_Name = autor.Last_Name,
                };
                // Devuelve la vista "Edit" pasando el objeto AuthorDTO como modelo.
                return View(dto);

            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(Index));
            }
        }
    

        [HttpPost]
        public async Task<IActionResult> Edit(AuthorDTO dto)
        {
            try
            {   // Verifica si el modelo recibido es válido según las reglas de validación definidas en el DTO.
                if (!ModelState.IsValid)
                {
                    return View(dto);// Si los datos del modelo no son válidos, vuelve a la vista de creación.
                }

                // Busca el autor en la base de datos con el ID proporcionado en el DTO.
                Author author = await _context.Authors.FirstOrDefaultAsync(a => a.Id == dto.Id);

                if (author is null)
                {
                    // Si el autor no se encuentra, devuelve un resultado NotFound.
                    return NotFound();
                }
                // Actualiza los datos del autor con los valores del DTO.
                author.Name = dto.Name;
                author.Last_Name = dto.Last_Name;

                // Marca el autor como modificado en el contexto de datos.
                _context.Authors.Update(author);
               
                await _context.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                return RedirectToAction(nameof(Index));
            }

        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                Author autor = await _context.Authors.FirstOrDefaultAsync(a => a.Id == id);

                if (autor is null)
                {
                    return RedirectToAction(nameof(Index));
                }
                // Elimina el autor de la base de datos.
                _context.Authors.Remove(autor);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(Index));
            }
        }


    }
}