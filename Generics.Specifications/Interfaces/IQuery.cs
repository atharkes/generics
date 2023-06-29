namespace Generics.Specifications.Interfaces {
    public interface IQuery<T> : IQuery<T, T> { }

    public interface IQuery<TBase, T> {
        IQueryable<T> Apply(IQueryable<TBase> queryable);
    }
}
