using Microsoft.EntityFrameworkCore;
using web.Entities;

namespace web.Data 
{
    public class DatabaseContext : DbContext
    {
        protected readonly IConfiguration configuration;
        public DatabaseContext(IConfiguration configuration) 
        {
            this.configuration = configuration;
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options) 
        {
            options.UseSqlServer(configuration.GetConnectionString("AzureContext"));
        }

        protected override void OnModelCreating (ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                        .ToTable("User")
                        .Property(u => u.Id)
                        .ValueGeneratedOnAdd();
            modelBuilder.Entity<Post>()
                        .ToTable("Post")
                        .Property(p => p.Id)
                        .ValueGeneratedOnAdd();;
            modelBuilder.Entity<Comment>()
                        .ToTable("Comment")
                        .Property(c => c.Id)
                        .ValueGeneratedOnAdd();;

        }
    }
}