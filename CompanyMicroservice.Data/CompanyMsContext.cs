namespace CompanyMicroservice.Data
{
    using CompanyMicroservice.Models;
    using Microsoft.EntityFrameworkCore;

    public class CompanyMsContext : DbContext
    {
        public CompanyMsContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<ExperienceLevel> ExperienceLevels { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ExperienceLevel>()
                .HasData
                (
                    new ExperienceLevel {Id = 1, Type = "Intern"},
                    new ExperienceLevel {Id = 2, Type = "Junior"},
                    new ExperienceLevel {Id = 3, Type = "Mid-level"},
                    new ExperienceLevel {Id = 4, Type = "Senior"}
                );
        }
    }
}
