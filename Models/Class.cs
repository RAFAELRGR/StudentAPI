using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace StudentApplication.Models
    {
        public class Class
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int ClassId { get; set; }
            public string? ClassName { get; set; }
            public bool ActiveClass { get; set; } = true;


        }
    }
