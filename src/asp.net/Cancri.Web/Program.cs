// <copyright file="Program.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Cancri.Web
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    using AntDesign.Pro.Layout;

    using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// program.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// main.
        /// </summary>
        /// <param name="args">startup params.</param>
        /// <returns>void.</returns>
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            Console.WriteLine(builder.HostEnvironment.BaseAddress);

            builder.Services.AddScoped(sp => new HttpClient { });
            builder.Services.AddAntDesign();
            builder.Services.Configure<ProSettings>(builder.Configuration.GetSection("ProSettings"));

            builder.Logging.AddProvider(new CustomLoggingProvider());

            await builder.Build().RunAsync();
        }

        /// <inheritdoc/>
        public class CustomLoggingProvider : ILoggerProvider
        {
            private static ILogger logger;

            /// <inheritdoc/>
            public ILogger CreateLogger(string categoryName)
            {
                return logger = LoggerFactory.Create(configure =>
                {
                    configure.SetMinimumLevel(LogLevel.Information);
                }).CreateLogger("Cancri.Web");
            }

            /// <inheritdoc/>
            public void Dispose()
            {
                logger = null;
            }
        }
    }
}