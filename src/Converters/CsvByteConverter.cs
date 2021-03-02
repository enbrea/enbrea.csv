#region ENBREA.CSV - Copyright (C) 2021 STÜBER SYSTEMS GmbH
/*    
 *    ENBREA.CSV 
 *    
 *    Copyright (C) 2021 STÜBER SYSTEMS GmbH
 *
 *    Licensed under the MIT License, Version 2.0. 
 * 
 */
#endregion

using System.Globalization;

namespace Enbrea.Csv
{
    /// <summary>
    /// Implementation of a byte converter to or from CSV
    /// </summary>
    public class CsvByteConverter : CsvDefaultNumberConverter
    {
        public CsvByteConverter() : base(typeof(byte))
        {
        }

        public CsvByteConverter(CultureInfo cultureInfo, string[] formats)
            : base(typeof(byte), cultureInfo, formats)
        {
        }

        public CsvByteConverter(CultureInfo cultureInfo, string[] formats, NumberStyles numberStyle)
            : base(typeof(byte), cultureInfo, formats, numberStyle)
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
                    return byte.Parse(value, (NumberStyles)NumberStyle, CultureInfo);
                }
                else
                {
                    return byte.Parse(value, CultureInfo);
                }
            }
        }
    }
}