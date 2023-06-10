using Generics.Specifications.Interfaces;

namespace Generics.Infrastructure.Interfaces {
    public interface IRepository {
        Task<T?> Find<T>(uint id) where T : class;
        Task<T> Get<T>(uint id) where T : class;

        Task<uint> Count<T>() where T : class;
        Task<uint> Count<T, TResult>(ISpecification<T, TResult> specification) where T : class;

        Task<bool> Any<T>() where T : class;
        Task<bool> Any<T, TResult>(ISpecification<T, TResult> specification) where T : class;

        Task<TResult> Single<T, TResult>(ISpecification<T, TResult> specification) where T : class;
        Task<TResult?> SingleOrDefault<T, TResult>(ISpecification<T, TResult> specification) where T : class;

        Task<T> First<T>() where T : class;
        Task<T?> FirstOrDefault<T>() where T : class;
        Task<TResult> First<T, TResult>(ISpecification<T, TResult> specification) where T : class;
        Task<TResult?> FirstOrDefault<T, TResult>(ISpecification<T, TResult> specification) where T : class;

        Task<IEnumerable<T>> List<T>() where T : class;
        Task<IEnumerable<TResult>> List<T, TResult>(ISpecification<T, TResult> specification) where T : class;

        Task Update<T>(T entity) where T : class;
        Task UpdateRange<T>(IEnumerable<T> entities) where T : class;

        Task Add(object entity);
        Task Add<T>(T entity) where T : class;
        Task AddRange(IEnumerable<object> entities);
        Task AddRange<T>(IEnumerable<T> entities) where T : class;

        Task Remove(object entity);
        Task Remove<T>(T entity) where T : class;
        Task Remove<T>(uint id) where T : class;
        Task RemoveRange(IEnumerable<object> entities);
        Task RemoveRange<T>(IEnumerable<T> entities) where T : class;
        Task RemoveRange<T>(IEnumerable<uint> ids) where T : class;
    }

    public interface IRepository<T> {
        Task<T?> Find(uint id);
        Task<T> Get(uint id);

        Task<uint> Count();
        Task<uint> Count<TResult>(ISpecification<T, TResult> specification);

        Task<bool> Any();
        Task<bool> Any<TResult>(ISpecification<T, TResult> specification);

        Task<TResult> Single<TResult>(ISpecification<T, TResult> specification);
        Task<TResult?> SingleOrDefault<TResult>(ISpecification<T, TResult> specification);

        Task<T> First();
        Task<T?> FirstOrDefault();
        Task<TResult> First<TResult>(ISpecification<T, TResult> specification);
        Task<TResult?> FirstOrDefault<TResult>(ISpecification<T, TResult> specification);

        Task<IEnumerable<T>> List();
        Task<IEnumerable<TResult>> List<TResult>(ISpecification<T, TResult> specification);

        Task Update(T entity);
        Task UpdateRange(IEnumerable<T> entities);

        Task Add(T entity);
        Task AddRange(IEnumerable<T> entities);

        Task Remove(T entity);
        Task RemoveRange(IEnumerable<T> entities);
        Task Remove(uint id);
        Task RemoveRange(IEnumerable<uint> ids);
    }
}
