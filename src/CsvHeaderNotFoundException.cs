﻿#region ENBREA.CSV - Copyright (c) STÜBER SYSTEMS GmbH
/*    
 *    ENBREA.CSV 
 *    
 *    Copyright (c) STÜBER SYSTEMS GmbH
 *
 *    Licensed under the MIT License, Version 2.0. 
 */
#endregion

using System;

namespace Enbrea.Csv
{
    /// <summary>
    /// Represents an error that occurs when trying to access a csv header by name which does not exist.
    /// </summary>
    [Serializable]
    public class CsvHeaderNotFoundException : Exception
    {
        public CsvHeaderNotFoundException(string message)
            : base(message)
        { }
    }
}

