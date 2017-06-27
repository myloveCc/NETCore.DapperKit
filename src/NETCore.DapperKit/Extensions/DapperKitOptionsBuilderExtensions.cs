using System;
using System.Collections.Generic;
using System.Text;
using NETCore.DapperKit.Shared;
using NETCore.DapperKit.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace NETCore.DapperKit.Extensions
{
    public static class DapperKitOptionsBuilderExtensions
    {
        public static IDapperKitOptionsBuilder UseDapper(IDapperKitOptionsBuilder builder, DapperKitOptions options, ServiceLifetime lifetime = ServiceLifetime.Scoped)
        {
            Check.Argument.IsNotNull(builder, nameof(builder), "The DapperKitOptionsBuilder is null");
            Check.Argument.IsNotNull(options, nameof(options), "The DapperKitOptions is null");

            return builder.UseDapper(options, lifetime);
        }
    }
}
