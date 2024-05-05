using Library.Web.Data;
using Library.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Library.Web.Controllers
{
    //los controladores en la programación sirven para coordinar la interacción entre el usuario,
    //la interfaz de usuario y la lógica subyacente de una aplicación de software
    public class AuthorsController : Controller
    {
        private readonly DataContext _context;

        public AuthorsController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            //Lista de autores en un metodo asincrono
            IEnumerable<Author> list = await _context.Authors.ToListAsync();

            return View(list);
            //vamos a crear una vista 
        }


    }
}
