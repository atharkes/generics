using Generics.Specifications.Interfaces;
using System.Linq.Expressions;

namespace Generics.Specifications.Queries {
    public class SelectManyQuery<TBase, TPreviousResult, TResult> : RecursiveQuery<TBase, TPreviousResult, TResult> {
        public Expression<Func<TPreviousResult, IEnumerable<TResult>>> Selector { get; }

        public SelectManyQuery(IQuery<TBase, TPreviousResult> child, Expression<Func<TPreviousResult, IEnumerable<TResult>>> selector) : base(child)
            => Selector = selector;

        public override IQueryable<TResult> Apply(IQueryable<TBase> queryable)
            => Child.Apply(queryable).SelectMany(Selector);
    }
}
