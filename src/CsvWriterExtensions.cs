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

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Enbrea.Csv
{
    /// <summary>
    /// Extensions for <see cref="CsvWriter"/>
    /// </summary>
    public static class CsvWriterExtensions
    {
        /// <summary>
        /// Writes a list of values to the CSV streem and closes the current row within the CSV stream.
        /// </summary>
        /// <param name="csvWriter">The <see cref="CsvWriter"/></param>
        /// <param name="values">The list of values to be written.</param>
        public static void WriteLineValues(this CsvWriter csvWriter, IEnumerable<string> values)
        {
            csvWriter.WriteValues(values);
            csvWriter.WriteLine();
        }

        /// <summary>
        /// Writes a list of values to the CSV streem and closes the current row within the CSV stream.
        /// </summary>
        /// <param name="csvWriter">The <see cref="CsvWriter"/></param>
        /// <param name="values">The list of values to be written.</param>
        public static void WriteLineValues(this CsvWriter csvWriter, params string[] values)
        {
            csvWriter.WriteLineValues((IEnumerable<string>)values);
        }

        /// <summary>
        /// Writes a list of values to the CSV streem and closes the current row within the CSV stream.
        /// </summary>
        /// <param name="csvWriter">The <see cref="CsvWriter"/></param>
        /// <param name="values">The list of values to be written.</param>
        /// <returns>A task that represents the asynchronous write operation.</returns>
        public static async Task WriteLineValuesAsync(this CsvWriter csvWriter, IEnumerable<string> values)
        {
            await csvWriter.WriteValuesAsync(values);
            await csvWriter.WriteLineAsync();
        }

        /// <summary>
        /// Writes a list of values to the CSV streem and closes the current row within the CSV stream.
        /// </summary>
        /// <param name="csvWriter">The <see cref="CsvWriter"/></param>
        /// <param name="values">The list of values to be written.</param>
        /// <returns>A task that represents the asynchronous write operation.</returns>
        public static async Task WriteLineValuesAsync(this CsvWriter csvWriter, params string[] values)
        {
            await csvWriter.WriteLineValuesAsync((IEnumerable<string>)values);
        }

        /// <summary>
        /// Writes a list of values to the CSV streem
        /// </summary>
        /// <param name="csvWriter">The <see cref="CsvWriter"/></param>
        /// <param name="values">The list of values to be written.</param>
        public static void WriteValues(this CsvWriter csvWriter, IEnumerable<string> values)
        {
            if (values == null)
            {
                throw new ArgumentNullException("values");
            }
            foreach (string value in values)
            {
                csvWriter.WriteValue(value, false);
            }
        }

        /// <summary>
        /// Writes a list of values to the CSV streem
        /// </summary>
        /// <param name="csvWriter">The <see cref="CsvWriter"/></param>
        /// <param name="values">The list of values to be written.</param>
        public static void WriteValues(this CsvWriter csvWriter, params string[] values)
        {
            csvWriter.WriteValues((IEnumerable<string>)values);
        }

        /// <summary>
        /// Writes a list of values to the CSV streem
        /// </summary>
        /// <param name="csvWriter">The <see cref="CsvWriter"/></param>
        /// <param name="values">The list of values to be written.</param>
        /// <returns>A task that represents the asynchronous write operation.</returns>
        public static async Task WriteValuesAsync(this CsvWriter csvWriter, IEnumerable<string> values)
        {
            if (values == null)
            {
                throw new ArgumentNullException("values");
            }
            foreach (string value in values)
            {
                await csvWriter.WriteValueAsync(value, false);
            }
        }

        /// <summary>
        /// Writes a list of values to the CSV streem
        /// </summary>
        /// <param name="csvWriter">The <see cref="CsvWriter"/></param>
        /// <param name="values">The list of values to be written.</param>
        /// <returns>A task that represents the asynchronous write operation.</returns>
        public static async Task WriteValuesAsync(this CsvWriter csvWriter, params string[] values)
        {
            await csvWriter.WriteValuesAsync((IEnumerable<string>)values);
        }
    }
}

