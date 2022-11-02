
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
using System.Globalization;

namespace Enbrea.Csv
{
    /// <summary>
    /// Implementation of a formattable value converter to or from CSV
    /// </summary>
    public class CsvDefaultFormattableConverter : CsvDefaultConverter
    {
        public CsvDefaultFormattableConverter(Type conversionType)
            : base(conversionType)
        {
        }

        public CsvDefaultFormattableConverter(Type conversionType, IFormatProvider formatProvider)
            : base(conversionType, formatProvider)
        {
        }

        public CsvDefaultFormattableConverter(Type conversionType, IFormatProvider formatProvider, string[] formats)
            : base(conversionType, formatProvider)
        {
            Formats = formats;
        }

        public string[] Formats { get; set; }

        public override string ToString(object value)
        {
            if ((value != null) && (Formats != null) && (Formats.Length > 0) && (typeof(IFormattable).IsAssignableFrom(_conversionType)))
            {
                return (value as IFormattable).ToString(Formats[0], FormatProvider);
            }
            else
            {
                return base.ToString(value);
            }
        }
    }
}