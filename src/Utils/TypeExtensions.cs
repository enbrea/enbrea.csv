#region ENBREA.CSV - Copyright (C) 2023 STÜBER SYSTEMS GmbH
/*    
 *    ENBREA.CSV 
 *    
 *    Copyright (C) 2023 STÜBER SYSTEMS GmbH
 *
 *    Licensed under the MIT License, Version 2.0. 
 */
#endregion

using System;
using System.Reflection;

namespace Enbrea.Csv
{
    /// <summary>
    /// Extensions for <see cref="Type"/>
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// A type member visitor
        /// </summary>
        /// <param name="type">The type</param>
        /// <param name="action">The action method called for every member</param>
        public static void VisitMembers(this Type type, Action<string, MemberInfo, bool> action)
        {
            foreach (MemberInfo memberInfo in type.GetMembers())
            {
                var memberIgnoreAttr = memberInfo.GetCustomAttribute<CsvNotMappedAttribute>();
                if (memberIgnoreAttr == null)
                {
                    var memberHeaderAttr = memberInfo.GetCustomAttribute<CsvHeaderAttribute>();
                    if (memberHeaderAttr != null)
                    {
                        action(memberHeaderAttr.Name, memberInfo, false);
                        foreach (var alternativeName in memberHeaderAttr.AlterntiveNames)
                        {
                            action(alternativeName, memberInfo, true);
                        }
                    }
                    else
                    {
                        switch (memberInfo.MemberType)
                        {
                            case MemberTypes.Field:
                                if (((FieldInfo)memberInfo).IsPublic)
                                {
                                    action(memberInfo.Name, memberInfo, false);
                                }
                                break;
                            case MemberTypes.Property:
                                if ((((PropertyInfo)memberInfo).GetSetMethod() != null && ((PropertyInfo)memberInfo).GetSetMethod().IsPublic) ||
                                    (((PropertyInfo)memberInfo).GetGetMethod() != null && ((PropertyInfo)memberInfo).GetGetMethod().IsPublic))
                                {
                                        action(memberInfo.Name, memberInfo, false);
                                }
                                break;
                        };
                    }
                }
            }
        }
    }
}