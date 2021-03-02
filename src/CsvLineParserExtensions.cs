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

namespace Enbrea.Csv
{
    /// <summary>
    /// Extensions for <see cref="CsvLineParser"/>
    /// </summary>
    public static class CsvLineParserExtensions 
    {
        /// <summary>
        /// Parses a CSV text line and gives back the values as string collection.
        /// </summary>
        /// <param name="csvLineReader">The <see cref="CsvLineParser"/></param>
        /// <param name="line">A CSV formated text line.</param>
        /// <param name="values">List of parsed values.</param>
        /// <returns>
        /// Number of parsed values
        /// </returns>
        public static int Read(this CsvLineParser csvLineReader, string line, ICollection<string> values)
        {
            if (values == null)
            {
                throw new ArgumentNullException("values");
            }

            values.Clear();

            return csvLineReader.Read(line, (i, s) => { values.Add(s); });
        }

        /// <summary>
        /// Parses a CSV text line and gives back the values as string array.
        /// </summary>
        /// <param name="csvLineReader">The <see cref="CsvLineParser"/></param>
        /// <param name="line">A CSV formated text line</param>
        /// <returns>
        /// Array of parsed values.
        /// </returns>
        public static string[] Read(this CsvLineParser csvLineReader, string line)
        {
            var values = new List<string>();

            return (csvLineReader.Read(line, (i, s) => { values.Add(s); }) > 0) ? values.ToArray() : null;
        }

        /// <summary>
        /// Parses a CSV text line and returns back again the complete line as CSV string. 
        /// </summary>
        /// <param name="csvLineReader">The <see cref="CsvLineParser"/></param>
        /// <param name="line">A CSV formated text line</param>
        /// <returns>
        /// The complete row as CSV string if values are available; otherwise null.
        /// </returns>
        public static string Validate(this CsvLineParser csvLineReader, string line)
        {
            var csvLineBuilder = new CsvLineBuilder()
            {
                Configuration = csvLineReader.Configuration
            };

            return (csvLineReader.Read(line, (i, s) => { csvLineBuilder.Append(s); }) > 0) ? csvLineBuilder.ToString() : null;
        }
    }
}