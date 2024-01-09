#region ENBREA.CSV - Copyright (c) STÜBER SYSTEMS GmbH
/*    
 *    ENBREA.CSV 
 *    
 *    Copyright (c) STÜBER SYSTEMS GmbH
 *
 *    Licensed under the MIT License, Version 2.0. 
 */
#endregion

using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Enbrea.Csv
{
    /// <summary>
    /// Extensions for <see cref="CsvDictionary"/>
    /// </summary>
    public static class CsvDictionaryExtensions
    {
        /// <summary>
        /// Reads a csv dictionary out of the stream, creates a new object instance and tries to assign all values 
        /// from the csv dictionary via reflection. 
        /// </summary>
        /// <typeparam name="TEntity">The custom object type</typeparam>
        /// <param name="csvDictionary">The <see cref="CsvDictionary"/></param>
        /// <param name="textReader">A <see cref="TextReader"/></param>
        /// <returns>Pointer to the newly created object instance</returns>
        public static TEntity Load<TEntity>(this CsvDictionary csvDictionary, TextReader textReader)
        {
            if (csvDictionary.Load(textReader) > 0)
            {
                return csvDictionary.CreateAndGetValues<TEntity>();
            }
            else 
            {
                return default;
            }
        }

        /// <summary>
        /// Reads a csv dictionary out of the stream, creates a new object instance and tries to assign all values 
        /// from the csv dictionary via reflection. 
        /// </summary>
        /// <typeparam name="TEntity">The custom object type</typeparam>
        /// <param name="csvDictionary">The <see cref="CsvDictionary"/></param>
        /// <param name="textReader">A <see cref="TextReader"/></param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public static async Task<TEntity> LoadAsync<TEntity>(this CsvDictionary csvDictionary, TextReader textReader, CancellationToken cancellationToken = default)
        {
            if (await csvDictionary.LoadAsync(textReader, cancellationToken).ConfigureAwait(false) > 0)
            {
                return csvDictionary.CreateAndGetValues<TEntity>();
            }
            else
            {
                return default;
            }
        }

        /// <summary>
        /// Applies all relevant values from an object to the csv dictionary via reflection and writes the csv 
        /// dictionary to a stream.
        /// </summary>
        /// <typeparam name="TEntity">The custom object type</typeparam>
        /// <param name="csvDictionary">The <see cref="CsvDictionary"/></param>
        /// <param name="textWriter">A <see cref="TextWriter"/></param>
        /// <param name="entity">The object instance</param>
        public static void Store<TEntity>(this CsvDictionary csvDictionary, TextWriter textWriter, TEntity entity)
        {
            csvDictionary.SetValues(entity);
            csvDictionary.Store(textWriter);
        }

        /// <summary>
        /// Applies all relevant values from an object to the csv dictionary via reflection and writes the csv 
        /// dictionary to a stream.
        /// </summary>
        /// <typeparam name="TEntity">The custom object type</typeparam>
        /// <param name="csvDictionary">The <see cref="CsvDictionary"/></param>
        /// <param name="textWriter">A <see cref="TextWriter"/></param>
        /// <param name="entity">The object instance</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public static async Task StoreAsync<TEntity>(this CsvDictionary csvDictionary, TextWriter textWriter, TEntity entity, CancellationToken cancellationToken = default)
        {
            csvDictionary.SetValues(entity);
            await csvDictionary.StoreAsync(textWriter, cancellationToken).ConfigureAwait(false);
        }

    }
}
