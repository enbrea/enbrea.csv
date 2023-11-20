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
    /// Implementation of a <see cref="float"/> converter to or from CSV
    /// </summary>
    public class CsvSingleConverter : CsvDefaultNumberConverter
    {
        public CsvSingleConverter() : base(typeof(float))
        {
        }

        public CsvSingleConverter(IFormatProvider formatProvider, string[] formats)
            : base(typeof(float), formatProvider, formats)
        {
        }

        public CsvSingleConverter(IFormatProvider formatProvider, string[] formats, NumberStyles numberStyle)
            : base(typeof(float), formatProvider, formats, numberStyle)
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
                    return float.Parse(value, (NumberStyles)NumberStyle, FormatProvider);
                }
                else
                {
                    return float.Parse(value, FormatProvider);
                }
            }
        }
    }
}