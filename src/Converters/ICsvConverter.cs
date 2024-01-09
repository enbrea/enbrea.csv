#region ENBREA.CSV - Copyright (c) STÜBER SYSTEMS GmbH
/*    
 *    ENBREA.CSV 
 *    
 *    Copyright (c) STÜBER SYSTEMS GmbH
 *
 *    Licensed under the MIT License, Version 2.0. 
 * 
 */
#endregion

namespace Enbrea.Csv
{
    /// <summary>
    /// Converts a value or object to or from text.
    /// </summary>
    public interface ICsvConverter
    {
        /// <summary>
        /// Converts a given CSV value string into a value or object
        /// </summary>
        /// <param name="value">A CSV value string</param>
        /// <returns>The value or object</returns>
        object FromString(string value);

        /// <summary>
        /// Converts a given value or object into a CSV value string
        /// </summary>
        /// <param name="value">A value or object</param>
        /// <returns>The CSV value string</returns>
        string ToString(object value);
    }
}