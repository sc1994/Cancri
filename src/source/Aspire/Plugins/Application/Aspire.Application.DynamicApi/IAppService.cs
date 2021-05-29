// <copyright file="IAppService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Aspire.Application
{
    using Panda.DynamicWebApi;
    using Panda.DynamicWebApi.Attributes;

    /// <summary>
    /// application dynamic api.
    /// </summary>
    [DynamicWebApi]
    public interface IAppService : IDynamicWebApi
    {
    }
}
