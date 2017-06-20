using NETCore.DapperKit.Infrastructure.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace NETCore.DapperKit.ExpressionToSql.Internal
{
    public class FormaterFactory
    {
        public static ISqlFormater SqlFormater(DatabaseType type)
        {
            if (type == DatabaseType.MySQL)
            {
                return new MySQLFormater();
            }

            return new SQLServerFormater();
        }
    }
}
