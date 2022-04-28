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
    /// Represents the csv header that a property is mapped to.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class CsvHeaderAttribute : Attribute
    {
        /// <summary>
        /// A list of alternative csv header names the property is mapped to.
        /// </summary>
        public string[] AlterntiveNames;

        /// <summary>
        /// Gets the name of the csv header the property is mapped to.
        /// </summary>
        public string Name;

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvHeaderAttribute"/> class.
        /// </summary>
        /// <param name="name">The name of the csv header the property is mapped to.</param>
        /// <param name="alterntiveNames">Alternative csv header names the property is mapped to</param>
        public CsvHeaderAttribute(string name, params string[] alterntiveNames)
        {
            Name = name;
            AlterntiveNames = alterntiveNames;
        }
    }
}