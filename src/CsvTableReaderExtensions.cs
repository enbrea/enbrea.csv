#region ENBREA.CSV - Copyright (C) 2022 STÜBER SYSTEMS GmbH
/*    
 *    ENBREA.CSV 
 *    
 *    Copyright (C) 2022 STÜBER SYSTEMS GmbH
 *
 *    Licensed under the MIT License, Version 2.0. 
 * 
 */
#endregion

using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Enbrea.Csv
{
    /// <summary>
    /// Extensions for <see cref="CsvTableReaderExtensions"/>
    /// </summary>
    public static class CsvTableReaderExtensions
    {
        /// <summary>
        /// Reads all csv records out of the stream and gives back an enumerator for the csv custom 
        /// object instances.
        /// </summary>
        /// <typeparam name="TEntity">The custom csv object type</typeparam>
        /// <param name="csvTableReader">The <see cref="CsvTableReader"/></param>
        /// <returns>An enumerator of custom csv object instances</returns>
        public static IEnumerable<TEntity> ReadAll<TEntity>(this CsvTableReader csvTableReader)
        {
            while (csvTableReader.Read() > 0)
            {
                yield return csvTableReader.Get<TEntity>();
            }
        }

        /// <summary>
        /// Reads all csv records out of the stream and gives back an enumerator for the csv custom 
        /// object instances.
        /// </summary>
        /// <typeparam name="TEntity">The custom csv object type</typeparam>
        /// <param name="csvTableReader">The <see cref="CsvTableReader"/></param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>An async enumerator of custom csv object instances</returns>
        public static async IAsyncEnumerable<TEntity> ReadAllAsync<TEntity>(this CsvTableReader csvTableReader, [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            while (await csvTableReader.ReadAsync().ConfigureAwait(false) > 0)
            {
                cancellationToken.ThrowIfCancellationRequested();
                yield return csvTableReader.Get<TEntity>();
            }
        }
    }
}
