using Library.Web.Data; // Importa el espacio de nombres que contiene las clases relacionadas con el acceso a datos.
using Library.Web.Data.Entities; // Importa el espacio de nombres que contiene las entidades de datos.
using Library.Web.DTO;
using Microsoft.AspNetCore.Mvc; // Importa el espacio de nombres que contiene las clases relacionadas con ASP.NET Core MVC.
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore; // Importa el espacio de nombres que contiene las clases relacionadas con Entity Framework Core.

namespace Library.Web.Controllers
{
    public class BooksController : Controller
    {
        private readonly DataContext _context;

        public BooksController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // Obtiene todos los libros incluyendo la información del autor y los envía a la vista.
            IEnumerable<Book> list = await _context.Books.Include(b => b.Author).ToListAsync();
            return View(list);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            // Prepara un nuevo DTO de libro con la lista de autores y lo envía a la vista de creación.
            BookDTO bookDTO = new BookDTO
            {
                Authors = await _context.Authors.Select(a => new SelectListItem
                {
                    Text = a.FullName,
                    Value = a.Id.ToString(),
                }).ToArrayAsync(),
            };
            return View(bookDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Create(BookDTO dto)
        {
            try
            {
                // Verifica si el modelo es válido antes de guardar el nuevo libro.
                if (!ModelState.IsValid)
                {
                    // Si hay errores de validación, vuelve a cargar la vista de creación con los datos y mensajes de error.
                    dto.Authors = await _context.Authors.Select(a => new SelectListItem
                    {
                        Text = a.FullName,
                        Value = a.Id.ToString(),
                    }).ToArrayAsync();
                    return View(dto);
                }

                // Crea un nuevo libro utilizando los datos del DTO y lo guarda en la base de datos.
                Book book = new Book
                {
                    Description = dto.Description,
                    PublishDate = dto.PublishDate,
                    Title = dto.Title,
                    Author = await _context.Authors.FirstOrDefaultAsync(a => a.Id == dto.AuthorId)
                };
                await _context.Books.AddAsync(book);
                await _context.SaveChangesAsync();

                // Redirige a la acción Index después de crear el libro exitosamente.
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Si hay un error, redirige a la acción Index.
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute] int id)
        {
            try
            {
                // Busca el libro por su ID e incluye la información del autor.
                Book? book = await _context.Books.Include(b => b.Author).FirstOrDefaultAsync(b => b.Id == id);

                if (book is null)
                {
                    // Si el libro no se encuentra, redirige a la acción Index.
                    return RedirectToAction(nameof(Index));
                }

                // Prepara un DTO con la información del libro y la lista de autores para la vista de edición.
                BookDTO dto = new BookDTO
                {
                    Id = id,
                    AuthorId = book.Author.Id,
                    Description = book.Description,
                    PublishDate = book.PublishDate,
                    Title = book.Title,
                    Authors = await _context.Authors.Select(a => new SelectListItem
                    {
                        Text = a.FullName,
                        Value = a.Id.ToString(),
                    }).ToArrayAsync(),
                };
                return View(dto);
            }
            catch (Exception ex)
            {
                // Si hay un error, redirige a la acción Index.
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(BookDTO dto)
        {
            try
            {
                // Verifica si el modelo es válido antes de actualizar el libro.
                if (!ModelState.IsValid)
                {
                    // Si hay errores de validación, vuelve a cargar la vista de edición con los datos y mensajes de error.
                    dto.Authors = await _context.Authors.Select(a => new SelectListItem
                    {
                        Text = a.FullName,
                        Value = a.Id.ToString(),
                    }).ToArrayAsync();
                    return View(dto);
                }

                // Busca el libro por su ID y actualiza sus datos con los del DTO.
                Book book = await _context.Books.FirstOrDefaultAsync(a => a.Id == dto.Id);
                if (book is null)
                {
                    // Si el libro no se encuentra, devuelve un error 404.
                    return NotFound();
                }

                // Actualiza los campos del libro con los datos del DTO.
                book.PublishDate = dto.PublishDate;
                book.Title = dto.Title;
                book.Author = await _context.Authors.FirstOrDefaultAsync(a => a.Id == dto.AuthorId);
                book.Description = dto.Description;

                // Guarda los cambios en la base de datos.
                _context.Books.Update(book);
                await _context.SaveChangesAsync();

                // Redirige a la acción Index después de editar el libro exitosamente.
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Si hay un error, redirige a la acción Index.
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                // Busca el libro por su ID y lo elimina de la base de datos.
                Book book = await _context.Books.FirstOrDefaultAsync(a => a.Id == id);

                if (book is null)
                {
                    return RedirectToAction(nameof(Index));
                }

                _context.Books.Remove(book);
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
