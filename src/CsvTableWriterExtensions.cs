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
        /// Writes values directly to the csv stream and opens a new row.
        /// </summary>
        /// <param name="csvTableWriter">The <see cref="CsvTableWriter"/></param>
        /// <param name="values">List of values</param>
        /// <returns>A task that represents the asynchronous write operation.</returns>
        public static async Task WriteAsync(this CsvTableWriter csvTableWriter, IEnumerable<object> values)
        {
            int i = 0;
            foreach (var value in values)
            {
                csvTableWriter.SetValue(i, value);
                i++;
            }
            await csvTableWriter.WriteAsync();
        }

        /// <summary>
        /// Writes values directly to the csv stream and opens a new row.
        /// </summary>
        /// <param name="csvTableWriter">The <see cref="CsvTableWriter"/></param>
        /// <param name="values">List of values</param>
        /// <returns>A task that represents the asynchronous write operation.</returns>
        public static async Task WriteAsync(this CsvTableWriter csvTableWriter, params object[] values)
        {
            await csvTableWriter.WriteAsync((IEnumerable<object>)values);
        }
    }
}
