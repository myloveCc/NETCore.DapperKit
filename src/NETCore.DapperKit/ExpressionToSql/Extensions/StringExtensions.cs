using System;
using System.Collections.Generic;
using System.Text;

namespace NETCore.DapperKit.ExpressionToSql.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// like '% _ _ %'
        /// </summary>
        public static bool Like(this string property, string value)
        {
            return true;
        }

        /// <summary>
        /// like '% _ _ _'
        /// </summary>
        public static bool LikeLeft(this string property, string value)
        {
            return true;
        }

        /// <summary>
        /// like '_ _ _ %'
        /// </summary>
        public static bool LikeRight(this string property, string value)
        {
            return true;
        }

        /// <summary>
        /// int '_,_,_'
        /// </summary>
        public static bool In<T>(this T property, IEnumerable<T> ary) where T : IComparable, IComparable<T>
        {
            return true;
        }
    }
}
