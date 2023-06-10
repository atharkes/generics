using Generics.Specifications.Interfaces;
using Microsoft.EntityFrameworkCore;
using QueryInterceptor.Core;

namespace Generics.Specifications.EntityFramework {
    public static class DbSetExtensions {
        public static IQueryable<T> Apply<T>(this DbSet<T> dbSet, ISpecification<T> specification) where T : class
            => specification.Apply(dbSet.AsQueryable()).InterceptWith(EntityFrameworkExpressionModifier.Default);

        public static IQueryable<TResult> Apply<T, TResult>(this DbSet<T> dbSet, ISpecification<T, TResult> specification) where T : class
            => specification.Apply(dbSet.AsQueryable()).InterceptWith(EntityFrameworkExpressionModifier.Default);
    }
}
