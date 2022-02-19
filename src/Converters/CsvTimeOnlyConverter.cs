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
    /// Implementation of a <see cref="TimeOnly"/> converter to or from CSV
    /// </summary>
    public class CsvTimeOnlyConverter : CsvDefaultFormattableConverter
    {
        public CsvTimeOnlyConverter() : base(typeof(TimeOnly))
        {
        }

        public CsvTimeOnlyConverter(CultureInfo cultureInfo, string[] formats)
            : base(typeof(TimeOnly), cultureInfo, formats)
        {
        }

        public CsvTimeOnlyConverter(CultureInfo cultureInfo, string[] formats, DateTimeStyles dateTimeStyle)
            : base(typeof(TimeOnly), cultureInfo, formats)
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
                        return TimeOnly.ParseExact(value, Formats, CultureInfo, (DateTimeStyles)DateTimeStyle);
                    }
                    else
                    {
                        return TimeOnly.ParseExact(value, Formats, CultureInfo);
                    }
                }
                else 
                {
                    if (DateTimeStyle != null)
                    {
                        return TimeOnly.Parse(value, CultureInfo, (DateTimeStyles)DateTimeStyle);
                    }
                    else
                    {
                        return TimeOnly.Parse(value, CultureInfo);
                    }
                }
            }
        }
    }
#endif
}