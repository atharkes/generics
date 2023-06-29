using Generics.Specifications.Interfaces;

namespace Generics.Specifications.Queries {
    public class TakeQuery<T> : RecursiveQuery<T> {
        public uint Amount { get; }

        public TakeQuery(IQuery<T> child, uint amount) : base(child)
            => Amount = amount;

        public override IQueryable<T> Apply(IQueryable<T> queryable)
            => base.Apply(queryable).Take((int)Amount);
    }

    public class TakeQuery<TBase, TResult> : RecursiveQuery<TBase, TResult> {
        public uint Amount { get; }

        public TakeQuery(IQuery<TBase, TResult> child, uint amount) : base(child)
            => Amount = amount;

        public override IQueryable<TResult> Apply(IQueryable<TBase> queryable)
            => base.Apply(queryable).Take((int)Amount);
    }
}
