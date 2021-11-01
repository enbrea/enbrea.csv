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
        /// <param name="csvReader">A <see cref="CsvReader"/></param>
        /// <returns>Pointer to the newly created object instance</returns>
        public static TEntity Load<TEntity>(this CsvDictionary csvDictionary, CsvReader csvReader)
        {
            if (csvDictionary.Load(csvReader) > 0)
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
        /// <param name="csvReader">A <see cref="CsvReader"/></param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public static async Task<TEntity> LoadAsync<TEntity>(this CsvDictionary csvDictionary, CsvReader csvReader)
        {
            if (await csvDictionary.LoadAsync(csvReader) > 0)
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
        /// <param name="csvWriter">A <see cref="CsvWriter"/></param>
        /// <param name="entity">The object instance</param>
        public static void Store<TEntity>(this CsvDictionary csvDictionary, CsvWriter csvWriter, TEntity entity)
        {
            csvDictionary.SetValues(entity);
            csvDictionary.Store(csvWriter);
        }

        /// <summary>
        /// Applies all relevant values from an object to the csv dictionary via reflection and writes the csv 
        /// dictionary to a stream.
        /// </summary>
        /// <typeparam name="TEntity">The custom object type</typeparam>
        /// <param name="csvDictionary">The <see cref="CsvDictionary"/></param>
        /// <param name="csvWriter">A <see cref="CsvWriter"/></param>
        /// <param name="entity">The object instance</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public static async Task StoreAsync<TEntity>(this CsvDictionary csvDictionary, CsvWriter csvWriter, TEntity entity)
        {
            csvDictionary.SetValues(entity);
            await csvDictionary.StoreAsync(csvWriter);
        }

    }
}
