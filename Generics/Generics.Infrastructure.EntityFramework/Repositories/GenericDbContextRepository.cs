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

        public async Task Add(T entity, CancellationToken cancellationToken = default) {
            await _dbSet.AddAsync(entity, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task AddRange(IEnumerable<T> entities, CancellationToken cancellationToken = default) {
            await _dbSet.AddRangeAsync(entities, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> Any(CancellationToken cancellationToken = default)
            => await _dbSet.AnyAsync(cancellationToken);

        public async Task<bool> Any<TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default)
            => await _dbSet.Apply(specification).AnyAsync(cancellationToken);

        public async Task<uint> Count(CancellationToken cancellationToken = default)
            => (uint)await _dbSet.CountAsync(cancellationToken);

        public async Task<uint> Count<TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default)
            => (uint)await _dbSet.Apply(specification).CountAsync(cancellationToken);

        public async Task<T?> Find(uint id, CancellationToken cancellationToken = default)
            => await _dbSet.FindAsync(new object[] { id }, cancellationToken: cancellationToken);

        public async Task<T> First(CancellationToken cancellationToken = default)
            => await _dbSet.FirstAsync(cancellationToken);

        public async Task<T?> FirstOrDefault(CancellationToken cancellationToken = default)
            => await _dbSet.FirstOrDefaultAsync(cancellationToken);

        public async Task<TResult> First<TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default)
            => await _dbSet.Apply(specification).FirstAsync(cancellationToken);

        public async Task<TResult?> FirstOrDefault<TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default)
            => await _dbSet.Apply(specification).FirstOrDefaultAsync(cancellationToken);

        public async Task<T> Get(uint id, CancellationToken cancellationToken = default)
            => await Find(id, cancellationToken) ?? throw new KeyNotFoundException($"{nameof(T)} not found with id ({id})");

        public async Task<IEnumerable<T>> List(CancellationToken cancellationToken = default)
            => await _dbSet.ToListAsync(cancellationToken);

        public async Task<IEnumerable<TResult>> List<TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default)
            => await _dbSet.Apply(specification).ToListAsync(cancellationToken);

        public async Task Remove(T entity, CancellationToken cancellationToken = default) {
            _dbSet.Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task Remove(uint id, CancellationToken cancellationToken = default)
            => await Remove(await Get(id, cancellationToken), cancellationToken);

        public async Task RemoveRange(IEnumerable<T> entities, CancellationToken cancellationToken = default) {
            _dbSet.RemoveRange(entities);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task RemoveRange(IEnumerable<uint> ids, CancellationToken cancellationToken = default)
            => await RemoveRange(await Task.WhenAll(ids.Select(id => Get(id, cancellationToken))), cancellationToken);

        public async Task<TResult> Single<TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default)
            => await _dbSet.Apply(specification).SingleAsync(cancellationToken);

        public async Task<TResult?> SingleOrDefault<TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default)
            => await _dbSet.Apply(specification).SingleOrDefaultAsync(cancellationToken);

        public async Task Update(T entity, CancellationToken cancellationToken = default) {
            _dbSet.Update(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateRange(IEnumerable<T> entities, CancellationToken cancellationToken = default) {
            _dbSet.UpdateRange(entities);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
