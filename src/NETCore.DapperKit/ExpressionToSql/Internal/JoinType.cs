using System;
using System.Collections.Generic;
using System.Text;

namespace NETCore.DapperKit.ExpressionToSql.Internal
{
    public enum JoinType
    {
        NONE,
        INNER,
        LEFT,
        RIGHT,
        FULL
    }
}
