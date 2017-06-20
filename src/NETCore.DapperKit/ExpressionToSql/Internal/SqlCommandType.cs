using System;
using System.Collections.Generic;
using System.Text;

namespace NETCore.DapperKit.ExpressionToSql.Internal
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
