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

using System.Collections.Generic;

namespace Enbrea.Csv
{
    /// <summary>
    /// Extensions for <see cref="CsvLineTableBuilder"/>
    /// </summary>
    public static class CsvLineTableBuilderExtensions
    {
        /// <summary>
        /// Converts values directly to a csv text line.
        /// </summary>
        /// <param name="csvLineTableBuilder">The <see cref="CsvLineTableBuilder"/></param>
        /// <param name="values">List of values</param>
        /// <returns>
        /// A CSV formatted string if values are available; otherwise null.
        /// </returns>
        public static string ToString(this CsvLineTableBuilder csvLineTableBuilder, IEnumerable<object> values)
        {
            int i = 0;
            foreach (var value in values)
            {
                csvLineTableBuilder.SetValue(i, value);
                i++;
            }
            return csvLineTableBuilder.ToString();
        }

        /// <summary>
        /// Converts values directly to a csv text line.
        /// </summary>
        /// <param name="csvLineTableBuilder">The <see cref="CsvLineTableBuilder"/></param>
        /// <param name="values">List of values</param>
        /// <returns>
        /// A CSV formatted string if values are available; otherwise null.
        /// </returns>
        public static string ToString(this CsvLineTableBuilder csvLineTableBuilder, params object[] values)
        {
            return csvLineTableBuilder.ToString((IEnumerable<object>)values);
        }

        /// <summary>
        /// Converts values directly to a csv text line.
        /// </summary>
        /// <typeparam name="TEntity">The custom csv object type</typeparam>
        /// <param name="csvLineTableBuilder">The <see cref="CsvLineTableBuilder"/></param>
        /// <param name="entity">The csv object</param>
        /// <returns>
        /// A CSV formatted string if values are available; otherwise null.
        /// </returns>
        public static string ToString<TEntity>(this CsvLineTableBuilder csvLineTableBuilder, TEntity entity)
        {
            csvLineTableBuilder.SetValues(entity);
            return csvLineTableBuilder.ToString();
        }
    }
}

