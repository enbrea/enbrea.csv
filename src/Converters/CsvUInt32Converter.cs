
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

using System.Globalization;

namespace Enbrea.Csv
{
    /// <summary>
    /// Implementation of a <see cref="uint"/> converter to or from CSV
    /// </summary>
    public class CsvUInt32Converter : CsvDefaultNumberConverter
    {
        public CsvUInt32Converter() : base(typeof(uint))
        {
        }

        public CsvUInt32Converter(CultureInfo cultureInfo, string[] formats)
            : base(typeof(uint), cultureInfo, formats)
        {
        }

        public CsvUInt32Converter(CultureInfo cultureInfo, string[] formats, NumberStyles numberStyle)
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
                    return uint.Parse(value, (NumberStyles)NumberStyle, CultureInfo);
                }
                else
                {
                    return uint.Parse(value, CultureInfo);
                }
            }
        }
    }
}