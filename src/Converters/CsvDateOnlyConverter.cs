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
#if NET6_0_OR_GREATER
    /// <summary>
    /// Implementation of a <see cref="DateOnly"/> converter to or from CSV
    /// </summary>
    public class CsvDateOnlyConverter : CsvDefaultFormattableConverter
    {
        public CsvDateOnlyConverter() : base(typeof(DateOnly))
        {
        }

        public CsvDateOnlyConverter(IFormatProvider formatProvider, string[] formats)
            : base(typeof(DateOnly), formatProvider, formats)
        {
        }

        public CsvDateOnlyConverter(IFormatProvider formatProvider, string[] formats, DateTimeStyles dateTimeStyle)
            : base(typeof(DateOnly), formatProvider, formats)
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
                        return DateOnly.ParseExact(value, Formats, FormatProvider, (DateTimeStyles)DateTimeStyle);
                    }
                    else
                    {
                        return DateOnly.ParseExact(value, Formats, FormatProvider);
                    }
                }
                else 
                {
                    if (DateTimeStyle != null)
                    {
                        return DateOnly.Parse(value, FormatProvider, (DateTimeStyles)DateTimeStyle);
                    }
                    else
                    {
                        return DateOnly.Parse(value, FormatProvider);
                    }
                }
            }
        }
    }
#endif
}