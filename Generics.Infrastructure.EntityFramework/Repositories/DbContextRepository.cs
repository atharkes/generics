using Generics.Infrastructure.Interfaces;
using Generics.Specifications.EntityFramework;
using Generics.Specifications.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Generics.Infrastructure.EntityFramework.Repositories
{
    public class DbContextRepository : IRepository
    {
        private readonly DbContext _dbContext;

        public DbContextRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(object item, CancellationToken cancellationToken = default)
        {
            await _dbContext.AddAsync(item, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task Add<T>(T item, CancellationToken cancellationToken = default) where T : class
        {
            await _dbContext.Set<T>().AddAsync(item, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task AddRange(IEnumerable<object> items, CancellationToken cancellationToken = default)
        {
            await _dbContext.AddRangeAsync(items, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task AddRange<T>(IEnumerable<T> items, CancellationToken cancellationToken = default) where T : class
        {
            await _dbContext.Set<T>().AddRangeAsync(items, cancellationToken);
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

        public async Task<bool> Contains<T>(uint id, CancellationToken cancellationToken = default) where T : class
            => await Find<T>(id, cancellationToken) is not null;

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

        public async Task Remove(object item, CancellationToken cancellationToken = default)
        {
            _dbContext.Remove(item);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task Remove<T>(T item, CancellationToken cancellationToken = default) where T : class
        {
            _dbContext.Set<T>().Remove(item);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task Remove<T>(uint id, CancellationToken cancellationToken = default) where T : class
            => await Remove(await Get<T>(id, cancellationToken), cancellationToken);

        public async Task RemoveRange(IEnumerable<object> items, CancellationToken cancellationToken = default)
        {
            _dbContext.RemoveRange(items);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task RemoveRange<T>(IEnumerable<T> items, CancellationToken cancellationToken = default) where T : class
        {
            _dbContext.Set<T>().RemoveRange(items);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task RemoveRange<T>(IEnumerable<uint> ids, CancellationToken cancellationToken = default) where T : class
            => await RemoveRange(await Task.WhenAll(ids.Select(id => Get<T>(id, cancellationToken))), cancellationToken);

        public async Task<T> Single<T>(CancellationToken cancellationToken = default) where T : class
            => await _dbContext.Set<T>().SingleAsync(cancellationToken);

        public async Task<TResult> Single<T, TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default) where T : class
            => await _dbContext.Set<T>().Apply(specification).SingleAsync(cancellationToken);

        public async Task<T?> SingleOrDefault<T>(CancellationToken cancellationToken = default) where T : class
            => await _dbContext.Set<T>().SingleOrDefaultAsync(cancellationToken);

        public async Task<TResult?> SingleOrDefault<T, TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default) where T : class
            => await _dbContext.Set<T>().Apply(specification).SingleOrDefaultAsync(cancellationToken);

        public async Task Update<T>(T item, CancellationToken cancellationToken = default) where T : class
        {
            _dbContext.Set<T>().Update(item);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateRange<T>(IEnumerable<T> items, CancellationToken cancellationToken = default) where T : class
        {
            _dbContext.Set<T>().UpdateRange(items);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
