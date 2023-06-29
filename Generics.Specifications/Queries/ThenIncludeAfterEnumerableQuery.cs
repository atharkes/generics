using Generics.Specifications.Extensions;
using Generics.Specifications.Interfaces;
using System.Linq.Expressions;

namespace Generics.Specifications.Queries {
    public class ThenIncludeAfterEnumerableQuery<T, TPreviousProperty, TProperty> : RecursiveQuery<T>, IIncludedQuery<T, TProperty> {
        public Expression<Func<TPreviousProperty, TProperty>> Selector { get; }

        protected override IIncludedQuery<T, IEnumerable<TPreviousProperty>> Child { get; }

        public ThenIncludeAfterEnumerableQuery(IIncludedQuery<T, IEnumerable<TPreviousProperty>> child, Expression<Func<TPreviousProperty, TProperty>> selector) : base(child) {
            Child = child;
            Selector = selector;
        }

        public override IIncludableQueryable<T, TProperty> Apply(IQueryable<T> queryable)
            => Child.Apply(queryable).ThenInclude(Selector);
    }

    public class ThenIncludeAfterEnumerableQuery<TBase, TResult, TPreviousProperty, TProperty> : RecursiveQuery<TBase, TResult>, IIncludedQuery<TBase, TResult, TProperty> {
        public Expression<Func<TPreviousProperty, TProperty>> Selector { get; }

        protected override IIncludedQuery<TBase, TResult, IEnumerable<TPreviousProperty>> Child { get; }

        public ThenIncludeAfterEnumerableQuery(IIncludedQuery<TBase, TResult, IEnumerable<TPreviousProperty>> child, Expression<Func<TPreviousProperty, TProperty>> selector) : base(child) {
            Child = child;
            Selector = selector;
        }

        public override IIncludableQueryable<TResult, TProperty> Apply(IQueryable<TBase> queryable)
            => Child.Apply(queryable).ThenInclude(Selector);
    }
}
