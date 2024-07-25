using schoolMvc.DAL.Enums;
using schoolMvc.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace schoolMvc.PL.ViewModel.Task
{
    public class TaskVM
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        [Display(Name = "Description")]
        [Required]
        public string Description { get; set; }

        [Display(Name = "Period")]
        [Required]
        public int Period { get; set; }

        //[Display(Name = "Select a Teacher")]
        //[Required]
        //public string TeacherId { get; set; }
        //public string TeacherName { get; set; }

        [Display(Name = "Select a Student")]
        [Required]
        public string StudentId { get; set; }

        public string StudentName { get; set; }

        public string Status { get; set; }
    }
}
