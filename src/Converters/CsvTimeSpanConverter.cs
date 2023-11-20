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
using System.Globalization;

namespace Enbrea.Csv
{
    /// <summary>
    /// Implementation of a <see cref="TimeSpan"/> converter to or from CSV
    /// </summary>
    public class CsvTimeSpanConverter : CsvDefaultFormattableConverter
    {
        public CsvTimeSpanConverter() : base(typeof(TimeSpan))
        {
        }

        public CsvTimeSpanConverter(TimeSpanStyles timeSpanStyle) : base(typeof(TimeSpan))
        {
            TimeSpanStyle = timeSpanStyle;
        }

        public TimeSpanStyles? TimeSpanStyle { get; set; }
        
        public override object FromString(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }
            else
            {
                if (Formats != null && Formats.Length > 0)
                {
                    if (TimeSpanStyle != null)
                    {
                        return TimeSpan.ParseExact(value, Formats, FormatProvider, (TimeSpanStyles)TimeSpanStyle);
                    }
                    else
                    {
                        return TimeSpan.ParseExact(value, Formats, FormatProvider);
                    }
                }
                else
                {
                    return TimeSpan.Parse(value, FormatProvider);
                }
            }
        }
    }
}