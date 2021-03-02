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
    /// Implementation of a DateTimeOffset converter to or from CSV
    /// </summary>
    public class CsvDateTimeOffsetConverter : CsvDefaultFormattableConverter
    {
        public CsvDateTimeOffsetConverter() : base(typeof(DateTimeOffset))
        {
        }

        public CsvDateTimeOffsetConverter(CultureInfo cultureInfo, string[] formats)
            : base(typeof(DateTimeOffset), cultureInfo, formats)
        {
        }

        public CsvDateTimeOffsetConverter(CultureInfo cultureInfo, string[] formats, DateTimeStyles dateTimeStyle)
            : base(typeof(DateTimeOffset), cultureInfo, formats)
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
                        return DateTimeOffset.ParseExact(value, Formats, CultureInfo, (DateTimeStyles)DateTimeStyle);
                    }
                    else
                    {
                        return DateTimeOffset.ParseExact(value, Formats, CultureInfo);
                    }
                }
                else 
                {
                    if (DateTimeStyle != null)
                    {
                        return DateTimeOffset.Parse(value, CultureInfo, (DateTimeStyles)DateTimeStyle);
                    }
                    else
                    {
                        return DateTimeOffset.Parse(value, CultureInfo);
                    }
                }
            }
        }
    }
}