using System;
using System.Collections.Generic;
using System.Text;

namespace NETCore.DapperKit.ExpressionVisitor.Internal
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
