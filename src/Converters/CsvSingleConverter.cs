
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
    /// Implementation of a <see cref="float"/> converter to or from CSV
    /// </summary>
    public class CsvSingleConverter : CsvDefaultNumberConverter
    {
        public CsvSingleConverter() : base(typeof(float))
        {
        }

        public CsvSingleConverter(CultureInfo cultureInfo, string[] formats)
            : base(typeof(float), cultureInfo, formats)
        {
        }

        public CsvSingleConverter(CultureInfo cultureInfo, string[] formats, NumberStyles numberStyle)
            : base(typeof(float), cultureInfo, formats, numberStyle)
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
                    return float.Parse(value, (NumberStyles)NumberStyle, CultureInfo);
                }
                else
                {
                    return float.Parse(value, CultureInfo);
                }
            }
        }
    }
}