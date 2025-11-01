using E_Commerce.Domain.Entities.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Contracts
{
    public interface IUnitOfWork
    {
        // save change
        Task<int> SaveChangeAsync();
        IGenericRepository<TEntity,TKey> GetRepository<TEntity,TKey>()  where TEntity : BaseEntity<TKey>;
        
    }
}
