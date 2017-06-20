using System;
using System.Collections.Generic;
using System.Text;

namespace NETCore.DapperKit.ExpressionToSql.Internal
{
    public interface ISqlFormater
    {
        string Prefix { get; }

        string Left { get; }

        string Right { get; }
    }
}
