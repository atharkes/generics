using Generics.Infrastructure.Interfaces;
using Generics.Specifications.EntityFramework;
using Generics.Specifications.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Generics.Infrastructure.EntityFramework.Repositories {
    public class GenericDbContextRepository<T> : IRepository<T> where T : class {
        private readonly DbContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public GenericDbContextRepository(DbContext dbContext) {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }

        public async Task Add(T entity) {
            await _dbSet.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddRange(IEnumerable<T> entities) {
            await _dbSet.AddRangeAsync(entities);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> Any()
            => await _dbSet.AnyAsync();

        public async Task<bool> Any<TResult>(ISpecification<T, TResult> specification)
            => await _dbSet.Apply(specification).AnyAsync();

        public async Task<uint> Count()
            => (uint)await _dbSet.CountAsync();

        public async Task<uint> Count<TResult>(ISpecification<T, TResult> specification)
            => (uint)await _dbSet.Apply(specification).CountAsync();

        public async Task<T?> Find(uint id)
            => await _dbSet.FindAsync(id);

        public async Task<T> First()
            => await _dbSet.FirstAsync();

        public async Task<T?> FirstOrDefault()
            => await _dbSet.FirstOrDefaultAsync();

        public async Task<TResult> First<TResult>(ISpecification<T, TResult> specification)
            => await _dbSet.Apply(specification).FirstAsync();

        public async Task<TResult?> FirstOrDefault<TResult>(ISpecification<T, TResult> specification)
            => await _dbSet.Apply(specification).FirstOrDefaultAsync();

        public async Task<T> Get(uint id)
            => await Find(id) ?? throw new KeyNotFoundException($"{nameof(T)} not found with id ({id})");

        public async Task<IEnumerable<T>> List()
            => await _dbSet.ToListAsync();

        public async Task<IEnumerable<TResult>> List<TResult>(ISpecification<T, TResult> specification)
            => await _dbSet.Apply(specification).ToListAsync();

        public async Task Remove(T entity) {
            _dbSet.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Remove(uint id)
            => await Remove(await Get(id));

        public async Task RemoveRange(IEnumerable<T> entities) {
            _dbSet.RemoveRange(entities);
            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveRange(IEnumerable<uint> ids)
            => await RemoveRange(await Task.WhenAll(ids.Select(Get)));

        public async Task<TResult> Single<TResult>(ISpecification<T, TResult> specification)
            => await _dbSet.Apply(specification).SingleAsync();

        public async Task<TResult?> SingleOrDefault<TResult>(ISpecification<T, TResult> specification)
            => await _dbSet.Apply(specification).SingleOrDefaultAsync();

        public async Task Update(T entity) {
            _dbSet.Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateRange(IEnumerable<T> entities) {
            _dbSet.UpdateRange(entities);
            await _dbContext.SaveChangesAsync();
        }
    }
}
