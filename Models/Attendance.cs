using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace StudentApplication.Models
{
    public class Attendance
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public  int AttendanceId { get; set; }
        public  int StudentId { get; set; }
        [ForeignKey("StudentId")]
        //public Student Student { get; set; }
        public  int SubjectId { get; set; }
        [ForeignKey("SubjectId")]
        //public Subject Subject { get; set; }
        public  DateOnly Date { get; set; }
        public bool AttendanceFlag { get; set; }


    }
}

