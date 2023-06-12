using Generics.Infrastructure.Interfaces;
using Generics.Specifications.EntityFramework;
using Generics.Specifications.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Generics.Infrastructure.EntityFramework.Repositories {
    public class DbContextRepository : IRepository {
        private readonly DbContext _dbContext;

        public DbContextRepository(DbContext dbContext) {
            _dbContext = dbContext;
        }

        public async Task Add(object entity, CancellationToken cancellationToken = default) {
            await _dbContext.AddAsync(entity, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task Add<T>(T entity, CancellationToken cancellationToken = default) where T : class {
            await _dbContext.Set<T>().AddAsync(entity, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task AddRange(IEnumerable<object> entities, CancellationToken cancellationToken = default) {
            await _dbContext.AddRangeAsync(entities, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task AddRange<T>(IEnumerable<T> entities, CancellationToken cancellationToken = default) where T : class {
            await _dbContext.Set<T>().AddRangeAsync(entities, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> Any<T>(CancellationToken cancellationToken = default) where T : class
            => await _dbContext.Set<T>().AnyAsync(cancellationToken);

        public async Task<bool> Any<T, TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default) where T : class
            => await _dbContext.Set<T>().Apply(specification).AnyAsync(cancellationToken);

        public async Task<uint> Count<T>(CancellationToken cancellationToken = default) where T : class
            => (uint)await _dbContext.Set<T>().CountAsync(cancellationToken);

        public async Task<uint> Count<T, TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default) where T : class
            => (uint)await _dbContext.Set<T>().Apply(specification).CountAsync(cancellationToken);

        public async Task<T?> Find<T>(uint id, CancellationToken cancellationToken = default) where T : class
            => await _dbContext.FindAsync<T>(id, cancellationToken);

        public async Task<T> First<T>(CancellationToken cancellationToken = default) where T : class
            => await _dbContext.Set<T>().FirstAsync(cancellationToken);

        public async Task<TResult> First<T, TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default) where T : class
            => await _dbContext.Set<T>().Apply(specification).FirstAsync(cancellationToken);

        public async Task<T?> FirstOrDefault<T>(CancellationToken cancellationToken = default) where T : class
            => await _dbContext.Set<T>().FirstOrDefaultAsync(cancellationToken);

        public async Task<TResult?> FirstOrDefault<T, TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default) where T : class
            => await _dbContext.Set<T>().Apply(specification).FirstOrDefaultAsync(cancellationToken);

        public async Task<T> Get<T>(uint id, CancellationToken cancellationToken = default) where T : class
            => await Find<T>(id, cancellationToken) ?? throw new KeyNotFoundException($"{nameof(T)} not found with id ({id})");

        public async Task<IEnumerable<T>> List<T>(CancellationToken cancellationToken = default) where T : class
            => await _dbContext.Set<T>().ToListAsync(cancellationToken);

        public async Task<IEnumerable<TResult>> List<T, TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default) where T : class
            => await _dbContext.Set<T>().Apply(specification).ToListAsync(cancellationToken);

        public async Task Remove(object entity, CancellationToken cancellationToken = default) {
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task Remove<T>(T entity, CancellationToken cancellationToken = default) where T : class {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task Remove<T>(uint id, CancellationToken cancellationToken = default) where T : class
            => await Remove(await Get<T>(id, cancellationToken), cancellationToken);

        public async Task RemoveRange(IEnumerable<object> entities, CancellationToken cancellationToken = default) {
            _dbContext.RemoveRange(entities);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task RemoveRange<T>(IEnumerable<T> entities, CancellationToken cancellationToken = default) where T : class {
            _dbContext.Set<T>().RemoveRange(entities);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task RemoveRange<T>(IEnumerable<uint> ids, CancellationToken cancellationToken = default) where T : class
            => await RemoveRange(await Task.WhenAll(ids.Select(id => Get<T>(id, cancellationToken))), cancellationToken);

        public async Task<TResult> Single<T, TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default) where T : class
            => await _dbContext.Set<T>().Apply(specification).SingleAsync(cancellationToken);

        public async Task<TResult?> SingleOrDefault<T, TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default) where T : class
            => await _dbContext.Set<T>().Apply(specification).SingleOrDefaultAsync(cancellationToken);

        public async Task Update<T>(T entity, CancellationToken cancellationToken = default) where T : class {
            _dbContext.Set<T>().Update(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateRange<T>(IEnumerable<T> entities, CancellationToken cancellationToken = default) where T : class {
            _dbContext.Set<T>().UpdateRange(entities);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
