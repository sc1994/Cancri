// <copyright file="Entity.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Aspire.Entity
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using FreeSql.DataAnnotations;

    /// <summary>
    /// entity.
    /// </summary>
    /// <typeparam name="TPrimaryKey">primary key.</typeparam>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:File may only contain a single type", Justification = "<挂起>")]
    public class Entity<TPrimaryKey> : IEntity<TPrimaryKey>
    {
        /// <summary>
        /// Gets or sets id.
        /// </summary>
        [Column(IsIdentity = true, IsPrimary = true)]
        public virtual TPrimaryKey Id { get; set; }
    }

    /// <inheritdoc/>
    public class Entity : Entity<Guid>, IEntity
    {
    }
}
