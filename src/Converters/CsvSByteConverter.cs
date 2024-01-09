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
    /// Implementation of a <see cref="sbyte"/> converter to or from CSV
    /// </summary>
    public class CsvSByteConverter : CsvDefaultNumberConverter
    {
        public CsvSByteConverter() : base(typeof(sbyte))
        {
        }

        public CsvSByteConverter(IFormatProvider formatProvider, string[] formats)
            : base(typeof(sbyte), formatProvider, formats)
        {
        }

        public CsvSByteConverter(IFormatProvider formatProvider, string[] formats, NumberStyles numberStyle)
            : base(typeof(sbyte), formatProvider, formats, numberStyle)
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
                    return sbyte.Parse(value, (NumberStyles)NumberStyle, FormatProvider);
                }
                else
                {
                    return sbyte.Parse(value, FormatProvider);
                }
            }
        }
    }
}