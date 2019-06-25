using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace NittyGritty.Utilities
{
    public static class ExpressionUtilities
    {
        /// <summary>Returns the property name of the property specified in the given lambda (e.g. GetPropertyName(i => i.MyProperty)). </summary>
        /// <typeparam name="TClass">The type of the class with the property. </typeparam>
        /// <typeparam name="TProperty">The property type. </typeparam>
        /// <param name="expression">The lambda with the property. </param>
        /// <returns>The name of the property in the lambda. </returns>
        public static string GetPropertyName<TClass, TProperty>(Expression<Func<TClass, TProperty>> expression)
        {
            return expression.Body is UnaryExpression
                ? ((MemberExpression)(((UnaryExpression)expression.Body).Operand)).Member.Name
                : ((MemberExpression)expression.Body).Member.Name;
        }

        /// <summary>Returns the property name of the property specified in the given lambda (e.g. GetPropertyName(i => i.MyProperty)). </summary>
        /// <typeparam name="TProperty">The property type. </typeparam>
        /// <param name="expression">The lambda with the property. </param>
        /// <returns>The name of the property in the lambda. </returns>
        public static string GetPropertyName<TProperty>(Expression<Func<TProperty>> expression)
        {
            return expression.Body is UnaryExpression
                ? ((MemberExpression)(((UnaryExpression)expression.Body).Operand)).Member.Name
                : ((MemberExpression)expression.Body).Member.Name;
        }

        /// <summary>Returns the property name of the property specified in the given lambda (e.g. GetPropertyName(i => i.MyProperty)). </summary>
        /// <typeparam name="TClass">The type of the class with the property. </typeparam>
        /// <param name="expression">The lambda with the property. </param>
        /// <returns>The name of the property in the lambda. </returns>
        public static string GetPropertyName<TClass>(Expression<Func<TClass, object>> expression)
        {
            return expression.Body is UnaryExpression
                ? ((MemberExpression)(((UnaryExpression)expression.Body).Operand)).Member.Name
                : ((MemberExpression)expression.Body).Member.Name;
        }
    }
}
