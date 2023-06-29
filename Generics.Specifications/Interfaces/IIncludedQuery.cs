using Generics.Specifications.Extensions;

namespace Generics.Specifications.Interfaces {
    public interface IIncludedQuery<T, out TProperty> : IQuery<T>, IIncludedQuery<T, T, TProperty> { }

    public interface IIncludedQuery<in TBase, out TResult, out TProperty> : IQuery<TBase, TResult> {
        new IIncludableQueryable<TResult, TProperty> Apply(IQueryable<TBase> queryable);
    }
}
