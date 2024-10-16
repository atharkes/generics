using Generics.Specifications.Extensions;
using System.Linq.Expressions;
using System.Reflection;

namespace Generics.Specifications.Enumerable {
    public class EnumerableExpressionModifier : ExpressionVisitor {
        public static readonly EnumerableExpressionModifier Default = new();

        protected override Expression VisitMethodCall(MethodCallExpression node) {
            var isIncludeMethod = IncludeExtensions.IsIncludeMethod(node.Method)
                || IncludeExtensions.IsThenIncludeAfterEnumerableMethod(node.Method)
                || IncludeExtensions.IsThenIncludeAfterReferenceMethod(node.Method);

            if (!isIncludeMethod) {
                return base.VisitMethodCall(node);
            }

            var expression = (node.Arguments[1] as dynamic).Operand.Body.Arguments[0];

            if (node.Arguments[1] is not UnaryExpression unary) {
                return base.Visit(node.Arguments[0]);
            }

            var operand = unary.Operand;
            if (operand is not LambdaExpression lambda) {
                return base.Visit(node.Arguments[0]);
            }

            var body = lambda.Body;
            if (body is not MethodCallExpression method) {
                throw new Exception();
            }

            if (method.Arguments[0] is not MemberExpression member) {
                throw new Exception();
            }

            (member.Member as PropertyInfo).

            var value = method.Arguments[1];

            // Get referenced property/field: node.Arguments[1].Operand.Body.Arguments[0] 

            // Evaluate inner expression: node.Arguments[1].Operand.Body.Arguments[1]
            // Store evaluated value.

            throw new NotImplementedException();
        }
    }
}
