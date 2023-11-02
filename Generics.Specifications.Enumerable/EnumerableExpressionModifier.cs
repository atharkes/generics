﻿using Generics.Specifications.Extensions;
using System.Linq.Expressions;

namespace Generics.Specifications.Enumerable {
    public class EnumerableExpressionModifier : ExpressionVisitor {
        public static readonly EnumerableExpressionModifier Default = new();

        protected override Expression VisitMethodCall(MethodCallExpression node)
            => IncludeExtensions.IsIncludeMethod(node.Method)
            || IncludeExtensions.IsThenIncludeAfterEnumerableMethod(node.Method)
            || IncludeExtensions.IsThenIncludeAfterReferenceMethod(node.Method)
            ? Visit(node.Arguments[0])
            : base.VisitMethodCall(node);
    }
}
