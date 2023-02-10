#region ENBREA.CSV - Copyright (C) 2023 STÜBER SYSTEMS GmbH
/*    
 *    ENBREA.CSV 
 *    
 *    Copyright (C) 2023 STÜBER SYSTEMS GmbH
 *
 *    Licensed under the MIT License, Version 2.0. 
 * 
 */
#endregion

using System;

namespace Enbrea.Csv
{
    /// <summary>
    /// Maps csv data to and from object members
    /// </summary>
    public interface ICsvClassMapper
    {
        /// <summary>
        /// Checks whether a member with the given name exists
        /// </summary>
        /// <param name="name">Member name</param>
        /// <returns>TRUE if a member with this name exists, otherwise FALSE</returns>
        bool ContainsValue(string name);

        /// <summary>
        /// Gives back the value of the named member from a given object instance
        /// </summary>
        /// <param name="entity">The object instance</param>
        /// <param name="name">Member name</param>
        /// <returns>A value</returns>
        object GetValue(object entity, string name);

        /// <summary>
        /// Gives back the value type of the named member from a given object instance
        /// </summary>
        /// <param name="name">Member name</param>
        /// <returns>A value type</returns>
        Type GetValueType(string name);

        /// <summary>
        /// Sets the value of the named member from a given object instance
        /// </summary>
        /// <param name="entity">The object instance</param>
        /// <param name="name">Member name</param>
        /// <param name="value">The new value</param>
        void SetValue(object entity, string name, object value);
    }
}