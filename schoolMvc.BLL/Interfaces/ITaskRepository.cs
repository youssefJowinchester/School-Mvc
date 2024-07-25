using schoolMvc.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace schoolMvc.BLL.Interfaces
{
    public interface ITaskRepository
    {

        #region Get Data Of Task - Student - Teacher 

        // Get All Task
        Task<IEnumerable<SchoolTask>> GetAllTasks();

        // Get One Task
        Task<SchoolTask> GetByID(int id);
        
        // Get Tasks Of one Student
        Task<IEnumerable<SchoolTask>> GetAllTaskStudent(string studentId);
        
        // Get Tasks Of One Teacher
        Task<IEnumerable<SchoolTask>> GetAllTaskTeacher(string teacherId);

        #endregion

        #region Add _ Delete (Task)
        void AddTask(string teacherId,SchoolTask task);
        bool DeleteTask(SchoolTask date);
        #endregion

        #region   Update_Task
        Task UpdateStudentTask(SchoolTask date);
        Task UpdateTeacherTask(SchoolTask date);
        #endregion

        
        Task<int> Complete();

    }
}
