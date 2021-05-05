#region ENBREA.CSV - Copyright (C) 2021 STÜBER SYSTEMS GmbH
/*    Copyright (C) 2021 STÜBER SYSTEMS GmbH
 *    ENBREA.CSV 
 *    
 *    Copyright (C) 2021 STÜBER SYSTEMS GmbH
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
        bool ContainsValue(string name);

        object GetValue(object entity, string name);

        Type GetValueType(string name);

        void SetValue(object entity, string name, object value);
    }
}