using Microsoft.EntityFrameworkCore; // Importa el espacio de nombres que contiene las clases relacionadas con Entity Framework Core.
using Library.Web.Data.Entities; // Importa el espacio de nombres que contiene las entidades de datos de la aplicación.

namespace Library.Web.Data
{
    // Clase que representa el contexto de datos de la aplicación.
    public class DataContext : DbContext
    {
        // Constructor que recibe las opciones de configuración del contexto de datos.
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            // No es necesario agregar lógica adicional en el constructor en este caso.
        }

        // Define un conjunto de entidades para la tabla de autores en la base de datos.
        public DbSet<Author> Authors { get; set; }

        // Define un conjunto de entidades para la tabla de libros en la base de datos.
        public DbSet<Book> Books { get; set; }
    }
}