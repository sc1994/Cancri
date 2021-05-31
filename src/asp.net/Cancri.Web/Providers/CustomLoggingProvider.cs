// <copyright file="CustomLoggingProvider.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Cancri.Web.Providers
{
    using Microsoft.Extensions.Logging;

    /// <inheritdoc/>
    public class CustomLoggingProvider : ILoggerProvider
    {
        private static ILogger logger;
        private static object lockObject = new ();

        /// <inheritdoc/>
        public ILogger CreateLogger(string categoryName)
        {
            if (logger != null)
            {
                return logger;
            }

            lock (lockObject)
            {
                return logger = LoggerFactory.Create(configure =>
                {
                    configure.SetMinimumLevel(LogLevel.Information);
                }).CreateLogger("Cancri.Web");
            }
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            logger = null;
        }
    }
}