using Generics.Specifications.Interfaces;

namespace Generics.Specifications.Queries {
    public class RecursiveQuery<T> : BaseQuery<T> {
        protected virtual IQuery<T> Child { get; }

        protected RecursiveQuery(IQuery<T> child)
            => Child = child;

        public override IQueryable<T> Apply(IQueryable<T> queryable)
            => Child.Apply(queryable);
    }

    public class RecursiveQuery<TBase, TResult> : BaseQuery<TBase, TResult> {
        protected virtual IQuery<TBase, TResult> Child { get; }

        protected RecursiveQuery(IQuery<TBase, TResult> child)
            => Child = child;

        public override IQueryable<TResult> Apply(IQueryable<TBase> queryable)
            => Child.Apply(queryable);
    }

    public abstract class RecursiveQuery<TBase, TPreviousResult, TResult> : BaseQuery<TBase, TResult> {
        protected virtual IQuery<TBase, TPreviousResult> Child { get; }

        protected RecursiveQuery(IQuery<TBase, TPreviousResult> child)
            => Child = child;
    }
}
