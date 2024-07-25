using schoolMvc.DAL.Enums;
using System.ComponentModel.DataAnnotations;

namespace schoolMvc.PL.ViewModel.Task
{
    public class StudentTaskVM
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }

        [Display(Name = "Description")]
        [Required]
        public string Description { get; set; }

        [Display(Name = "Period")]
        [Required]
        public int Period { get; set; }

        public ChosesStatus Status { get; set; }

        public string TeacherName { get; set; }
        public string StudentId { get; set; }
    }

}
