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
        public static bool Like(this string obj, string value)
        {
            return true;
        }

        /// <summary>
        /// like '% _ _ _'
        /// </summary>
        public static bool LikeLeft(this string obj, string value)
        {
            return true;
        }

        /// <summary>
        /// like '_ _ _ %'
        /// </summary>
        public static bool LikeRight(this string obj, string value)
        {
            return true;
        }

        /// <summary>
        /// int '_,_,_'
        /// </summary>
        public static bool In<T>(this string obj, T ary) where T : IEnumerable<T>
        {
            return true;
        }



        /// <summary>
        /// Count(column_name)
        /// </summary>
        public static bool Count(this object obj)
        {
            return true;
        }

    }
}
