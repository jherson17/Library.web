using System.Collections.Generic;
using Library.Web.Core;
using Library.Web.Data;
using Library.Web.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Web.Service
{
    public interface IAuthorsService
    {
        public Task<Response<List<Author>>> GetListAsyc();

    }

    public class AuthorsService : IAuthorsService
    {
        private readonly DataContext _context;

        public AuthorsService(DataContext context) 
        {  
            _context = context;
        }

        public async Task<Response<List<Author>>> GetListAsyc()
        {
            try
            {
                throw new Exception("Test");

                List<Author> List = await _context.Authors.ToListAsync();
                Response<List<Author>> response = new Response<List<Author>>
                {
                    IsSucces = true,
                    Message = "Lista Obtenida",
                    Result = List
                };


                return response;
            }
            catch (Exception ex)
            {
                return new Response<List<Author>>
                {
                    IsSucces = false,
                    Message = ex.Message,

                };
                
            }
        }
    }
}
