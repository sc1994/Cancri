// <copyright file="LinqUtility.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Aspire
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Linq 工具.
    /// </summary>
    public static class LinqUtility
    {
        /// <summary>
        /// 迭代 Array.
        /// </summary>
        /// <typeparam name="T">T.</typeparam>
        /// <param name="array">array.</param>
        /// <param name="action">action.</param>
        public static void ForEach<T>(this T[] array, Action<T> action)
        {
            foreach (var item in array)
            {
                action(item);
            }
        }

        /// <summary>
        /// 迭代 IEnumerable.
        /// </summary>
        /// <typeparam name="T">T.</typeparam>
        /// <param name="array">array.</param>
        /// <param name="action">action.</param>
        public static void ForEach<T>(this IEnumerable<T> array, Action<T> action)
        {
            foreach (var item in array)
            {
                action(item);
            }
        }

        /// <summary>
        /// 第一个或者默认.
        /// </summary>
        /// <typeparam name="T">T.</typeparam>
        /// <param name="sourceAsync">Source Async.</param>
        /// <param name="predicate">Predicate.</param>
        /// <returns>Task T.</returns>
        public static async Task<T> FirstOrDefaultAsync<T>(this Task<IEnumerable<T>> sourceAsync, Func<T, bool> predicate = null)
        {
            var source = await sourceAsync;
            return source.FirstOrDefault(predicate ?? (x => true));
        }

        /// <summary>
        /// 第一个或者默认.
        /// </summary>
        /// <typeparam name="T">T.</typeparam>
        /// <param name="sourceAsync">Source Async.</param>
        /// <param name="predicate">Predicate.</param>
        /// <returns>Task T.</returns>
        public static async Task<T> FirstOrDefaultAsync<T>(this Task<List<T>> sourceAsync, Func<T, bool> predicate = null)
        {
            var source = await sourceAsync;
            return source.FirstOrDefault(predicate ?? (x => true));
        }

        /// <summary>
        /// To List.
        /// </summary>
        /// <typeparam name="T">T.</typeparam>
        /// <param name="sourceAsync">Source Async.</param>
        /// <returns>Task T.</returns>
        public static async Task<List<T>> ToListAsync<T>(this Task<IEnumerable<T>> sourceAsync)
        {
            var source = await sourceAsync;
            return source.ToList();
        }

        /// <summary>
        /// To Array.
        /// </summary>
        /// <typeparam name="T">T.</typeparam>
        /// <param name="sourceAsync">Source Async.</param>
        /// <returns>Task T.</returns>
        public static async Task<T[]> ToArrayAsync<T>(this Task<IEnumerable<T>> sourceAsync)
        {
            return await sourceAsync.ToListAsync().ToArrayAsync();
        }

        /// <summary>
        /// To Array.
        /// </summary>
        /// <typeparam name="T">T.</typeparam>
        /// <param name="sourceAsync">Source Async.</param>
        /// <returns>Task T.</returns>
        public static async Task<T[]> ToArrayAsync<T>(this Task<List<T>> sourceAsync)
        {
            var source = await sourceAsync;
            return source.ToArray();
        }

        /// <summary>
        /// async select.
        /// </summary>
        /// <typeparam name="T">source.</typeparam>
        /// <typeparam name="TResult">result.</typeparam>
        /// <param name="sourceAsync">source async array.</param>
        /// <param name="selector">selector.</param>
        /// <returns>result async enumerable.</returns>
        public static async Task<IEnumerable<TResult>> SelectAsync<T, TResult>(this Task<IEnumerable<T>> sourceAsync, Func<T, TResult> selector)
        {
            var source = await sourceAsync;
            return source.Select(selector);
        }

        /// <summary>
        /// async select.
        /// </summary>
        /// <typeparam name="T">source.</typeparam>
        /// <typeparam name="TResult">result.</typeparam>
        /// <param name="sourceAsync">source async array.</param>
        /// <param name="selector">selector.</param>
        /// <returns>result async enumerable.</returns>
        public static async Task<IEnumerable<TResult>> SelectAsync<T, TResult>(this Task<T[]> sourceAsync, Func<T, int, TResult> selector)
        {
            var source = await sourceAsync;
            return source.Select(selector);
        }

        /// <summary>
        /// join.
        /// </summary>
        /// <param name="source">Source.</param>
        /// <param name="separator">Separator.</param>
        /// <returns>String.</returns>
        public static string Join(this IEnumerable<string> source, string separator)
        {
            return string.Join(separator, source);
        }

        /// <summary>
        /// any.
        /// </summary>
        /// <typeparam name="T">source type.</typeparam>
        /// <param name="sourceAsync">source.</param>
        /// <returns>String.</returns>
        public static async Task<bool> AnyAsync<T>(this Task<IEnumerable<T>> sourceAsync)
        {
            var source = await sourceAsync;
            return source.Any();
        }
    }
}
