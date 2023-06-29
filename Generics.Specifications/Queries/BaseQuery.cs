using Generics.Specifications.Interfaces;

namespace Generics.Specifications.Queries {
    public class BaseQuery<T> : IQuery<T> {
        public virtual IQueryable<T> Apply(IQueryable<T> queryable) => queryable;
    }

    public abstract class BaseQuery<TBase, TResult> : IQuery<TBase, TResult> {
        public abstract IQueryable<TResult> Apply(IQueryable<TBase> queryable);
    }
}
