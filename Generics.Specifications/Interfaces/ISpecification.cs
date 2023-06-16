namespace Generics.Specifications.Interfaces {
    public interface ISpecification<T> : ISpecification<T, T> {
        new IQuery<T> Query { get; }

        ISpecification<T> With(Func<IQuery<T>, IQuery<T>> queryFunction);

        IQuery<T, T> ISpecification<T, T>.Query
            => Query;
    }

    public interface ISpecification<TBase, TResult> {
        IQuery<TBase, TResult> Query { get; }

        IQueryable<TResult> Apply(IQueryable<TBase> queryable)
            => Query.Apply(queryable);

        ISpecification<TBase, TNewResult> With<TNewResult>(Func<IQuery<TBase, TResult>, IQuery<TBase, TNewResult>> queryFunction);
    }
}
