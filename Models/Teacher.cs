using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace StudentApplication.Models
{
    public class Teacher
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TeacherId { get; set; }
        public string? TeacherName { get; set; }
        public string? TeacherLastName { get; set; }
        public string? TeacherEmail { get; set; }
        public string? TeacherPhone {  get; set; }
        public bool ActiveTeacher { get; set; }


    }
}
