using System;
using System.Collections.Generic;
using System.Text;

namespace NETCore.DapperKit
{
    public class DapperKitOptions
    {
        /// <summary>
        /// connection string
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// database type
        /// </summary>
        public DatabaseType DatabaseType { get; set; } = DatabaseType.SQLServer;

        /// <summary>
        /// command time out
        /// </summary>
        public int? CommandTimeout { get; set; }

    }
}
