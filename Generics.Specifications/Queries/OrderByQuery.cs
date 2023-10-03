using Generics.Specifications.Extensions;
using Generics.Specifications.Interfaces;
using System.Linq.Expressions;

namespace Generics.Specifications.Queries {
    public class OrderByQuery<T, TProperty> : RecursiveQuery<T>, IOrderedQuery<T> {
        public Expression<Func<T, TProperty>> Selector { get; }
        public bool Descending { get; }

        public OrderByQuery(IQuery<T> child, Expression<Func<T, TProperty>> selector, bool descending) : base(child) {
            Selector = selector;
            Descending = descending;
        }

        public override IOrderedQueryable<T> Apply(IQueryable<T> queryable)
            => base.Apply(queryable).OrderBy(Selector, Descending);
    }

    public class OrderQuery<TBase, TResult, TProperty> : RecursiveQuery<TBase, TResult>, IOrderedQuery<TBase, TResult> {
        public Expression<Func<TResult, TProperty>> Selector { get; }
        public bool Descending { get; }

        public OrderQuery(IQuery<TBase, TResult> child, Expression<Func<TResult, TProperty>> selector, bool descending) : base(child) {
            Selector = selector;
            Descending = descending;
        }

        public override IOrderedQueryable<TResult> Apply(IQueryable<TBase> queryable)
            => base.Apply(queryable).OrderBy(Selector, Descending);
    }
}
