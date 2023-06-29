using Generics.Specifications.Interfaces;

namespace Generics.Specifications.Queries {
    public class BaseQuery<T> : IQuery<T> {
        public virtual IQueryable<T> Apply(IQueryable<T> queryable) => queryable;
    }

    public abstract class BaseQuery<TBase, T> : IQuery<TBase, T> {
        public abstract IQueryable<T> Apply(IQueryable<TBase> queryable);
    }
}
