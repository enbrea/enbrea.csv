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
    /// Implementation of a <see cref="byte"/>  converter to or from CSV
    /// </summary>
    public class CsvByteConverter : CsvDefaultNumberConverter
    {
        public CsvByteConverter() : base(typeof(byte))
        {
        }

        public CsvByteConverter(IFormatProvider formatProvider, string[] formats)
            : base(typeof(byte), formatProvider, formats)
        {
        }

        public CsvByteConverter(IFormatProvider formatProvider, string[] formats, NumberStyles numberStyle)
            : base(typeof(byte), formatProvider, formats, numberStyle)
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
                    return byte.Parse(value, (NumberStyles)NumberStyle, FormatProvider);
                }
                else
                {
                    return byte.Parse(value, FormatProvider);
                }
            }
        }
    }
}