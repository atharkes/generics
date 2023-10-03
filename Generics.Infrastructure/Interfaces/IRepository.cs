using Generics.Specifications.Interfaces;

namespace Generics.Infrastructure.Interfaces {
    /// <summary> A <see cref="IRepository"/> to store items </summary>
    public interface IRepository {
        /// <summary> Add an <paramref name="item"/> to the <see cref="IRepository"/>. </summary>
        /// <param name="item">The <see langword="object"/> to add to the <see cref="IRepository"/>.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task"/> that represents adding the <paramref name="item"/> to the <see cref="IRepository"/>.</returns>
        Task Add(object item, CancellationToken cancellationToken = default);

        /// <summary> Add an <paramref name="item"/> to the <see cref="IRepository"/>. </summary>
        /// <typeparam name="T">The <see cref="Type"/> of the <paramref name="item"/> to add.</typeparam>
        /// <param name="item">The <typeparamref name="T"/> to add to the <see cref="IRepository"/>.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task"/> that represents adding the <paramref name="item"/> to the <see cref="IRepository"/>.</returns>
        Task Add<T>(T item, CancellationToken cancellationToken = default) where T : class;

        /// <summary> Add a range of <paramref name="items"/> to the <see cref="IRepository"/>. </summary>
        /// <param name="items">The <see langword="object"/>s to add to the <see cref="IRepository"/>.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task"/> that represents adding the <paramref name="items"/> to the <see cref="IRepository"/>.</returns>
        Task AddRange(IEnumerable<object> items, CancellationToken cancellationToken = default);

        /// <summary> Add a range of <paramref name="items"/> to the <see cref="IRepository"/>. </summary>
        /// <typeparam name="T">The <see cref="Type"/> of the <paramref name="items"/> to add.</typeparam>
        /// <param name="items">The <typeparamref name="T"/>s to add to the <see cref="IRepository"/>.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task"/> that represents adding the <paramref name="items"/> to the <see cref="IRepository"/>.</returns>
        Task AddRange<T>(IEnumerable<T> items, CancellationToken cancellationToken = default) where T : class;

        /// <summary> Determine whether there is any <typeparamref name="T"/> in the <see cref="IRepository"/>. </summary>
        /// <typeparam name="T">The <see cref="Type"/> of items to check.</typeparam>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task{TResult}"/> that represents checking whether there is any <typeparamref name="T"/> in the <see cref="IRepository"/>.</returns>
        Task<bool> Any<T>(CancellationToken cancellationToken = default) where T : class;

        /// <summary> Determine whether there is any <typeparamref name="TResult"/> that satisfies the <paramref name="specification"/> in the <see cref="IRepository"/>. </summary>
        /// <typeparam name="T">The <see cref="Type"/> the <paramref name="specification"/> operates on.</typeparam>
        /// <typeparam name="TResult">The <see cref="Type"/> of the result as determined by the <paramref name="specification"/>.</typeparam>
        /// <param name="specification">The <see cref="ISpecification{TBase, TResult}"/> that specifies which items to look for.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task{TResult}"/> that represents checking whether there is any <typeparamref name="TResult"/> that satisfies the <paramref name="specification"/> in the <see cref="IRepository"/>.</returns>
        Task<bool> Any<T, TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default) where T : class;

        /// <summary> Determine whether a <typeparamref name="T"/> with the specified <paramref name="id"/> exists in the <see cref="IRepository"/>. </summary>
        /// <typeparam name="T">The <see cref="Type"/> of item to look for.</typeparam>
        /// <param name="id">The identifier to look for.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task{TResult}"/> that represents checking whether a <typeparamref name="T"/> with the specified <paramref name="id"/> exists in the <see cref="IRepository"/>.</returns>
        Task<bool> Contains<T>(uint id, CancellationToken cancellationToken = default) where T : class;

        /// <summary> Determine the count of <typeparamref name="T"/>s in the <see cref="IRepository"/>. </summary>
        /// <typeparam name="T">The <see cref="Type"/> of items to count.</typeparam>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task{TResult}"/> that represents getting the count of <typeparamref name="T"/> items in the <see cref="IRepository"/>.</returns>
        Task<uint> Count<T>(CancellationToken cancellationToken = default) where T : class;

