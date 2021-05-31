// <copyright file="Startup.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Cancri.Service
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Xml;

    using Aspire;
    using Aspire.Application;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.StaticFiles;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Microsoft.OpenApi.Models;

    /// <summary>
    /// startup.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">services.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddApplicationDynamicApi(options =>
            {
                options.AddAssemblyOptions(typeof(Startup).Assembly, "api", "GET");
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Cancri.Service", Version = "v1" });
                c.DocInclusionPredicate((docName, description) => true);
            });
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">app.</param>
        /// <param name="env">env.</param>
        /// <param name="logger">logger.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Cancri.Service v1"));
            }

            app.UseRouting();

            app.UseDefaultFiles();
            var fileExtensionContentTypeProvider = new FileExtensionContentTypeProvider();
            var webConfigPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "web.config");
            logger.LogInformation("web config path: {0}", webConfigPath);

            var webConfig = new XmlDocument();
            webConfig.LoadXml(File.ReadAllText(webConfigPath));

            // 简单解析一下 web config xml.
            var mimeMaps = webConfig.DocumentElement.SelectNodes("/configuration/system.webServer/staticContent/mimeMap");
            foreach (XmlNode item in mimeMaps)
            {
                var fileExtension = item.Attributes["fileExtension"].Value;
                var mimeType = item.Attributes["mimeType"].Value;
                logger.LogInformation("set type: {0}, mime: {1}.", fileExtension, mimeType);

                fileExtensionContentTypeProvider.Mappings.Remove(fileExtension);
                fileExtensionContentTypeProvider.Mappings.Add(fileExtension, mimeType);
            }

            app.UseStaticFiles(new StaticFileOptions
            {
                ContentTypeProvider = fileExtensionContentTypeProvider,
            });

            app.UseAuthorization();

            app.UseCors(configurePolicy =>
            {
                configurePolicy.AllowAnyMethod();
                configurePolicy.AllowAnyHeader();
                configurePolicy.AllowCredentials();
                configurePolicy.WithOrigins("http://localhost:5000");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
