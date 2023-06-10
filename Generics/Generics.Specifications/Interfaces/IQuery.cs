using System.Linq.Expressions;

namespace Generics.Specifications.Interfaces {
    public interface IQuery<T> : IQuery<T, T> {
        new IQuery<T> Where(Expression<Func<T, bool>> criteria);

        new IOrderedQuery<T> Order<TProperty>(Expression<Func<T, TProperty>> selector, bool descending) where TProperty : IComparable<TProperty>;
        new IOrderedQuery<T> OrderBy<TProperty>(Expression<Func<T, TProperty>> selector) where TProperty : IComparable<TProperty>
            => Order(selector, false);
        new IOrderedQuery<T> OrderByDescending<TProperty>(Expression<Func<T, TProperty>> selector) where TProperty : IComparable<TProperty>
            => Order(selector, true);

        new IIncludedQuery<T, TProperty> Include<TProperty>(Expression<Func<T, TProperty>> selector);

        new IQuery<T> Skip(uint amount);
        new IQuery<T> Take(uint amount);

        #region Default Implementation Redirects
        IQuery<T, T> IQuery<T, T>.Where(Expression<Func<T, bool>> criteria)
            => Where(criteria);
        IOrderedQuery<T, T> IQuery<T, T>.Order<TProperty>(Expression<Func<T, TProperty>> selector, bool descending)
            => Order(selector, descending);
        IQuery<T, T> IQuery<T, T>.Skip(uint amount)
            => Skip(amount);
        IQuery<T, T> IQuery<T, T>.Take(uint amount)
            => Take(amount);
        IIncludedQuery<T, T, TProperty> IQuery<T, T>.Include<TProperty>(Expression<Func<T, TProperty>> selector)
            => Include(selector);
        #endregion
    }

    public interface IQuery<TBase, T> {
        IQuery<TBase, T> Where(Expression<Func<T, bool>> criteria);

        IOrderedQuery<TBase, T> Order<TProperty>(Expression<Func<T, TProperty>> selector, bool descending) where TProperty : IComparable<TProperty>;
        IOrderedQuery<TBase, T> OrderBy<TProperty>(Expression<Func<T, TProperty>> selector) where TProperty : IComparable<TProperty>
            => Order(selector, false);
        IOrderedQuery<TBase, T> OrderByDescending<TProperty>(Expression<Func<T, TProperty>> selector) where TProperty : IComparable<TProperty>
            => Order(selector, true);

        IQuery<TBase, TProperty> Select<TProperty>(Expression<Func<T, TProperty>> selector);
        IQuery<TBase, TProperty> SelectMany<TProperty>(Expression<Func<T, IEnumerable<TProperty>>> selector);

        IIncludedQuery<TBase, T, TProperty> Include<TProperty>(Expression<Func<T, TProperty>> selector);

        IQuery<TBase, T> Skip(uint amount);
        IQuery<TBase, T> Take(uint amount);

        IQueryable<T> Apply(IQueryable<TBase> queryable);
    }
}
