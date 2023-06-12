using Generics.Specifications.Interfaces;

namespace Generics.Infrastructure.Interfaces {
    public interface IRepository {
        Task<T?> Find<T>(uint id, CancellationToken cancellationToken = default) where T : class;
        Task<T> Get<T>(uint id, CancellationToken cancellationToken = default) where T : class;

        Task<uint> Count<T>(CancellationToken cancellationToken = default) where T : class;
        Task<uint> Count<T, TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default) where T : class;

        Task<bool> Any<T>(CancellationToken cancellationToken = default) where T : class;
        Task<bool> Any<T, TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default) where T : class;

        Task<TResult> Single<T, TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default) where T : class;
        Task<TResult?> SingleOrDefault<T, TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default) where T : class;

        Task<T> First<T>(CancellationToken cancellationToken = default) where T : class;
        Task<T?> FirstOrDefault<T>(CancellationToken cancellationToken = default) where T : class;
        Task<TResult> First<T, TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default) where T : class;
        Task<TResult?> FirstOrDefault<T, TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default) where T : class;

        Task<IEnumerable<T>> List<T>(CancellationToken cancellationToken = default) where T : class;
        Task<IEnumerable<TResult>> List<T, TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default) where T : class;

        Task Update<T>(T entity, CancellationToken cancellationToken = default) where T : class;
        Task UpdateRange<T>(IEnumerable<T> entities, CancellationToken cancellationToken = default) where T : class;

        Task Add(object entity, CancellationToken cancellationToken = default);
        Task Add<T>(T entity, CancellationToken cancellationToken = default) where T : class;
        Task AddRange(IEnumerable<object> entities, CancellationToken cancellationToken = default);
        Task AddRange<T>(IEnumerable<T> entities, CancellationToken cancellationToken = default) where T : class;

        Task Remove(object entity, CancellationToken cancellationToken = default);
        Task Remove<T>(T entity, CancellationToken cancellationToken = default) where T : class;
        Task Remove<T>(uint id, CancellationToken cancellationToken = default) where T : class;
        Task RemoveRange(IEnumerable<object> entities, CancellationToken cancellationToken = default);
        Task RemoveRange<T>(IEnumerable<T> entities, CancellationToken cancellationToken = default) where T : class;
        Task RemoveRange<T>(IEnumerable<uint> ids, CancellationToken cancellationToken = default) where T : class;
    }

    public interface IRepository<T> {
        Task<T?> Find(uint id, CancellationToken cancellationToken = default);
        Task<T> Get(uint id, CancellationToken cancellationToken = default);

        Task<uint> Count(CancellationToken cancellationToken = default);
        Task<uint> Count<TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default);

        Task<bool> Any(CancellationToken cancellationToken = default);
        Task<bool> Any<TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default);

        Task<TResult> Single<TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default);
        Task<TResult?> SingleOrDefault<TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default);

        Task<T> First(CancellationToken cancellationToken = default);
        Task<T?> FirstOrDefault(CancellationToken cancellationToken = default);
        Task<TResult> First<TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default);
        Task<TResult?> FirstOrDefault<TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default);

        Task<IEnumerable<T>> List(CancellationToken cancellationToken = default);
        Task<IEnumerable<TResult>> List<TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default);

        Task Update(T entity, CancellationToken cancellationToken = default);
        Task UpdateRange(IEnumerable<T> entities, CancellationToken cancellationToken = default);

        Task Add(T entity, CancellationToken cancellationToken = default);
        Task AddRange(IEnumerable<T> entities, CancellationToken cancellationToken = default);

        Task Remove(T entity, CancellationToken cancellationToken = default);
        Task RemoveRange(IEnumerable<T> entities, CancellationToken cancellationToken = default);
        Task Remove(uint id, CancellationToken cancellationToken = default);
        Task RemoveRange(IEnumerable<uint> ids, CancellationToken cancellationToken = default);
    }
}
