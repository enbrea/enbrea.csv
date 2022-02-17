
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
    /// Implementation of a sbyte converter to or from CSV
    /// </summary>
    public class CsvSByteConverter : CsvDefaultNumberConverter
    {
        public CsvSByteConverter() : base(typeof(sbyte))
        {
        }

        public CsvSByteConverter(CultureInfo cultureInfo, string[] formats)
            : base(typeof(sbyte), cultureInfo, formats)
        {
        }

        public CsvSByteConverter(CultureInfo cultureInfo, string[] formats, NumberStyles numberStyle)
            : base(typeof(sbyte), cultureInfo, formats, numberStyle)
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
                    return sbyte.Parse(value, (NumberStyles)NumberStyle, CultureInfo);
                }
                else
                {
                    return sbyte.Parse(value, CultureInfo);
                }
            }
        }
    }
}