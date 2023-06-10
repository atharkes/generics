using Generics.Specifications.Interfaces;
using Generics.Specifications.Queries;

namespace Generics.Specifications {
    public class Specification<T> : ISpecification<T> {
        public static Specification<T> Empty => new();

        public IQuery<T> Query { get; protected set; }

        public Specification() : this(new BaseQuery<T>()) { }

        public Specification(Func<IQuery<T>, IQuery<T>> queryFunction) : this(queryFunction.Invoke(new BaseQuery<T>())) { }

        public Specification(IQuery<T> query)
            => Query = query;

        public ISpecification<T> With(Func<IQuery<T>, IQuery<T>> queryFunction)
            => new Specification<T>(queryFunction.Invoke(Query));

        public ISpecification<T, TNewResult> With<TNewResult>(Func<IQuery<T, T>, IQuery<T, TNewResult>> queryFunction)
            => new Specification<T, TNewResult>(queryFunction.Invoke(Query));
    }

    public class Specification<TBase, TResult> : ISpecification<TBase, TResult> {
        public IQuery<TBase, TResult> Query { get; protected set; }

        public Specification(IQuery<TBase, TResult> query)
            => Query = query;

        public Specification(Func<IQuery<TBase>, IQuery<TBase, TResult>> queryFunction)
            => Query = queryFunction.Invoke(new BaseQuery<TBase>());

        public ISpecification<TBase, TNewResult> With<TNewResult>(Func<IQuery<TBase, TResult>, IQuery<TBase, TNewResult>> queryFunction)
            => new Specification<TBase, TNewResult>(queryFunction.Invoke(Query));
    }
}
