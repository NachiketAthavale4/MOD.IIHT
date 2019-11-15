using Microsoft.EntityFrameworkCore;
using MOD.TrainingService.Models;

namespace MOD.TrainingService.Contexts
{
    public class TrainingContext : DbContext
    {
        public TrainingContext(DbContextOptions<TrainingContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>().HasIndex(usr => usr.UserName).IsUnique();
        }

        public DbSet<Training> Trainings { get; set; }
        public DbSet<Technology> Technologies { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
