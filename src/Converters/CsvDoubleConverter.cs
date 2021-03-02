
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
    /// Implementation of a double converter to or from CSV
    /// </summary>
    public class CsvDoubleConverter : CsvDefaultNumberConverter
    {
        public CsvDoubleConverter() : base(typeof(double))
        {
        }

        public CsvDoubleConverter(CultureInfo cultureInfo, string[] formats)
            : base(typeof(double), cultureInfo, formats)
        {
        }

        public CsvDoubleConverter(CultureInfo cultureInfo, string[] formats, NumberStyles numberStyle)
            : base(typeof(double), cultureInfo, formats, numberStyle)
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
                    return double.Parse(value, (NumberStyles)NumberStyle, CultureInfo);
                }
                else
                {
                    return double.Parse(value, CultureInfo);
                }
            }
        }
    }
}