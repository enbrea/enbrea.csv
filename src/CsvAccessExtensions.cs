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
    /// Extensions for<see cref="CsvAccess"/>
    /// </summary>
    public static class CsvAccessExtensions
    {
        public static void AddConverter<T>(this CsvAccess csvAccess, ICsvConverter valueConverter)
        {
            csvAccess.ConverterResolver.AddConverter<T>(valueConverter);
        }

        public static void SetCultureInfo<T>(this CsvAccess csvAccess, CultureInfo cultureInfo)
        {
            var converter = csvAccess.ConverterResolver.GetConverter<T>();
            if (converter is CsvDefaultConverter valueConverter)
            {
                valueConverter.CultureInfo = cultureInfo;
            }
        }

        public static void SetDateTimeStyle<T>(this CsvAccess csvAccess, DateTimeStyles dateTimeStyle)
        {
            var converter = csvAccess.ConverterResolver.GetConverter<T>();
            if (converter is CsvDateTimeConverter dateTimeConverter)
            {
                dateTimeConverter.DateTimeStyle = dateTimeStyle;
            }
            else if (converter is CsvDateTimeOffsetConverter dateTimeOffsetConverter)
            {
                dateTimeOffsetConverter.DateTimeStyle = dateTimeStyle;
            }
#if NET6_0_OR_GREATER
            else if (converter is CsvDateOnlyConverter dateOnlyConverter)
            {
                dateOnlyConverter.DateTimeStyle = dateTimeStyle;
            }
            else if (converter is CsvTimeOnlyConverter timeOnlyConverter)
            {
                timeOnlyConverter.DateTimeStyle = dateTimeStyle;
            }
#endif
        }

        public static void SetFormats<T>(this CsvAccess csvAccess, params string[] formats)
        {
            var converter = csvAccess.ConverterResolver.GetConverter<T>();
            if (converter is CsvDefaultFormattableConverter formatableConverter)
            {
                formatableConverter.Formats = formats;
            }
        }

        public static void SetNumberStyle<T>(this CsvAccess csvAccess, NumberStyles numberStyle)
        {
            var converter = csvAccess.ConverterResolver.GetConverter<T>();
            if (converter is CsvDefaultNumberConverter numberValueConverter)
            {
                numberValueConverter.NumberStyle = numberStyle;
            }
        }

        public static void SetTimeSpanStyle<T>(this CsvAccess csvAccess, TimeSpanStyles timeSpanStyle)
        {
            var converter = csvAccess.ConverterResolver.GetConverter<T>();
            if (converter is CsvTimeSpanConverter timeSpanConverter)
            {
                timeSpanConverter.TimeSpanStyle = timeSpanStyle;
            }
        }

        public static void SetTrueFalseString<T>(this CsvAccess csvAccess, string trueString, string falseString)
        {
            var converter = csvAccess.ConverterResolver.GetConverter<T>();
            if (converter is CsvBooleanConverter booleanConverter)
            {
                booleanConverter.TrueStrings = new string[] { trueString };
                booleanConverter.FalseStrings = new string[] { falseString };
            }
        }

        public static void SetTrueFalseStrings<T>(this CsvAccess csvAccess, string[] trueStrings, string[] falseStrings)
        {
            var converter = csvAccess.ConverterResolver.GetConverter<T>();
            if (converter is CsvBooleanConverter booleanConverter)
            {
                booleanConverter.TrueStrings = trueStrings;
                booleanConverter.FalseStrings = falseStrings;
            }
        }

        public static void SetUriKind<T>(this CsvAccess csvAccess, UriKind uriKind)
        {
            var converter = csvAccess.ConverterResolver.GetConverter<T>();
            if (converter is CsvUriConverter uriConverter)
            {
                uriConverter.UriKind = uriKind;
            }
        }
    }
}