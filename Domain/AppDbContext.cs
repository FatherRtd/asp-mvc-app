using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Domain
{
	public class AppDbContext : DbContext
	{
		public DbSet<User> Users { get; set; } = null!;
		public DbSet<Team> Teams { get; set; } = null!;
		public DbSet<Activity> Activities { get; set; } = null!;
		public DbSet<ActivityType> ActivityTypes { get; set; } = null!;

		public AppDbContext(DbContextOptions options) : base(options)
		{
			
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<User>().ToTable("user");

			modelBuilder.Entity<Team>().ToTable("team");

			modelBuilder.Entity<Team>()
			            .HasMany(x => x.Members)
			            .WithMany(x => x.Teams)
			            .UsingEntity("team_user");

			modelBuilder.Entity<Team>()
			            .HasOne(x => x.Owner)
			            .WithMany(x => x.OwnedTeams)
			            .HasForeignKey(x => x.OwnerId);

			modelBuilder.Entity<Activity>().ToTable("activity");

			modelBuilder.Entity<ActivityType>().ToTable("activity_type");
		}
	}
}
