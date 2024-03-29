﻿#region ENBREA.CSV - Copyright (c) STÜBER SYSTEMS GmbH
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
    /// Implementation of a <see cref="DateTime"/> converter to or from CSV
    /// </summary>
    public class CsvDateTimeConverter : CsvDefaultFormattableConverter
    {
        public CsvDateTimeConverter() : base(typeof(DateTime))
        {
        }

        public CsvDateTimeConverter(CultureInfo cultureInfo, string[] formats)
            : base(typeof(DateTime), cultureInfo, formats)
        {
        }

        public CsvDateTimeConverter(CultureInfo cultureInfo, string[] formats, DateTimeStyles dateTimeStyle)
            : base(typeof(DateTime), cultureInfo, formats)
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
                        return DateTime.ParseExact(value, Formats, FormatProvider, (DateTimeStyles)DateTimeStyle);
                    }
                    else
                    {
                        return DateTime.ParseExact(value, Formats, FormatProvider);
                    }
                }
                else 
                {
                    if (DateTimeStyle != null)
                    {
                        return DateTime.Parse(value, FormatProvider, (DateTimeStyles)DateTimeStyle);
                    }
                    else
                    {
                        return DateTime.Parse(value, FormatProvider);
                    }
                }
            }
        }
    }
}