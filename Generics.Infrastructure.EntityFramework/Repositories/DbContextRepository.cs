using Generics.Infrastructure.Interfaces;
using Generics.Specifications.EntityFramework;
using Generics.Specifications.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Generics.Infrastructure.EntityFramework.Repositories {
    /// <summary> A <see cref="DbContextRepository"/> wrapping a <see cref="DbContext"/>. </summary>
    public class DbContextRepository : IRepository {
        private readonly DbContext _dbContext;

        /// <summary> Initialize a new instance of the <see cref="DbContextRepository"/> using a <paramref name="dbContext"/>. </summary>
        /// <param name="dbContext">The <see cref="DbContext"/> used in the <see cref="DbContextRepository"/> instance. </param>
        public DbContextRepository(DbContext dbContext)
            => _dbContext = dbContext;

        /// <summary> Add an <paramref name="item"/> to the <see cref="DbContextRepository"/>. </summary>
        /// <param name="item">The <see langword="object"/> to add to the <see cref="DbContextRepository"/>.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task"/> that represents adding the <paramref name="item"/> to the <see cref="DbContextRepository"/>.</returns>
        public async Task Add(object item, CancellationToken cancellationToken = default) {
            _ = await _dbContext.AddAsync(item, cancellationToken);
            _ = await _dbContext.SaveChangesAsync(cancellationToken);
        }

        /// <summary> Add an <paramref name="item"/> to the <see cref="DbContextRepository"/>. </summary>
        /// <typeparam name="T">The <see cref="Type"/> of the <paramref name="item"/> to add.</typeparam>
        /// <param name="item">The <typeparamref name="T"/> to add to the <see cref="DbContextRepository"/>.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task"/> that represents adding the <paramref name="item"/> to the <see cref="DbContextRepository"/>.</returns>
        public async Task Add<T>(T item, CancellationToken cancellationToken = default) where T : class {
            _ = await _dbContext.Set<T>().AddAsync(item, cancellationToken);
            _ = await _dbContext.SaveChangesAsync(cancellationToken);
        }

        /// <summary> Add a range of <paramref name="items"/> to the <see cref="DbContextRepository"/>. </summary>
        /// <param name="items">The <see langword="object"/>s to add to the <see cref="DbContextRepository"/>.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task"/> that represents adding the <paramref name="items"/> to the <see cref="DbContextRepository"/>.</returns>
        public async Task AddRange(IEnumerable<object> items, CancellationToken cancellationToken = default) {
            await _dbContext.AddRangeAsync(items, cancellationToken);
            _ = await _dbContext.SaveChangesAsync(cancellationToken);
        }

        /// <summary> Add a range of <paramref name="items"/> to the <see cref="DbContextRepository"/>. </summary>
        /// <typeparam name="T">The <see cref="Type"/> of the <paramref name="items"/> to add.</typeparam>
        /// <param name="items">The <typeparamref name="T"/>s to add to the <see cref="DbContextRepository"/>.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task"/> that represents adding the <paramref name="items"/> to the <see cref="DbContextRepository"/>.</returns>
        public async Task AddRange<T>(IEnumerable<T> items, CancellationToken cancellationToken = default) where T : class {
            await _dbContext.Set<T>().AddRangeAsync(items, cancellationToken);
            _ = await _dbContext.SaveChangesAsync(cancellationToken);
        }

        /// <summary> Determine whether there is any <typeparamref name="T"/> in the <see cref="DbContextRepository"/>. </summary>
        /// <typeparam name="T">The <see cref="Type"/> of items to check.</typeparam>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task{TResult}"/> that represents checking whether there is any <typeparamref name="T"/> in the <see cref="DbContextRepository"/>.</returns>
        public async Task<bool> Any<T>(CancellationToken cancellationToken = default) where T : class
            => await _dbContext.Set<T>().AnyAsync(cancellationToken);

        /// <summary> Determine whether there is any <typeparamref name="TResult"/> that satisfies the <paramref name="specification"/> in the <see cref="DbContextRepository"/>. </summary>
        /// <typeparam name="T">The <see cref="Type"/> the <paramref name="specification"/> operates on.</typeparam>
        /// <typeparam name="TResult">The <see cref="Type"/> of the result as determined by the <paramref name="specification"/>.</typeparam>
        /// <param name="specification">The <see cref="ISpecification{TBase, TResult}"/> that specifies which items to look for.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task{TResult}"/> that represents checking whether there is any <typeparamref name="TResult"/> that satisfies the <paramref name="specification"/> in the <see cref="DbContextRepository"/>.</returns>
        public async Task<bool> Any<T, TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default) where T : class
            => await _dbContext.Set<T>().Apply(specification).AnyAsync(cancellationToken);

        /// <summary> Determine whether a <typeparamref name="T"/> with the specified <paramref name="id"/> exists in the <see cref="DbContextRepository"/>. </summary>
        /// <typeparam name="T">The <see cref="Type"/> of item to look for.</typeparam>
        /// <param name="id">The identifier to look for.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task{TResult}"/> that represents checking whether a <typeparamref name="T"/> with the specified <paramref name="id"/> exists in the <see cref="DbContextRepository"/>.</returns>
        public async Task<bool> Contains<T>(uint id, CancellationToken cancellationToken = default) where T : class
            => await Find<T>(id, cancellationToken) is not null;

        /// <summary> Determine the count of <typeparamref name="T"/>s in the <see cref="DbContextRepository"/>. </summary>
        /// <typeparam name="T">The <see cref="Type"/> of items to count.</typeparam>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task{TResult}"/> that represents getting the count of <typeparamref name="T"/> items in the <see cref="DbContextRepository"/>.</returns>
        public async Task<uint> Count<T>(CancellationToken cancellationToken = default) where T : class
            => (uint)await _dbContext.Set<T>().CountAsync(cancellationToken);

        /// <summary> Determine the count of <typeparamref name="TResult"/>s that satisfy the <paramref name="specification"/> in the <see cref="DbContextRepository"/>. </summary>
        /// <typeparam name="T">The <see cref="Type"/> the <paramref name="specification"/> operates on.</typeparam>
        /// <typeparam name="TResult">The <see cref="Type"/> of the result as determined by the <paramref name="specification"/>.</typeparam>
        /// <param name="specification">The <see cref="ISpecification{TBase, TResult}"/> that specifies which items to look for.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task{TResult}"/> that represents getting the count of <typeparamref name="TResult"/>s that satisfy the <paramref name="specification"/> in the <see cref="DbContextRepository"/>.</returns>
        public async Task<uint> Count<T, TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default) where T : class
            => (uint)await _dbContext.Set<T>().Apply(specification).CountAsync(cancellationToken);

        /// <summary> Find the <typeparamref name="T"/> with the specified <paramref name="id"/> in the <see cref="DbContextRepository"/>. </summary>
        /// <typeparam name="T">The <see cref="Type"/> of item to look for.</typeparam>
        /// <param name="id">The identifier to look for.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task{TResult}"/> that represents finding the <typeparamref name="T"/> with the specified <paramref name="id"/> in the <see cref="DbContextRepository"/>, or <see langword="null"/> if there wasn't any.</returns>
        public async Task<T?> Find<T>(uint id, CancellationToken cancellationToken = default) where T : class
            => await _dbContext.FindAsync<T>(id, cancellationToken);

        /// <summary> Get the first <typeparamref name="T"/> in the <see cref="DbContextRepository"/>. </summary>
        /// <typeparam name="T">The <see cref="Type"/> of item to look for.</typeparam>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task{TResult}"/> that represents getting the first <typeparamref name="T"/> in the <see cref="DbContextRepository"/>.</returns>
        /// <exception cref="InvalidOperationException">There are no <typeparamref name="T"/>s in the <see cref="DbContextRepository"/>.</exception>
        public async Task<T> First<T>(CancellationToken cancellationToken = default) where T : class
            => await _dbContext.Set<T>().FirstAsync(cancellationToken);

        /// <summary> Get the first <typeparamref name="TResult"/> that satisfies the <paramref name="specification"/> from the <see cref="DbContextRepository"/>. </summary>
        /// <typeparam name="T">The <see cref="Type"/> the <paramref name="specification"/> operates on.</typeparam>
        /// <typeparam name="TResult">The <see cref="Type"/> of the result as determined by the <paramref name="specification"/>.</typeparam>
        /// <param name="specification">The <see cref="ISpecification{TBase, TResult}"/> that specifies which items to look for.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task{TResult}"/> that represents getting the first <typeparamref name="TResult"/> that satisfies the <paramref name="specification"/> from the <see cref="DbContextRepository"/>.</returns>
        /// <exception cref="InvalidOperationException">There are no <typeparamref name="TResult"/>s that satisfy the <paramref name="specification"/> in the <see cref="DbContextRepository"/>.</exception>
        public async Task<TResult> First<T, TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default) where T : class
            => await _dbContext.Set<T>().Apply(specification).FirstAsync(cancellationToken);

        /// <summary> Get the first <typeparamref name="T"/> from the <see cref="DbContextRepository"/>. </summary>
        /// <typeparam name="T">The <see cref="Type"/> of item to look for.</typeparam>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task{TResult}"/> that represents getting the first <typeparamref name="T"/> from the <see cref="DbContextRepository"/>, or <see langword="default"/>(<typeparamref name="T"/>) if there wasn't any.</returns>
        public async Task<T?> FirstOrDefault<T>(CancellationToken cancellationToken = default) where T : class
            => await _dbContext.Set<T>().FirstOrDefaultAsync(cancellationToken);

        /// <summary> Get the first <typeparamref name="TResult"/> in the <see cref="DbContextRepository"/> that satisfies the <paramref name="specification"/>. </summary>
        /// <typeparam name="T">The <see cref="Type"/> the <paramref name="specification"/> operates on.</typeparam>
        /// <typeparam name="TResult">The <see cref="Type"/> of the result as determined by the <paramref name="specification"/>.</typeparam>
        /// <param name="specification">The <see cref="ISpecification{TBase, TResult}"/> that specifies which items to look for.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task{TResult}"/> that represents getting the first <typeparamref name="TResult"/> in the <see cref="DbContextRepository"/> that satisfies the <paramref name="specification"/>, or <see langword="default"/>(<typeparamref name="TResult"/>) if there wasn't any.</returns>
        public async Task<TResult?> FirstOrDefault<T, TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default) where T : class
            => await _dbContext.Set<T>().Apply(specification).FirstOrDefaultAsync(cancellationToken);

        /// <summary> Get the <typeparamref name="T"/> with the specified <paramref name="id"/> from the <see cref="DbContextRepository"/>. </summary>
        /// <typeparam name="T">The <see cref="Type"/> of item to look for.</typeparam>
        /// <param name="id">The identifier to look for.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task{TResult}"/> that represents getting the <typeparamref name="T"/> with the specified <paramref name="id"/> from the <see cref="DbContextRepository"/>.</returns>
        /// <exception cref="KeyNotFoundException">The <see cref="DbContextRepository"/> doesn't contain a <typeparamref name="T"/> with the specified <paramref name="id"/>.</exception>
        public async Task<T> Get<T>(uint id, CancellationToken cancellationToken = default) where T : class
            => await Find<T>(id, cancellationToken) ?? throw new KeyNotFoundException($"{nameof(T)} not found with id ({id})");

        /// <summary> Get all <typeparamref name="T"/> in the <see cref="DbContextRepository"/>. </summary>
        /// <typeparam name="T">The <see cref="Type"/> of items to look for.</typeparam>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task{TResult}"/> that represents getting all <typeparamref name="T"/> in the <see cref="DbContextRepository"/>.</returns>
        public async Task<IEnumerable<T>> List<T>(CancellationToken cancellationToken = default) where T : class
            => await _dbContext.Set<T>().ToListAsync(cancellationToken);

        /// <summary> Get all <typeparamref name="TResult"/>s that satisfy the <paramref name="specification"/> from the <see cref="DbContextRepository"/>. </summary>
        /// <typeparam name="T">The <see cref="Type"/> the <paramref name="specification"/> operates on.</typeparam>
        /// <typeparam name="TResult">The <see cref="Type"/> of the result as determined by the <paramref name="specification"/>.</typeparam>
        /// <param name="specification">The <see cref="ISpecification{TBase, TResult}"/> that specifies which items to look for.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task{TResult}"/> that represents getting all <typeparamref name="TResult"/>s that satisfy the <paramref name="specification"/> from the <see cref="DbContextRepository"/>.</returns>
        public async Task<IEnumerable<TResult>> List<T, TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default) where T : class
            => await _dbContext.Set<T>().Apply(specification).ToListAsync(cancellationToken);

        /// <summary> Remove an <paramref name="item"/> from the <see cref="DbContextRepository"/>. </summary>
        /// <param name="item">The <see langword="object"/> to remove from the <see cref="DbContextRepository"/>.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task"/> that represents removing the <paramref name="item"/> from the <see cref="DbContextRepository"/>.</returns>
        public async Task Remove(object item, CancellationToken cancellationToken = default) {
            _ = _dbContext.Remove(item);
            _ = await _dbContext.SaveChangesAsync(cancellationToken);
        }

        /// <summary> Remove an <paramref name="item"/> from the <see cref="DbContextRepository"/>. </summary>
        /// <typeparam name="T">The <see cref="Type"/> of <paramref name="item"/> to remove.</typeparam>
        /// <param name="item">The <typeparamref name="T"/> to remove from the <see cref="DbContextRepository"/>.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task"/> that represents removing the <paramref name="item"/> from the <see cref="DbContextRepository"/>.</returns>
        public async Task Remove<T>(T item, CancellationToken cancellationToken = default) where T : class {
            _ = _dbContext.Set<T>().Remove(item);
            _ = await _dbContext.SaveChangesAsync(cancellationToken);
        }

        /// <summary> Remove a <typeparamref name="T"/> with the specified <paramref name="id"/> from the <see cref="DbContextRepository"/>. </summary>
        /// <typeparam name="T">The <see cref="Type"/> of item to remove.</typeparam>
        /// <param name="id">The identifier of the <typeparamref name="T"/> to remove from the <see cref="DbContextRepository"/>.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task"/> that represents removing the <typeparamref name="T"/> with the specified <paramref name="id"/> from the <see cref="DbContextRepository"/>.</returns>
        public async Task Remove<T>(uint id, CancellationToken cancellationToken = default) where T : class
            => await Remove(await Get<T>(id, cancellationToken), cancellationToken);

        /// <summary> Remove multiple <paramref name="items"/> from the <see cref="DbContextRepository"/>. </summary>
        /// <param name="items">The <see langword="object"/>s to remove from the <see cref="DbContextRepository"/>.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task"/> that represents removing the <paramref name="items"/> from the <see cref="DbContextRepository"/>.</returns>
        public async Task RemoveRange(IEnumerable<object> items, CancellationToken cancellationToken = default) {
            _dbContext.RemoveRange(items);
            _ = await _dbContext.SaveChangesAsync(cancellationToken);
        }

        /// <summary> Remove multiple <paramref name="items"/> from the <see cref="DbContextRepository"/>. </summary>
        /// <typeparam name="T">The <see cref="Type"/> of <paramref name="items"/> to remove.</typeparam>
        /// <param name="items">The <typeparamref name="T"/>s to remove from the <see cref="DbContextRepository"/>.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task"/> that represents removing the <paramref name="items"/> from the <see cref="DbContextRepository"/>.</returns>
        public async Task RemoveRange<T>(IEnumerable<T> items, CancellationToken cancellationToken = default) where T : class {
            _dbContext.Set<T>().RemoveRange(items);
            _ = await _dbContext.SaveChangesAsync(cancellationToken);
        }

        /// <summary> Remove multiple <typeparamref name="T"/>s by their respective <paramref name="ids"/> from the <see cref="DbContextRepository"/>. </summary>
        /// <typeparam name="T">The <see cref="Type"/> of items to remove.</typeparam>
        /// <param name="ids">The identifiers of the <typeparamref name="T"/>s to remove from the <see cref="DbContextRepository"/>.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task"/> that represents removing the <typeparamref name="T"/>s by their respective <paramref name="ids"/> from the <see cref="DbContextRepository"/>.</returns>
        public async Task RemoveRange<T>(IEnumerable<uint> ids, CancellationToken cancellationToken = default) where T : class
            => await RemoveRange(await Task.WhenAll(ids.Select(id => Get<T>(id, cancellationToken))), cancellationToken);

        /// <summary> Get a single <typeparamref name="T"/> from the <see cref="DbContextRepository"/>. </summary>
        /// <typeparam name="T">The <see cref="Type"/> of item to look for.</typeparam>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task{TResult}"/> that represents getting a single <typeparamref name="T"/> from the <see cref="DbContextRepository"/>.</returns>
        /// <exception cref="InvalidOperationException">There are zero or more than one <typeparamref name="T"/>s in the <see cref="DbContextRepository"/>.</exception>
        public async Task<T> Single<T>(CancellationToken cancellationToken = default) where T : class
            => await _dbContext.Set<T>().SingleAsync(cancellationToken);

        /// <summary> Get a single <typeparamref name="TResult"/> that satisfies the <paramref name="specification"/> from the <see cref="DbContextRepository"/>. </summary>
        /// <typeparam name="T">The <see cref="Type"/> the <paramref name="specification"/> operates on.</typeparam>
        /// <typeparam name="TResult">The <see cref="Type"/> of the result as determined by the <paramref name="specification"/>.</typeparam>
        /// <param name="specification">The <see cref="ISpecification{TBase, TResult}"/> that specifies which items to look for.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task{TResult}"/> that represents getting a single <typeparamref name="TResult"/> that satisfies the <paramref name="specification"/> from the <see cref="DbContextRepository"/>.</returns>
        /// <exception cref="InvalidOperationException">There are zero or more than one <typeparamref name="TResult"/>s that satisfy the <paramref name="specification"/> in the <see cref="DbContextRepository"/>.</exception>
        public async Task<TResult> Single<T, TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default) where T : class
            => await _dbContext.Set<T>().Apply(specification).SingleAsync(cancellationToken);

        /// <summary> Get a single <typeparamref name="T"/> from the <see cref="DbContextRepository"/>. </summary>
        /// <typeparam name="T">The <see cref="Type"/> of item to look for.</typeparam>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task{TResult}"/> that represents getting a single <typeparamref name="T"/> from the <see cref="DbContextRepository"/>, or <see langword="default"/>(<typeparamref name="T"/>) if there wasn't any.</returns>
        /// <exception cref="InvalidOperationException">There are more than one <typeparamref name="T"/>s in the <see cref="DbContextRepository"/>.</exception>
        public async Task<T?> SingleOrDefault<T>(CancellationToken cancellationToken = default) where T : class
            => await _dbContext.Set<T>().SingleOrDefaultAsync(cancellationToken);

        /// <summary> Get a single <typeparamref name="TResult"/> that satisfies the <paramref name="specification"/> from the <see cref="DbContextRepository"/>. </summary>
        /// <typeparam name="T">The <see cref="Type"/> the <paramref name="specification"/> operates on.</typeparam>
        /// <typeparam name="TResult">The <see cref="Type"/> of the result as determined by the <paramref name="specification"/>.</typeparam>
        /// <param name="specification">The <see cref="ISpecification{TBase, TResult}"/> that specifies which items to look for.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task{TResult}"/> that represents getting a single <typeparamref name="TResult"/> that satisfies the <paramref name="specification"/> from the <see cref="DbContextRepository"/>.</returns>
        /// <exception cref="InvalidOperationException">There are more than one <typeparamref name="TResult"/>s that satisfy the <paramref name="specification"/> in the <see cref="DbContextRepository"/>.</exception>
        public async Task<TResult?> SingleOrDefault<T, TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default) where T : class
            => await _dbContext.Set<T>().Apply(specification).SingleOrDefaultAsync(cancellationToken);

        /// <summary> Update an <paramref name="item"/> in the <see cref="DbContextRepository"/>. </summary>
        /// <typeparam name="T">The <see cref="Type"/> of <paramref name="item"/> to update.</typeparam>
        /// <param name="item">The <typeparamref name="T"/> to update.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task"/> that represents updating the <paramref name="item"/> in the <see cref="DbContextRepository"/>.</returns>
        public async Task Update<T>(T item, CancellationToken cancellationToken = default) where T : class {
            _ = _dbContext.Set<T>().Update(item);
            _ = await _dbContext.SaveChangesAsync(cancellationToken);
        }

        /// <summary> Update multiple <paramref name="items"/> in the <see cref="DbContextRepository"/>. </summary>
        /// <typeparam name="T">The <see cref="Type"/> of <paramref name="items"/> to update.</typeparam>
        /// <param name="items">The <see cref="IEnumerable{T}"/> of <typeparamref name="T"/>s to update.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task"/> that represents updating the <paramref name="items"/> in the <see cref="DbContextRepository"/>.</returns>
        public async Task UpdateRange<T>(IEnumerable<T> items, CancellationToken cancellationToken = default) where T : class {
            _dbContext.Set<T>().UpdateRange(items);
            _ = await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
