using System.Security;
using Microsoft.EntityFrameworkCore;
using SaveUp.Web.API.Entities;
using SaveUp.Web.API.Exception;
using SaveUp.Web.API.Extensions;

namespace SaveUp.Web.API;

public class SaveUpDbContext : DbContext
{
    private readonly IUser user;

    public SaveUpDbContext()
    {
    }

    public SaveUpDbContext(DbContextOptions<SaveUpDbContext> options)
    : base(options)
    {
    }

    public SaveUpDbContext(DbContextOptions<SaveUpDbContext> options, IUser user)
    : base(options)
    {
        this.user = user;
    }

    public DbSet<User> Users { get; set; }

    public DbSet<Entrie> Entries { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>().HasIndex(u => u.Username).IsUnique();

        modelBuilder.ApplyGlobalFilters<ITenantEntity>(e => e.TenantId == this.user.Id);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        try
        {
            this.SetTenantId();

            return base.SaveChangesAsync(cancellationToken);
        }
        catch (System.Exception ex)
        {
            throw new SaveUpDatabaseException(ex.Message, ex);
        }
    }

    private void SetTenantId()
    {
        var entries = this.ChangeTracker.Entries().Where(x => x.State is EntityState.Added or EntityState.Modified);

        foreach (var entry in entries)
        {
            var entity = entry.Entity as ITenantEntity;

            if (entry.State == EntityState.Added && entity is not null)
            {
                entity.TenantId = this.user.Id;
            }
            else if (entry.State == EntityState.Modified && entity is not null)
            {
            }

            if (entity is not null && entity.TenantId != this.user.Id)
            {
                throw new SecurityException($"Tenant mismatch {entity.TenantId} User {this.user.Id} {this.user.Username}");
            }
        }
    }
}