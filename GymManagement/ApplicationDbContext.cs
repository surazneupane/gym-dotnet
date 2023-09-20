using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using GymManagement.Models; // Make sure you include the correct namespace for your models

namespace GymManagement
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("Data Source=localhost;Initial Catalog=GymDBNew;Integrated Security=True;")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<ApplicationDbContext>());

        }

        public DbSet<Register> Users { get; set; }
        public DbSet<MembershipType> MembershipTypes { get; set; }
        public DbSet<Membership> Memberships { get; set; }
        public DbSet<TrainingSession> TrainingSessions { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<InterestRecord> InterestRecords { get; set; }

        public DbSet<Attendance> Attendances { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Register>().ToTable("Users");
            modelBuilder.Entity<MembershipType>().ToTable("MembershipTypes");
            modelBuilder.Entity<Membership>().ToTable("Memberships");
            modelBuilder.Entity<TrainingSession>().ToTable("TrainingSessions");
            modelBuilder.Entity<Class>().ToTable("Classes"); 

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }
}

