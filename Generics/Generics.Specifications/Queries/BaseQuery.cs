using Generics.Specifications.Interfaces;
using System.Linq.Expressions;

namespace Generics.Specifications.Queries {
    public class BaseQuery<T> : IQuery<T> {
        public IIncludedQuery<T, TProperty> Include<TProperty>(Expression<Func<T, TProperty>> selector)
            => new IncludeQuery<T, TProperty>(this, selector);

        public IOrderedQuery<T> OrderBy<TProperty>(Expression<Func<T, TProperty>> selector, bool descending) where TProperty : IComparable<TProperty>
            => new OrderByQuery<T, TProperty>(this, selector, descending);

        public IQuery<T, TProperty> Select<TProperty>(Expression<Func<T, TProperty>> selector)
            => new SelectQuery<T, T, TProperty>(this, selector);

        public IQuery<T, TProperty> SelectMany<TProperty>(Expression<Func<T, IEnumerable<TProperty>>> selector)
            => new SelectManyQuery<T, T, TProperty>(this, selector);

        public IQuery<T> Skip(uint amount)
            => new SkipQuery<T>(this, amount);

        public IQuery<T> Take(uint amount)
            => new TakeQuery<T>(this, amount);

        public IQuery<T> Where(Expression<Func<T, bool>> criteria)
            => new WhereQuery<T>(this, criteria);

        public virtual IQueryable<T> Apply(IQueryable<T> queryable) => queryable;
    }

    public abstract class BaseQuery<TBase, T> : IQuery<TBase, T> {
        public IIncludedQuery<TBase, T, TProperty> Include<TProperty>(Expression<Func<T, TProperty>> selector)
            => new IncludeQuery<TBase, T, TProperty>(this, selector);

        public IOrderedQuery<TBase, T> OrderBy<TProperty>(Expression<Func<T, TProperty>> selector, bool descending) where TProperty : IComparable<TProperty>
            => new OrderQuery<TBase, T, TProperty>(this, selector, descending);

        public IQuery<TBase, TProperty> Select<TProperty>(Expression<Func<T, TProperty>> selector)
            => new SelectQuery<TBase, T, TProperty>(this, selector);

        public IQuery<TBase, TProperty> SelectMany<TProperty>(Expression<Func<T, IEnumerable<TProperty>>> selector)
            => new SelectManyQuery<TBase, T, TProperty>(this, selector);

        public IQuery<TBase, T> Skip(uint amount)
            => new SkipQuery<TBase, T>(this, amount);

        public IQuery<TBase, T> Take(uint amount)
            => new TakeQuery<TBase, T>(this, amount);

        public IQuery<TBase, T> Where(Expression<Func<T, bool>> criteria)
            => new WhereQuery<TBase, T>(this, criteria);

        public abstract IQueryable<T> Apply(IQueryable<TBase> queryable);
    }
}
