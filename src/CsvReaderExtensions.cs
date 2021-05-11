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
    /// Extensions for <see cref="CsvReader"/>
    /// </summary>
    public static class CsvReaderExtensions 
    {
        /// <summary>
        /// Reads all csv records out of the stream and gives back an enumerator of newly encoded csv strings. 
        /// This method can be used as a line by line syntax checker.
        /// parsing and building
        /// </summary>
        /// <param name="csvReader">The <see cref="CsvReader"/></param>
        /// <returns>
        /// An enumerator of newly encoded csv strings
        /// </returns>
        public static IEnumerable<string> NormalizeAll(this CsvReader csvReader)
        {
            var csvLineBuilder = new CsvLineBuilder()
            {
                Configuration = csvReader.Configuration
            };

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
        /// <returns>
        /// An async enumerator of newly encoded csv strings
        /// </returns>
        public static async IAsyncEnumerable<string> NormalizeAllAsync(this CsvReader csvReader)
        {
            var csvLineBuilder = new CsvLineBuilder()
            {
                Configuration = csvReader.Configuration
            };

            while (await csvReader.ReadLineAsync((i, s) => { csvLineBuilder.Append(s); }) > 0)
            {
                yield return csvLineBuilder.ToString();
                csvLineBuilder.Clear();
            }
        }

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
        /// <returns>
        /// An async enumerator of csv records. Each record is an array of parsed values.
        /// <returns>
        public async static IAsyncEnumerable<string[]> ReadAllAsync(this CsvReader csvReader)
        {
            var values = new List<string>();

            while (await csvReader.ReadLineAsync((i, s) => { values.Add(s); }) > 0)
            {
                yield return values.ToArray();
                values.Clear();
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
                throw new ArgumentNullException("values");
            }

            values.Clear();

            return csvReader.ReadLine((i, s) => { values.Add(s); });
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
        public static async Task<int> ReadLineAsync(this CsvReader csvReader, ICollection<string> values)
        {
            if (values == null)
            {
                throw new ArgumentNullException("values");
            }

            values.Clear();

            return await csvReader.ReadLineAsync((i, s) => { values.Add(s); });
        }
    }
}