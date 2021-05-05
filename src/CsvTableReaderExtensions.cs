#region ENBREA.CSV - Copyright (C) 2021 STÜBER SYSTEMS GmbH
/*    
 *    ENBREA.CSV 
 *    
 *    Copyright (C) 2021 STÜBER SYSTEMS GmbH
 *
 *    Licensed under the MIT License, Version 2.0. 
 * 
 */
#endregion

using System.Collections.Generic;

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
        /// <returns>An async enumerator of custom csv object instances</returns>
        public static async IAsyncEnumerable<TEntity> ReadAllAsync<TEntity>(this CsvTableReader csvTableReader)
        {
            while (await csvTableReader.ReadAsync() > 0)
            {
                yield return csvTableReader.Get<TEntity>();
            }
        }
    }
}
