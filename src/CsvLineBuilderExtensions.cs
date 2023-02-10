#region ENBREA.CSV - Copyright (C) 2023 STÜBER SYSTEMS GmbH
/*    
 *    ENBREA.CSV 
 *    
 *    Copyright (C) 2023 STÜBER SYSTEMS GmbH
 *
 *    Licensed under the MIT License, Version 2.0. 
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
        /// <param name="csvLineBuilder">The <see cref="CsvLineBuilder"/></param>
        /// <param name="values">List of string values</param>
        /// <returns>
        /// A CSV formated text line
        /// </returns>
        public static string ToString(this CsvLineBuilder csvLineBuilder, IEnumerable<string> values)
        {
            if (values == null)
            {
                throw new ArgumentNullException(nameof(values));
            }
            csvLineBuilder.Clear();
            foreach (string value in values)
            {
                csvLineBuilder.Append(value);
            }
            return csvLineBuilder.ToString();
        }

        /// <summary>
        /// Converts a list of string values to a CSV text line
        /// </summary>
        /// <param name="csvLineBuilder">The <see cref="CsvLineBuilder"/></param>
        /// <param name="values">List of string values</param>
        /// <returns>
        /// A CSV formated text line
        /// </returns>
        public static string ToString(this CsvLineBuilder csvLineBuilder, params string[] values)
        {
            return csvLineBuilder.ToString(values as IEnumerable<string>);
        }
    }
}