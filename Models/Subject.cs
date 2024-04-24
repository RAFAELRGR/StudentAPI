using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace StudentApplication.Models
{
    public class Subject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SubjectId { get; set; }
        public int ClassId { get; set; }
        [ForeignKey("ClassId")]
        //public Class Class { get; set; }
        public int TeacherId { get; set; }
        [ForeignKey("TeacherId")]
        //public Teacher Teacher { get; set; }
        public string? SubjectName { get; set; }
        public string? Subjectcode { get; set; }
        public bool ActiveSubject { get; set; }


    }
}
