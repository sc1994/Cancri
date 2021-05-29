// <copyright file="IEntity.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Aspire.Entity
{
    using System;

    /// <summary>
    /// entity.
    /// </summary>
    /// <typeparam name="TPrimaryKey">primary key.</typeparam>
    public interface IEntity<TPrimaryKey>
    {
        /// <summary>
        /// Gets or sets id.
        /// </summary>
        TPrimaryKey Id { get; set; }
    }

    /// <inheritdoc/>
    public interface IEntity : IEntity<Guid>
    {
    }
}
