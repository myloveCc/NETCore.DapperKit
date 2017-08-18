using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;


namespace NETCore.DapperKit
{
    public interface IDapperContext
    {
        /// <summary>
        /// DapperKit options
        /// </summary>
        DapperKitOptions Options { get; }

        /// <summary>
        /// create db collection
        /// </summary>
        /// <returns></returns>
        IDbConnection DbConnection { get; }

    }
}
