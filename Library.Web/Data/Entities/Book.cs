using System.ComponentModel.DataAnnotations;

namespace Library.Web.Data.Entities
{
    public class Book
    {
        //estas propiedades se crean con prop 
        //Las propiedades son variables asociadas a objetos que nos permiten describir su estado o características
        [MaxLength(128, ErrorMessage = "El campo {0} de tener al menos un caracter ")]
        public int Id { get; set; }
        [MaxLength(128, ErrorMessage = "El campo {0} de tener al menos un caracter ")]
        public string Title { get; set; }
        [MaxLength(128, ErrorMessage = "El campo {0} de tener al menos un caracter ")]
        public string Description { get; set; }
        [MaxLength(128, ErrorMessage = "El campo {0} de tener al menos un caracter ")]
        public DateTime PublishDate { get; set; }
        [MaxLength(128, ErrorMessage = "El campo {0} de tener al menos un caracter ")]
        public Author Author { get; set; }
    }
}
