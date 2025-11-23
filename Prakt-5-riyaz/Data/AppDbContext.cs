namespace Prakt_5_riyaz.Data;
using Microsoft.EntityFrameworkCore;
using Prakt_5_riyaz.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Project> Projects { get; set; }
    public DbSet<ProjectTask> Tasks { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Comment> Comments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Связь Проект - Задачи
        modelBuilder.Entity<Project>()
            .HasMany(p => p.Tasks)
            .WithOne(t => t.Project)
            .HasForeignKey(t => t.ProjectID);

        // Связь Сотрудник - Задачи
        modelBuilder.Entity<Employee>()
            .HasMany(e => e.Tasks)
            .WithOne(t => t.Employee)
            .HasForeignKey(t => t.EmployeeID);

        // Связь Задача - Комментарии
        modelBuilder.Entity<ProjectTask>()
            .HasMany(t => t.Comments)
            .WithOne(c => c.Task)
            .HasForeignKey(c => c.TaskId);

        // Связь Сотрудник - Комментарии
        modelBuilder.Entity<Employee>()
            .HasMany(e => e.Comments)
            .WithOne(c => c.Employee)
            .HasForeignKey(c => c.EmployeeId);
    }
}
