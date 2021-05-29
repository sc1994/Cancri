// <copyright file="IRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Aspire.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Aspire.Entity;

    /// <summary>
    /// repository.
    /// </summary>
    /// <typeparam name="TEntity">entity.</typeparam>
    /// <typeparam name="TPrimaryKey">primary key.</typeparam>
    public interface IRepository<TEntity, in TPrimaryKey>
        where TEntity : IEntity<TPrimaryKey>
    {
        /// <summary>
        /// insert.
        /// </summary>
        /// <param name="entity">entity.</param>
        /// <returns>成功与否.</returns>
        public async Task<bool> InsertAsync(TEntity entity)
        {
            return await this.InsertBatchAsync(new[] { entity }) == 1;
        }

        /// <summary>
        /// insert and get the entity.
        /// </summary>
        /// <param name="entity">entity.</param>
        /// <returns>inserted entity.</returns>
        Task<TEntity> InsertThenEntityAsync(TEntity entity);

        /// <summary>
        /// batch insert.
        /// </summary>
        /// <param name="entities">entity array.</param>
        /// <returns>影响行数.</returns>
        Task<long> InsertBatchAsync(TEntity[] entities);

        /// <summary>
        /// batch insert.
        /// </summary>
        /// <param name="entities">entity array.</param>
        /// <returns>影响行数.</returns>
        public Task<long> InsertBatchAsync(IEnumerable<TEntity> entities)
        {
            return this.InsertBatchAsync(entities.ToArray());
        }

        /// <summary>
        /// delete.
        /// </summary>
        /// <param name="primaryKey">primary key.</param>
        /// <returns>成功与否.</returns>
        public async Task<bool> DeleteAsync(TPrimaryKey primaryKey)
        {
            return await this.DeleteBatchAsync(x => x.Id.Equals(primaryKey)) == 1;
        }

        /// <summary>
        /// batch delete.
        /// </summary>
        /// <param name="primaryKeys">primary key array.</param>
        /// <returns>影响行数.</returns>
        public Task<long> DeleteBatchAsync(TPrimaryKey[] primaryKeys)
        {
            return this.DeleteBatchAsync(x => primaryKeys.Contains(x.Id));
        }

        /// <summary>
        /// batch delete.
        /// </summary>
        /// <param name="primaryKeys">primary key array.</param>
        /// <returns>影响行数.</returns>
        public Task<long> DeleteBatchAsync(IEnumerable<TPrimaryKey> primaryKeys)
        {
            return this.DeleteBatchAsync(primaryKeys.ToArray());
        }

        /// <summary>
        /// batch delete.
        /// </summary>
        /// <param name="filter">过滤条件.</param>
        /// <returns>影响行数.</returns>
        Task<long> DeleteBatchAsync(Expression<Func<TEntity, bool>> filter);

        /// <summary>
        /// update.
        /// </summary>
        /// <param name="newEntity">new entity.</param>
        /// <returns>成功与否.</returns>
        public async Task<bool> UpdateAsync(TEntity newEntity)
        {
            return await this.UpdateBatchAsync(new[] { newEntity }) == 1;
        }

        /// <summary>
        /// batch update.
        /// </summary>
        /// <param name="newEntities">new entitiy array.</param>
        /// <returns>影响行数.</returns>
        Task<long> UpdateBatchAsync(TEntity[] newEntities);

        /// <summary>
        /// batch update.
        /// </summary>
        /// <param name="newEntities">new entitiy array.</param>
        /// <returns>影响行数.</returns>
        public Task<long> UpdateBatchAsync(IEnumerable<TEntity> newEntities)
        {
            return this.UpdateBatchAsync(newEntities.ToArray());
        }

        /// <summary>
        /// get.
        /// </summary>
        /// <param name="primaryKey">primary key.</param>
        /// <returns>list.</returns>
        public Task<TEntity> GetAsync(TPrimaryKey primaryKey)
        {
            return this.GetListAsync(x => x.Id.Equals(primaryKey), 1)
                       .FirstOrDefaultAsync();
        }

        /// <summary>
        /// get list.
        /// </summary>
        /// <param name="primaryKeys">primary key array.</param>
        /// <returns>list.</returns>
        public Task<IEnumerable<TEntity>> GetListAsync(TPrimaryKey[] primaryKeys)
        {
            return this.GetListAsync(x => primaryKeys.Contains(x.Id), primaryKeys.Length);
        }

        /// <summary>
        /// get list.
        /// </summary>
        /// <param name="primaryKeys">primary key array.</param>
        /// <returns>list.</returns>
        Task<IEnumerable<TEntity>> GetListAsync(IEnumerable<TPrimaryKey> primaryKeys)
        {
            return this.GetListAsync(primaryKeys.ToArray());
        }

        /// <summary>
        /// get list.
        /// </summary>
        /// <param name="filter">filter.</param>
        /// <param name="limit">default no limit.</param>
        /// <returns>list.</returns>
        Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> filter, long limit = 0);
    }

    /// <inheritdoc/>
    public interface IRepository<TEntity> : IRepository<TEntity, Guid>
        where TEntity : IEntity
    {
    }
}
