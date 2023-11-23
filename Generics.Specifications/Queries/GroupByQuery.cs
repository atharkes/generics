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

    public class GroupByQuery<TBase, TPreviousResult, TKey, TElement> : RecursiveQuery<TBase, TPreviousResult, IGrouping<TKey, TElement>> {
        public Expression<Func<TPreviousResult, TKey>> KeySelector { get; }
        public Expression<Func<TPreviousResult, TElement>> ElementSelector { get; }

        public GroupByQuery(
            IQuery<TBase, TPreviousResult> child,
            Expression<Func<TPreviousResult, TKey>> keySelector,
            Expression<Func<TPreviousResult, TElement>> elementSelector
        ) : base(child) {
            KeySelector = keySelector;
            ElementSelector = elementSelector;
        }

        public override IQueryable<IGrouping<TKey, TElement>> Apply(IQueryable<TBase> queryable)
            => Child.Apply(queryable).GroupBy(KeySelector, ElementSelector);
    }

    public class GroupByResultQuery<TBase, TPreviousResult, TKey, TResult> : RecursiveQuery<TBase, TPreviousResult, TResult> {
        public Expression<Func<TPreviousResult, TKey>> KeySelector { get; }
        public Expression<Func<TKey, IEnumerable<TPreviousResult>, TResult>> ResultSelector { get; }

        public GroupByResultQuery(
            IQuery<TBase, TPreviousResult> child,
            Expression<Func<TPreviousResult, TKey>> keySelector,
            Expression<Func<TKey, IEnumerable<TPreviousResult>, TResult>> resultSelector
        ) : base(child) {
            KeySelector = keySelector;
            ResultSelector = resultSelector;
        }

        public override IQueryable<TResult> Apply(IQueryable<TBase> queryable)
            => Child.Apply(queryable).GroupBy(KeySelector, ResultSelector);
    }

    public class GroupByResultQuery<TBase, TPreviousResult, TKey, TElement, TResult> : RecursiveQuery<TBase, TPreviousResult, TResult> {
        public Expression<Func<TPreviousResult, TKey>> KeySelector { get; }
        public Expression<Func<TPreviousResult, TElement>> ElementSelector { get; }
        public Expression<Func<TKey, IEnumerable<TElement>, TResult>> ResultSelector { get; }

        public GroupByResultQuery(
            IQuery<TBase, TPreviousResult> child,
            Expression<Func<TPreviousResult, TKey>> keySelector,
            Expression<Func<TPreviousResult, TElement>> elementSelector,
            Expression<Func<TKey, IEnumerable<TElement>, TResult>> resultSelector
        ) : base(child) {
            KeySelector = keySelector;
            ElementSelector = elementSelector;
            ResultSelector = resultSelector;
        }

        public override IQueryable<TResult> Apply(IQueryable<TBase> queryable)
            => Child.Apply(queryable).GroupBy(KeySelector, ElementSelector, ResultSelector);
    }
}
