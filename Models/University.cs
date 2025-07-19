using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EjadaTraineesManagementSystem.Models
{
    [Table("Universities", Schema = "HR")]
    public class University
    {
        [Key]
        [Display(Name = "ID")]
        public int UniversityId { get; set; }

        [Required]
        [Display(Name = "University")]
        [Column(TypeName = "varchar(200)")]
        public string UniversityName { get; set; } = string.Empty;
    }
}
