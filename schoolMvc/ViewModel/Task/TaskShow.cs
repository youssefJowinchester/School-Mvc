using System.ComponentModel.DataAnnotations;

namespace schoolMvc.PL.ViewModel.Task
{
    public class TaskShow
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        [Display(Name = "Description")]
        [Required]
        public string Description { get; set; }

        [Display(Name = "Period")]
        [Required]
        public int Period { get; set; }

        [Display(Name = "Teacher Name")]
        public string TeacherName { get; set; }

        [Display(Name = "Student Name")]
        public string StudentName { get; set; }

        public string Status { get; set; }
    }
}
