using Generics.Specifications.Interfaces;
using System.Linq.Expressions;

namespace Generics.Specifications.Queries {
    public class GroupByQuery<TBase, TPreviousResult, TKey> : RecursiveQuery<TBase, TPreviousResult, IGrouping<TKey, TPreviousResult>> {
        public Expression<Func<TPreviousResult, TKey>> KeySelector { get; }

        public GroupByQuery(
            IQuery<TBase, TPreviousResult> child,
            Expression<Func<TPreviousResult, TKey>> keySelector
        ) : base(child) {
            KeySelector = keySelector;
        }

        public override IQueryable<IGrouping<TKey, TPreviousResult>> Apply(IQueryable<TBase> queryable)
            => Child.Apply(queryable).GroupBy(KeySelector);
    }

    public class GroupByQuery<TBase, TPreviousResult, TKey, TResult> : RecursiveQuery<TBase, TPreviousResult, IGrouping<TKey, TResult>> {
        public Expression<Func<TPreviousResult, TKey>> KeySelector { get; }
        public Expression<Func<TPreviousResult, TResult>>? ElementSelector { get; }

        public GroupByQuery(
            IQuery<TBase, TPreviousResult> child,
            Expression<Func<TPreviousResult, TKey>> keySelector,
            Expression<Func<TPreviousResult, TResult>>? elementSelector = null
        ) : base(child) {
            KeySelector = keySelector;
            ElementSelector = elementSelector;
        }

        public override IQueryable<IGrouping<TKey, TResult>> Apply(IQueryable<TBase> queryable)
            => Child.Apply(queryable).GroupBy(KeySelector, ElementSelector);
    }
}
