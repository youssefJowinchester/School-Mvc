
using schoolMvc.DAL.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace schoolMvc.DAL.Models
{
    public class SchoolTask
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public int Period { get; set; }
        public ChosesStatus status { get; set; } = ChosesStatus.Pending;

        [ForeignKey("TeacherId")]
        public string TeacherId { get; set; }
        public Teacher Teacher { get; set; }

        [ForeignKey("StudentId")]
        public string StudentId { get; set; }
        public Student Student { get; set; }
    }
}
