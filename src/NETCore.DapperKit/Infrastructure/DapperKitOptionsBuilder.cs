using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using NETCore.DapperKit.Infrastructure.Internal;
using NETCore.DapperKit.Shared;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NETCore.DapperKit.Core;

namespace NETCore.DapperKit.Infrastructure
{
    public class DapperKitOptionsBuilder : IDapperKitOptionsBuilder
    {
        /// <summary>
        /// Gets the service collection in which the interception based services are added.
        /// </summary>
        public IServiceCollection serviceCollection { get; }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/> service collection</param>
        public DapperKitOptionsBuilder(IServiceCollection services)
        {
            this.serviceCollection = services;
        }

        /// <summary>
        /// use dapper
        /// </summary>
        /// <param name="options">dapper options</param>
        /// <param name="lifetime">sevice left time</param>
        /// <returns></returns>
        public IDapperKitOptionsBuilder UseDapper(DapperKitOptions options, ServiceLifetime lifetime)
        {
            Check.Argument.IsNotNull(options, nameof(options), "The dapper options is null");

            AddProviderService(options);
            serviceCollection.TryAdd(new ServiceDescriptor(typeof(IDapperRepository), typeof(DapperRepository), lifetime));

            return this;
        }

        /// <summary>
        /// add provider service 
        /// </summary>
        /// <param name="options"></param>
        private void AddProviderService(DapperKitOptions options)
        {
            DapperKitProvider provider = new DapperKitProvider(options);
            serviceCollection.TryAddSingleton<IDapperKitProvider>(provider);
        }
    }
}
