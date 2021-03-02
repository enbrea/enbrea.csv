
#region ENBREA.CSV - Copyright (C) 2021 STÜBER SYSTEMS GmbH
/*    
 *    ENBREA.CSV 
 *    
 *    Copyright (C) 2021 STÜBER SYSTEMS GmbH
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

        public CsvDefaultNumberConverter(Type conversionType, CultureInfo cultureInfo, string[] formats)
            : base(conversionType, cultureInfo, formats)
        {
        }

        public CsvDefaultNumberConverter(Type conversionType, CultureInfo cultureInfo, string[] formats, NumberStyles numberStyle)
            : base(conversionType, cultureInfo, formats)
        {
            NumberStyle = numberStyle;
        }

        public NumberStyles? NumberStyle { get; set; }
    }
}