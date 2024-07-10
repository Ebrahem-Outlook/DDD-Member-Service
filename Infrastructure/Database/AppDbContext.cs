using Application.Core.Abstractions.Data;
using Domain.Core.BaseType;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.Database;

public sealed class AppDbContext : DbContext, IDbContext, IUnitOfWork
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public new Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public new DbSet<TEntity> Set<TEntity>() where TEntity : Entity
    {
        throw new NotImplementedException();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
}
