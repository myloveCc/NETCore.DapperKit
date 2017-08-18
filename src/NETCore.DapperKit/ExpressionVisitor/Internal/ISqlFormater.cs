using System;
using System.Collections.Generic;
using System.Text;

namespace NETCore.DapperKit.ExpressionVisitor.Internal
{
    public interface ISqlFormater
    {
        string Prefix { get; }

        string Left { get; }

        string Right { get; }
    }
}
