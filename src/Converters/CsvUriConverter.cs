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

namespace Enbrea.Csv
{
    /// <summary>
    /// Implementation of a <see cref="Uri"/> converter to or from CSV
    /// </summary>
    public class CsvUriConverter : CsvDefaultConverter
    {
        public CsvUriConverter() : base(typeof(Uri))
        {
        }

        public CsvUriConverter(UriKind uriKind) : base(typeof(Uri))
        {
            UriKind = uriKind;
        }

        public UriKind? UriKind { get; set; }

        public override object FromString(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }
            else
            {
                if (UriKind != null)
                {
                    return new Uri(value, (UriKind)UriKind);
                }
                else
                {
                    return new Uri(value);
                }
            }
        }
    }
}