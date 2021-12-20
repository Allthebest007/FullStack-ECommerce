using InveonECommerce.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InveonECommerce.Data.DAL.Repository.EntityFramework
{
    public class EFRepository<Tentity> : IRepository<Tentity> where Tentity : BaseEntity
    {
        protected readonly EFDBContext _context;
        private readonly DbSet<Tentity> _dbset;

        public EFRepository(EFDBContext context)
        {
            _context = context;
            _dbset = _context.Set<Tentity>();
        }


        public void Add(Tentity entity)
        {
            _dbset.Add(entity);
        }

        public async Task AddAsync(Tentity entity)
        {
            await _dbset.AddAsync(entity);
        }

        public void AddRange(IEnumerable<Tentity> entities)
        {
            _dbset.AddRange(entities);
        }

        public async Task AddRangeAsync(IEnumerable<Tentity> entities)
        {
            await _dbset.AddRangeAsync(entities);
        }

        public Tentity FirstOrDefault(Expression<Func<Tentity, bool>> predicate)
        {
            return _dbset.Where(predicate).FirstOrDefault();
        }

        public async Task<Tentity> FirstOrDefaultAsync(Expression<Func<Tentity, bool>> predicate)
        {
            return await _dbset.Where(predicate).FirstOrDefaultAsync();
        }

        public IQueryable<Tentity> Get(Expression<Func<Tentity, bool>> predicate)
        {
            return _dbset.Where(predicate);
        }

        public List<Tentity> GetAll(Expression<Func<Tentity, bool>> predicate = null)
        {
            return predicate == null ?
                _dbset.ToList() :
                _dbset.Where(predicate).ToList();
        }

        public async Task<List<Tentity>> GetAllAsync(Expression<Func<Tentity, bool>> predicate = null)
        {
            return predicate == null ?
                await _dbset.ToListAsync() :
                await _dbset.Where(predicate).ToListAsync();
        }

        public Tentity GetById(int id)
        {
            return _dbset.Find(id);
        }

        public async Task<Tentity> GetByIdAsync(int id)
        {
            return await _dbset.FindAsync(id);
        }

        public void Remove(Tentity entity)
        {
            _dbset.Remove(entity);
        }

        public void RemoveRange(ICollection<Tentity> entities)
        {
            _dbset.RemoveRange(entities);
        }

        public Tentity Update(Tentity entity)
        {
            _context.Entry<Tentity>(entity).State = EntityState.Modified;
            return entity;
        }
    }
}
