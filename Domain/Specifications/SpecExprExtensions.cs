using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Domain.Specifications
{
    //表达式增强类，实现Not、And、Or方法
    public static class SpecExprExtensions
    {
        // 实现逻辑非（NOT）操作的方法
        public static Expression<Func<T, bool>> Not<T>(this Expression<Func<T, bool>> one)
        {
            // 获取参数表达式
            var candidateExpr = one.Parameters[0];
            // 对主体表达式进行逻辑非操作
            var body = Expression.Not(one.Body);
            // 创建新的 lambda 表达式，并返回
            return Expression.Lambda<Func<T, bool>>(body, candidateExpr);
        }

        // 实现逻辑与（AND）操作的方法
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> one,
            Expression<Func<T, bool>> another)
        {
            // 首先定义好一个 ParameterExpression
            var candidateExpr = Expression.Parameter(typeof(T), "candidate");
            // 创建一个 ParameterReplacer 实例，用于替换表达式中的参数
            var parameterReplacer = new ParameterReplacer(candidateExpr);

            // 将表达式树的参数统一替换成我们定义好的 candidateExpr
            var left = parameterReplacer.Replace(one.Body);
            var right = parameterReplacer.Replace(another.Body);

            // 构建新的表达式树，表示逻辑与操作
            var body = Expression.And(left, right);

            // 创建新的 lambda 表达式，并返回
            return Expression.Lambda<Func<T, bool>>(body, candidateExpr);
        }

        // 实现逻辑或（OR）操作的方法
        public static Expression<Func<T, bool>> Or<T>(
            this Expression<Func<T, bool>> one,
            Expression<Func<T, bool>> another)
        {
            // 首先定义好一个 ParameterExpression
            var candidateExpr = Expression.Parameter(typeof(T), "candidate");
            // 创建一个 ParameterReplacer 实例，用于替换表达式中的参数
            var parameterReplacer = new ParameterReplacer(candidateExpr);

            // 将表达式树的参数统一替换成我们定义好的 candidateExpr
            var left = parameterReplacer.Replace(one.Body);
            var right = parameterReplacer.Replace(another.Body);
            // 构建新的表达式树，表示逻辑或操作
            var body = Expression.Or(left, right);

            // 创建新的 lambda 表达式，并返回
            return Expression.Lambda<Func<T, bool>>(body, candidateExpr);
        }
        //根据列名数组和用户输入生成一个多列单条件的lambda
        public static Specification<T> BuildSpecification<T>(string[] columnNames, string userInput)
        {
            ParameterExpression param = Expression.Parameter(typeof(T), "x");
            Expression finalExpression = null;

            foreach (var columnName in columnNames)
            {
                // 使用 Expression.PropertyOrField 创建一个 MemberExpression，表示访问对象的属性或字段
                MemberExpression member = Expression.PropertyOrField(param, columnName);

                // 使用 Expression.Constant 创建一个 ConstantExpression，表示一个常量值（用户输入的条件）
                ConstantExpression constant = Expression.Constant(userInput);

                // 获取字符串类型的 Contains 方法
                MethodInfo method = typeof(string).GetMethod("Contains", new[] { typeof(string) });

                // 使用 Expression.Call 创建一个 MethodCallExpression，表示调用指定对象上的指定方法
                MethodCallExpression methodCall = Expression.Call(member, method, constant);
                if (finalExpression == null)
                {
                    finalExpression = methodCall;
                }
                else
                {
                    finalExpression = Expression.OrElse(finalExpression, methodCall);
                }
            }

            Expression<Func<T, bool>> lambda = Expression.Lambda<Func<T, bool>>(finalExpression, param);
            return Specification<T>.Eval(lambda);
        }

        /// <summary>
        /// 根据属性名称生成表达式
        /// </summary>
        /// <param name="propertyName">属性名称字符串</param>
        /// <returns></returns>
        public static Expression<Func<T, dynamic>> GetExpression<T>(string[] propertyPath)
        {
            // 创建参数表达式，表示对象实例
            ParameterExpression parameter = Expression.Parameter(typeof(T), "x");

            // 根据属性路径构建属性访问表达式
            Expression propertyExpression = parameter;
            foreach (var propertyName in propertyPath)
            {
                propertyExpression = Expression.Property(propertyExpression, propertyName);
            }
            // 创建 lambda 表达式
            Expression<Func<T, dynamic>> lambda = Expression.Lambda<Func<T, dynamic>>(propertyExpression, parameter);
            return lambda;
        }
    }
}
