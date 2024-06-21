using DMReservation.Domain.Interfaces.Infra;
using DMReservation.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace DMReservation.Infra.Repositories
{
    public abstract class RepositoryGeneric<TEntity, TId> : IRepositoryGeneric<TEntity, TId>
        where TEntity : class
        where TId : struct
    {
        protected readonly DataContext _context;
        protected DbSet<TEntity> _entities;

        public RepositoryGeneric(DataContext dataContext)
        {
            _context = dataContext;
            _entities = _context.Set<TEntity>();
        }


        public async Task AddAsync(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Added;
            await _context.AddAsync(entity);
        }

        public async Task<bool> CommitAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<TEntity> FindIdAsync(TId id)
        {
            return await _context.FindAsync<TEntity>(id);
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await _entities.ToListAsync();
        }

        public void Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.Update(entity);
        }

        public void Remove(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Deleted;
            _context.Remove(entity);
        }
    }
}
