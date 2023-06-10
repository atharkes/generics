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

        public async Task Add(object entity) {
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Add<T>(T entity) where T : class {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddRange(IEnumerable<object> entities) {
            await _dbContext.AddRangeAsync(entities);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddRange<T>(IEnumerable<T> entities) where T : class {
            await _dbContext.Set<T>().AddRangeAsync(entities);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> Any<T>() where T : class
            => await _dbContext.Set<T>().AnyAsync();

        public async Task<bool> Any<T, TResult>(ISpecification<T, TResult> specification) where T : class
            => await _dbContext.Set<T>().Apply(specification).AnyAsync();

        public async Task<uint> Count<T>() where T : class
            => (uint)await _dbContext.Set<T>().CountAsync();

        public async Task<uint> Count<T, TResult>(ISpecification<T, TResult> specification) where T : class
            => (uint)await _dbContext.Set<T>().Apply(specification).CountAsync();

        public async Task<T?> Find<T>(uint id) where T : class
            => await _dbContext.FindAsync<T>(id);

        public async Task<T> First<T>() where T : class
            => await _dbContext.Set<T>().FirstAsync();

        public async Task<TResult> First<T, TResult>(ISpecification<T, TResult> specification) where T : class
            => await _dbContext.Set<T>().Apply(specification).FirstAsync();

        public async Task<T?> FirstOrDefault<T>() where T : class
            => await _dbContext.Set<T>().FirstOrDefaultAsync();

        public async Task<TResult?> FirstOrDefault<T, TResult>(ISpecification<T, TResult> specification) where T : class
            => await _dbContext.Set<T>().Apply(specification).FirstOrDefaultAsync();

        public async Task<T> Get<T>(uint id) where T : class
            => await Find<T>(id) ?? throw new KeyNotFoundException($"{nameof(T)} not found with id ({id})");

        public async Task<IEnumerable<T>> List<T>() where T : class
            => await _dbContext.Set<T>().ToListAsync();

        public async Task<IEnumerable<TResult>> List<T, TResult>(ISpecification<T, TResult> specification) where T : class
            => await _dbContext.Set<T>().Apply(specification).ToListAsync();

        public async Task Remove(object entity) {
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Remove<T>(T entity) where T : class {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Remove<T>(uint id) where T : class
            => await Remove(await Get<T>(id));

        public async Task RemoveRange(IEnumerable<object> entities) {
            _dbContext.RemoveRange(entities);
            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveRange<T>(IEnumerable<T> entities) where T : class {
            _dbContext.Set<T>().RemoveRange(entities);
            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveRange<T>(IEnumerable<uint> ids) where T : class
            => await RemoveRange(await Task.WhenAll(ids.Select(Get<T>)));

        public async Task<TResult> Single<T, TResult>(ISpecification<T, TResult> specification) where T : class
            => await _dbContext.Set<T>().Apply(specification).SingleAsync();

        public async Task<TResult?> SingleOrDefault<T, TResult>(ISpecification<T, TResult> specification) where T : class
            => await _dbContext.Set<T>().Apply(specification).SingleOrDefaultAsync();

        public async Task Update<T>(T entity) where T : class {
            _dbContext.Set<T>().Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateRange<T>(IEnumerable<T> entities) where T : class {
            _dbContext.Set<T>().UpdateRange(entities);
            await _dbContext.SaveChangesAsync();
        }
    }
}
