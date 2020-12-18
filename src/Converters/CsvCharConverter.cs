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

namespace Enbrea.Csv
{
    /// <summary>
    /// Implementation of a char converter to or from CSV
    /// </summary>
    public class CsvCharConverter : CsvDefaultConverter
    {
        public CsvCharConverter() : base(typeof(char))
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
                if (value.Length > 1)
                {
                    value = value.Trim();
                }
                return char.Parse(value);
            }
        }
    }
}