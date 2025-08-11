using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EjadaTraineesManagementSystem.Models
{
    [Table("Trainees", Schema = "HR")]
    public class Trainee
    {
        [Key]
        [Display(Name = "ID")]
        public int TraineeId { get; set; }

        [Display(Name = "TraineeName")]
        [Column(TypeName = "varchar(250)")]
        [Required(ErrorMessage = "This field is required.")]
        public string TraineeName { get; set; } = string.Empty;

        [Display(Name = "ImageUser")]
        [Column(TypeName = "varchar(250)")]
        public string? ImageUser { get; set; }

        [Display(Name = "StartDate")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "This field is required.")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? StartDate { get; set; }

        [Display(Name = "PhoneNumber")]
        [MaxLength(10, ErrorMessage = "Phone Number must be 10 characters.")]
        [MinLength(10, ErrorMessage = "Phone Number must be 10 characters.")]
        [Column(TypeName = "varchar(10)")]
        [Required(ErrorMessage = "This field is required.")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Display(Name = "Major")]
        [Column(TypeName = "varchar(250)")]
        [Required(ErrorMessage = "This field is required.")]
        public string Major { get; set; } = string.Empty;

        [Display(Name = "Email")]
        [Column(TypeName = "varchar(250)")]
        [Required(ErrorMessage = "This field is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "This field is required.")]
        public int? DepartmentId { get; set; }
        [ForeignKey("DepartmentId")]
        public Department? Department { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        public int? UniversityId { get; set; }
        [ForeignKey("UniversityId")]
        public University? University { get; set; }

        public ICollection<SupervisorTrainee> SupervisorTrainees { get; set; } = new List<SupervisorTrainee>();

        [NotMapped]
        public IEnumerable<Users> Supervisors => SupervisorTrainees?.Select(st => st.SupervisorUser);

        [NotMapped]
        public List<string> SupervisorIds { get; set; } = new List<string>();
    }
}