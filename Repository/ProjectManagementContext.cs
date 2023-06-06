using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository;
public class ProjectManagementContext : DbContext
{
    public ProjectManagementContext(DbContextOptions options) : base(options) { }

    public DbSet<Employee> Employees { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<CompanyProject> CompanyProjects { get; set; }
    public DbSet<ParticipatingProject> ParticipatingProjects { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) { }
}
