using Generics.Specifications.Interfaces;
using System.Linq.Expressions;

namespace Generics.Specifications.Queries {
    public class WhereQuery<T> : RecursiveQuery<T> {
        public Expression<Func<T, bool>> Criteria { get; }

        public WhereQuery(IQuery<T> child, Expression<Func<T, bool>> criteria) : base(child)
            => Criteria = criteria;

        public override IQueryable<T> Apply(IQueryable<T> queryable)
            => base.Apply(queryable).Where(Criteria);
    }

    public class WhereQuery<TBase, T> : RecursiveQuery<TBase, T> {
        public Expression<Func<T, bool>> Criteria { get; }

        public WhereQuery(IQuery<TBase, T> child, Expression<Func<T, bool>> criteria) : base(child)
            => Criteria = criteria;

        public override IQueryable<T> Apply(IQueryable<TBase> queryable)
            => base.Apply(queryable).Where(Criteria);
    }
}
