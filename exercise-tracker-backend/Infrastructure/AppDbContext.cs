using Infrastructure.DatabaseModels;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<User>? Users { get; set; }
        public DbSet<Workout>? Workouts { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Workout>()
                        .HasOne(w => w.User)
                        .WithMany()
                        .HasForeignKey(w => w.UserId)
                        .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
