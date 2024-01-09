#region ENBREA.CSV - Copyright (c) STÜBER SYSTEMS GmbH
/*    
 *    ENBREA.CSV 
 *    
 *    Copyright (c) STÜBER SYSTEMS GmbH
 *
 *    Licensed under the MIT License, Version 2.0. 
 * 
 */
#endregion

using System;
using System.Globalization;

namespace Enbrea.Csv
{
    /// <summary>
    /// Implementation of a <see cref="Guid"/> converter to or from CSV
    /// </summary>
    public class CsvGuidConverter : CsvDefaultFormattableConverter
    {
        public CsvGuidConverter() : base(typeof(Guid))
        {
        }

        public CsvGuidConverter(CultureInfo cultureInfo, string[] formats)
            : base(typeof(Guid), cultureInfo, formats)
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
                return new Guid(value);
            }
        }
    }
}