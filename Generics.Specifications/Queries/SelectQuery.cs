using Generics.Specifications.Interfaces;
using System.Linq.Expressions;

namespace Generics.Specifications.Queries {
    public class SelectQuery<TBase, TPreviousResult, TResult> : RecursiveQuery<TBase, TPreviousResult, TResult> {
        public Expression<Func<TPreviousResult, TResult>> Selector { get; }

        public SelectQuery(IQuery<TBase, TPreviousResult> child, Expression<Func<TPreviousResult, TResult>> selector) : base(child)
            => Selector = selector;

        public override IQueryable<TResult> Apply(IQueryable<TBase> queryable)
            => Child.Apply(queryable).Select(Selector);
    }
}
