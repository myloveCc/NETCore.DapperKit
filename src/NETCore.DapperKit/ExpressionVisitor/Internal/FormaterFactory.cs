
using System;
using System.Collections.Generic;
using System.Text;

namespace NETCore.DapperKit.ExpressionVisitor.Internal
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
