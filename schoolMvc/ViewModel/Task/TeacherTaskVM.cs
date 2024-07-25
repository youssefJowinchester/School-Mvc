using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace schoolMvc.PL.ViewModel.Task
{
    public class TeacherTaskVM
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }

        [Display(Name = "Description")]
        [Required]
        public string Description { get; set; }

        [Display(Name = "Period")]
        [Required]
        public int Period { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }

        [Display(Name = "Student Name")]
        public string StudentName { get; set; }
    }
}
