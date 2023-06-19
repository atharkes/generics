using Generics.Infrastructure.Interfaces;
using Generics.Specifications.EntityFramework;
using Generics.Specifications.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Generics.Infrastructure.EntityFramework.Repositories
{
    /// <summary> A <see cref="GenericDbContextRepository{T}"/> wrapping a <see cref="DbContext"/> and <see cref="DbSet{TEntity}"/>. </summary>
    /// <typeparam name="T">The type to operate on in this <see cref="GenericDbContextRepository{T}"/>.</typeparam>
    public class GenericDbContextRepository<T> : IRepository<T> where T : class
    {
        private readonly DbContext _dbContext;
        private readonly DbSet<T> _dbSet;

        /// <summary> Initialize a new instance of the <see cref="GenericDbContextRepository{T}"/> using a <paramref name="dbContext"/>. </summary>
        /// <param name="dbContext">The <see cref="DbContext"/> used in this <see cref="GenericDbContextRepository{T}"/>.</param>
        public GenericDbContextRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }

        /// <summary> Add an <paramref name="item"/> to the <see cref="GenericDbContextRepository{T}"/>. </summary>
        /// <param name="item">The <typeparamref name="T"/> to add to the <see cref="GenericDbContextRepository{T}"/>.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task"/> that represents adding the <paramref name="item"/> to the <see cref="GenericDbContextRepository{T}"/>.</returns>
        public async Task Add(T item, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddAsync(item, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        /// <summary> Add a range of <paramref name="items"/> to the <see cref="GenericDbContextRepository{T}"/>. </summary>
        /// <param name="items">The <typeparamref name="T"/>s to add to the <see cref="GenericDbContextRepository{T}"/>.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task"/> that represents adding the <paramref name="items"/> to the <see cref="GenericDbContextRepository{T}"/>.</returns>
        public async Task AddRange(IEnumerable<T> items, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddRangeAsync(items, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        /// <summary> Determine whether there is any <typeparamref name="T"/> in the <see cref="GenericDbContextRepository{T}"/>. </summary>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task{TResult}"/> that represents checking whether there is any <typeparamref name="T"/> in the <see cref="GenericDbContextRepository{T}"/>.</returns>
        public async Task<bool> Any(CancellationToken cancellationToken = default)
            => await _dbSet.AnyAsync(cancellationToken);

        /// <summary> Determine whether there is any <typeparamref name="T"/> in the <see cref="GenericDbContextRepository{T}"/> that satisfies the <paramref name="specification"/>. </summary>
        /// <typeparam name="TResult">The type of result as determined by the <paramref name="specification"/>.</typeparam>
        /// <param name="specification">The <see cref="ISpecification{TBase, TResult}"/> that specifies which items to look for.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task{TResult}"/> that represents checking whether there is any <typeparamref name="T"/> in the <see cref="GenericDbContextRepository{T}"/> that satisfies the <paramref name="specification"/>.</returns>
        public async Task<bool> Any<TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default)
            => await _dbSet.Apply(specification).AnyAsync(cancellationToken);

        /// <summary> Determine the count of <typeparamref name="T"/> items in the <see cref="GenericDbContextRepository{T}"/>. </summary>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task{TResult}"/> that represents getting the count of <typeparamref name="T"/> items in the <see cref="GenericDbContextRepository{T}"/>.</returns>
        public async Task<uint> Count(CancellationToken cancellationToken = default)
            => (uint)await _dbSet.CountAsync(cancellationToken);

        /// <summary> Determine the count of <typeparamref name="T"/> items in the <see cref="GenericDbContextRepository{T}"/> that satisfy the <paramref name="specification"/>. </summary>
        /// <typeparam name="TResult">The type of result as determined by the <paramref name="specification"/>.</typeparam>
        /// <param name="specification">The <see cref="ISpecification{TBase, TResult}"/> that specifies which items to look for.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task{TResult}"/> that represents getting the count of <typeparamref name="T"/> items in the <see cref="GenericDbContextRepository{T}"/> that satisfy the <paramref name="specification"/>.</returns>
        public async Task<uint> Count<TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default)
            => (uint)await _dbSet.Apply(specification).CountAsync(cancellationToken);

        /// <summary> Determine whether a <typeparamref name="T"/> exists in the <see cref="GenericDbContextRepository{T}"/> with the specified <paramref name="id"/>. </summary>
        /// <param name="id">The identifier to look for.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task{TResult}"/> that represents checking whether a <typeparamref name="T"/> exists in the <see cref="GenericDbContextRepository{T}"/> with the specified <paramref name="id"/>.</returns>
        public async Task<bool> Exists(uint id, CancellationToken cancellationToken = default)
            => await Find(id, cancellationToken) is not null;

        /// <summary> Find the <typeparamref name="T"/> in the <see cref="GenericDbContextRepository{T}"/> with the specified <paramref name="id"/>. </summary>
        /// <param name="id">The identifier to look for.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task{TResult}"/> that represents finding the <typeparamref name="T"/> with the specified <paramref name="id"/>, or <see langword="null"/> if there wasn't any.</returns>
        public async Task<T?> Find(uint id, CancellationToken cancellationToken = default)
            => await _dbSet.FindAsync(new object[] { id }, cancellationToken: cancellationToken);

        /// <summary> Find the first <typeparamref name="T"/> in the <see cref="GenericDbContextRepository{T}"/>. </summary>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task{TResult}"/> that represents getting the first <typeparamref name="T"/> in the <see cref="GenericDbContextRepository{T}"/>.</returns>
        public async Task<T> First(CancellationToken cancellationToken = default)
            => await _dbSet.FirstAsync(cancellationToken);

        /// <summary> Find the first <typeparamref name="T"/> in the <see cref="GenericDbContextRepository{T}"/>. </summary>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task{TResult}"/> that represents getting the first <typeparamref name="T"/> in the <see cref="GenericDbContextRepository{T}"/>, or <see langword="default"/>(<typeparamref name="T"/>) if there wasn't any.</returns>
        public async Task<T?> FirstOrDefault(CancellationToken cancellationToken = default)
            => await _dbSet.FirstOrDefaultAsync(cancellationToken);

        /// <summary> Find the first <typeparamref name="T"/> in the <see cref="GenericDbContextRepository{T}"/> that satisfies the <paramref name="specification"/>. </summary>
        /// <typeparam name="TResult">The type of result as determined by the <paramref name="specification"/>.</typeparam>
        /// <param name="specification">The <see cref="ISpecification{TBase, TResult}"/> that specifies which items to look for.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task{TResult}"/> that represents getting the first <typeparamref name="TResult"/> in the <see cref="GenericDbContextRepository{T}"/> that satisfies the <paramref name="specification"/>.</returns>
        public async Task<TResult> First<TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default)
            => await _dbSet.Apply(specification).FirstAsync(cancellationToken);

        /// <summary> Find the first <typeparamref name="T"/> in the <see cref="GenericDbContextRepository{T}"/> that satisfies the <paramref name="specification"/>. </summary>
        /// <typeparam name="TResult">The type of result as determined by the <paramref name="specification"/>.</typeparam>
        /// <param name="specification">The <see cref="ISpecification{TBase, TResult}"/> that specifies which items to look for.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task{TResult}"/> that represents getting the first <typeparamref name="TResult"/> in the <see cref="GenericDbContextRepository{T}"/> that satisfies the <paramref name="specification"/>, or <see langword="default"/>(<typeparamref name="TResult"/>) if there wasn't any.</returns>
        public async Task<TResult?> FirstOrDefault<TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default)
            => await _dbSet.Apply(specification).FirstOrDefaultAsync(cancellationToken);

        /// <summary> Get the <typeparamref name="T"/> from the <see cref="GenericDbContextRepository{T}"/> with the specified <paramref name="id"/>. </summary>
        /// <param name="id">The identifier to look for.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task{TResult}"/> that represents getting the <typeparamref name="T"/> from the <see cref="GenericDbContextRepository{T}"/> with the specified <paramref name="id"/>.</returns>
        /// <exception cref="KeyNotFoundException">The <see cref="GenericDbContextRepository{T}"/> doesn't contain a <typeparamref name="T"/> with the specified <paramref name="id"/>.</exception>
        public async Task<T> Get(uint id, CancellationToken cancellationToken = default)
            => await Find(id, cancellationToken) ?? throw new KeyNotFoundException($"{nameof(T)} not found with id ({id})");

        /// <summary> Get all <typeparamref name="T"/> in the <see cref="GenericDbContextRepository{T}"/>. </summary>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task{TResult}"/> that represents getting all <typeparamref name="T"/> in the <see cref="GenericDbContextRepository{T}"/>.</returns>
        public async Task<IEnumerable<T>> List(CancellationToken cancellationToken = default)
            => await _dbSet.ToListAsync(cancellationToken);

        /// <summary> Get all <typeparamref name="T"/> in the <see cref="GenericDbContextRepository{T}"/> that satisfy the <paramref name="specification"/>. </summary>
        /// <typeparam name="TResult">The type of result as determined by the <paramref name="specification"/>.</typeparam>
        /// <param name="specification">The <see cref="ISpecification{TBase, TResult}"/> that specifies which items to look for.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>>A <see cref="Task{TResult}"/> that represents getting all <typeparamref name="T"/> in the <see cref="GenericDbContextRepository{T}"/> that satisfy the <paramref name="specification"/>.</returns>
        public async Task<IEnumerable<TResult>> List<TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default)
            => await _dbSet.Apply(specification).ToListAsync(cancellationToken);

        public async Task Remove(T entity, CancellationToken cancellationToken = default)
        {
            _dbSet.Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task Remove(uint id, CancellationToken cancellationToken = default)
            => await Remove(await Get(id, cancellationToken), cancellationToken);

        public async Task RemoveRange(IEnumerable<T> items, CancellationToken cancellationToken = default)
        {
            _dbSet.RemoveRange(items);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task RemoveRange(IEnumerable<uint> ids, CancellationToken cancellationToken = default)
            => await RemoveRange(await Task.WhenAll(ids.Select(id => Get(id, cancellationToken))), cancellationToken);

        public async Task<TResult> Single<TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default)
            => await _dbSet.Apply(specification).SingleAsync(cancellationToken);

        public async Task<TResult?> SingleOrDefault<TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default)
            => await _dbSet.Apply(specification).SingleOrDefaultAsync(cancellationToken);

        public async Task Update(T item, CancellationToken cancellationToken = default)
        {
            _dbSet.Update(item);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateRange(IEnumerable<T> items, CancellationToken cancellationToken = default)
        {
            _dbSet.UpdateRange(items);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
