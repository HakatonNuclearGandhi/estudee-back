using api.Models.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Task = api.Models.Domain.Task;

namespace api.Data;

public class ApplicationDbContext: IdentityDbContext<User>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }

    public DbSet<Subject> Subjects { get; set; }

    public DbSet<Task> Tasks { get; set; }
    
    public DbSet<Status> Status { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<Status>().HasKey(s => s.statusId);
        builder.Entity<Subject>().HasKey(s => s.subjectId);
        builder.Entity<Task>().HasKey(s => s.taskId);
    }
}