using Generics.Specifications.Extensions;
using Generics.Specifications.Interfaces;
using System.Linq.Expressions;

namespace Generics.Specifications.Queries {
    public class OrderQuery<T, TProperty> : RecursiveQuery<T>, IOrderedQuery<T> {
        public Expression<Func<T, TProperty>> Selector { get; }
        public bool Descending { get; }

        public OrderQuery(IQuery<T> child, Expression<Func<T, TProperty>> selector, bool descending) : base(child) {
            Selector = selector;
            Descending = descending;
        }

        public override IOrderedQueryable<T> Apply(IQueryable<T> queryable)
            => base.Apply(queryable).OrderBy(Selector, Descending);
    }

    public class OrderQuery<TBase, T, TProperty> : RecursiveQuery<TBase, T>, IOrderedQuery<TBase, T> {
        public Expression<Func<T, TProperty>> Selector { get; }
        public bool Descending { get; }

        public OrderQuery(IQuery<TBase, T> child, Expression<Func<T, TProperty>> selector, bool descending) : base(child) {
            Selector = selector;
            Descending = descending;
        }

        public override IOrderedQueryable<T> Apply(IQueryable<TBase> queryable)
            => base.Apply(queryable).OrderBy(Selector, Descending);
    }
}
