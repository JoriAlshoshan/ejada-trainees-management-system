using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EjadaTraineesManagementSystem.Models
{
   
    [Table("Trainees", Schema = "HR")]
    public class Trainee
    {
        [Key]
        [Display(Name = "ID")]
        public int? TraineeId { get; set; }


        [Display(Name = "Name")]
        [Column(TypeName = "varchar(250)")]
        public string TraineeName { get; set; } = string.Empty;


        [Display(Name = "Image")]
        [Column(TypeName = "varchar(250)")]
        public string? ImageUser { get; set; }


        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:dd-MMMM_yyyy}")]
        public DateTime StartDate { get; set; }


        [Display(Name = "Phone")]
        [MaxLength(10)]
        [MinLength(10)]
        [Column(TypeName = "varchar(10)")]
        public string PhoneNumber { get; set; } 


        [Display(Name = "Major")]
        [Column(TypeName = "varchar(250)")]
        public string Major { get; set; } = string.Empty;


        [Display(Name = "Supervisor")]
        [Column(TypeName = "varchar(250)")]
        public string Supervisor { get; set; } = string.Empty;


        [Display(Name = "Email")]
        [Column(TypeName = "varchar(250)")]
        public string Email { get; set; } = string.Empty;


        public int DepartmentId { get; set; }
        [ForeignKey("DepartmentId")]
        public Department? Department { get; set; }

        public int UniversityId { get; set; }
        [ForeignKey("UniversityId")]
        public University? University { get; set; }


    }
}
