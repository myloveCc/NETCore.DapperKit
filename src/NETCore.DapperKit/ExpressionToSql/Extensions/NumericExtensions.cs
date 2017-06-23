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

        private static bool IsNumeric(Type type)
        {
            return NumericTypes.Contains(Nullable.GetUnderlyingType(type) ?? type);
        }

        private static T CheckObjType<T>(T obj)
        {
            var type = typeof(T);
            if (!IsNumeric(type))
            {
                throw new Exception($"{nameof(T)} is not a numeric type");
            }

            return default(T);
        }

        /// <summary>
        /// MAX(column_name)
        /// </summary>
        public static T Max<T>(this T obj) where T : IComparable, IComparable<T>
        {
            return CheckObjType(obj);
        }

        /// <summary>
        /// Min(column_name)
        /// </summary>
        public static T Min<T>(this T obj) where T : IComparable, IComparable<T>
        {
            return CheckObjType(obj);
        }

        /// <summary>
        /// Avg(column_name)
        /// </summary>
        public static T Avg<T>(this T obj) where T : IComparable, IComparable<T>
        {
            return CheckObjType(obj);
        }

        /// <summary>
        /// Sum(column_name)
        /// </summary>
        public static T Sum<T>(this T obj) where T : IComparable, IComparable<T>
        {
            return CheckObjType(obj);
        }

        /// <summary>
        /// Count(column_name)
        /// </summary>
        public static T Count<T>(this T obj) where T : IComparable, IComparable<T>
        {
            return CheckObjType(obj);
        }
    }
}
