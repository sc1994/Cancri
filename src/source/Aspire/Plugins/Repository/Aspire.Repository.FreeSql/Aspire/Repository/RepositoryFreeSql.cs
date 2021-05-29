// <copyright file="RepositoryFreeSql.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Aspire.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Aspire.Entity;

    /// <inheritdoc/>
    public class RepositoryFreeSql<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey>
        where TEntity : Entity<TPrimaryKey>
    {
        private readonly IFreeSql freeSql;

        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryFreeSql{TEntity, TPrimaryKey}"/> class.
        /// </summary>
        /// <param name="freeSql">free sql instance.</param>
        public RepositoryFreeSql(IFreeSql freeSql)
        {
            this.freeSql = freeSql;
        }

        /// <inheritdoc/>
        public virtual async Task<long> DeleteBatchAsync(Expression<Func<TEntity, bool>> filter)
        {
            var ids = await this.GetListAsync(filter).SelectAsync(x => x.Id).ToArrayAsync();
            return await this.freeSql.Delete<TEntity>(ids).ExecuteAffrowsAsync();
        }

        /// <inheritdoc/>
        public virtual async Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> filter, long limit = 0)
        {
            var where = this.freeSql.Select<TEntity>().Where(filter);
            if (limit > 0)
            {
                var limit32 = int.MaxValue;
                if (limit < limit32)
                {
                    limit32 = int.Parse(limit.ToString());
                }

                where = where.Take(limit32);
            }

            return await where.ToListAsync().ToArrayAsync();
        }

        /// <inheritdoc/>
        public virtual async Task<long> InsertBatchAsync(TEntity[] entities)
        {
            return await this.freeSql.Insert(entities).ExecuteAffrowsAsync();
        }

        /// <inheritdoc/>
        public virtual async Task<TEntity> InsertThenEntityAsync(TEntity entity)
        {
            return await this.freeSql.Insert(entity).ExecuteInsertedAsync().FirstOrDefaultAsync();
        }

        /// <inheritdoc/>
        public virtual async Task<long> UpdateBatchAsync(TEntity[] newEntities)
        {
            return await this.freeSql.Update<TEntity>().SetSource(newEntities).ExecuteAffrowsAsync();
        }
    }

    /// <inheritdoc/>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:File may only contain a single type", Justification = "<挂起>")]
    public class RepositoryFreeSql<TEntity> : RepositoryFreeSql<TEntity, Guid>, IRepository<TEntity>
        where TEntity : Entity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryFreeSql{TEntity}"/> class.
        /// </summary>
        /// <param name="freeSql">free sql instance.</param>
        public RepositoryFreeSql(IFreeSql freeSql)
            : base(freeSql)
        {
        }
    }
}
