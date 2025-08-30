using Microsoft.EntityFrameworkCore;
using OpenProfileAPI.Application.Common.Interfaces;
using OpenProfileAPI.Domain.Common;
using OpenProfileAPI.Infrastructure.Data;

namespace OpenProfileAPI.Infrastructure.Repositories;

public class DataRepository<T> : QueryRepository<T>, IDataRepository<T> where T : BaseAuditableEntity
{
    //private readonly ApplicationDbContext _applicationDbContext;

    public DataRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
    {
        _query = _dbSet;
    }

    public void Add(T entity, int addedBy)
    {
        var dateTime = DateTime.UtcNow;

        entity.CreatedBy = addedBy;
        entity.LastModifiedBy = addedBy;
        entity.Created = dateTime;
        entity.LastModified = dateTime;
        _applicationDbContext.Add(entity);

        foreach (var entry in _applicationDbContext.ChangeTracker.Entries())
        {
            //_logger.LogDebug($"Entity: {entry.Entity.GetType().Name}, State: {entry.State}");
        }
    }

    public void Attach(T entity, int attachedBy)
    {
         var dateTime = DateTime.UtcNow;

        entity.LastModifiedBy = attachedBy;
        entity.LastModified = dateTime;
        _applicationDbContext.Attach(entity);
        _applicationDbContext.Entry(entity).State = EntityState.Modified;
    }

    //public void Remove(T entity, Guid removedBy)
    //{
    //    entity.DeletedBy = removedBy;
    //    entity.DeletedAt = DateTime.UtcNow;
    //}

    public void Delete(T entity)
    {
        _applicationDbContext.Remove(entity);
    }
}

