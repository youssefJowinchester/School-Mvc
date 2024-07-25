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
    public class TeacherRepository : GenricRepository<Teacher>, ITeacherRepository
    {
        public TeacherRepository(AppDbContext context) : base(context)
        {
        }
    }
}
