#region ENBREA.CSV - Copyright (C) 2020 STÜBER SYSTEMS GmbH
/*    
 *    ENBREA.CSV 
 *    
 *    Copyright (C) 2020 STÜBER SYSTEMS GmbH
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
    /// Extensions for <see cref="CsvLineBuilder"/>
    /// </summary>
    public static class CsvLineBuilderExtensions
    {
        /// <summary>
        /// Converts a list of string values to a CSV text line
        /// </summary>
        /// <param name="csvLineWriter">The <see cref="CsvLineBuilder"/></param>
        /// <param name="values">List of string values</param>
        /// <returns>
        /// A CSV formated text line
        /// </returns>
        public static string Write(this CsvLineBuilder csvLineWriter, IEnumerable<string> values)
        {
            if (values == null)
            {
                throw new ArgumentNullException("values");
            }
            csvLineWriter.Clear();
            foreach (string value in values)
            {
                csvLineWriter.Append(value);
            }
            return csvLineWriter.ToString();
        }

        /// <summary>
        /// Converts a list of string values to a CSV text line
        /// </summary>
        /// <param name="csvLineWriter">The <see cref="CsvLineBuilder"/></param>
        /// <param name="values">List of string values</param>
        /// <returns>
        /// A CSV formated text line
        /// </returns>
        public static string Write(this CsvLineBuilder csvLineWriter, params string[] values)
        {
            return csvLineWriter.Write(values as IEnumerable<string>);
        }
    }
}