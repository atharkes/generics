using Generics.Specifications.Extensions;
using Generics.Specifications.Interfaces;
using System.Linq.Expressions;

namespace Generics.Specifications.Queries {
    public class ThenIncludeAfterReferenceQuery<T, TPreviousProperty, TProperty> : RecursiveQuery<T>, IIncludedQuery<T, TProperty> {
        public Expression<Func<TPreviousProperty, TProperty>> Selector { get; }

        protected override IIncludedQuery<T, TPreviousProperty> Child { get; }

        public ThenIncludeAfterReferenceQuery(IIncludedQuery<T, TPreviousProperty> child, Expression<Func<TPreviousProperty, TProperty>> selector) : base(child) {
            Child = child;
            Selector = selector;
        }

        public override IIncludableQueryable<T, TProperty> Apply(IQueryable<T> queryable)
            => Child.Apply(queryable).ThenInclude(Selector);
    }

    public class ThenIncludeAfterReferenceQuery<TBase, T, TPreviousProperty, TProperty> : RecursiveQuery<TBase, T>, IIncludedQuery<TBase, T, TProperty> {
        public Expression<Func<TPreviousProperty, TProperty>> Selector { get; }

        protected override IIncludedQuery<TBase, T, TPreviousProperty> Child { get; }

        public ThenIncludeAfterReferenceQuery(IIncludedQuery<TBase, T, TPreviousProperty> child, Expression<Func<TPreviousProperty, TProperty>> selector) : base(child) {
            Child = child;
            Selector = selector;
        }

        public override IIncludableQueryable<T, TProperty> Apply(IQueryable<TBase> queryable)
            => Child.Apply(queryable).ThenInclude(Selector);
    }
}
