#region ENBREA.CSV - Copyright (C) 2020 STÜBER SYSTEMS GmbH
/*    
 *    ENBREA.CSV 
 *    
 *    Copyright (C) 2020 STÜBER SYSTEMS GmbH
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
    /// Extensions for<see cref="CsvTableReader"/> <see cref="CsvTableWriter"/>, <see cref="CsvLineTableReader"/> and 
    /// <see cref="CsvLineTableWriter"/>
    /// </summary>
    public static class CsvTableAccessExtensions
    {
        public static void AddConverter<T>(this CsvTableAccess csvTableAccess, ICsvConverter valueConverter)
        {
            csvTableAccess.ConverterResolver.AddConverter<T>(valueConverter);
        }

        public static void SetCultureInfo<T>(this CsvTableAccess csvTableAccess, CultureInfo cultureInfo)
        {
            var converter = csvTableAccess.ConverterResolver.GetConverter<T>();
            if (converter is CsvDefaultConverter valueConverter)
            {
                valueConverter.CultureInfo = cultureInfo;
            }
        }

        public static void SetDateTimeStyle<T>(this CsvTableAccess csvTableAccess, DateTimeStyles dateTimeStyle)
        {
            var converter = csvTableAccess.ConverterResolver.GetConverter<T>();
            if (converter is CsvDateTimeConverter dateTimeConverter)
            {
                dateTimeConverter.DateTimeStyle = dateTimeStyle;
            }
            else if (converter is CsvDateTimeOffsetConverter dateTimeOffsetConverter)
            {
                dateTimeOffsetConverter.DateTimeStyle = dateTimeStyle;
            }
        }

        public static void SetFormats<T>(this CsvTableAccess csvTableAccess, params string[] formats)
        {
            var converter = csvTableAccess.ConverterResolver.GetConverter<T>();
            if (converter is CsvDefaultFormattableConverter formatableConverter)
            {
                formatableConverter.Formats = formats;
            }
        }

        public static void SetNumberStyle<T>(this CsvTableAccess csvTableAccess, NumberStyles numberStyle)
        {
            var converter = csvTableAccess.ConverterResolver.GetConverter<T>();
            if (converter is CsvDefaultNumberConverter numberValueConverter)
            {
                numberValueConverter.NumberStyle = numberStyle;
            }
        }

        public static void SetTimeSpanStyle<T>(this CsvTableAccess csvTableAccess, TimeSpanStyles timeSpanStyle)
        {
            var converter = csvTableAccess.ConverterResolver.GetConverter<T>();
            if (converter is CsvTimeSpanConverter timeSpanConverter)
            {
                timeSpanConverter.TimeSpanStyle = timeSpanStyle;
            }
        }

        public static void SetTrueFalseString<T>(this CsvTableAccess csvTableAccess, string trueString, string falseString)
        {
            var converter = csvTableAccess.ConverterResolver.GetConverter<T>();
            if (converter is CsvBooleanConverter booleanConverter)
            {
                booleanConverter.TrueStrings = new string[] { trueString };
                booleanConverter.FalseStrings = new string[] { falseString };
            }
        }

        public static void SetTrueFalseStrings<T>(this CsvTableAccess csvTableAccess, string[] trueStrings, string[] falseStrings)
        {
            var converter = csvTableAccess.ConverterResolver.GetConverter<T>();
            if (converter is CsvBooleanConverter booleanConverter)
            {
                booleanConverter.TrueStrings = trueStrings;
                booleanConverter.FalseStrings = falseStrings;
            }
        }

        public static void SetUriKind<T>(this CsvTableAccess csvTableAccess, UriKind uriKind)
        {
            var converter = csvTableAccess.ConverterResolver.GetConverter<T>();
            if (converter is CsvUriConverter uriConverter)
            {
                uriConverter.UriKind = uriKind;
            }
        }
    }
}