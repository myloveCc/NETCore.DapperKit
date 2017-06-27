using System;
using System.Collections.Generic;
using System.Text;

namespace NETCore.DapperKit.ExpressionVisitor.Internal
{
    /// <summary>
    /// sql command typ
    /// </summary>
    public enum SqlCommandType
    {
        Insert,
        Delete,
        Update,
        Select,
        Calculate
    }
}
