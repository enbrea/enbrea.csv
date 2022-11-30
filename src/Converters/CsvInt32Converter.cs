
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
    /// Implementation of a <see cref="int"/> converter to or from CSV
    /// </summary>
    public class CsvInt32Converter : CsvDefaultNumberConverter
    {
        public CsvInt32Converter() : base(typeof(int))
        {
        }

        public CsvInt32Converter(IFormatProvider formatProvider, string[] formats)
            : base(typeof(int), formatProvider, formats)
        {
        }

        public CsvInt32Converter(IFormatProvider formatProvider, string[] formats, NumberStyles numberStyle)
            : base(typeof(int), formatProvider, formats, numberStyle)
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
                    return int.Parse(value, (NumberStyles)NumberStyle, FormatProvider);
                }
                else
                {
                    return int.Parse(value, FormatProvider);
                }
            }
        }
    }
}