using Generics.Specifications.Interfaces;
using System.Linq.Expressions;

namespace Generics.Specifications.Queries {
    public class SelectQuery<TBase, T, TProperty> : RecursiveQuery<TBase, T, TProperty> {
        public Expression<Func<T, TProperty>> Selector { get; }

        public SelectQuery(IQuery<TBase, T> child, Expression<Func<T, TProperty>> selector) : base(child)
            => Selector = selector;

        public override IQueryable<TProperty> Apply(IQueryable<TBase> queryable)
            => Child.Apply(queryable).Select(Selector);
    }
}
