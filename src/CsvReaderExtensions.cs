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

        /// <summary>
        /// Reads out the next row out of the current CSV stream and gives back the values
        /// as string array.
        /// </summary>
        /// <param name="csvReader">The <see cref="CsvReader"/></param>
        /// <returns>
        /// Array of parsed values.
        /// </returns>
        public static string[] ReadLine(this CsvReader csvReader)
        {
            var values = new List<string>();

            return (csvReader.ReadLine((i, s) => { values.Add(s); }) > 0) ? values.ToArray() : null;
        }

        /// <summary>
        /// Reads out the next row out of the current CSV stream and gives back the values
        /// as string array.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The value of the TResult
        //  parameter contains an array of parsed values.</returns>
        /// <returns>
        public static async Task<string[]> ReadLineAsync(this CsvReader csvReader)
        {
            var values = new List<string>();

            return (await csvReader.ReadLineAsync((i, s) => { values.Add(s); }) > 0) ? values.ToArray() : null;
        }

        /// <summary>
        /// Reads out the next row out of the current CSV stream and returns back again the 
        /// complete row as CSV string. 
        /// </summary>
        /// <param name="csvReader">The <see cref="CsvReader"/></param>
        /// <returns>
        /// The complete row as CSV string if values are available; otherwise null.
        /// </returns>
        public static string Normalize(this CsvReader csvReader)
        {
            var csvLineBuilder = new CsvLineBuilder()
            {
                Configuration = csvReader.Configuration
            };

            return (csvReader.ReadLine((i, s) => { csvLineBuilder.Append(s); }) > 0) ? csvLineBuilder.ToString() : null;
        }

        /// <summary>
        /// Reads out the next row out of the current CSV stream and returns back again the
        /// complete row as CSV string. 
        /// </summary>
        /// <param name="csvReader">The <see cref="CsvReader"/></param>
        /// <returns>A task that represents the asynchronous operation. The value of the TResult
        //  parameter contains the complete row as CSV string if values are available; otherwise null.
        /// </returns>
        public static async Task<string> NormalizeAsync(this CsvReader csvReader)
        {
            var csvLineBuilder = new CsvLineBuilder()
            {
                Configuration = csvReader.Configuration
            };

            return (await csvReader.ReadLineAsync((i, s) => { csvLineBuilder.Append(s); }) > 0) ? csvLineBuilder.ToString() : null;
        }
    }
}