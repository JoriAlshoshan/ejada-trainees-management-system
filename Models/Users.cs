using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
namespace EjadaTraineesManagementSystem.Models
{
    public class Users :IdentityUser
    {
        public string fullName {  get; set; }

    }
}
