using Generics.Specifications.Interfaces;

namespace Generics.Specifications.Queries {
    public class RecursiveQuery<T> : BaseQuery<T> {
        protected virtual IQuery<T> Child { get; }

        protected RecursiveQuery(IQuery<T> child)
            => Child = child;

        public override IQueryable<T> Apply(IQueryable<T> queryable)
            => Child.Apply(queryable);
    }

    public class RecursiveQuery<TBase, T> : BaseQuery<TBase, T> {
        protected virtual IQuery<TBase, T> Child { get; }

        protected RecursiveQuery(IQuery<TBase, T> child)
            => Child = child;

        public override IQueryable<T> Apply(IQueryable<TBase> queryable)
            => Child.Apply(queryable);
    }

    public abstract class RecursiveQuery<TBase, TPrevious, TCurrent> : BaseQuery<TBase, TCurrent> {
        protected virtual IQuery<TBase, TPrevious> Child { get; }

        protected RecursiveQuery(IQuery<TBase, TPrevious> child)
            => Child = child;
    }
}
