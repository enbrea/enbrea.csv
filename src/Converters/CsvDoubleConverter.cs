
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
    /// Implementation of a <see cref="double"/> converter to or from CSV
    /// </summary>
    public class CsvDoubleConverter : CsvDefaultNumberConverter
    {
        public CsvDoubleConverter() : base(typeof(double))
        {
        }

        public CsvDoubleConverter(IFormatProvider formatProvider, string[] formats)
            : base(typeof(double), formatProvider, formats)
        {
        }

        public CsvDoubleConverter(IFormatProvider formatProvider, string[] formats, NumberStyles numberStyle)
            : base(typeof(double), formatProvider, formats, numberStyle)
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
                    return double.Parse(value, (NumberStyles)NumberStyle, FormatProvider);
                }
                else
                {
                    return double.Parse(value, FormatProvider);
                }
            }
        }
    }
}