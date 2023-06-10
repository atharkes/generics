using Generics.Specifications.Extensions;
using Generics.Specifications.Interfaces;
using System.Linq.Expressions;

namespace Generics.Specifications.Queries {
    public class IncludeQuery<T, TProperty> : RecursiveQuery<T>, IIncludedQuery<T, TProperty> {
        public Expression<Func<T, TProperty>> Selector { get; }

        public IncludeQuery(IQuery<T> child, Expression<Func<T, TProperty>> selector) : base(child)
            => Selector = selector;

        public override IIncludableQueryable<T, TProperty> Apply(IQueryable<T> queryable)
            => base.Apply(queryable).Include(Selector);
    }

    public class IncludeQuery<TBase, T, TProperty> : RecursiveQuery<TBase, T>, IIncludedQuery<TBase, T, TProperty> {
        public Expression<Func<T, TProperty>> Selector { get; }

        public IncludeQuery(IQuery<TBase, T> child, Expression<Func<T, TProperty>> selector) : base(child)
            => Selector = selector;

        public override IIncludableQueryable<T, TProperty> Apply(IQueryable<TBase> queryable)
            => base.Apply(queryable).Include(Selector);
    }
}
