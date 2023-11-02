using Generics.Specifications.Interfaces;
using QueryInterceptor.Core;

namespace Generics.Specifications.Enumerable {
    public static class EnumerableExtensions {
        public static IEnumerable<T> Apply<T>(this IEnumerable<T> enumerable, ISpecification<T> specification)
            => specification.Apply(enumerable.AsQueryable()).InterceptWith(EnumerableExpressionModifier.Default);

        public static IEnumerable<TResult> Apply<T, TResult>(this IEnumerable<T> enumerable, ISpecification<T, TResult> specification)
            => specification.Apply(enumerable.AsQueryable()).InterceptWith(EnumerableExpressionModifier.Default);

        /// <summary>
        /// References for further development:
        /// https://www.csharp411.com/c-object-clone-wars/
        /// https://devblogs.microsoft.com/premier-developer/dissecting-the-new-constraint-in-c-a-perfect-example-of-a-leaky-abstraction/
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="enumerable"></param>
        /// <param name="navigationFunction"></param>
        /// <param name="modificationFunction"></param>
        /// <returns></returns>
        public static IEnumerable<T> Modify<T, TProperty>(
            this IEnumerable<T> enumerable,
            Func<T, TProperty> navigationFunction,
            Func<TProperty, TProperty> modificationFunction
        ) => enumerable.Select(item => {
            // Only works when parameterless constructor is defined
            var newItem = Activator.CreateInstance<T>();

            // Only works when both setters and getters are available
            foreach (var property in item.GetType().GetProperties()) {
                property.SetValue(newItem, property.GetValue(item));
            }

            var modifiedValue = modificationFunction(navigationFunction(item));
            // TODO: Apply modified value to new item:
            // navigationFunction(item) = modifiedValue;

            return newItem;
        });
    }
}
