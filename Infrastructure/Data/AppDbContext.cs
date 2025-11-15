// Infrastructure/AppDbContext.cs
using Infrastructure.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUserEntity, ApplicationRoleEntity, string>
    {
        public DbSet<StudentEntity> Students { get; set; }
        public DbSet<TeacherEntity> Teachers { get; set; }
        public DbSet<DepartmentEntity> Departments { get; set; }
        public DbSet<ErrorLogEntity> ErrorLogs { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Rename main tables
            builder.Entity<ApplicationUserEntity>().ToTable("Users");
            builder.Entity<ApplicationRoleEntity>().ToTable("Roles");

            // Rename related tables with explicit generic type <string>
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");

            // Department Table
            builder.Entity<DepartmentEntity>().Property(d => d.Created)
                .HasDefaultValueSql("GETDATE()");

            foreach (var relationship in builder.Model.GetEntityTypes().
                SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }

    }
}
