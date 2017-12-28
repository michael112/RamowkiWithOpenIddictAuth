using Microsoft.EntityFrameworkCore;

using RamowkiWithOpenIddictAuth.Models.Authentication;
using RamowkiWithOpenIddictAuth.Models;

namespace RamowkiWithOpenIddictAuth
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Programme> Programmes { get; set; }
        public DbSet<ScheduledProgramme> ScheduledProgrammes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRoleJoin>().HasOne(j => j.User).WithMany(u => u.RoleJoins);
            modelBuilder.Entity<UserRoleJoin>().HasOne(j => j.Role).WithMany();
            modelBuilder.Entity<ScheduledProgramme>().OwnsOne(c => c.BeginTime);
            modelBuilder.Entity<ScheduledProgramme>().HasOne(s => s.Programme).WithMany();
            modelBuilder.Entity<ScheduledProgramme>().HasOne(s => s.Day).WithMany();
            modelBuilder.Entity<WeekDay>();
            modelBuilder.Entity<DateDay>();
            base.OnModelCreating(modelBuilder);
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
    }
}
