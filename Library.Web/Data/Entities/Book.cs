using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations; // Importa el espacio de nombres que contiene las clases para validación de datos.
using System; // Importa el espacio de nombres que contiene clases y estructuras fundamentales del tiempo y fecha.

namespace Library.Web.Data.Entities
{
    // Clase que representa la entidad Libro en la base de datos.
    public class Book
    {
        // Propiedad que representa el identificador del libro.
        [MaxLength(128, ErrorMessage = "El campo {0} debe tener al menos un caracter.")]
        public int Id { get; set; }

        // Propiedad que representa el título del libro.
        [MaxLength(128, ErrorMessage = "El campo {0} debe tener al menos un caracter.")]
        public string Title { get; set; }

        // Propiedad que representa la descripción del libro.
        [MaxLength(128, ErrorMessage = "El campo {0} debe tener al menos un caracter.")]
        public string Description { get; set; }
 
        // Propiedad que representa la fecha de publicación del libro.
        public DateTime PublishDate { get; set; }

        // Propiedad que representa el autor del libro.
        [MaxLength(128, ErrorMessage = "El campo {0} debe tener al menos un caracter.")]
        public Author Author { get; set; }
    }
}