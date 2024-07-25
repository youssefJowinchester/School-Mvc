namespace schoolMvc.DAL.Models
{
    public class Teacher : ApplicationUser
    {
        public List<SchoolTask> TeacherTask { get; set; }
    }
}
