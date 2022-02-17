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

using System;

namespace Enbrea.Csv
{
    /// <summary>
    /// Denotes that a property should be excluded from csv mapping.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class CsvNotMappedAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CsvNotMappedAttribute"/> class.
        /// </summary>
        public CsvNotMappedAttribute()
        {
        }
    }
}