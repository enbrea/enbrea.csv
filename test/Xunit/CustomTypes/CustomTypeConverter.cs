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

using System.Text.Json;

namespace Enbrea.Csv.Tests
{
    public class CustomTypeConverter : ICsvConverter
    {
        public virtual object FromString(string value)
        {
            return JsonSerializer.Deserialize<CustomType>(value);
        }

        public string ToString(object value)
        {
            if (value is CustomType objectValue)
            {
                return JsonSerializer.Serialize(objectValue, new JsonSerializerOptions { WriteIndented = false });
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
