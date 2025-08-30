using System.Reflection;
using System.Reflection.Emit;
using OpenProfileAPI.Application.Common.Interfaces;
using OpenProfileAPI.Domain.Common;
using OpenProfileAPI.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace OpenProfileAPI.Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<int>, int>, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public new DbSet<User> Users => base.Set<User>();
    public DbSet<UserProfile> UserProfile => Set<UserProfile>();
    public DbSet<Role> Role => Set<Role>();
    public DbSet<Right> Right => Set<Right>();
    public DbSet<RoleRight> RoleRight => Set<RoleRight>();
    public DbSet<UserRole> UserRole => Set<UserRole>();
    public DbSet<UserRight> UserRight => Set<UserRight>();
    public DbSet<AuthPolicy> AuthPolicies => Set<AuthPolicy>();
    public DbSet<LoginAttempts> LoginAttempts => Set<LoginAttempts>();
    public DbSet<Category> Category => Set<Category>();
    public DbSet<EntityType> EntityType => Set<EntityType>();
    public DbSet<UserFile> UserFile => Set<UserFile>();
    public DbSet<FileStore> FileStore => Set<FileStore>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            if (!entityType.IsOwned())
            {
                builder.Entity(entityType.ClrType).ToTable(entityType.ClrType.Name);
            }
        }

        // Disable Identity tables if truly unused
        builder.Entity<IdentityRole<int>>().ToTable((string?)null);
        builder.Entity<IdentityUserRole<int>>().ToTable((string?)null);
        builder.Entity<IdentityUserClaim<int>>().ToTable((string?)null);
        builder.Entity<IdentityUserToken<int>>().ToTable((string?)null);
        builder.Entity<IdentityRoleClaim<int>>().ToTable((string?)null);
        builder.Entity<IdentityUserLogin<int>>().ToTable((string?)null);

        // Configure User relationships
        builder.Entity<User>(entity =>
        {
            entity.HasOne(u => u.UserProfile)
                .WithOne(up => up.User)
                .HasForeignKey<UserProfile>(up => up.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            entity.HasMany(u => u.UserRights)
                .WithOne(ur => ur.User)
                .HasForeignKey(ur => ur.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            entity.HasMany(u => u.UserRoles)
                .WithOne(ur => ur.User)
                .HasForeignKey(ur => ur.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        });

        // Configure Role relationships
        builder.Entity<Role>(entity =>
        {
            entity.HasMany(r => r.RoleRights)
                .WithOne(rr => rr.Role)
                .HasForeignKey(rr => rr.RoleId)
                .OnDelete(DeleteBehavior.NoAction);

            entity.HasMany(r => r.UserRoles)
                .WithOne(ur => ur.Role)
                .HasForeignKey(ur => ur.RoleId)
                .OnDelete(DeleteBehavior.NoAction);
        });

        // Configure Right relationships
        builder.Entity<Right>(entity =>
        {
            entity.HasMany(r => r.RoleRights)
                .WithOne(rr => rr.Right)
                .HasForeignKey(rr => rr.RightId)
                .OnDelete(DeleteBehavior.NoAction);

            entity.HasMany(r => r.UserRights)
                .WithOne(ur => ur.Right)
                .HasForeignKey(ur => ur.RightId)
                .OnDelete(DeleteBehavior.NoAction);
        });

        // BaseAuditableEntity navigation configuration
        //foreach (var entityType in builder.Model.GetEntityTypes())
        //{
        //    if (typeof(BaseAuditableEntity).IsAssignableFrom(entityType.ClrType))
        //    {
        //        builder.Entity(entityType.ClrType).HasOne(typeof(User), nameof(BaseAuditableEntity.CreatedBy))
        //            .WithMany()
        //            .HasForeignKey(nameof(BaseAuditableEntity.CreatedBy))
        //            .OnDelete(DeleteBehavior.Restrict);

        //        builder.Entity(entityType.ClrType).HasOne(typeof(User), nameof(BaseAuditableEntity.LastModifiedBy))
        //            .WithMany()
        //            .HasForeignKey(nameof(BaseAuditableEntity.LastModifiedBy))
        //            .OnDelete(DeleteBehavior.Restrict);
        //    }
        //}

        //builder.Entity<EntityType>(entity =>
        //{
        //    entity.HasKey(e => e.Id);

        //    entity.Property(e => e.Id)
        //        .ValueGeneratedNever();
        //});

        //builder.Entity<Category>(entity =>
        //{
        //    entity.HasKey(e => e.Id);

        //    entity.Property(e => e.Id)
        //        .ValueGeneratedNever();
        //});
    }
}
