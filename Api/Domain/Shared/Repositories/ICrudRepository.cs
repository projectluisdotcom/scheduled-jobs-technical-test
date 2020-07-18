using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain
{
    public interface ICrudRepository<in TId, TEntity>
    {
        Task Save(TEntity entity);
        Task<ICollection<TEntity>> FindAll();
        Task<TEntity> FindById(TId id);
        Task Update(TEntity car);
        Task Update(ICollection<Job> jobs);
        Task Delete(Job job);
    }
}