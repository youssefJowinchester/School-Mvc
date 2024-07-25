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
    public class StudentRepository : GenricRepository<Student> , IStudentRepository
    {
        public StudentRepository(AppDbContext context) : base(context) { }


    }
}
