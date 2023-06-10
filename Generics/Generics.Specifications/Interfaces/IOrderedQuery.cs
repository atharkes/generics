using System.Linq.Expressions;

namespace Generics.Specifications.Interfaces {
    public interface IOrderedQuery<T> : IQuery<T>, IOrderedQuery<T, T> {
        new IOrderedQuery<T> Then<TProperty>(Expression<Func<T, TProperty>> selector, bool descending) where TProperty : IComparable<TProperty>;
        new IOrderedQuery<T> ThenBy<TProperty>(Expression<Func<T, TProperty>> selector) where TProperty : IComparable<TProperty>
            => Then(selector, false);
        new IOrderedQuery<T> ThenByDescending<TProperty>(Expression<Func<T, TProperty>> selector) where TProperty : IComparable<TProperty>
            => Then(selector, true);

        IOrderedQuery<T, T> IOrderedQuery<T, T>.Then<TProperty>(Expression<Func<T, TProperty>> selector, bool descending)
            => Then(selector, descending);
    }

    public interface IOrderedQuery<TBase, T> : IQuery<TBase, T> {
        IOrderedQuery<TBase, T> Then<TProperty>(Expression<Func<T, TProperty>> selector, bool descending) where TProperty : IComparable<TProperty>;
        IOrderedQuery<TBase, T> ThenBy<TProperty>(Expression<Func<T, TProperty>> selector) where TProperty : IComparable<TProperty>
            => Then(selector, false);
        IOrderedQuery<TBase, T> ThenByDescending<TProperty>(Expression<Func<T, TProperty>> selector) where TProperty : IComparable<TProperty>
            => Then(selector, true);
    }
}
