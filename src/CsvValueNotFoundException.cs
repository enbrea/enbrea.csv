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
    /// Represents an error that occurs when trying to access a csv value by name or index which does not exist.
    /// </summary>
    [Serializable]
    public class CsvValueNotFoundException : Exception
    {
        public CsvValueNotFoundException(string message)
            : base(message)
        { }
    }
}

