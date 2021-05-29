// <copyright file="ApplicationDynamicApiService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Aspire.Application
{
    using Microsoft.Extensions.DependencyInjection;

    using Panda.DynamicWebApi;

    using System;
    using System.Reflection;

    /// <summary>
    /// application dynamic api injecting services.
    /// </summary>
    public static class ApplicationDynamicApiService
    {
        /// <summary>
        /// add dynamic api.
        /// </summary>
        /// <param name="services">service collection.</param>
        /// <param name="options">options. 参见 https://github.com/pda-team/Panda.DynamicWebApi#3configuration .</param>
        /// <returns>remain service collection.</returns>
        public static IServiceCollection AddApplicationDynamicApi(this IServiceCollection services, Action<DynamicWebApiOptions> options)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (options is null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            services.AddDynamicWebApi(options);

            return services;
        }
    }
}
