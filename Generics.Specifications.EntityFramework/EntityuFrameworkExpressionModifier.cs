using Generics.Specifications.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;

namespace Generics.Specifications.EntityFramework {
    public class EntityFrameworkExpressionModifier : ExpressionVisitor {
        public static readonly EntityFrameworkExpressionModifier Default = new();

        static readonly MethodInfo s_includeMethodInfo
            = typeof(EntityFrameworkQueryableExtensions).GetTypeInfo().GetDeclaredMethods(nameof(EntityFrameworkQueryableExtensions.Include))
            .Single(mi => mi.GetGenericArguments().Length == 2 && mi.GetParameters().All(pi => pi.ParameterType != typeof(string)));

        static readonly MethodInfo s_thenIncludeAfterEnumerableMethodInfo
            = typeof(EntityFrameworkQueryableExtensions).GetTypeInfo().GetDeclaredMethods(nameof(EntityFrameworkQueryableExtensions.ThenInclude))
            .Where(mi => mi.GetGenericArguments().Length == 3).Single(mi => { var typeInfo = mi.GetParameters().First().ParameterType.GenericTypeArguments[1]; return typeInfo.IsGenericType && typeInfo.GetGenericTypeDefinition() == typeof(IEnumerable<>); });

        static readonly MethodInfo s_thenIncludeAfterReferenceMethodInfo
            = typeof(EntityFrameworkQueryableExtensions).GetTypeInfo().GetDeclaredMethods(nameof(EntityFrameworkQueryableExtensions.ThenInclude))
            .Single(mi => mi.GetGenericArguments().Length == 3 && mi.GetParameters().First().ParameterType.GenericTypeArguments[1].IsGenericParameter);


        protected override Expression VisitMethodCall(MethodCallExpression node) {
            var arguments = new List<Expression>();
            foreach (var nextNode in node.Arguments) arguments.Add(Visit(nextNode));

            var method = node.Method;
            var genericArguments = method.GetGenericArguments();
            if (QueryableIncludeExtensions.IsIncludeMethod(method))
                return Expression.Call(null, s_includeMethodInfo.MakeGenericMethod(genericArguments), arguments);
            else if (QueryableIncludeExtensions.IsThenIncludeAfterEnumerableMethod(method))
                return Expression.Call(null, s_thenIncludeAfterEnumerableMethodInfo.MakeGenericMethod(genericArguments), arguments);
            else if (QueryableIncludeExtensions.IsThenIncludeAfterReferenceMethod(method))
                return Expression.Call(null, s_thenIncludeAfterReferenceMethodInfo.MakeGenericMethod(genericArguments), arguments);
            else return base.VisitMethodCall(node);
        }
    }
}
