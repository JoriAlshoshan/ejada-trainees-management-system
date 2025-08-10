using Microsoft.EntityFrameworkCore;
using EjadaTraineesManagementSystem.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace EjadaTraineesManagementSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext<Users>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Trainee> Trainees { get; set; }
        public DbSet<University> Universities { get; set; }
        public DbSet<SupervisorTrainee> SupervisorTrainees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SupervisorTrainee>()
                .HasKey(st => new { st.SupervisorId, st.TraineeId });

            modelBuilder.Entity<SupervisorTrainee>()
                .HasOne(st => st.SupervisorUser)
                .WithMany(s => s.SupervisorTrainees)
                .HasForeignKey(st => st.SupervisorId);

            modelBuilder.Entity<SupervisorTrainee>()
                .HasOne(st => st.Trainee)
                .WithMany(t => t.SupervisorTrainees)
                .HasForeignKey(st => st.TraineeId);
        }
    }
}
