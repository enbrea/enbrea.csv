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
    /// Implementation of a <see cref="DateTimeOffset"/> converter to or from CSV
    /// </summary>
    public class CsvDateTimeOffsetConverter : CsvDefaultFormattableConverter
    {
        public CsvDateTimeOffsetConverter() : base(typeof(DateTimeOffset))
        {
        }

        public CsvDateTimeOffsetConverter(IFormatProvider formatProvider, string[] formats)
            : base(typeof(DateTimeOffset), formatProvider, formats)
        {
        }

        public CsvDateTimeOffsetConverter(IFormatProvider formatProvider, string[] formats, DateTimeStyles dateTimeStyle)
            : base(typeof(DateTimeOffset), formatProvider, formats)
        {
            DateTimeStyle = dateTimeStyle;
        }

        public DateTimeStyles? DateTimeStyle { get; set; }
        
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
                    if (DateTimeStyle != null)
                    {
                        return DateTimeOffset.ParseExact(value, Formats, FormatProvider, (DateTimeStyles)DateTimeStyle);
                    }
                    else
                    {
                        return DateTimeOffset.ParseExact(value, Formats, FormatProvider);
                    }
                }
                else 
                {
                    if (DateTimeStyle != null)
                    {
                        return DateTimeOffset.Parse(value, FormatProvider, (DateTimeStyles)DateTimeStyle);
                    }
                    else
                    {
                        return DateTimeOffset.Parse(value, FormatProvider);
                    }
                }
            }
        }
    }
}