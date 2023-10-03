using Generics.Specifications.Interfaces;
using Generics.Specifications.Queries;

namespace Generics.Specifications {
    public class QuerySpecification<T> : IQuerySpecification<T> {
        public static QuerySpecification<T> Empty => new();

        public IQuery<T> Query { get; protected set; }

        public QuerySpecification() : this(new BaseQuery<T>()) { }

        public QuerySpecification(Func<IQuery<T>, IQuery<T>> queryFunction) : this(queryFunction.Invoke(new BaseQuery<T>())) { }

        public QuerySpecification(IQuery<T> query)
            => Query = query;

        public ISpecification<T> With(Func<IQuery<T>, IQuery<T>> queryFunction)
            => new QuerySpecification<T>(queryFunction.Invoke(Query));

        public ISpecification<T, TNewResult> With<TNewResult>(Func<IQuery<T, T>, IQuery<T, TNewResult>> queryFunction)
            => new QuerySpecification<T, TNewResult>(queryFunction.Invoke(Query));
    }

    public class QuerySpecification<TBase, TResult> : IQuerySpecification<TBase, TResult> {
        public IQuery<TBase, TResult> Query { get; protected set; }

        public QuerySpecification(IQuery<TBase, TResult> query)
            => Query = query;

        public QuerySpecification(Func<IQuery<TBase>, IQuery<TBase, TResult>> queryFunction)
            => Query = queryFunction.Invoke(new BaseQuery<TBase>());

        public ISpecification<TBase, TNewResult> With<TNewResult>(Func<IQuery<TBase, TResult>, IQuery<TBase, TNewResult>> queryFunction)
            => new QuerySpecification<TBase, TNewResult>(queryFunction.Invoke(Query));
    }
}
