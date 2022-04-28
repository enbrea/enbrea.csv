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
using System.Collections.Generic;
using System.Reflection;

namespace Enbrea.Csv
{
    /// <summary>
    /// Default implementation of an <see cref="ICsvClassMapper"/>
    /// </summary>
    public class CsvDefaultClassMapper : ICsvClassMapper
    {
        private readonly Dictionary<string, MemberInfo> _memberInfoMaps = new Dictionary<string, MemberInfo>();

        public CsvDefaultClassMapper(Type type)
        {
            type.VisitMembers((name, memberInfo, isAlternativeName) => _memberInfoMaps.Add(name, memberInfo));
        }

        public bool ContainsValue(string name)
        {
            return _memberInfoMaps.ContainsKey(name);
        }

        public object GetValue(object entity, string name)
        {
            if (_memberInfoMaps.TryGetValue(name, out var memberInfoMap))
            {
                return memberInfoMap.MemberType switch
                {
                    MemberTypes.Field => ((FieldInfo)memberInfoMap).GetValue(entity),
                    MemberTypes.Property => ((PropertyInfo)memberInfoMap).GetValue(entity),
                    _ => throw new NotImplementedException(),
                };
            }
            else
            {
                throw new CsvHeaderNotFoundException($"Header {name} does not exist.");
            }
        }

        public Type GetValueType(string name)
        {
            if (_memberInfoMaps.TryGetValue(name, out var memberInfoMap))
            {
                return memberInfoMap.MemberType switch
                {
                    MemberTypes.Field => Nullable.GetUnderlyingType(((FieldInfo)memberInfoMap).FieldType) ?? ((FieldInfo)memberInfoMap).FieldType,
                    MemberTypes.Property => Nullable.GetUnderlyingType(((PropertyInfo)memberInfoMap).PropertyType) ?? ((PropertyInfo)memberInfoMap).PropertyType,
                    _ => throw new NotImplementedException(),
                };
            }
            else
            {
                throw new CsvHeaderNotFoundException($"Header {name} does not exist.");
            }
        }

        public void SetValue(object entity, string name, object value)
        {
            if (_memberInfoMaps.TryGetValue(name, out var memberInfoMap))
            {
                switch (memberInfoMap.MemberType)
                {
                    case MemberTypes.Field: 
                        ((FieldInfo)memberInfoMap).SetValue(entity, value);
                        break;
                    case MemberTypes.Property: 
                        ((PropertyInfo)memberInfoMap).SetValue(entity, value);
                        break;
                    default:
                        throw new NotImplementedException();
                };
            }
            else
            {
                throw new CsvHeaderNotFoundException($"Header {name} does not exist.");
            }
        }
    }
}