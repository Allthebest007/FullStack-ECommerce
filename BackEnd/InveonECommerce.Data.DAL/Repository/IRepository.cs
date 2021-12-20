using InveonECommerce.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InveonECommerce.Data.DAL.Repository
{
    public interface IRepository<Tentity> where Tentity : BaseEntity
    {
        //GetById with async
        Task<Tentity> GetByIdAsync(int id);
        Tentity GetById(int id);

        //GetAll with async
        Task<List<Tentity>> GetAllAsync(Expression<Func<Tentity, bool>> predicate = null);
        List<Tentity> GetAll(Expression<Func<Tentity, bool>> predicate = null);

        // Where
        IQueryable<Tentity> Get(Expression<Func<Tentity, bool>> predicate );

        //FirstorDefault with async
        Task<Tentity> FirstOrDefaultAsync(Expression<Func<Tentity, bool>> predicate);
        Tentity FirstOrDefault(Expression<Func<Tentity, bool>> predicate);

        // Add with async
        Task AddAsync(Tentity entity);
        Task AddRangeAsync(IEnumerable<Tentity> entities);
        void Add(Tentity entity);
        void AddRange(IEnumerable<Tentity> entities);

        // Remove & update
        void Remove(Tentity entity);
        void RemoveRange(ICollection<Tentity> entities);
        Tentity Update(Tentity entity);
    }
}
