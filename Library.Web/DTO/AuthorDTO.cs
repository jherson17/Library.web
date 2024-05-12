using System.ComponentModel.DataAnnotations;

namespace Library.Web.DTO
{
    public class AuthorDTO
    {
        public int Id { get; set; }

        // Propiedad que representa el nombre del autor.
        [MaxLength(128, ErrorMessage = "El campo {0} debe tener al menos un caracter.")]
        public string Name { get; set; }

        // Propiedad que representa el apellido del autor.
        [MaxLength(128, ErrorMessage = "El campo {0} debe tener al menos un caracter.")]
        public string Last_Name { get; set; }
    }
}
