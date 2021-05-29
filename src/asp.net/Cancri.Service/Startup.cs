// <copyright file="Startup.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Cancri.Service
{
    using Aspire.Application;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.StaticFiles;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
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
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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
            fileExtensionContentTypeProvider.Mappings.Remove(".blat");
            fileExtensionContentTypeProvider.Mappings.Remove(".dll");
            fileExtensionContentTypeProvider.Mappings.Remove(".dat");
            fileExtensionContentTypeProvider.Mappings.Remove(".json");
            fileExtensionContentTypeProvider.Mappings.Remove(".wasm");
            fileExtensionContentTypeProvider.Mappings.Remove(".woff");
            fileExtensionContentTypeProvider.Mappings.Remove(".woff2");
            fileExtensionContentTypeProvider.Mappings.Remove(".css");
            fileExtensionContentTypeProvider.Mappings.Remove(".js");
            fileExtensionContentTypeProvider.Mappings.Add(".blat", "application/octet-stream");
            fileExtensionContentTypeProvider.Mappings.Add(".dll", "application/octet-stream");
            fileExtensionContentTypeProvider.Mappings.Add(".dat", "application/octet-stream");
            fileExtensionContentTypeProvider.Mappings.Add(".json", "application/json");
            fileExtensionContentTypeProvider.Mappings.Add(".wasm", "application/wasm");
            fileExtensionContentTypeProvider.Mappings.Add(".woff", "application/font-woff");
            fileExtensionContentTypeProvider.Mappings.Add(".woff2", "application/font-woff");
            fileExtensionContentTypeProvider.Mappings.Add(".css", "application/css");
            fileExtensionContentTypeProvider.Mappings.Add(".js", "application/javascript");
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
