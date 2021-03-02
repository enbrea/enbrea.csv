
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
    /// Implementation of a Int64 converter to or from CSV
    /// </summary>
    public class CsvInt64Converter : CsvDefaultNumberConverter
    {
        public CsvInt64Converter() : base(typeof(long))
        {
        }

        public CsvInt64Converter(CultureInfo cultureInfo, string[] formats)
            : base(typeof(long), cultureInfo, formats)
        {
        }

        public CsvInt64Converter(CultureInfo cultureInfo, string[] formats, NumberStyles numberStyle)
            : base(typeof(long), cultureInfo, formats, numberStyle)
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
                    return long.Parse(value, (NumberStyles)NumberStyle, CultureInfo);
                }
                else
                {
                    return long.Parse(value, CultureInfo);
                }
            }
        }
    }
}