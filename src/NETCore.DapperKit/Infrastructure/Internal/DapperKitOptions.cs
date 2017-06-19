using System;
using System.Collections.Generic;
using System.Text;

namespace NETCore.DapperKit.Infrastructure.Internal
{
    public class DapperKitOptions
    {
        /// <summary>
        /// connection string
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// command time out
        /// </summary>
        public int? CommandTimeout { get; set; }
    }
}
