
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
    /// Implementation of a Int16 converter to or from CSV
    /// </summary>
    public class CsvInt16Converter : CsvDefaultNumberConverter
    {
        public CsvInt16Converter() : base(typeof(short))
        {
        }

        public CsvInt16Converter(CultureInfo cultureInfo, string[] formats)
            : base(typeof(short), cultureInfo, formats)
        {
        }

        public CsvInt16Converter(CultureInfo cultureInfo, string[] formats, NumberStyles numberStyle)
            : base(typeof(short), cultureInfo, formats, numberStyle)
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
                    return short.Parse(value, (NumberStyles)NumberStyle, CultureInfo);
                }
                else
                {
                    return short.Parse(value, CultureInfo);
                }
            }
        }
    }
}