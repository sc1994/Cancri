// <copyright file="Program.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Cancri.Web
{
    using System;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    using AntDesign.Pro.Layout;

    using Cancri.Web.Providers;

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
        /// <param name="args">args.</param>
        /// <returns>void.</returns>
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient()
            {
#if DEBUG
                BaseAddress = new Uri("http://localhost:5001"),
#endif
            });
            builder.Services.AddAntDesign();
            builder.Services.Configure<ProSettings>(builder.Configuration.GetSection("ProSettings"));

            builder.Logging.AddProvider(new CustomLoggingProvider());

            await builder.Build().RunAsync();
        }
    }
}