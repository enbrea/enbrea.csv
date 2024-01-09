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
    /// Implementation of a <see cref="long"/> converter to or from CSV
    /// </summary>
    public class CsvInt64Converter : CsvDefaultNumberConverter
    {
        public CsvInt64Converter() : base(typeof(long))
        {
        }

        public CsvInt64Converter(IFormatProvider formatProvider, string[] formats)
            : base(typeof(long), formatProvider, formats)
        {
        }

        public CsvInt64Converter(IFormatProvider formatProvider, string[] formats, NumberStyles numberStyle)
            : base(typeof(long), formatProvider, formats, numberStyle)
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
                    return long.Parse(value, (NumberStyles)NumberStyle, FormatProvider);
                }
                else
                {
                    return long.Parse(value, FormatProvider);
                }
            }
        }
    }
}