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

    public class WhereQuery<TBase, TResult> : RecursiveQuery<TBase, TResult> {
        public Expression<Func<TResult, bool>> Criteria { get; }

        public WhereQuery(IQuery<TBase, TResult> child, Expression<Func<TResult, bool>> criteria) : base(child)
            => Criteria = criteria;

        public override IQueryable<TResult> Apply(IQueryable<TBase> queryable)
            => base.Apply(queryable).Where(Criteria);
    }
}
