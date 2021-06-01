// <copyright file="FileAppService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Cancri.Service.FileService
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web;

    using Aspire.Application;

    using Cancri.Dto.FileService;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// file service.
    /// </summary>
    public class FileAppService : IAppService
    {
        private readonly ILogger<FileAppService> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileAppService"/> class.
        /// </summary>
        /// <param name="logger">logger.</param>
        public FileAppService(ILogger<FileAppService> logger)
        {
            this.logger = logger;
        }

        /// <summary>
        /// get file list by path.
        /// </summary>
        /// <param name="path">path.</param>
        /// <returns>file item list.</returns>
        public IEnumerable<FileItemDto> Get(string path)
        {
            path = HttpUtility.UrlDecode(path);
            this.logger.LogInformation("path value : {0}", path);

            return Directory.GetDirectories(path).Select(x =>
            {
                var info = new DirectoryInfo(x);
                return new FileItemDto(info.Name, true, -1, info.CreationTime);
            }).Concat(Directory.GetFiles(path).Select(x =>
            {
                var info = new FileInfo(x);
                return new FileItemDto(info.Name, false, info.Length, info.CreationTime, info.LastWriteTime);
            }));
        }
    }
}
