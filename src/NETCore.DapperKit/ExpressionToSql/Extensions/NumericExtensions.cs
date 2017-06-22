using System;
using System.Collections.Generic;
using System.Text;

namespace NETCore.DapperKit.ExpressionToSql.Extensions
{
    public static class NumericExtensions
    {
        private static readonly HashSet<Type> NumericTypes = new HashSet<Type>
        {
            typeof(int),  typeof(double),  typeof(decimal),
            typeof(long), typeof(short),   typeof(sbyte),
            typeof(byte), typeof(ulong),   typeof(ushort),
            typeof(uint), typeof(float)
        };

        private static bool IsNumeric(Type myType)
        {
            return NumericTypes.Contains(Nullable.GetUnderlyingType(myType) ?? myType);
        }

        /// <summary>
        /// MAX(column_name)
        /// </summary>
        public static bool Max<T>(this T obj) where T : IComparable, IComparable<T>
        {
            var type = typeof(T);

            if (IsNumeric(type))
            {
                return true;
            }
            throw new Exception($"{nameof(T)} is not a numeric type");
        }

        /// <summary>
        /// Min(column_name)
        /// </summary>
        public static bool Min<T>(this T obj) where T : IComparable, IComparable<T>
        {
            var type = typeof(T);

            if (IsNumeric(type))
            {
                return true;
            }
            throw new Exception($"{nameof(T)} is not a numeric type");
        }

        /// <summary>
        /// Avg(column_name)
        /// </summary>
        public static bool Avg<T>(this T obj) where T : IComparable, IComparable<T>
        {
            var type = typeof(T);

            if (IsNumeric(type))
            {
                return true;
            }
            throw new Exception($"{nameof(T)} is not a numeric type");
        }

        /// <summary>
        /// Sum(column_name)
        /// </summary>
        public static bool Sum<T>(this T obj) where T : IComparable, IComparable<T>
        {
            var type = typeof(T);

            if (IsNumeric(type))
            {
                return true;
            }
            throw new Exception($"{nameof(T)} is not a numeric type");
        }

    }
}
