
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
    /// Implementation of a enum converter to or from CSV
    /// </summary>
    public class CsvDefaultEnumConverter : CsvDefaultFormattableConverter
    {
        public CsvDefaultEnumConverter(Type conversionType)
            : base(conversionType)
        {
        }

        public CsvDefaultEnumConverter(Type conversionType, CultureInfo cultureInfo)
            : base(conversionType, cultureInfo)
        {
        }

        public CsvDefaultEnumConverter(Type conversionType, CultureInfo cultureInfo, string[] formats)
            : base(conversionType, cultureInfo)
        {
            Formats = formats;
        }

        public CsvDefaultEnumConverter(Type conversionType, CultureInfo cultureInfo, string[] formats, bool ignoreCase)
            : base(conversionType, cultureInfo)
        {
            Formats = formats;
            IgnoreCase = ignoreCase;
        }

        public bool IgnoreCase { get; set; } = true;

        public override object FromString(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }
            else
            {
                if (_conversionType.IsEnum)
                {
                    return Enum.Parse(_conversionType, value, IgnoreCase);
                }
                else
                {
                    return base.FromString(value);
                }
            }
        }
    }
}