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
    /// Implementation of a <see cref="bool"/> converter to or from CSV
    /// </summary>
    public class CsvBooleanConverter : CsvDefaultConverter
    {
        public CsvBooleanConverter() : base(typeof(bool))
        {
        }

        public CsvBooleanConverter(CultureInfo cultureInfo)
             : base(typeof(bool), cultureInfo)
        {
        }

        public CsvBooleanConverter(CultureInfo cultureInfo, string[] trueStrings, string[] falseStrings)
             : base(typeof(bool), cultureInfo)
        {
            TrueStrings = trueStrings;
            FalseStrings = falseStrings;
        }

        public string[] FalseStrings { get; set; } = { bool.FalseString };
        public string[] TrueStrings { get; set; } = { bool.TrueString };

        public override object FromString(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }
            else
            {
                return value.ToBoolean(StringComparer.InvariantCultureIgnoreCase, TrueStrings, FalseStrings);
            }
        }

        public override string ToString(object value)
        {
            if (value == null)
            {
                return string.Empty;
            }
            else
            {
                if (value is bool boolValue && TrueStrings != null && TrueStrings.Length > 0 && FalseStrings != null && FalseStrings.Length > 0)
                {
                    return boolValue.ToString(TrueStrings[0], FalseStrings[0]);
                }
                else
                {
                    return base.ToString(value);
                }
            }
        }
    }
}