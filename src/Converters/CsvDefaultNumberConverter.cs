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

using System;
using System.Globalization;

namespace Enbrea.Csv
{
    /// <summary>
    /// Implementation of a numeric value converter to or from CSV
    /// </summary>
    public class CsvDefaultNumberConverter : CsvDefaultFormattableConverter
    {
        public CsvDefaultNumberConverter(Type conversionType) : base(conversionType)
        {
        }

        public CsvDefaultNumberConverter(Type conversionType, IFormatProvider formatProvider, string[] formats)
            : base(conversionType, formatProvider, formats)
        {
        }

        public CsvDefaultNumberConverter(Type conversionType, IFormatProvider formatProvider, string[] formats, NumberStyles numberStyle)
            : base(conversionType, formatProvider, formats)
        {
            NumberStyle = numberStyle;
        }

        public NumberStyles? NumberStyle { get; set; }
    }
}