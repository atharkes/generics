using Generics.Infrastructure.Interfaces;
using Generics.Specifications.EntityFramework;
using Generics.Specifications.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Generics.Infrastructure.EntityFramework.Repositories {
    /// <summary> A <see cref="GenericDbContextRepository{T}"/> wrapping a <see cref="DbContext"/> and <see cref="DbSet{TEntity}"/>. </summary>
    /// <typeparam name="T">The type to operate on in this <see cref="GenericDbContextRepository{T}"/>.</typeparam>
    public class GenericDbContextRepository<T> : IRepository<T> where T : class {
        private readonly DbContext _dbContext;
        private readonly DbSet<T> _dbSet;

        /// <summary> Initialize a new instance of the <see cref="GenericDbContextRepository{T}"/> using a <paramref name="dbContext"/>. </summary>
        /// <param name="dbContext">The <see cref="DbContext"/> used in the <see cref="GenericDbContextRepository{T}"/> instance .</param>
        public GenericDbContextRepository(DbContext dbContext) {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }

        /// <summary> Add an <paramref name="item"/> to the <see cref="GenericDbContextRepository{T}"/>. </summary>
        /// <param name="item">The <typeparamref name="T"/> to add to the <see cref="GenericDbContextRepository{T}"/>.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task"/> that represents adding the <paramref name="item"/> to the <see cref="GenericDbContextRepository{T}"/>.</returns>
        public async Task Add(T item, CancellationToken cancellationToken = default) {
            _ = await _dbSet.AddAsync(item, cancellationToken);
            _ = await _dbContext.SaveChangesAsync(cancellationToken);
        }

        /// <summary> Add a range of <paramref name="items"/> to the <see cref="GenericDbContextRepository{T}"/>. </summary>
        /// <param name="items">The <typeparamref name="T"/>s to add to the <see cref="GenericDbContextRepository{T}"/>.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task"/> that represents adding the <paramref name="items"/> to the <see cref="GenericDbContextRepository{T}"/>.</returns>
        public async Task AddRange(IEnumerable<T> items, CancellationToken cancellationToken = default) {
            await _dbSet.AddRangeAsync(items, cancellationToken);
            _ = await _dbContext.SaveChangesAsync(cancellationToken);
        }

        /// <summary> Determine whether there is any <typeparamref name="T"/> in the <see cref="GenericDbContextRepository{T}"/>. </summary>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task{TResult}"/> that represents checking whether there is any <typeparamref name="T"/> in the <see cref="GenericDbContextRepository{T}"/>.</returns>
        public async Task<bool> Any(CancellationToken cancellationToken = default)
            => await _dbSet.AnyAsync(cancellationToken);

        /// <summary> Determine whether there is any <typeparamref name="TResult"/> that satisfies the <paramref name="specification"/> in the <see cref="GenericDbContextRepository{T}"/>. </summary>
        /// <typeparam name="TResult">The <see cref="Type"/> of the result as determined by the <paramref name="specification"/>.</typeparam>
        /// <param name="specification">The <see cref="ISpecification{TBase, TResult}"/> that specifies which items to look for.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task{TResult}"/> that represents checking whether there is any <typeparamref name="TResult"/> that satisfies the <paramref name="specification"/> in the <see cref="GenericDbContextRepository{T}"/>.</returns>
        public async Task<bool> Any<TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default)
            => await _dbSet.Apply(specification).AnyAsync(cancellationToken);

        /// <summary> Determine whether a <typeparamref name="T"/> with the specified <paramref name="id"/> exists in the <see cref="GenericDbContextRepository{T}"/>. </summary>
        /// <param name="id">The identifier to look for.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task{TResult}"/> that represents checking whether a <typeparamref name="T"/> with the specified <paramref name="id"/> exists in the <see cref="GenericDbContextRepository{T}"/>.</returns>
        public async Task<bool> Contains(uint id, CancellationToken cancellationToken = default)
            => await Find(id, cancellationToken) is not null;

        /// <summary> Determine the count of <typeparamref name="T"/>s in the <see cref="GenericDbContextRepository{T}"/>. </summary>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task{TResult}"/> that represents getting the count of <typeparamref name="T"/> items in the <see cref="GenericDbContextRepository{T}"/>.</returns>
        public async Task<uint> Count(CancellationToken cancellationToken = default)
            => (uint)await _dbSet.CountAsync(cancellationToken);

        /// <summary> Determine the count of <typeparamref name="TResult"/>s that satisfy the <paramref name="specification"/> in the <see cref="GenericDbContextRepository{T}"/>. </summary>
        /// <typeparam name="TResult">The <see cref="Type"/> of the result as determined by the <paramref name="specification"/>.</typeparam>
        /// <param name="specification">The <see cref="ISpecification{TBase, TResult}"/> that specifies which items to look for.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task{TResult}"/> that represents getting the count of <typeparamref name="TResult"/>s that satisfy the <paramref name="specification"/> in the <see cref="GenericDbContextRepository{T}"/>.</returns>
        public async Task<uint> Count<TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default)
            => (uint)await _dbSet.Apply(specification).CountAsync(cancellationToken);

        /// <summary> Find the <typeparamref name="T"/> with the specified <paramref name="id"/> in the <see cref="GenericDbContextRepository{T}"/>. </summary>
        /// <param name="id">The identifier to look for.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task{TResult}"/> that represents finding the <typeparamref name="T"/> with the specified <paramref name="id"/> in the <see cref="GenericDbContextRepository{T}"/>, or <see langword="null"/> if there wasn't any.</returns>
        public async Task<T?> Find(uint id, CancellationToken cancellationToken = default)
            => await _dbSet.FindAsync(new object[] { id }, cancellationToken: cancellationToken);

        /// <summary> Get the first <typeparamref name="T"/> in the <see cref="GenericDbContextRepository{T}"/>. </summary>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task{TResult}"/> that represents getting the first <typeparamref name="T"/> in the <see cref="GenericDbContextRepository{T}"/>.</returns>
        /// <exception cref="InvalidOperationException">There are no <typeparamref name="T"/>s in the <see cref="GenericDbContextRepository{T}"/>.</exception>
        public async Task<T> First(CancellationToken cancellationToken = default)
            => await _dbSet.FirstAsync(cancellationToken);

        /// <summary> Get the first <typeparamref name="TResult"/> that satisfies the <paramref name="specification"/> from the <see cref="GenericDbContextRepository{T}"/>. </summary>
        /// <typeparam name="TResult">The <see cref="Type"/> of the result as determined by the <paramref name="specification"/>.</typeparam>
        /// <param name="specification">The <see cref="ISpecification{TBase, TResult}"/> that specifies which items to look for.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task{TResult}"/> that represents getting the first <typeparamref name="TResult"/> that satisfies the <paramref name="specification"/> from the <see cref="GenericDbContextRepository{T}"/>.</returns>
        /// <exception cref="InvalidOperationException">There are no <typeparamref name="TResult"/>s that satisfy the <paramref name="specification"/> in the <see cref="GenericDbContextRepository{T}"/>.</exception>
        public async Task<TResult> First<TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default)
            => await _dbSet.Apply(specification).FirstAsync(cancellationToken);

        /// <summary> Get the first <typeparamref name="T"/> from the <see cref="GenericDbContextRepository{T}"/>. </summary>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task{TResult}"/> that represents getting the first <typeparamref name="T"/> from the <see cref="GenericDbContextRepository{T}"/>, or <see langword="default"/>(<typeparamref name="T"/>) if there wasn't any.</returns>
        public async Task<T?> FirstOrDefault(CancellationToken cancellationToken = default)
            => await _dbSet.FirstOrDefaultAsync(cancellationToken);

        /// <summary> Get the first <typeparamref name="TResult"/> in the <see cref="GenericDbContextRepository{T}"/> that satisfies the <paramref name="specification"/>. </summary>
        /// <typeparam name="TResult">The <see cref="Type"/> of the result as determined by the <paramref name="specification"/>.</typeparam>
        /// <param name="specification">The <see cref="ISpecification{TBase, TResult}"/> that specifies which items to look for.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task{TResult}"/> that represents getting the first <typeparamref name="TResult"/> in the <see cref="GenericDbContextRepository{T}"/> that satisfies the <paramref name="specification"/>, or <see langword="default"/>(<typeparamref name="TResult"/>) if there wasn't any.</returns>
        public async Task<TResult?> FirstOrDefault<TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default)
            => await _dbSet.Apply(specification).FirstOrDefaultAsync(cancellationToken);

        /// <summary> Get the <typeparamref name="T"/> with the specified <paramref name="id"/> from the <see cref="GenericDbContextRepository{T}"/>. </summary>
        /// <param name="id">The identifier to look for.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task{TResult}"/> that represents getting the <typeparamref name="T"/> with the specified <paramref name="id"/> from the <see cref="GenericDbContextRepository{T}"/>.</returns>
        /// <exception cref="KeyNotFoundException">The <see cref="GenericDbContextRepository{T}"/> doesn't contain a <typeparamref name="T"/> with the specified <paramref name="id"/>.</exception>
        public async Task<T> Get(uint id, CancellationToken cancellationToken = default)
            => await Find(id, cancellationToken) ?? throw new KeyNotFoundException($"{nameof(T)} not found with id ({id})");

        /// <summary> Get all <typeparamref name="T"/> in the <see cref="GenericDbContextRepository{T}"/>. </summary>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task{TResult}"/> that represents getting all <typeparamref name="T"/> in the <see cref="GenericDbContextRepository{T}"/>.</returns>
        public async Task<IEnumerable<T>> List(CancellationToken cancellationToken = default)
            => await _dbSet.ToListAsync(cancellationToken);

        /// <summary> Get all <typeparamref name="TResult"/>s that satisfy the <paramref name="specification"/> from the <see cref="GenericDbContextRepository{T}"/>. </summary>
        /// <typeparam name="TResult">The <see cref="Type"/> of the result as determined by the <paramref name="specification"/>.</typeparam>
        /// <param name="specification">The <see cref="ISpecification{TBase, TResult}"/> that specifies which items to look for.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task{TResult}"/> that represents getting all <typeparamref name="TResult"/>s that satisfy the <paramref name="specification"/> from the <see cref="GenericDbContextRepository{T}"/>.</returns>
        public async Task<IEnumerable<TResult>> List<TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default)
            => await _dbSet.Apply(specification).ToListAsync(cancellationToken);

        /// <summary> Remove an <paramref name="item"/> from the <see cref="GenericDbContextRepository{T}"/>. </summary>
        /// <param name="item">The <typeparamref name="T"/> to remove from the <see cref="GenericDbContextRepository{T}"/>.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task"/> that represents removing the <paramref name="item"/> from the <see cref="GenericDbContextRepository{T}"/>.</returns>
        public async Task Remove(T item, CancellationToken cancellationToken = default) {
            _ = _dbSet.Remove(item);
            _ = await _dbContext.SaveChangesAsync(cancellationToken);
        }

        /// <summary> Remove a <typeparamref name="T"/> with the specified <paramref name="id"/> from the <see cref="GenericDbContextRepository{T}"/>. </summary>
        /// <param name="id">The identifier of the <typeparamref name="T"/> to remove from the <see cref="GenericDbContextRepository{T}"/>.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task"/> that represents removing the <typeparamref name="T"/> with the specified <paramref name="id"/> from the <see cref="GenericDbContextRepository{T}"/>.</returns>
        public async Task Remove(uint id, CancellationToken cancellationToken = default)
            => await Remove(await Get(id, cancellationToken), cancellationToken);

        /// <summary> Remove multiple <paramref name="items"/> from the <see cref="GenericDbContextRepository{T}"/>. </summary>
        /// <param name="items">The <typeparamref name="T"/>s to remove from the <see cref="GenericDbContextRepository{T}"/>.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task"/> that represents removing the <paramref name="items"/> from the <see cref="GenericDbContextRepository{T}"/>.</returns>
        public async Task RemoveRange(IEnumerable<T> items, CancellationToken cancellationToken = default) {
            _dbSet.RemoveRange(items);
            _ = await _dbContext.SaveChangesAsync(cancellationToken);
        }

        /// <summary> Remove multiple <typeparamref name="T"/>s by their respective <paramref name="ids"/> from the <see cref="GenericDbContextRepository{T}"/>. </summary>
        /// <param name="ids">The identifiers of the <typeparamref name="T"/>s to remove from the <see cref="GenericDbContextRepository{T}"/>.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task"/> that represents removing the <typeparamref name="T"/>s by their respective <paramref name="ids"/> from the <see cref="GenericDbContextRepository{T}"/>.</returns>
        public async Task RemoveRange(IEnumerable<uint> ids, CancellationToken cancellationToken = default)
            => await RemoveRange(await Task.WhenAll(ids.Select(id => Get(id, cancellationToken))), cancellationToken);

        /// <summary> Get a single <typeparamref name="T"/> from the <see cref="GenericDbContextRepository{T}"/>. </summary>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task{TResult}"/> that represents getting a single <typeparamref name="T"/> from the <see cref="GenericDbContextRepository{T}"/>.</returns>
        /// <exception cref="InvalidOperationException">There are zero or more than one <typeparamref name="T"/>s in the <see cref="GenericDbContextRepository{T}"/>.</exception>
        public async Task<T> Single(CancellationToken cancellationToken = default)
            => await _dbSet.SingleAsync(cancellationToken);

        /// <summary> Get a single <typeparamref name="TResult"/> that satisfies the <paramref name="specification"/> from the <see cref="GenericDbContextRepository{T}"/>. </summary>
        /// <typeparam name="TResult">The <see cref="Type"/> of the result as determined by the <paramref name="specification"/>.</typeparam>
        /// <param name="specification">The <see cref="ISpecification{TBase, TResult}"/> that specifies which items to look for.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task{TResult}"/> that represents getting a single <typeparamref name="TResult"/> that satisfies the <paramref name="specification"/> from the <see cref="GenericDbContextRepository{T}"/>.</returns>
        /// <exception cref="InvalidOperationException">There are zero or more than one <typeparamref name="TResult"/>s that satisfy the <paramref name="specification"/> in the <see cref="GenericDbContextRepository{T}"/>.</exception>
        public async Task<TResult> Single<TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default)
            => await _dbSet.Apply(specification).SingleAsync(cancellationToken);

        /// <summary> Get a single <typeparamref name="T"/> from the <see cref="GenericDbContextRepository{T}"/>. </summary>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task{TResult}"/> that represents getting a single <typeparamref name="T"/> from the <see cref="GenericDbContextRepository{T}"/>, or <see langword="default"/>(<typeparamref name="T"/>) if there wasn't any.</returns>
        /// <exception cref="InvalidOperationException">There are more than one <typeparamref name="T"/>s in the <see cref="GenericDbContextRepository{T}"/>.</exception>
        public async Task<T?> SingleOrDefault(CancellationToken cancellationToken = default)
            => await _dbSet.SingleOrDefaultAsync(cancellationToken);

        /// <summary> Get a single <typeparamref name="TResult"/> that satisfies the <paramref name="specification"/> from the <see cref="GenericDbContextRepository{T}"/>. </summary>
        /// <typeparam name="TResult">The <see cref="Type"/> of the result as determined by the <paramref name="specification"/>.</typeparam>
        /// <param name="specification">The <see cref="ISpecification{TBase, TResult}"/> that specifies which items to look for.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task{TResult}"/> that represents getting a single <typeparamref name="TResult"/> that satisfies the <paramref name="specification"/> from the <see cref="GenericDbContextRepository{T}"/>.</returns>
        /// <exception cref="InvalidOperationException">There are more than one <typeparamref name="TResult"/>s that satisfy the <paramref name="specification"/> in the <see cref="GenericDbContextRepository{T}"/>.</exception>
        public async Task<TResult?> SingleOrDefault<TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default)
            => await _dbSet.Apply(specification).SingleOrDefaultAsync(cancellationToken);

        /// <summary> Update an <paramref name="item"/> in the <see cref="GenericDbContextRepository{T}"/>. </summary>
        /// <param name="item">The <typeparamref name="T"/> to update.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task"/> that represents updating the <paramref name="item"/> in the <see cref="GenericDbContextRepository{T}"/>.</returns>
        public async Task Update(T item, CancellationToken cancellationToken = default) {
            _ = _dbSet.Update(item);
            _ = await _dbContext.SaveChangesAsync(cancellationToken);
        }

        /// <summary> Update multiple <paramref name="items"/> in the <see cref="GenericDbContextRepository{T}"/>. </summary>
        /// <param name="items">The <see cref="IEnumerable{T}"/> of <typeparamref name="T"/>s to update.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task"/> that represents updating the <paramref name="items"/> in the <see cref="GenericDbContextRepository{T}"/>.</returns>
        public async Task UpdateRange(IEnumerable<T> items, CancellationToken cancellationToken = default) {
            _dbSet.UpdateRange(items);
            _ = await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