        /// <summary> Determine the count of <typeparamref name="TResult"/>s that satisfy the <paramref name="specification"/> in the <see cref="IRepository"/>. </summary>
        /// <typeparam name="T">The <see cref="Type"/> the <paramref name="specification"/> operates on.</typeparam>
        /// <typeparam name="TResult">The <see cref="Type"/> of the result as determined by the <paramref name="specification"/>.</typeparam>
        /// <param name="specification">The <see cref="ISpecification{TBase, TResult}"/> that specifies which items to look for.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task{TResult}"/> that represents getting the count of <typeparamref name="TResult"/>s that satisfy the <paramref name="specification"/> in the <see cref="IRepository"/>.</returns>
        Task<uint> Count<T, TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default) where T : class;

        /// <summary> Find the <typeparamref name="T"/> with the specified <paramref name="id"/> in the <see cref="IRepository"/>. </summary>
        /// <typeparam name="T">The <see cref="Type"/> of item to look for.</typeparam>
        /// <param name="id">The identifier to look for.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task{TResult}"/> that represents finding the <typeparamref name="T"/> with the specified <paramref name="id"/> in the <see cref="IRepository"/>, or <see langword="null"/> if there wasn't any.</returns>
        Task<T?> Find<T>(uint id, CancellationToken cancellationToken = default) where T : class;

        /// <summary> Get the first <typeparamref name="T"/> from the <see cref="IRepository"/>. </summary>
        /// <typeparam name="T">The <see cref="Type"/> of item to look for.</typeparam>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task{TResult}"/> that represents getting the first <typeparamref name="T"/> in the <see cref="IRepository"/>.</returns>
        /// <exception cref="InvalidOperationException">There are no <typeparamref name="T"/>s in the <see cref="IRepository"/>.</exception>
        Task<T> First<T>(CancellationToken cancellationToken = default) where T : class;

        /// <summary> Get the first <typeparamref name="TResult"/> that satisfies the <paramref name="specification"/> from the <see cref="IRepository"/>. </summary>
        /// <typeparam name="T">The <see cref="Type"/> the <paramref name="specification"/> operates on.</typeparam>
        /// <typeparam name="TResult">The <see cref="Type"/> of the result as determined by the <paramref name="specification"/>.</typeparam>
        /// <param name="specification">The <see cref="ISpecification{TBase, TResult}"/> that specifies which items to look for.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task{TResult}"/> that represents getting the first <typeparamref name="TResult"/> that satisfies the <paramref name="specification"/> from the <see cref="IRepository"/>.</returns>
        /// <exception cref="InvalidOperationException">There are no <typeparamref name="TResult"/>s that satisfy the <paramref name="specification"/> in the <see cref="IRepository"/>.</exception>
        Task<TResult> First<T, TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default) where T : class;

        /// <summary> Get the first <typeparamref name="T"/> from the <see cref="IRepository"/>. </summary>
        /// <typeparam name="T">The <see cref="Type"/> of item to look for.</typeparam>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task{TResult}"/> that represents getting the first <typeparamref name="T"/> from the <see cref="IRepository"/>, or <see langword="default"/>(<typeparamref name="T"/>) if there wasn't any.</returns>
        Task<T?> FirstOrDefault<T>(CancellationToken cancellationToken = default) where T : class;

        /// <summary> Get the first <typeparamref name="TResult"/> that satisfies the <paramref name="specification"/> from the <see cref="IRepository"/>. </summary>
        /// <typeparam name="T">The <see cref="Type"/> the <paramref name="specification"/> operates on.</typeparam>
        /// <typeparam name="TResult">The <see cref="Type"/> of the result as determined by the <paramref name="specification"/>.</typeparam>
        /// <param name="specification">The <see cref="ISpecification{TBase, TResult}"/> that specifies which items to look for.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task{TResult}"/> that represents getting the first <typeparamref name="TResult"/> that satisfies the <paramref name="specification"/> from the <see cref="IRepository"/>, or <see langword="default"/>(<typeparamref name="TResult"/>) if there wasn't any.</returns>
        Task<TResult?> FirstOrDefault<T, TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default) where T : class;

        /// <summary> Get the <typeparamref name="T"/> with the specified <paramref name="id"/> from the <see cref="IRepository"/>. </summary>
        /// <typeparam name="T">The <see cref="Type"/> of item to look for.</typeparam>
        /// <param name="id">The identifier to look for.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task{TResult}"/> that represents getting the <typeparamref name="T"/> with the specified <paramref name="id"/> from the <see cref="IRepository"/>.</returns>
        /// <exception cref="KeyNotFoundException">The <see cref="IRepository"/> doesn't contain a <typeparamref name="T"/> with the specified <paramref name="id"/>.</exception>
        Task<T> Get<T>(uint id, CancellationToken cancellationToken = default) where T : class;

        /// <summary> Get all <typeparamref name="T"/> in the <see cref="IRepository"/>. </summary>
        /// <typeparam name="T">The <see cref="Type"/> of items to look for.</typeparam>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task{TResult}"/> that represents getting all <typeparamref name="T"/> in the <see cref="IRepository"/>.</returns>
        Task<IEnumerable<T>> List<T>(CancellationToken cancellationToken = default) where T : class;

        /// <summary> Get all <typeparamref name="TResult"/>s that satisfy the <paramref name="specification"/> from the <see cref="IRepository"/>. </summary>
        /// <typeparam name="T">The <see cref="Type"/> the <paramref name="specification"/> operates on.</typeparam>
        /// <typeparam name="TResult">The <see cref="Type"/> of the result as determined by the <paramref name="specification"/>.</typeparam>
        /// <param name="specification">The <see cref="ISpecification{TBase, TResult}"/> that specifies which items to look for.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task{TResult}"/> that represents getting all <typeparamref name="TResult"/>s that satisfy the <paramref name="specification"/> from the <see cref="IRepository"/>.</returns>
        Task<IEnumerable<TResult>> List<T, TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default) where T : class;

        /// <summary> Remove an <paramref name="item"/> from the <see cref="IRepository"/>. </summary>
        /// <param name="item">The <see langword="object"/> to remove from the <see cref="IRepository"/>.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task"/> that represents removing the <paramref name="item"/> from the <see cref="IRepository"/>.</returns>
        Task Remove(object item, CancellationToken cancellationToken = default);

        /// <summary> Remove an <paramref name="item"/> from the <see cref="IRepository"/>. </summary>
        /// <typeparam name="T">The <see cref="Type"/> of <paramref name="item"/> to remove.</typeparam>
        /// <param name="item">The <typeparamref name="T"/> to remove from the <see cref="IRepository"/>.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task"/> that represents removing the <paramref name="item"/> from the <see cref="IRepository"/>.</returns>
        Task Remove<T>(T item, CancellationToken cancellationToken = default) where T : class;

        /// <summary> Remove a <typeparamref name="T"/> with the specified <paramref name="id"/> from the <see cref="IRepository"/>. </summary>
        /// <typeparam name="T">The <see cref="Type"/> of item to remove.</typeparam>
        /// <param name="id">The identifier of the <typeparamref name="T"/> to remove from the <see cref="IRepository"/>.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task"/> that represents removing the <typeparamref name="T"/> with the specified <paramref name="id"/> from the <see cref="IRepository"/>.</returns>
        Task Remove<T>(uint id, CancellationToken cancellationToken = default) where T : class;

        /// <summary> Remove multiple <paramref name="items"/> from the <see cref="IRepository"/>. </summary>
        /// <param name="items">The <see langword="object"/>s to remove from the <see cref="IRepository"/>.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task"/> that represents removing the <paramref name="items"/> from the <see cref="IRepository"/>.</returns>
        Task RemoveRange(IEnumerable<object> items, CancellationToken cancellationToken = default);

        /// <summary> Remove multiple <paramref name="items"/> from the <see cref="IRepository"/>. </summary>
        /// <typeparam name="T">The <see cref="Type"/> of <paramref name="items"/> to remove.</typeparam>
        /// <param name="items">The <typeparamref name="T"/>s to remove from the <see cref="IRepository"/>.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task"/> that represents removing the <paramref name="items"/> from the <see cref="IRepository"/>.</returns>
        Task RemoveRange<T>(IEnumerable<T> items, CancellationToken cancellationToken = default) where T : class;

        /// <summary> Remove multiple <typeparamref name="T"/>s by their respective <paramref name="ids"/> from the <see cref="IRepository"/>. </summary>
        /// <typeparam name="T">The <see cref="Type"/> of items to remove.</typeparam>
        /// <param name="ids">The identifiers of the <typeparamref name="T"/>s to remove from the <see cref="IRepository"/>.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task"/> that represents removing the <typeparamref name="T"/>s by their respective <paramref name="ids"/> from the <see cref="IRepository"/>.</returns>
        Task RemoveRange<T>(IEnumerable<uint> ids, CancellationToken cancellationToken = default) where T : class;

        /// <summary> Get a single <typeparamref name="T"/> from the <see cref="IRepository"/>. </summary>
        /// <typeparam name="T">The <see cref="Type"/> of item to look for.</typeparam>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task{TResult}"/> that represents getting a single <typeparamref name="T"/> from the <see cref="IRepository"/>.</returns>
        /// <exception cref="InvalidOperationException">There are zero or more than one <typeparamref name="T"/>s in the <see cref="IRepository"/>.</exception>
        Task<T> Single<T>(CancellationToken cancellationToken = default) where T : class;

        /// <summary> Get a single <typeparamref name="TResult"/> that satisfies the <paramref name="specification"/> from the <see cref="IRepository"/>. </summary>
        /// <typeparam name="T">The <see cref="Type"/> the <paramref name="specification"/> operates on.</typeparam>
        /// <typeparam name="TResult">The <see cref="Type"/> of the result as determined by the <paramref name="specification"/>.</typeparam>
        /// <param name="specification">The <see cref="ISpecification{TBase, TResult}"/> that specifies which items to look for.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task{TResult}"/> that represents getting a single <typeparamref name="TResult"/> that satisfies the <paramref name="specification"/> from the <see cref="IRepository"/>.</returns>
        /// <exception cref="InvalidOperationException">There are zero or more than one <typeparamref name="TResult"/>s that satisfy the <paramref name="specification"/> in the <see cref="IRepository"/>.</exception>
        Task<TResult> Single<T, TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default) where T : class;

        /// <summary> Get a single <typeparamref name="T"/> from the <see cref="IRepository"/>. </summary>
        /// <typeparam name="T">The <see cref="Type"/> of item to look for.</typeparam>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task{TResult}"/> that represents getting a single <typeparamref name="T"/> from the <see cref="IRepository"/>, or <see langword="default"/>(<typeparamref name="T"/>) if there wasn't any.</returns>
        /// <exception cref="InvalidOperationException">There are more than one <typeparamref name="T"/>s in the <see cref="IRepository"/>.</exception>
        Task<T?> SingleOrDefault<T>(CancellationToken cancellationToken = default) where T : class;

        /// <summary> Get a single <typeparamref name="TResult"/> that satisfies the <paramref name="specification"/> from the <see cref="IRepository"/>. </summary>
        /// <typeparam name="T">The <see cref="Type"/> the <paramref name="specification"/> operates on.</typeparam>
        /// <typeparam name="TResult">The <see cref="Type"/> of the result as determined by the <paramref name="specification"/>.</typeparam>
        /// <param name="specification">The <see cref="ISpecification{TBase, TResult}"/> that specifies which items to look for.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task{TResult}"/> that represents getting a single <typeparamref name="TResult"/> that satisfies the <paramref name="specification"/> from the <see cref="IRepository"/>.</returns>
        /// <exception cref="InvalidOperationException">There are more than one <typeparamref name="TResult"/>s that satisfy the <paramref name="specification"/> in the <see cref="IRepository"/>.</exception>
        Task<TResult?> SingleOrDefault<T, TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default) where T : class;

        /// <summary> Update an <paramref name="item"/> in the <see cref="IRepository"/>. </summary>
        /// <typeparam name="T">The <see cref="Type"/> of <paramref name="item"/> to update.</typeparam>
        /// <param name="item">The <typeparamref name="T"/> to update.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task"/> that represents updating the <paramref name="item"/> in the <see cref="IRepository"/>.</returns>
        Task Update<T>(T item, CancellationToken cancellationToken = default) where T : class;

        /// <summary> Update multiple <paramref name="items"/> in the <see cref="IRepository"/>. </summary>
        /// <typeparam name="T">The <see cref="Type"/> of <paramref name="items"/> to update.</typeparam>
        /// <param name="items">The <see cref="IEnumerable{T}"/> of <typeparamref name="T"/>s to update.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="Task"/> that represents updating the <paramref name="items"/> in the <see cref="IRepository"/>.</returns>
        Task UpdateRange<T>(IEnumerable<T> items, CancellationToken cancellationToken = default) where T : class;
    }
}
