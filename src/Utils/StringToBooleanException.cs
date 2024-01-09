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
    /// Represents an error that occurs during string to bool parsing inside a <see cref="StringExtensions"/> instance.
    /// </summary>
    [Serializable]
    public class StringToBooleanException : Exception
    {
        public StringToBooleanException(string message)
            : base(message)
        { }
    }
}

