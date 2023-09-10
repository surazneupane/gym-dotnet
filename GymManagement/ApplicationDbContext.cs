using GymManagement.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace GymManagement
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext() : base("demoASPEntities")
		{
		}
		public DbSet<Register> Users { get; set; }
		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Register>().ToTable("Users");
			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
			base.OnModelCreating(modelBuilder);
		}
	}
}