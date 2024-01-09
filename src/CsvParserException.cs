#region ENBREA.CSV - Copyright (c) STÜBER SYSTEMS GmbH
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
    /// Represents an error that occurs during CSV parsing inside a <see cref="CsvReader"/> or 
    /// <see cref="CsvLineParser"/>  instance.
    /// </summary>
    [Serializable]
    public class CsvParserException : Exception
    {
        public CsvParserException(string message)
            : base(message)
        { }
    }
}

