using Microsoft.EntityFrameworkCore;
using schoolMvc.BLL.Interfaces;
using schoolMvc.DAL.Data;
using schoolMvc.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace schoolMvc.BLL.Repositories
{
    public class GenricRepository<T> : IGenricRepository<T> where T : ApplicationUser
    {
        private readonly AppDbContext _context;

        public GenricRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<T> Get(string id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            return entity;
        }



        public async Task<IEnumerable<T>> GetAll()
        {
            if(typeof(T) == typeof(Student))
            {
                return (IEnumerable<T>) await _context.Students.ToListAsync();
            }
            else if (typeof(T) == typeof(Teacher))
            {
                return (IEnumerable<T>) await _context.Teachers.ToListAsync();
            }
            else
            {
                return await _context.Set<T>().ToListAsync();
            }
        }




    }
}
