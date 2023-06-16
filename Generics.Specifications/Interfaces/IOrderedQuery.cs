using System.Linq.Expressions;

namespace Generics.Specifications.Interfaces {
    public interface IOrderedQuery<T> : IQuery<T>, IOrderedQuery<T, T> {
        new IOrderedQuery<T> ThenBy<TProperty>(Expression<Func<T, TProperty>> selector, bool descending) where TProperty : IComparable<TProperty>;
        new IOrderedQuery<T> ThenBy<TProperty>(Expression<Func<T, TProperty>> selector) where TProperty : IComparable<TProperty>
            => ThenBy(selector, false);
        new IOrderedQuery<T> ThenByDescending<TProperty>(Expression<Func<T, TProperty>> selector) where TProperty : IComparable<TProperty>
            => ThenBy(selector, true);
        IOrderedQuery<T, T> IOrderedQuery<T, T>.ThenBy<TProperty>(Expression<Func<T, TProperty>> selector, bool descending)
            => ThenBy(selector, descending);

        new IOrderedQueryable<T> Apply(IQueryable<T> queryable);
        IOrderedQueryable<T> IOrderedQuery<T, T>.Apply(IQueryable<T> queryable) => Apply(queryable);
    }

    public interface IOrderedQuery<TBase, T> : IQuery<TBase, T> {
        IOrderedQuery<TBase, T> ThenBy<TProperty>(Expression<Func<T, TProperty>> selector, bool descending) where TProperty : IComparable<TProperty>;
        IOrderedQuery<TBase, T> ThenBy<TProperty>(Expression<Func<T, TProperty>> selector) where TProperty : IComparable<TProperty>
            => ThenBy(selector, false);
        IOrderedQuery<TBase, T> ThenByDescending<TProperty>(Expression<Func<T, TProperty>> selector) where TProperty : IComparable<TProperty>
            => ThenBy(selector, true);

        new IOrderedQueryable<T> Apply(IQueryable<TBase> queryable);
        IQueryable<T> IQuery<TBase, T>.Apply(IQueryable<TBase> queryable)
            => Apply(queryable);
    }
}
