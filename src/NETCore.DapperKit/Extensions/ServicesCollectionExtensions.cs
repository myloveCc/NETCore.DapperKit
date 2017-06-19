using Microsoft.Extensions.DependencyInjection;
using NETCore.DapperKit.Infrastructure;
using NETCore.DapperKit.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace NETCore.DapperKit.Extensions
{
    public static class ServicesCollectionExtensions
    {
        public static IServiceCollection AddDapperKit(this IServiceCollection serviceCollection, Action<DapperKitOptionsBuilder> optionsAction)
        {
            Check.Argument.IsNotNull(serviceCollection, nameof(serviceCollection), "IServiceCollection is not dependency injection");
            Check.Argument.IsNotNull(optionsAction, nameof(optionsAction));

            optionsAction.Invoke(new DapperKitOptionsBuilder(serviceCollection));

            return serviceCollection;
        }
    }
}
