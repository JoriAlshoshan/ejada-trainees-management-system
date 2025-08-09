using System.ComponentModel.DataAnnotations;

namespace EjadaTraineesManagementSystem.ViewModels
{
    public class ChangePasswordViewMiodel
    {
        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "The password Must between 8 - 20 charcter lang")]
        [DataType(DataType.Password)]
        [Compare("ConfirmPassword", ErrorMessage = "Password dose not match")]
        public string newPassword { get; set; }

        [Required(ErrorMessage = "Confirm Password is Required")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
