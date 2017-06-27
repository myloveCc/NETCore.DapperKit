using NETCore.DapperKit.ExpressionVisitor.Core;
using NETCore.DapperKit.ExpressionVisitor.Query.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace NETCore.DapperKit.ExpressionVisitor.Query
{
    public class PageQueryAble<T> : BaseCollectionQueryAble<T>, IPageQueryAble<T> where T : class
    {
        public PageQueryAble(ISqlBuilder sqlBuilder, IDapperContext provider) : base(sqlBuilder, provider)
        {

        }

        public IPageQueryAble<T> Take(int takeNum)
        {
            if (takeNum <= 0)
            {
                throw new Exception($"Take method's arg {nameof(takeNum)} must be great than 0");
            }
            SqlBuilder.Take(takeNum);
            return this;
        }
    }
}
