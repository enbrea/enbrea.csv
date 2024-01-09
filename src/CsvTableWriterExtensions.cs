#region ENBREA.CSV - Copyright (c) STÜBER SYSTEMS GmbH
/*    
 *    ENBREA.CSV 
 *    
 *    Copyright (c) STÜBER SYSTEMS GmbH
 *
 *    Licensed under the MIT License, Version 2.0. 
 */
#endregion

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Enbrea.Csv
{
    /// <summary>
    /// Extensions for <see cref="CsvTableWriter"/>
    /// </summary>
    public static class CsvTableWriterExtensions
    {
        /// <summary>
        /// Writes values directly to the csv stream and opens a new row.
        /// </summary>
        /// <param name="csvTableWriter">The <see cref="CsvTableWriter"/></param>
        /// <param name="values">List of values</param>
        public static void Write(this CsvTableWriter csvTableWriter, IEnumerable<object> values)
        {
            if (values == null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            int i = 0;
            foreach (var value in values)
            {
                csvTableWriter.SetValue(i, value);
                i++;
            }
            csvTableWriter.Write();
        }

        /// <summary>
        /// Writes values directly to the csv stream and opens a new row.
        /// </summary>
        /// <param name="csvTableWriter">The <see cref="CsvTableWriter"/></param>
        /// <param name="values">List of values</param>
        public static void Write(this CsvTableWriter csvTableWriter, params object[] values)
        {
            csvTableWriter.Write((IEnumerable<object>)values);
        }

        /// <summary>
        /// Writes a custom csv object directly to the csv stream and opens a new row.
        /// </summary>
        /// <typeparam name="TEntity">The custom csv object type</typeparam>
        /// <param name="csvTableWriter">The <see cref="CsvTableWriter"/></param>
        /// <param name="entity">The csv custom object</param>
        public static void Write<TEntity>(this CsvTableWriter csvTableWriter, TEntity entity)
        {
            csvTableWriter.SetValues(entity);
            csvTableWriter.Write();
        }

        /// <summary>
        /// Writes a list of custom csv objects directly to the csv stream and opens a new row.
        /// </summary>
        /// <typeparam name="TEntity">The custom csv object type</typeparam>
        /// <param name="csvTableWriter">The <see cref="CsvTableWriter"/></param>
        /// <param name="entities">List of csv custom objects</param>
        public static void WriteAll<TEntity>(this CsvTableWriter csvTableWriter, IEnumerable<TEntity> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }

            foreach (var entity in entities)
            {
                csvTableWriter.SetValues(entity);
                csvTableWriter.Write();
            }
        }

        /// <summary>
        /// Writes a list of custom csv objects directly to the csv stream and opens a new row.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="csvTableWriter">The <see cref="CsvTableWriter"/></param>
        /// <param name="entities">List of csv custom objects</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>A task that represents the asynchronous write operation.</returns>
        public static async Task WriteAllAsync<TEntity>(this CsvTableWriter csvTableWriter, IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }

            foreach (var entity in entities)
            {
                cancellationToken.ThrowIfCancellationRequested();
                csvTableWriter.SetValues(entity);
                await csvTableWriter.WriteAsync().ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Writes values directly to the csv stream and opens a new row.
        /// </summary>
        /// <param name="csvTableWriter">The <see cref="CsvTableWriter"/></param>
        /// <param name="values">List of values</param>
        /// <returns>A task that represents the asynchronous write operation.</returns>
        public static async Task WriteAsync(this CsvTableWriter csvTableWriter, IEnumerable<object> values)
        {
            if (values == null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            int i = 0;
            foreach (var value in values)
            {
                csvTableWriter.SetValue(i, value);
                i++;
            }
            await csvTableWriter.WriteAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Writes values directly to the csv stream and opens a new row.
        /// </summary>
        /// <param name="csvTableWriter">The <see cref="CsvTableWriter"/></param>
        /// <param name="values">List of values</param>
        /// <returns>A task that represents the asynchronous write operation.</returns>
        public static async Task WriteAsync(this CsvTableWriter csvTableWriter, params object[] values)
        {
            await csvTableWriter.WriteAsync((IEnumerable<object>)values).ConfigureAwait(false);
        }

        /// <summary>
        /// Writes a custom csv object directly to the csv stream and opens a new row.
        /// </summary>
        /// <typeparam name="TEntity">The custom csv object type</typeparam>
        /// <param name="csvTableWriter">The <see cref="CsvTableWriter"/></param>
        /// <param name="entity">The csv object</param>
        /// <returns>A task that represents the asynchronous write operation.</returns>
        public static async Task WriteAsync<TEntity>(this CsvTableWriter csvTableWriter, TEntity entity)
        {
            csvTableWriter.SetValues(entity);
            await csvTableWriter.WriteAsync().ConfigureAwait(false);
        }
    }
}
