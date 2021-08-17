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

namespace Enbrea.Csv
{
    /// <summary>
    /// Extensions for <see cref="CsvLineTableWriter"/>
    /// </summary>
    public static class CsvLineTableWriterExtensions
    {
        /// <summary>
        /// Writes values directly to the csv line.
        /// </summary>
        /// <param name="csvLineTableWriter">The <see cref="CsvLineTableWriter"/></param>
        /// <param name="values">List of values</param>
        /// <returns>
        /// A CSV formatted string if values are available; otherwise null.
        /// </returns>
        public static string Write(this CsvLineTableWriter csvLineTableWriter, IEnumerable<object> values)
        {
            int i = 0;
            foreach (var value in values)
            {
                csvLineTableWriter.SetValue(i, value);
                i++;
            }
            return csvLineTableWriter.Write();
        }

        /// <summary>
        /// Writes values directly to the csv line.
        /// </summary>
        /// <param name="csvLineTableWriter">The <see cref="CsvLineTableWriter"/></param>
        /// <param name="values">List of values</param>
        /// <returns>
        /// A CSV formatted string if values are available; otherwise null.
        /// </returns>
        public static string Write(this CsvLineTableWriter csvLineTableWriter, params object[] values)
        {
            return csvLineTableWriter.Write((IEnumerable<object>)values);
        }

        /// <summary>
        /// Writes a custom csv object directly to the csv line.
        /// </summary>
        /// <typeparam name="TEntity">The custom csv object type</typeparam>
        /// <param name="csvTableWriter">The <see cref="CsvTableWriter"/></param>
        /// <param name="entity">The csv object</param>
        /// <returns>
        /// A CSV formatted string if values are available; otherwise null.
        /// </returns>
        public static string Write<TEntity>(this CsvLineTableWriter csvLineTableWriter, TEntity entity)
        {
            csvLineTableWriter.SetValues(entity);
            return csvLineTableWriter.Write();
        }
    }
}

