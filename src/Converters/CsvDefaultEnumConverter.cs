
#region ENBREA.CSV - Copyright (c) STÜBER SYSTEMS GmbH
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
    /// Implementation of a enum converter to or from CSV
    /// </summary>
    public class CsvDefaultEnumConverter : CsvDefaultConverter
    {
        public CsvDefaultEnumConverter(Type conversionType)
            : base(conversionType)
        {
        }

        public CsvDefaultEnumConverter(Type conversionType, CultureInfo cultureInfo)
            : base(conversionType, cultureInfo)
        {
        }

        public CsvDefaultEnumConverter(Type conversionType, CultureInfo cultureInfo, bool ignoreCase)
            : base(conversionType, cultureInfo)
        {
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