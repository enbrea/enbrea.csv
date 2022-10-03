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

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Enbrea.Csv
{
    /// <summary>
    /// Extensions for <see cref="CsvReader"/>
    /// </summary>
    public static class CsvReaderExtensions 
    {
        /// <summary>
        /// Reads all csv records out of the stream and gives back an enumerator. 
        /// </summary>
        /// <param name="csvReader">The <see cref="CsvReader"/></param>
        /// <returns>
        /// An enumerator of csv records. Each record is an array of parsed values.
        /// </returns>
        public static IEnumerable<string[]> ReadAll(this CsvReader csvReader)
        {
            var values = new List<string>();

            while (csvReader.ReadLine((i, s) => { values.Add(s); }) > 0)
            {
                yield return values.ToArray();
                values.Clear();
            }
        }

        /// <summary>
        /// Reads all csv records out of the stream and gives back an enumerator. 
        /// </summary>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// An async enumerator of csv records. Each record is an array of parsed values.
        /// <returns>
        public async static IAsyncEnumerable<string[]> ReadAllAsync(this CsvReader csvReader, [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            var values = new List<string>();

            while (await csvReader.ReadLineAsync((i, s) => { values.Add(s); }) > 0)
            {
                cancellationToken.ThrowIfCancellationRequested();
                yield return values.ToArray();
                values.Clear();
            }
        }

        /// <summary>
        /// Reads all csv records out of the stream and gives back an enumerator of newly encoded csv strings. 
        /// This method can be used as a line by line syntax checker.
        /// parsing and building
        /// </summary>
        /// <param name="csvReader">The <see cref="CsvReader"/></param>
        /// <returns>
        /// An enumerator of newly encoded csv strings
        /// </returns>
        public static IEnumerable<string> ReadAllLines(this CsvReader csvReader)
        {
            var csvLineBuilder = new CsvLineBuilder(csvReader.Configuration);

            while (csvReader.ReadLine((i, s) => { csvLineBuilder.Append(s); }) > 0)
            {
                yield return csvLineBuilder.ToString();
                csvLineBuilder.Clear();
            }
        }

        /// <summary>
        /// Reads all csv records out of the stream and gives back an enumerator of newly encoded csv strings. 
        /// This method can be used as a line by line syntax checker.
        /// </summary>
        /// <param name="csvReader">The <see cref="CsvReader"/></param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// An async enumerator of newly encoded csv strings
        /// </returns>
        public static async IAsyncEnumerable<string> ReadAllLinesAsync(this CsvReader csvReader, [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            var csvLineBuilder = new CsvLineBuilder(csvReader.Configuration);

            while (await csvReader.ReadLineAsync((i, s) => { csvLineBuilder.Append(s); }) > 0)
            {
                cancellationToken.ThrowIfCancellationRequested();
                yield return csvLineBuilder.ToString();
                csvLineBuilder.Clear();
            }
        }

        /// <summary>
        /// Reads out the next row out of the current CSV stream and gives back the values
        /// as string collection.
        /// </summary>
        /// <param name="csvReader">The <see cref="CsvReader"/></param>
        /// <param name="values">List of parsed values.</param>
        /// <returns>
        /// Number of parsed values
        /// </returns>
        public static int ReadLine(this CsvReader csvReader, ICollection<string> values)
        {
            if (values == null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            values.Clear();

            return csvReader.ReadLine((i, s) => { values.Add(s); });
        }

        /// <summary>
        /// Reads out the next row out of the current CSV stream and fills the values
        /// of a given string array.
        /// </summary>
        /// <param name="csvReader">The <see cref="CsvReader"/></param>
        /// <param name="values">Array of parsed values.</param>
        /// <returns>
        /// Number of parsed values
        /// <returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static int ReadLine(this CsvReader csvReader, string[] values)
        {
            if (values == null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            return csvReader.ReadLine((i, s) => { if (i < values.Length) values[i] = s; });
        }

        /// <summary>
        /// Reads out the next row out of the current CSV stream and gives back the values
        /// as string collection.
        /// </summary>
        /// <param name="csvReader">The <see cref="CsvReader"/></param>
        /// <param name="values">List of parsed values.</param>
        /// <returns>A task that represents the asynchronous operation. The value of the TResult
        //  parameter contains the number of parsed values.
        /// <returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static async Task<int> ReadLineAsync(this CsvReader csvReader, ICollection<string> values)
        {
            if (values == null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            values.Clear();

            return await csvReader.ReadLineAsync((i, s) => { values.Add(s); }).ConfigureAwait(false);
        }

        /// <summary>
        /// Reads out the next row out of the current CSV stream and fills the values
        /// of a given string array.
        /// </summary>
        /// <param name="csvReader">The <see cref="CsvReader"/></param>
        /// <param name="values">Array of parsed values.</param>
        /// <returns>A task that represents the asynchronous operation. The value of the TResult
        /// parameter contains the number of parsed values.</returns>
        /// <returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static async Task<int> ReadLineAsync(this CsvReader csvReader, string[] values)
        {
            if (values == null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            return await csvReader.ReadLineAsync((i, s) => { if (i < values.Length) values[i] = s; }).ConfigureAwait(false);
        }
    }
}