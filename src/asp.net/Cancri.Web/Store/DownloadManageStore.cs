// <copyright file="DownloadManageStore.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Cancri.Web.Store
{
    using System.Threading.Tasks;

    using Microsoft.JSInterop;

    /// <summary>
    /// 下载管理的 相关通用操作.
    /// </summary>
    public class DownloadManageStore
    {
        private const string RootPathKey = "DownloadManageStore:RootPathKey";
        private const string CurrentPathKey = "DownloadManageStore:CurrentPathKey";

        private readonly IJSRuntime jSRunTime;

        /// <summary>
        /// Initializes a new instance of the <see cref="DownloadManageStore"/> class.
        /// </summary>
        /// <param name="jSRunTime">js run time.</param>
        public DownloadManageStore(IJSRuntime jSRunTime)
        {
            this.jSRunTime = jSRunTime;
        }

        /// <summary>
        /// 设置根路径.
        /// </summary>
        /// <param name="rootPath">根路径.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task SetRootPath(string rootPath)
        {
            await this.jSRunTime.InvokeVoidAsync("localStorage.setItem", RootPathKey, rootPath);
        }

        /// <summary>
        /// 设置当前路径.
        /// </summary>
        /// <param name="currentPath">当前路径.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task SetCurrentPath(string currentPath)
        {
            await this.jSRunTime.InvokeVoidAsync("localStorage.setItem", CurrentPathKey, currentPath);
        }

        /// <summary>
        /// 获取根路径.
        /// </summary>
        /// <returns>根路径.</returns>
        public async Task<string> GetRootPath()
        {
            return await this.jSRunTime.InvokeAsync<string>("localStorage.getItem", RootPathKey);
        }

        /// <summary>
        /// 获取当前路径.
        /// </summary>
        /// <returns>当前路径.</returns>
        public async Task<string> GetCurrentPath()
        {
            return await this.jSRunTime.InvokeAsync<string>("localStorage.getItem", CurrentPathKey);
        }
    }
}
