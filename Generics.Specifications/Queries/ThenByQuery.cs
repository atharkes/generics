using Generics.Specifications.Extensions;
using Generics.Specifications.Interfaces;
using System.Linq.Expressions;

namespace Generics.Specifications.Queries {
    public class ThenByQuery<T, TProperty> : RecursiveQuery<T>, IOrderedQuery<T> {
        public Expression<Func<T, TProperty>> Selector { get; }
        public bool Descending { get; }

        protected override IOrderedQuery<T> Child { get; }

        public ThenByQuery(IOrderedQuery<T> child, Expression<Func<T, TProperty>> selector, bool descending) : base(child) {
            Child = child;
            Selector = selector;
            Descending = descending;
        }

        public override IOrderedQueryable<T> Apply(IQueryable<T> queryable)
            => Child.Apply(queryable).ThenBy(Selector, Descending);
    }

    public class ThenByQuery<TBase, TResult, TProperty> : RecursiveQuery<TBase, TResult>, IOrderedQuery<TBase, TResult> {
        public Expression<Func<TResult, TProperty>> Selector { get; }
        public bool Descending { get; }

        protected override IOrderedQuery<TBase, TResult> Child { get; }

        public ThenByQuery(IOrderedQuery<TBase, TResult> child, Expression<Func<TResult, TProperty>> selector, bool descending) : base(child) {
            Child = child;
            Selector = selector;
            Descending = descending;
        }

        public override IOrderedQueryable<TResult> Apply(IQueryable<TBase> queryable)
            => Child.Apply(queryable).ThenBy(Selector, Descending);
    }
}
