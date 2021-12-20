using InveonECommerce.Data.DAL.Repository;
using InveonECommerce.Data.DAL.Repository.EntityFramework;
using InveonECommerce.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InveonECommerce.Data.DAL.UnitOfWork
{
    public class EFUnitOfWork : IUnitOfWork
    {

        private Dictionary<Type, object> _repositories;
        private EFDBContext _context;
        public EFUnitOfWork(EFDBContext context)
        {
            _context = context;
            _repositories = new Dictionary<Type, object>();
        }

        public int Commit()
        {
            return _context.SaveChanges();
        }

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity
        {
            EFRepository<TEntity> repository;

            bool isFounded = _repositories.ContainsKey(typeof(TEntity));

            if (!isFounded)
            {
                repository = new EFRepository<TEntity>(_context);
                _repositories.Add(typeof(TEntity), repository);
            }
            else
            {
                repository = (EFRepository<TEntity>)_repositories.Where(repo => repo.Key == typeof(TEntity)).FirstOrDefault().Value;
            }

            return repository;

        }

       
        
    }
}
