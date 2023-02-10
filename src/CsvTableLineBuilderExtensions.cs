#region ENBREA.CSV - Copyright (C) 2023 STÜBER SYSTEMS GmbH
/*    
 *    ENBREA.CSV 
 *    
 *    Copyright (C) 2023 STÜBER SYSTEMS GmbH
 *
 *    Licensed under the MIT License, Version 2.0. 
 */
#endregion

using System.Collections.Generic;

namespace Enbrea.Csv
{
    /// <summary>
    /// Extensions for <see cref="CsvTableLineBuilder"/>
    /// </summary>
    public static class CsvTableLineBuilderExtensions
    {
        /// <summary>
        /// Converts values directly to a csv text line.
        /// </summary>
        /// <param name="csvLineBuilder">The <see cref="CsvTableLineBuilder"/></param>
        /// <param name="values">List of values</param>
        /// <returns>
        /// A CSV formatted string if values are available; otherwise null.
        /// </returns>
        public static string ToString(this CsvTableLineBuilder csvLineBuilder, IEnumerable<object> values)
        {
            int i = 0;
            foreach (var value in values)
            {
                csvLineBuilder.SetValue(i, value);
                i++;
            }
            return csvLineBuilder.ToString();
        }

        /// <summary>
        /// Converts values directly to a csv text line.
        /// </summary>
        /// <param name="csvLineBuilder">The <see cref="CsvTableLineBuilder"/></param>
        /// <param name="values">List of values</param>
        /// <returns>
        /// A CSV formatted string if values are available; otherwise null.
        /// </returns>
        public static string ToString(this CsvTableLineBuilder csvLineBuilder, params object[] values)
        {
            return csvLineBuilder.ToString((IEnumerable<object>)values);
        }

        /// <summary>
        /// Converts values directly to a csv text line.
        /// </summary>
        /// <typeparam name="TEntity">The custom csv object type</typeparam>
        /// <param name="csvLineBuilder">The <see cref="CsvTableLineBuilder"/></param>
        /// <param name="entity">The csv object</param>
        /// <returns>
        /// A CSV formatted string if values are available; otherwise null.
        /// </returns>
        public static string ToString<TEntity>(this CsvTableLineBuilder csvLineBuilder, TEntity entity)
        {
            csvLineBuilder.SetValues(entity);
            return csvLineBuilder.ToString();
        }
    }
}

