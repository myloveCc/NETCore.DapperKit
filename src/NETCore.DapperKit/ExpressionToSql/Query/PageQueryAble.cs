using NETCore.DapperKit.ExpressionToSql.Core;
using NETCore.DapperKit.ExpressionToSql.Query.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace NETCore.DapperKit.ExpressionToSql.Query
{
    public class PageQueryAble<T> : BaseCollectionQueryAble<T>, IPageQueryAble<T> where T : class
    {
        public PageQueryAble(ISqlBuilder sqlBuilder, IDapperKitProvider provider) : base(sqlBuilder, provider)
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
