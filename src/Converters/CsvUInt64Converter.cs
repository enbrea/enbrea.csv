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
    /// Implementation of a <see cref="ulong"/> converter to or from CSV
    /// </summary>
    public class CsvUInt64Converter : CsvDefaultNumberConverter
    {
        public CsvUInt64Converter() : base(typeof(ulong))
        {
        }

        public CsvUInt64Converter(IFormatProvider formatProvider, string[] formats)
            : base(typeof(ulong), formatProvider, formats)
        {
        }

        public CsvUInt64Converter(IFormatProvider formatProvider, string[] formats, NumberStyles numberStyle)
            : base(typeof(ulong), formatProvider, formats, numberStyle)
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
                    return ulong.Parse(value, (NumberStyles)NumberStyle, FormatProvider);
                }
                else
                {
                    return ulong.Parse(value, FormatProvider);
                }
            }
        }
    }
}