using NETCore.DapperKit.ExpressionVisitor.Core;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace NETCore.DapperKit.ExpressionVisitor.SqlVisitor.Interface
{
    public interface ISqlVisitor
    {
        ISqlBuilder Insert(Expression expression, ISqlBuilder ISqlBuilder);

        ISqlBuilder Update(Expression expression, ISqlBuilder ISqlBuilder);

        ISqlBuilder Select(Expression expression, ISqlBuilder ISqlBuilder);

        ISqlBuilder Join(Expression expression, ISqlBuilder ISqlBuilder);

        ISqlBuilder Where(Expression expression, ISqlBuilder ISqlBuilder);

        ISqlBuilder In(Expression expression, ISqlBuilder ISqlBuilder);

        ISqlBuilder GroupBy(Expression expression, ISqlBuilder ISqlBuilder);

        ISqlBuilder OrderBy(Expression expression, ISqlBuilder ISqlBuilder);

        ISqlBuilder ThenBy(Expression expression, ISqlBuilder ISqlBuilder);

        ISqlBuilder OrderByDescending(Expression expression, ISqlBuilder ISqlBuilder);

        ISqlBuilder ThenByDescending(Expression expression, ISqlBuilder ISqlBuilder);

        ISqlBuilder Max(Expression expression, ISqlBuilder ISqlBuilder);

        ISqlBuilder Min(Expression expression, ISqlBuilder ISqlBuilder);

        ISqlBuilder Avg(Expression expression, ISqlBuilder ISqlBuilder);

        ISqlBuilder Count(Expression expression, ISqlBuilder ISqlBuilder);

        ISqlBuilder Sum(Expression expression, ISqlBuilder ISqlBuilder);

        ISqlBuilder Delete(Expression expression, ISqlBuilder ISqlBuilder);


    }
}
