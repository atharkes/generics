using Generics.Specifications.Interfaces;

namespace Generics.Infrastructure.Interfaces {
    /// <summary> A <see cref="IRepository{T}"/> to store items of type <typeparamref name="T"/> </summary>
    /// <typeparam name="T">The <see cref="Type"/> of items in the <see cref="IRepository{T}"/></typeparam>
    public interface IRepository<T> {
        /// <summary> Add an <paramref name="item"/> to the <see cref="IRepository{T}"/>. </summary>
        /// <param name="item">The <typeparamref name="T"/> to add to the <see cref="IRepository{T}"/>.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task"/> that represents adding the <paramref name="item"/> to the <see cref="IRepository{T}"/>.</returns>
        Task Add(T item, CancellationToken cancellationToken = default);

        /// <summary> Add a range of <paramref name="items"/> to the <see cref="IRepository{T}"/>. </summary>
        /// <param name="items">The <typeparamref name="T"/>s to add to the <see cref="IRepository{T}"/>.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task"/> that represents adding the <paramref name="items"/> to the <see cref="IRepository{T}"/>.</returns>
        Task AddRange(IEnumerable<T> items, CancellationToken cancellationToken = default);

        /// <summary> Determine whether there is any <typeparamref name="T"/> in the <see cref="IRepository{T}"/>. </summary>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task{TResult}"/> that represents checking whether there is any <typeparamref name="T"/> in the <see cref="IRepository{T}"/>.</returns>
        Task<bool> Any(CancellationToken cancellationToken = default);

        /// <summary> Determine whether there is any <typeparamref name="TResult"/> that satisfies the <paramref name="specification"/> in the <see cref="IRepository{T}"/>. </summary>
        /// <typeparam name="TResult">The <see cref="Type"/> of the result as determined by the <paramref name="specification"/>.</typeparam>
        /// <param name="specification">The <see cref="ISpecification{TBase, TResult}"/> that specifies which items to look for.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task{TResult}"/> that represents checking whether there is any <typeparamref name="TResult"/> that satisfies the <paramref name="specification"/> in the <see cref="IRepository{T}"/>.</returns>
        Task<bool> Any<TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default);

        /// <summary> Determine whether a <typeparamref name="T"/> with the specified <paramref name="id"/> exists in the <see cref="IRepository{T}"/>. </summary>
        /// <param name="id">The identifier to look for.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task{TResult}"/> that represents checking whether a <typeparamref name="T"/> with the specified <paramref name="id"/> exists in the <see cref="IRepository{T}"/>.</returns>
        Task<bool> Contains(uint id, CancellationToken cancellationToken = default);

        /// <summary> Determine the count of <typeparamref name="T"/>s in the <see cref="IRepository{T}"/>. </summary>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task{TResult}"/> that represents getting the count of <typeparamref name="T"/> items in the <see cref="IRepository{T}"/>.</returns>
        Task<uint> Count(CancellationToken cancellationToken = default);

