using OpenProfileAPI.Domain.Common;

namespace OpenProfileAPI.Application.Common.Interfaces;
public interface IDataRepository<T> : IQueryRepository<T> where T : BaseAuditableEntity
{
    void Add(T entity, int addedBy);
    void Delete(T entity);
    void Attach(T entity, int attachedBy);
}
