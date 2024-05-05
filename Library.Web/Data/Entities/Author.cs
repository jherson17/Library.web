using System.ComponentModel.DataAnnotations;

namespace Library.Web.Data.Entities
{
    public class Author
    {
        [MaxLength(128, ErrorMessage = "El campo {0} de tener al menos un caracter ")]
        public int Id { get; set; }
        [MaxLength(128, ErrorMessage = "El campo {0} de tener al menos un caracter ")]
        public string Name { get; set; }
        [MaxLength(128, ErrorMessage = "El campo {0} de tener al menos un caracter ")]
        public string Last_Name { get; set; }
    }
}
