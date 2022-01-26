using Microsoft.EntityFrameworkCore;
using ProjectManager.Models;

namespace ProjectManager.Data
{
    public class ManagerAppDbContext : DbContext
    {
        public DbSet<Programmer> Programmers { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<TheManager> Manager { get; set; }

        public ManagerAppDbContext(DbContextOptions<ManagerAppDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TheManager>()
                .HasKey(c => new { c.ProgrammerId, c.ProjectId });
        }      
    }
}
