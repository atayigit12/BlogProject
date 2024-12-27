using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace BlogProject.DAL;

public partial class BlogDbContext : DbContext
{
    public BlogDbContext(DbContextOptions<BlogDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Blog> Blogs { get; set; }
    public virtual DbSet<BlogTag> BlogTags { get; set; }
    public virtual DbSet<Role> Roles { get; set; }
    public virtual DbSet<Tag> Tags { get; set; }
    public virtual DbSet<User> Users { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=BlogDB;Trusted_Connection=True;");
        optionsBuilder.ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Blog Entity
        modelBuilder.Entity<Blog>(entity =>
        {
            entity.HasKey(b => b.Id);
            entity.Property(b => b.Title).HasMaxLength(100).IsRequired();
            entity.Property(b => b.Content).IsRequired();
            entity.Property(b => b.Rating).HasPrecision(5, 2); // Example configuration
            entity.Property(b => b.PublishDate).HasColumnType("datetime");

            entity.HasOne(b => b.User)
                  .WithMany(u => u.Blogs)
                  .HasForeignKey(b => b.UserId)
                  .OnDelete(DeleteBehavior.ClientSetNull);
        });

        // BlogTag Entity
        modelBuilder.Entity<BlogTag>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasOne(bt => bt.Blog)
                  .WithMany(b => b.BlogTags)
                  .HasForeignKey(bt => bt.BlogId)
                  .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(bt => bt.Tag)
                  .WithMany(t => t.BlogTags)
                  .HasForeignKey(bt => bt.TagId)
                  .OnDelete(DeleteBehavior.ClientSetNull);
        });

        // User Entity
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.UserName).HasMaxLength(50).IsRequired();
            entity.Property(e => e.Password).HasMaxLength(50).IsRequired();

            entity.HasOne(u => u.Role)
                  .WithMany(r => r.Users)
                  .HasForeignKey(u => u.RoleId)
                  .OnDelete(DeleteBehavior.ClientSetNull);
        });

        // Role Entity
        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).HasMaxLength(50).IsRequired();
        });

        // Tag Entity
        modelBuilder.Entity<Tag>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).HasMaxLength(50).IsRequired();
        });

        modelBuilder.Entity<Role>().HasData(
        new Role { Id = 1, Name = "Admin" },
        new Role { Id = 2, Name = "User" }
    );

        modelBuilder.Entity<User>().HasData(
            new User { Id = 1, UserName = "admin", Password = "admin123", IsActive = true, RoleId = 1 },
            new User { Id = 2, UserName = "john", Password = "john123", IsActive = true, RoleId = 2 }
        );

        modelBuilder.Entity<Tag>().HasData(
            new Tag { Id = 1, Name = "Technology" },
            new Tag { Id = 2, Name = "Programming" }
        );

        modelBuilder.Entity<Blog>().HasData(
            new Blog { Id = 1, Title = "Introduction to EF Core", Content = "Learn EF Core basics", PublishDate = DateTime.Now, UserId = 1 }
        );

        modelBuilder.Entity<BlogTag>().HasData(
            new BlogTag { Id = 1, BlogId = 1, TagId = 1 }
        );
    }
}
