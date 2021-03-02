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

namespace Enbrea.Csv
{
    /// <summary>
    /// Implementation of a string converter to or from CSV
    /// </summary>
    public class CsvStringConverter : CsvDefaultConverter
    {
        public CsvStringConverter() : base(typeof(string))
        {
        }

        public override object FromString(string value)
        {
            return value;
        }

        public override string ToString(object value)
        {
            return (string)value;
        }
    }
}