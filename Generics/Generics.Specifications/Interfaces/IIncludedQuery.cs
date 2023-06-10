using Generics.Specifications.Extensions;

namespace Generics.Specifications.Interfaces {
    public interface IIncludedQuery<T, out TProperty> : IQuery<T>, IIncludedQuery<T, T, TProperty> { }

    public interface IIncludedQuery<TBase, T, out TProperty> : IQuery<TBase, T> {
        new IIncludableQueryable<T, TProperty> Apply(IQueryable<TBase> queryable);
        IQueryable<T> IQuery<TBase, T>.Apply(IQueryable<TBase> queryable)
            => Apply(queryable);
    }
}
