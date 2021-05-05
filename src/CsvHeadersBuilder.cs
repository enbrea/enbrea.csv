#region ENBREA.CSV - Copyright (C) 2021 STÜBER SYSTEMS GmbH
/*    
 *    ENBREA.CSV 
 *    
 *    Copyright (C) 2021 STÜBER SYSTEMS GmbH
 *
 *    Licensed under the MIT License, Version 2.0. 
 * 
 */
#endregion

using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Enbrea.Csv
{
    public class CsvHeadersBuilder<TEntity>
    {
        public void Append(CsvHeaders<TEntity> csvHeaders, Expression expression)
        {
            AppendExpression(csvHeaders, (dynamic)expression);
        }

        private void AppendExpression(CsvHeaders<TEntity> csvHeaders, Expression expression)
        {
            throw new ArgumentException($"Expression type {expression.NodeType} not supported.");
        }

        private void AppendExpression(CsvHeaders<TEntity> csvHeaders, UnaryExpression unaryExpression)
        {
            switch (unaryExpression.NodeType)
            {
                case ExpressionType.Convert:
                    Append(csvHeaders, unaryExpression.Operand);
                    break;
                default:
                    throw new ArgumentException($"Expression type {unaryExpression.NodeType} not supported.");
            };
        }

        private void AppendExpression(CsvHeaders<TEntity> csvHeaders, NewExpression newExpression)
        {
            switch (newExpression.NodeType)
            {
                case ExpressionType.New:
                    foreach (MemberExpression memberExpression in newExpression.Arguments)
                        AppendExpression(csvHeaders, memberExpression);
                        break;
                default:
                    throw new ArgumentException($"Expression type {newExpression.NodeType} not supported.");
            };
        }

        private void AppendExpression(CsvHeaders<TEntity> csvHeaders, MemberExpression expression)
        {
            if (expression.Expression.NodeType == ExpressionType.Parameter)
            {
                var memberInfo = expression.Member;
                var headerAttr = memberInfo.GetCustomAttribute<CsvHeaderAttribute>();
                if (headerAttr != null)
                    csvHeaders.Add(headerAttr.Name);
                else
                    csvHeaders.Add(memberInfo.Name);
            }
            else
            {
                Append(csvHeaders, expression.Expression);
            }
        }
    }
}
