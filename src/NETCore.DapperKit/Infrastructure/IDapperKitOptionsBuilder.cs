using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using NETCore.DapperKit.Infrastructure.Internal;

namespace NETCore.DapperKit.Infrastructure
{
    public interface IDapperKitOptionsBuilder
    {
        /// <summary>
        /// service collection
        /// </summary>
        IServiceCollection serviceCollection { get; }

        /// <summary>
        /// add dapper service
        /// </summary>
        /// <param name="options">dapper options</param>
        /// <param name="lifetime"><see cref="ServiceLifetime"/></param>
        /// <returns></returns>

        IDapperKitOptionsBuilder UseDapper(DapperKitOptions options, ServiceLifetime lifetime);
    }
}
