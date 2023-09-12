using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using GymManagement.Models; // Make sure you include the correct namespace for your models

namespace GymManagement
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("demoASPEntities")
        {
        }

        public DbSet<Register> Users { get; set; }
        public DbSet<MembershipType> MembershipTypes { get; set; }
        public DbSet<Membership> Memberships { get; set; }
        public DbSet<TrainingSession> TrainingSessions { get; set; } // Add this DbSet for TrainingSession
        public DbSet<Class> Classes { get; set; } // Add this DbSet for Class

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Register>().ToTable("Users");
            modelBuilder.Entity<MembershipType>().ToTable("MembershipTypes");
            modelBuilder.Entity<Membership>().ToTable("Memberships");
            modelBuilder.Entity<TrainingSession>().ToTable("TrainingSessions"); // Configure TrainingSession
            modelBuilder.Entity<Class>().ToTable("Classes"); // Configure Class

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }
}

