
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace StudentApplication.Models
{
    public class Grade
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GradeId { get; set; }
        public int StudentId { get; set; }
        [ForeignKey("StudentId")]
        //public Student Student { get; set; }
        public int SubjectId { get; set; }
        [ForeignKey("SubjectId")]
        //public Subject Subject { get; set; }
        public float Score { get; set; }
        public string? Description { get; set; }
        public bool ActiveGrade { get; set; }

    }
}




