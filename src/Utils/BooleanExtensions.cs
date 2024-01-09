#region ENBREA.CSV - Copyright (c) STÜBER SYSTEMS GmbH
/*    
 *    ENBREA.CSV 
 *    
 *    Copyright (c) STÜBER SYSTEMS GmbH
 *
 *    Licensed under the MIT License, Version 2.0. 
 */
#endregion

namespace Enbrea.Csv
{
    /// <summary>
    /// Extensions for <see cref="bool"/>
    /// </summary>
    public static class BooleanExtensions
    {
        /// <summary>
        /// Converts the value of this boolean instance to a string representation defined by params
        /// </summary>
        /// <param name="value">The <see cref="bool"/> instance</param>
        /// <param name="trueString">String representation for true</param>
        /// <param name="falseString">String representation for false</param>
        /// <returns>String representation</returns>
        public static string ToString(this bool value, string trueString, string falseString)
        {
            return value ? trueString : falseString;
        }
    }
}