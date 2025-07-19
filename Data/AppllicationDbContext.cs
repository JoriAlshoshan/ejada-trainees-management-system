using Microsoft.EntityFrameworkCore;
using System.Security.Principal;
using EjadaTraineesManagementSystem.Models;

namespace EjadaTraineesManagementSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Trainee> Trainees { get; set; }
        public DbSet<University> Universities { get; set; }
    }
}
