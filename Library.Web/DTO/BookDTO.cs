using Library.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Library.Web.DTO
{
    public class BookDTO
    {
        
        public int Id { get; set; }

        // Propiedad que representa el título del libro.
        [MaxLength(128, ErrorMessage = "El campo {0} debe tener al menos un caracter.")]
        [Required(ErrorMessage = "el campo {0} es requerida")]
        public string Title { get; set; }

        // Propiedad que representa la descripción del libro.
        [MaxLength(128, ErrorMessage = "El campo {0} debe tener al menos un caracter.")]
        [Required(ErrorMessage = "el campo {0} es requerida")]
        public string Description { get; set; }

        // Propiedad que representa la fecha de publicación del libro.
      
        public DateTime PublishDate { get; set; }

        public IEnumerable<SelectListItem>? Authors { get; set; }// Lista de autores disponibles.

        // Identificador del autor del libro.
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un Autor.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int AuthorId { get; set; }
    }
}
