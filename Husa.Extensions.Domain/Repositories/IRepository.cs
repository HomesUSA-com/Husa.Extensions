namespace Husa.Extensions.Domain.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Husa.Extensions.Domain.Entities;

    public interface IRepository<TEntity>
        where TEntity : Entity
    {
        Task<TEntity> GetById(Guid id, bool filterByCompany = false);

        Task<TEntity> AddAsync(TEntity entity);

        Task UpdateAsync(TEntity entity);

        Task SaveChangesAsync();

        Task SaveChangesAsync(TEntity entity);

        void Attach(TEntity entity);

        void Attach(IEnumerable<TEntity> entities);
    }
}
