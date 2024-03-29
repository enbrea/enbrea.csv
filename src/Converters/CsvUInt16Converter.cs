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

using System.Globalization;

namespace Enbrea.Csv
{
    /// <summary>
    /// Implementation of a <see cref="ushort"/> converter to or from CSV
    /// </summary>
    public class CsvUInt16Converter : CsvDefaultNumberConverter
    {
        public CsvUInt16Converter() : base(typeof(ushort))
        {
        }

        public CsvUInt16Converter(CultureInfo cultureInfo, string[] formats)
            : base(typeof(ushort), cultureInfo, formats)
        {
        }

        public CsvUInt16Converter(CultureInfo cultureInfo, string[] formats, NumberStyles numberStyle)
            : base(typeof(ushort), cultureInfo, formats, numberStyle)
        {
        }

        public override object FromString(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }
            else
            {
                if (NumberStyle != null)
                {
                    return ushort.Parse(value, (NumberStyles)NumberStyle, FormatProvider);
                }
                else
                {
                    return ushort.Parse(value, FormatProvider);
                }
            }
        }
    }
}