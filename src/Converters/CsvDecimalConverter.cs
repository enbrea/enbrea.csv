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
    /// Implementation of a <see cref="decimal"/> converter to or from CSV
    /// </summary>
    public class CsvDecimalConverter : CsvDefaultNumberConverter
    {
        public CsvDecimalConverter() : base(typeof(decimal))
        {
        }

        public CsvDecimalConverter(IFormatProvider formatProvider, string[] formats)
            : base(typeof(decimal), formatProvider, formats)
        {
        }

        public CsvDecimalConverter(IFormatProvider formatProvider, string[] formats, NumberStyles numberStyle)
            : base(typeof(decimal), formatProvider, formats, numberStyle)
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
                    return decimal.Parse(value, (NumberStyles)NumberStyle, FormatProvider);
                }
                else
                {
                    return decimal.Parse(value, FormatProvider);
                }
            }
        }
    }
}