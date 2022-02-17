
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
    /// Implementation of a UInt64 converter to or from CSV
    /// </summary>
    public class CsvUInt64Converter : CsvDefaultNumberConverter
    {
        public CsvUInt64Converter() : base(typeof(ulong))
        {
        }

        public CsvUInt64Converter(CultureInfo cultureInfo, string[] formats)
            : base(typeof(ulong), cultureInfo, formats)
        {
        }

        public CsvUInt64Converter(CultureInfo cultureInfo, string[] formats, NumberStyles numberStyle)
            : base(typeof(ulong), cultureInfo, formats, numberStyle)
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
                    return ulong.Parse(value, (NumberStyles)NumberStyle, CultureInfo);
                }
                else
                {
                    return ulong.Parse(value, CultureInfo);
                }
            }
        }
    }
}