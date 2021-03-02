
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
    /// Implementation of a Int32 converter to or from CSV
    /// </summary>
    public class CsvInt32Converter : CsvDefaultNumberConverter
    {
        public CsvInt32Converter() : base(typeof(int))
        {
        }

        public CsvInt32Converter(CultureInfo cultureInfo, string[] formats)
            : base(typeof(int), cultureInfo, formats)
        {
        }

        public CsvInt32Converter(CultureInfo cultureInfo, string[] formats, NumberStyles numberStyle)
            : base(typeof(int), cultureInfo, formats, numberStyle)
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
                    return int.Parse(value, (NumberStyles)NumberStyle, CultureInfo);
                }
                else
                {
                    return int.Parse(value, CultureInfo);
                }
            }
        }
    }
}