        /// <summary> Determine the count of <typeparamref name="TResult"/>s that satisfy the <paramref name="specification"/> in the <see cref="IRepository{T}"/>. </summary>
        /// <typeparam name="TResult">The <see cref="Type"/> of the result as determined by the <paramref name="specification"/>.</typeparam>
        /// <param name="specification">The <see cref="ISpecification{TBase, TResult}"/> that specifies which items to look for.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task{TResult}"/> that represents getting the count of <typeparamref name="TResult"/>s that satisfy the <paramref name="specification"/> in the <see cref="IRepository{T}"/>.</returns>
        Task<uint> Count<TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default);

        /// <summary> Find the <typeparamref name="T"/> with the specified <paramref name="id"/> in the <see cref="IRepository{T}"/>. </summary>
        /// <param name="id">The identifier to look for.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task{TResult}"/> that represents finding the <typeparamref name="T"/> with the specified <paramref name="id"/> in the <see cref="IRepository{T}"/>, or <see langword="null"/> if there wasn't any.</returns>
        Task<T?> Find(uint id, CancellationToken cancellationToken = default);

        /// <summary> Get the first <typeparamref name="T"/> from the <see cref="IRepository{T}"/>. </summary>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task{TResult}"/> that represents getting the first <typeparamref name="T"/> in the <see cref="IRepository{T}"/>.</returns>
        /// <exception cref="InvalidOperationException">There are no <typeparamref name="T"/>s in the <see cref="IRepository{T}"/>.</exception>
        Task<T> First(CancellationToken cancellationToken = default);

        /// <summary> Get the first <typeparamref name="TResult"/> that satisfies the <paramref name="specification"/> from the <see cref="IRepository{T}"/>. </summary>
        /// <typeparam name="TResult">The <see cref="Type"/> of the result as determined by the <paramref name="specification"/>.</typeparam>
        /// <param name="specification">The <see cref="ISpecification{TBase, TResult}"/> that specifies which items to look for.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task{TResult}"/> that represents getting the first <typeparamref name="TResult"/> that satisfies the <paramref name="specification"/> from the <see cref="IRepository{T}"/>.</returns>
        /// <exception cref="InvalidOperationException">There are no <typeparamref name="TResult"/>s that satisfy the <paramref name="specification"/> in the <see cref="IRepository{T}"/>.</exception>
        Task<TResult> First<TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default);

        /// <summary> Get the first <typeparamref name="T"/> from the <see cref="IRepository{T}"/>. </summary>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task{TResult}"/> that represents getting the first <typeparamref name="T"/> from the <see cref="IRepository{T}"/>, or <see langword="default"/>(<typeparamref name="T"/>) if there wasn't any.</returns>
        Task<T?> FirstOrDefault(CancellationToken cancellationToken = default);

        /// <summary> Get the first <typeparamref name="TResult"/> that satisfies the <paramref name="specification"/> from the <see cref="IRepository{T}"/>. </summary>
        /// <typeparam name="TResult">The <see cref="Type"/> of the result as determined by the <paramref name="specification"/>.</typeparam>
        /// <param name="specification">The <see cref="ISpecification{TBase, TResult}"/> that specifies which items to look for.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task{TResult}"/> that represents getting the first <typeparamref name="TResult"/> that satisfies the <paramref name="specification"/> from the <see cref="IRepository{T}"/>, or <see langword="default"/>(<typeparamref name="TResult"/>) if there wasn't any.</returns>
        Task<TResult?> FirstOrDefault<TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default);

        /// <summary> Get the <typeparamref name="T"/> with the specified <paramref name="id"/> from the <see cref="IRepository{T}"/>. </summary>
        /// <param name="id">The identifier to look for.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task{TResult}"/> that represents getting the <typeparamref name="T"/> with the specified <paramref name="id"/> from the <see cref="IRepository{T}"/>.</returns>
        /// <exception cref="KeyNotFoundException">The <see cref="IRepository{T}"/> doesn't contain a <typeparamref name="T"/> with the specified <paramref name="id"/>.</exception>
        Task<T> Get(uint id, CancellationToken cancellationToken = default);

        /// <summary> Get all <typeparamref name="T"/> in the <see cref="IRepository{T}"/>. </summary>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task{TResult}"/> that represents getting all <typeparamref name="T"/> in the <see cref="IRepository{T}"/>.</returns>
        Task<IEnumerable<T>> List(CancellationToken cancellationToken = default);

        /// <summary> Get all <typeparamref name="TResult"/>s that satisfy the <paramref name="specification"/> from the <see cref="IRepository{T}"/>. </summary>
        /// <typeparam name="TResult">The <see cref="Type"/> of the result as determined by the <paramref name="specification"/>.</typeparam>
        /// <param name="specification">The <see cref="ISpecification{TBase, TResult}"/> that specifies which items to look for.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task{TResult}"/> that represents getting all <typeparamref name="TResult"/>s that satisfy the <paramref name="specification"/> from the <see cref="IRepository{T}"/>.</returns>
        Task<IEnumerable<TResult>> List<TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default);

        /// <summary> Remove an <paramref name="item"/> from the <see cref="IRepository{T}"/>. </summary>
        /// <param name="item">The <typeparamref name="T"/> to remove from the <see cref="IRepository{T}"/>.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task"/> that represents removing the <paramref name="item"/> from the <see cref="IRepository{T}"/>.</returns>
        Task Remove(T item, CancellationToken cancellationToken = default);

        /// <summary> Remove a <typeparamref name="T"/> with the specified <paramref name="id"/> from the <see cref="IRepository{T}"/>. </summary>
        /// <param name="id">The identifier of the <typeparamref name="T"/> to remove from the <see cref="IRepository{T}"/>.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task"/> that represents removing the <typeparamref name="T"/> with the specified <paramref name="id"/> from the <see cref="IRepository{T}"/>.</returns>
        Task Remove(uint id, CancellationToken cancellationToken = default);

        /// <summary> Remove multiple <paramref name="items"/> from the <see cref="IRepository{T}"/>. </summary>
        /// <param name="items">The <typeparamref name="T"/>s to remove from the <see cref="IRepository{T}"/>.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task"/> that represents removing the <paramref name="items"/> from the <see cref="IRepository{T}"/>.</returns>
        Task RemoveRange(IEnumerable<T> items, CancellationToken cancellationToken = default);

        /// <summary> Remove multiple <typeparamref name="T"/>s by their respective <paramref name="ids"/> from the <see cref="IRepository{T}"/>. </summary>
        /// <param name="ids">The identifiers of the <typeparamref name="T"/>s to remove from the <see cref="IRepository{T}"/>.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task"/> that represents removing the <typeparamref name="T"/>s by their respective <paramref name="ids"/> from the <see cref="IRepository{T}"/>.</returns>
        Task RemoveRange(IEnumerable<uint> ids, CancellationToken cancellationToken = default);

        /// <summary> Get a single <typeparamref name="T"/> from the <see cref="IRepository{T}"/>. </summary>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task{TResult}"/> that represents getting a single <typeparamref name="T"/> from the <see cref="IRepository{T}"/>.</returns>
        /// <exception cref="InvalidOperationException">There are zero or more than one <typeparamref name="T"/>s in the <see cref="IRepository{T}"/>.</exception>
        Task<T> Single(CancellationToken cancellationToken = default);

        /// <summary> Get a single <typeparamref name="TResult"/> that satisfies the <paramref name="specification"/> from the <see cref="IRepository{T}"/>. </summary>
        /// <typeparam name="TResult">The <see cref="Type"/> of the result as determined by the <paramref name="specification"/>.</typeparam>
        /// <param name="specification">The <see cref="ISpecification{TBase, TResult}"/> that specifies which items to look for.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task{TResult}"/> that represents getting a single <typeparamref name="TResult"/> that satisfies the <paramref name="specification"/> from the <see cref="IRepository{T}"/>.</returns>
        /// <exception cref="InvalidOperationException">There are zero or more than one <typeparamref name="TResult"/>s that satisfy the <paramref name="specification"/> in the <see cref="IRepository{T}"/>.</exception>
        Task<TResult> Single<TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default);

        /// <summary> Get a single <typeparamref name="T"/> from the <see cref="IRepository{T}"/>. </summary>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task{TResult}"/> that represents getting a single <typeparamref name="T"/> from the <see cref="IRepository{T}"/>, or <see langword="default"/>(<typeparamref name="T"/>) if there wasn't any.</returns>
        /// <exception cref="InvalidOperationException">There are more than one <typeparamref name="T"/>s in the <see cref="IRepository{T}"/>.</exception>
        Task<T?> SingleOrDefault(CancellationToken cancellationToken = default);

        /// <summary> Get a single <typeparamref name="TResult"/> that satisfies the <paramref name="specification"/> from the <see cref="IRepository{T}"/>. </summary>
        /// <typeparam name="TResult">The <see cref="Type"/> of the result as determined by the <paramref name="specification"/>.</typeparam>
        /// <param name="specification">The <see cref="ISpecification{TBase, TResult}"/> that specifies which items to look for.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task{TResult}"/> that represents getting a single <typeparamref name="TResult"/> that satisfies the <paramref name="specification"/> from the <see cref="IRepository{T}"/>.</returns>
        /// <exception cref="InvalidOperationException">There are more than one <typeparamref name="TResult"/>s that satisfy the <paramref name="specification"/> in the <see cref="IRepository{T}"/>.</exception>
        Task<TResult?> SingleOrDefault<TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default);

        /// <summary> Update an <paramref name="item"/> in the <see cref="IRepository{T}"/>. </summary>
        /// <param name="item">The <typeparamref name="T"/> to update.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task"/> that represents updating the <paramref name="item"/> in the <see cref="IRepository{T}"/>.</returns>
        Task Update(T item, CancellationToken cancellationToken = default);

        /// <summary> Update multiple <paramref name="items"/> in the <see cref="IRepository{T}"/>. </summary>
        /// <param name="items">The <see cref="IEnumerable{T}"/> of <typeparamref name="T"/>s to update.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task"/> that represents updating the <paramref name="items"/> in the <see cref="IRepository{T}"/>.</returns>
        Task UpdateRange(IEnumerable<T> items, CancellationToken cancellationToken = default);
    }
}
