using Microsoft.EntityFrameworkCore;
using schoolMvc.BLL.Interfaces;
using schoolMvc.DAL.Data;
using schoolMvc.DAL.Enums;
using schoolMvc.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace schoolMvc.BLL.Repositories
{
    public class TaskRepository : ITaskRepository
    {

        private readonly AppDbContext _context;

        public TaskRepository(AppDbContext context)
        {
            _context = context;
        }




        #region Get Data Of Task 

        public async Task<IEnumerable<SchoolTask>> GetAllTasks()
        {
            return await _context.SchoolTasks.Include(S => S.Student).Include(T => T.Teacher).ToListAsync(); 
        }

        public async Task<IEnumerable<SchoolTask>> GetAllTaskStudent(string studentId)
        {
           return await _context.SchoolTasks.Where(S=>S.StudentId == studentId).Include(T=>T.Teacher).ToListAsync();
        }

        public async Task<IEnumerable<SchoolTask>> GetAllTaskTeacher(string teacherId)
        {
            return await _context.SchoolTasks.Where(T => T.TeacherId == teacherId).Include(S => S.Student).ToListAsync();
        }

        public async Task<SchoolTask> GetByID(int id)
        {
            var data = await _context.SchoolTasks.FindAsync(id);

            return data;
        }



        #endregion


        #region Add - Delete


        public void AddTask(string teacherId, SchoolTask task)
        {
              _context.SchoolTasks.Add(new SchoolTask()
              {
                  Period = task.Period,
                  Date = task.Date,
                  Description = task.Description,
                  status = ChosesStatus.Pending,
                  StudentId = task.StudentId,
                  TeacherId = teacherId,
              });
        }



        public bool DeleteTask(SchoolTask date)
        {
            var task = _context.SchoolTasks.FirstOrDefault(n=>n.Id == date.Id);
            if (task.status == ChosesStatus.Done)
            {
                _context.SchoolTasks.Remove(task);
                _context.SaveChanges();
            }

            return true;
        }


        #endregion


        #region Update Tasks
        public async Task UpdateStudentTask(SchoolTask date)
        {
            var task = await _context.SchoolTasks.FindAsync(date.Id);
            if(task != null)
            {
                task.status = date.status;
            }
        }

        public async Task UpdateTeacherTask(SchoolTask date)
        {
            var task = await _context.SchoolTasks.FindAsync(date.Id);
            if( task != null )
            {
                task.Period = date.Period;
                task.Date = date.Date;
                task.Description = date.Description;
                task.StudentId = date.StudentId;
            }
        }

        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }

        #endregion



    }
}
