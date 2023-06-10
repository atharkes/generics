using Generics.Specifications.Interfaces;
using System.Linq.Expressions;

namespace Generics.Specifications.Queries {
    public class SelectManyQuery<TBase, T, TProperty> : RecursiveQuery<TBase, T, TProperty> {
        public Expression<Func<T, IEnumerable<TProperty>>> Selector { get; }

        public SelectManyQuery(IQuery<TBase, T> child, Expression<Func<T, IEnumerable<TProperty>>> selector) : base(child)
            => Selector = selector;

        public override IQueryable<TProperty> Apply(IQueryable<TBase> queryable)
            => Child.Apply(queryable).SelectMany(Selector);
    }
}
