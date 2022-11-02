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

using System;
using System.Globalization;

namespace Enbrea.Csv
{
    /// <summary>
    /// Generic default implementation of a value converter to or from CSV
    /// </summary>
    public class CsvDefaultConverter : ICsvConverter
    {
        protected readonly Type _conversionType;

        public CsvDefaultConverter(Type conversionType)
        {
            FormatProvider = CultureInfo.InvariantCulture;
            _conversionType = conversionType;
        }

        public CsvDefaultConverter(Type conversionType, IFormatProvider formatProvider)
        {
            FormatProvider = formatProvider;
            _conversionType = conversionType;
        }

        public IFormatProvider FormatProvider { get; set; }
        
        public virtual object FromString(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }
            else 
            {
                return Convert.ChangeType(value, _conversionType, FormatProvider);
            }
        }

        public virtual string ToString(object value)
        {
            if (value == null)
            {
                return string.Empty;
            } 
            else 
            {
                return value.ToString();
            }
        }
    }
}