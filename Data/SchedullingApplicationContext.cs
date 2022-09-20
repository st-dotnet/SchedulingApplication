using Microsoft.EntityFrameworkCore;
using SchedulingApplication.Data.Entities;

namespace SchedulingApplication.Data
{
    public class SchedulingApplicationContext : DbContext
    {
        public SchedulingApplicationContext() { }
        public SchedulingApplicationContext(DbContextOptions<SchedulingApplicationContext> options) : base(options) { }
        public virtual DbSet<User> Users => Set<User>();
        public virtual DbSet<Role> Roles => Set<Role>();
        public virtual DbSet<Team> Teams => Set<Team>();
        public virtual DbSet<GameSchedule> GameSchedules => Set<GameSchedule>();
        public virtual DbSet<PracticeSchedule> PracticeSchedules => Set<PracticeSchedule>();
        public virtual DbSet<FieldLocation> FieldLocations => Set<FieldLocation>();
        public virtual DbSet<GameType> GameTypes => Set<GameType>();
        public virtual DbSet<Player> Players => Set<Player>();

        public virtual DbSet<Coach> Coaches => Set<Coach>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Team>(x => {
                string tableName = "Team";
                x.ToTable(tableName);
                x.HasKey(a => a.Id);
            });

            //Player
            modelBuilder.Entity<Player>(x => {
                string tableName = "Player";
                x.ToTable(tableName);
                x.HasKey(a => a.Id);
                x.HasOne(x => x.Team);
            });

            modelBuilder.Entity<GameSchedule>(x => {
                string tableName = "GameSchedule";
                x.ToTable(tableName);
                x.HasKey(a => a.Id);

                // to be able to have multiple addresses
                x.HasOne(a => a.PlayingAgainstTeam)
                    .WithMany(a => a.PlayingAgainstSchedules).HasForeignKey(x => x.PlayingAgainstId);

                x.HasOne(a => a.Team)
                    .WithMany(a => a.HomeSchedules).HasForeignKey(x => x.TeamId);
            });
        }
    }
}

