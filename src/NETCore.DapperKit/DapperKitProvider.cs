using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using NETCore.DapperKit.Infrastructure.Internal;
using System.Data.SqlClient;
using NETCore.DapperKit.Shared;

namespace NETCore.DapperKit
{
    public class DapperKitProvider : IDapperKitProvider
    {
        public DapperKitOptions Options { get; private set; }

        public DapperKitProvider(DapperKitOptions options)
        {
            Check.Argument.IsNotNull(options, nameof(options));
            Check.Argument.IsNotEmpty(options.ConnectionString, nameof(options.ConnectionString));

            Options = options;
        }

        /// <summary>
        /// create db connection
        /// </summary>
        /// <returns></returns>
        public IDbConnection DbConnection
        {
            get
            {
                return lazyDbConnection().Value;
            }
        }



        /// <summary>
        /// create lazy db connection
        /// </summary>
        /// <returns></returns>
        private Lazy<IDbConnection> lazyDbConnection()
        {
            return new Lazy<IDbConnection>(() =>
            {
                var conn = new SqlConnection(Options.ConnectionString);
                return conn;
            });
        }
    }
}
