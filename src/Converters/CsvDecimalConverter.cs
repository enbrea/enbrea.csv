
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
    /// Implementation of a decimal converter to or from CSV
    /// </summary>
    public class CsvDecimalConverter : CsvDefaultNumberConverter
    {
        public CsvDecimalConverter() : base(typeof(decimal))
        {
        }

        public CsvDecimalConverter(CultureInfo cultureInfo, string[] formats)
            : base(typeof(decimal), cultureInfo, formats)
        {
        }

        public CsvDecimalConverter(CultureInfo cultureInfo, string[] formats, NumberStyles numberStyle)
            : base(typeof(decimal), cultureInfo, formats, numberStyle)
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
                    return decimal.Parse(value, (NumberStyles)NumberStyle, CultureInfo);
                }
                else
                {
                    return decimal.Parse(value, CultureInfo);
                }
            }
        }
    }
}