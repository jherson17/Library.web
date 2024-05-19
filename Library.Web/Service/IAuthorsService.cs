using System.Collections.Generic;
using Library.Web.Core; // Namespace que probablemente contiene la clase Response.
using Library.Web.Data; // Namespace que contiene el DataContext.
using Library.Web.Data.Entities; // Namespace que contiene las entidades, incluyendo Author.
using Microsoft.EntityFrameworkCore; // Namespace que contiene funcionalidades de Entity Framework Core.

namespace Library.Web.Service
{
    // Interfaz que define el contrato para el servicio de autores.
    public interface IAuthorsService
    {
        // Método que devuelve una lista de autores de manera asíncrona encapsulada en una respuesta.
        public Task<Response<List<Author>>> GetListAsyc();
    }

    // Implementación del servicio de autores.
    public class AuthorsService : IAuthorsService
    {
        private readonly DataContext _context; // Campo privado para el contexto de datos.

        // Constructor que inyecta el contexto de datos.
        public AuthorsService(DataContext context)
        {
            _context = context;
        }

        // Implementación del método para obtener la lista de autores de manera asíncrona.
        public async Task<Response<List<Author>>> GetListAsyc()
        {
            try
            {
                // Obtiene la lista de autores de la base de datos de manera asíncrona.
                List<Author> list = await _context.Authors.ToListAsync();

                // Crea una respuesta exitosa con la lista obtenida.
                Response<List<Author>> response = new Response<List<Author>>
                {
                    IsSucces = true,
                    Message = "Lista Obtenida",
                    Result = list
                };

                return response;
            }
            catch (Exception ex)
            {
                // En caso de excepción, crea una respuesta de error con el mensaje de la excepción.
                return new Response<List<Author>>
                {
                    IsSucces = false,
                    Message = ex.Message
                };
            }
        }
    }
}
