#region ENBREA.CSV - Copyright (C) 2020 STÜBER SYSTEMS GmbH
/*    
 *    ENBREA.CSV 
 *    
 *    Copyright (C) 2020 STÜBER SYSTEMS GmbH
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
            CultureInfo = CultureInfo.InvariantCulture;
            _conversionType = conversionType;
        }

        public CsvDefaultConverter(Type conversionType, CultureInfo cultureInfo)
        {
            CultureInfo = cultureInfo;
            _conversionType = conversionType;
        }

        public CultureInfo CultureInfo { get; set; }
        
        public virtual object FromString(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }
            else 
            {
                return Convert.ChangeType(value, _conversionType, CultureInfo);
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