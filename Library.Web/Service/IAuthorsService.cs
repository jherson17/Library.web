using System.Collections.Generic;
using Library.Web.Data;
using Library.Web.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Web.Service
{
    public interface IAuthorsService
    {
        public Task<List<Author>> GetListAsyc();

    }

    public class AuthorsService : IAuthorsService
    {
        private readonly DataContext _context;

        public AuthorsService(DataContext context) 
        {  
            _context = context;
        }

        public async Task<List<Author>> GetListAsyc()
        {
            try
            {
                List<Author> List = await _context.Authors.ToListAsync();
                return List;
            }
            catch (Exception ex)
            {
                return null;
                
            }
        }
    }
}
