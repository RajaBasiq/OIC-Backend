using DAL.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }
        public DbSet<Student> Students { get; set; }
        public DbSet<Hostel> Hostels { get; set; }
        public DbSet<Library> Libraries { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<Guardian> Guardians { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Student>()
            .HasOne(s => s.Hostel)
            .WithMany(h => h.Students)
            .HasForeignKey(s => s.HostelId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Student>()
                .HasMany(s => s.Libraries)
                .WithMany(l => l.Students)
                .UsingEntity<Dictionary<string, object>>(
                    "StudentLibrary",
                    j => j
                        .HasOne<Library>()
                        .WithMany()
                        .HasForeignKey("LibraryId")
                        .OnDelete(DeleteBehavior.Cascade), // Prevent deleting Library if any Student is associated
                    j => j
                        .HasOne<Student>()
                        .WithMany()
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)  // Prevent deleting Student if any Library is associated
                );
            modelBuilder.Entity<Education>()
                .HasOne(e => e.Student)
                .WithMany(s => s.Educations)
                .HasForeignKey(e => e.StudentId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Student>()
                .HasOne(s => s.Guardian)
                .WithOne(g => g.Student)
                .HasForeignKey<Student>(s => s.GuardianId)
                .OnDelete(DeleteBehavior.Cascade);

        }


    }
}
