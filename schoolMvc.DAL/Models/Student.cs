namespace schoolMvc.DAL.Models
{
    public class Student : ApplicationUser
    {
        public List<SchoolTask> StudentTask { get; set; }

    }
}
