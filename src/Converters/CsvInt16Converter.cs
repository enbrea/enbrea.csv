
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
    /// Implementation of a <see cref="short"/> converter to or from CSV
    /// </summary>
    public class CsvInt16Converter : CsvDefaultNumberConverter
    {
        public CsvInt16Converter() : base(typeof(short))
        {
        }

        public CsvInt16Converter(IFormatProvider formatProvider, string[] formats)
            : base(typeof(short), formatProvider, formats)
        {
        }

        public CsvInt16Converter(IFormatProvider formatProvider, string[] formats, NumberStyles numberStyle)
            : base(typeof(short), formatProvider, formats, numberStyle)
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
                    return short.Parse(value, (NumberStyles)NumberStyle, FormatProvider);
                }
                else
                {
                    return short.Parse(value, FormatProvider);
                }
            }
        }
    }
}