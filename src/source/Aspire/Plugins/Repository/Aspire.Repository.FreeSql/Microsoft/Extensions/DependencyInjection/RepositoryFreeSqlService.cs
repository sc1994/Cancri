// <copyright file="RepositoryFreeSqlService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Aspire.Repository
{
    using System;
    using global::FreeSql;
    using global::FreeSql.Aop;
    using global::Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// free sql injecting services.
    /// </summary>
    public static class RepositoryFreeSqlService
    {
        /// <summary>
        /// add.
        /// </summary>
        /// <param name="services">service collection.</param>
        /// <param name="dataType">data base type.</param>
        /// <param name="connectionString">connection string.</param>
        /// <param name="auditValueEvent">audit value event.</param>
        /// <param name="curdAfterEvent">curd after event.</param>
        /// <param name="curdBeforeEvent">curd before event.</param>
        /// <returns>remain service collection.</returns>
        public static IServiceCollection AddRepositoryFreeSql(
            this IServiceCollection services,
            DataType dataType,
            string connectionString,
            EventHandler<AuditValueEventArgs> auditValueEvent = default,
            EventHandler<CurdAfterEventArgs> curdAfterEvent = default,
            EventHandler<CurdBeforeEventArgs> curdBeforeEvent = default)
        {
            services.AddSingleton(serviceProvider =>
            {
                var freeSql = new FreeSqlBuilder()
                .UseConnectionString(dataType, connectionString)
#if DEBUG
                .UseAutoSyncStructure(true)
#endif
                .Build();

                if (auditValueEvent != default)
                {
                    freeSql.Aop.AuditValue += auditValueEvent;
                }

                if (curdAfterEvent != default)
                {
                    freeSql.Aop.CurdAfter += curdAfterEvent;
                }

                if (curdBeforeEvent != default)
                {
                    freeSql.Aop.CurdBefore += curdBeforeEvent;
                }

                return freeSql;
            });

            services.AddSingleton(typeof(IRepository<>), typeof(RepositoryFreeSql<>));
            services.AddSingleton(typeof(IRepository<,>), typeof(RepositoryFreeSql<,>));

            return services;
        }
    }
}
