using Microsoft.EntityFrameworkCore;
using Todo.Data.Entities;

namespace Todo.Data;

public class ApplicationDbContext : DbContext
{
    public virtual DbSet<TodoItem> TodoItems => Set<TodoItem>();

    public ApplicationDbContext() {}

    public ApplicationDbContext(DbContextOptions options) : base(options) {}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<TodoItem>()
        .HasKey(ti => ti.Id);

        modelBuilder.Entity<TodoItem>().Property<int>(ti => ti.Id)
        .HasColumnType("integer")
        .ValueGeneratedOnAdd();

        modelBuilder.Entity<TodoItem>().Property(ti => ti.Name)
        .HasColumnType("nvarchar(25)")
        .HasMaxLength(25)
        .IsRequired();

        modelBuilder.Entity<TodoItem>().Property(ti => ti.Notes)
        .HasColumnType("nvarchar(500)")
        .HasMaxLength(500)
        .IsRequired();

        modelBuilder.Entity<TodoItem>().Property(ti => ti.Comments)
        .HasColumnType("nvarchar(500)")
        .HasMaxLength(500);

        modelBuilder.Entity<TodoItem>().Property(ti => ti.Completed)
        .HasColumnType("bit")
        .IsRequired();

        SeedData.Seed(modelBuilder);
    }
}