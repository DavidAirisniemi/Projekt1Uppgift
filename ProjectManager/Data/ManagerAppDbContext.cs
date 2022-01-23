using Microsoft.EntityFrameworkCore;
using ProjectManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
