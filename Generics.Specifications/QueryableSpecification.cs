using Generics.Specifications.Interfaces;

namespace Generics.Specifications {
    public class QueryableSpecification<T> : ISpecification<T> {
        public static QuerySpecification<T> Empty => new();

        public Func<IQueryable<T>, IQueryable<T>> QueryFunction { get; }

        public QueryableSpecification() : this(queryable => queryable) { }

        public QueryableSpecification(Func<IQueryable<T>, IQueryable<T>> queryFunction)
            => QueryFunction = queryFunction;

        public IQueryable<T> Apply(IQueryable<T> queryable)
            => QueryFunction(queryable);
    }

    public class QueryableSpecification<TBase, TResult> : ISpecification<TBase, TResult> {
        public Func<IQueryable<TBase>, IQueryable<TResult>> QueryFunction { get; }

        public QueryableSpecification(Func<IQueryable<TBase>, IQueryable<TResult>> queryFunction)
            => QueryFunction = queryFunction;

        public IQueryable<TResult> Apply(IQueryable<TBase> queryable)
            => QueryFunction(queryable);
    }
}
