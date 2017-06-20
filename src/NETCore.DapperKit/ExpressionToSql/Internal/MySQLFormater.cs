using System;
using System.Collections.Generic;
using System.Text;

namespace NETCore.DapperKit.ExpressionToSql.Internal
{
    public class MySQLFormater : ISqlFormater
    {
        public string Prefix => "@";

        public string Left => "`";

        public string Right => "`";
    }
}
