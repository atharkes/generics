using Generics.Specifications.Interfaces;

namespace Generics.Specifications.Queries {
    public class SkipQuery<T> : RecursiveQuery<T> {
        public uint Amount { get; }

        public SkipQuery(IQuery<T> child, uint amount) : base(child)
            => Amount = amount;

        public override IQueryable<T> Apply(IQueryable<T> queryable)
            => base.Apply(queryable).Skip((int)Amount);
    }

    public class SkipQuery<TBase, TResult> : RecursiveQuery<TBase, TResult> {
        public uint Amount { get; }

        public SkipQuery(IQuery<TBase, TResult> child, uint amount) : base(child)
            => Amount = amount;

        public override IQueryable<TResult> Apply(IQueryable<TBase> queryable)
            => base.Apply(queryable).Skip((int)Amount);
    }
}
