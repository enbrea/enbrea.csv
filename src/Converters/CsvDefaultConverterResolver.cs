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
using System.Collections.Generic;

namespace Enbrea.Csv
{
    /// <summary>
    /// Default implementation of an <see cref="ICsvConverterResolver"/>
    /// </summary>
    public class CsvDefaultConverterResolver : ICsvConverterResolver
    {
        private Dictionary<Type, ICsvConverter> _converters = new Dictionary<Type, ICsvConverter>();

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvDefaultConverterResolver"/> class.
        /// </summary>
        public CsvDefaultConverterResolver()
        {
            RegisterDefaultConverters();
            RegisterCustomConverters();
        }

        /// <summary>
        /// Registers a new value converter for a given type
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <param name="valueConverter">The converter instance</param>
        public void AddConverter<T>(ICsvConverter converter)
        {
            _converters[typeof(T)] = converter;
        }

        /// <summary>
        /// Gives back the value converter for a given type
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <returns>The converter instance</returns>
        public ICsvConverter GetConverter<T>()
        {
            return GetConverter(typeof(T));
        }

        /// <summary>
        /// Gives back the value converter for a given type
        /// </summary>
        /// <param name="type">The type</param>
        /// <returns>The converter instance</returns>
        public ICsvConverter GetConverter(Type type)
        {
            if (_converters.TryGetValue(type, out ICsvConverter converter))
            {
                return converter;
            }
            else
            {
                ICsvConverter defaultConverter;
                if (type.IsEnum)
                {
                    defaultConverter = new CsvDefaultEnumConverter(type);
                }
                else
                {
                    defaultConverter = new CsvDefaultConverter(type);
                }
                _converters.Add(type, defaultConverter);
                return defaultConverter;
            }
        }

        /// <summary>
        /// Unregisters the value converter for a given type
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        public void RemoveConverter<T>()
        {
            _converters.Remove(typeof(T));
        }

        protected virtual void RegisterCustomConverters()
        {
        }

        protected virtual void RegisterDefaultConverters()
        {
            AddConverter<bool?>(new CsvBooleanConverter());
            AddConverter<bool>(new CsvBooleanConverter());
            AddConverter<byte?>(new CsvByteConverter());
            AddConverter<byte>(new CsvByteConverter());
            AddConverter<char?>(new CsvCharConverter());
            AddConverter<char>(new CsvCharConverter());
            AddConverter<DateTime?>(new CsvDateTimeConverter());
            AddConverter<DateTime>(new CsvDateTimeConverter());
            AddConverter<DateTimeOffset?>(new CsvDateTimeOffsetConverter());
            AddConverter<DateTimeOffset>(new CsvDateTimeOffsetConverter());
            AddConverter<decimal?>(new CsvDecimalConverter());
            AddConverter<decimal>(new CsvDecimalConverter());
            AddConverter<double?>(new CsvDoubleConverter());
            AddConverter<double>(new CsvDoubleConverter());
            AddConverter<Guid?>(new CsvGuidConverter());
            AddConverter<Guid>(new CsvGuidConverter());
            AddConverter<int?>(new CsvInt32Converter());
            AddConverter<int>(new CsvInt32Converter());
            AddConverter<long?>(new CsvInt64Converter());
            AddConverter<long>(new CsvInt64Converter());
            AddConverter<sbyte?>(new CsvSByteConverter());
            AddConverter<sbyte>(new CsvSByteConverter());
            AddConverter<short?>(new CsvInt16Converter());
            AddConverter<short>(new CsvInt16Converter());
            AddConverter<string>(new CsvStringConverter());
            AddConverter<TimeSpan?>(new CsvTimeSpanConverter());
            AddConverter<TimeSpan>(new CsvTimeSpanConverter());
            AddConverter<uint?>(new CsvUInt32Converter());
            AddConverter<uint>(new CsvUInt32Converter());
            AddConverter<ulong?>(new CsvUInt64Converter());
            AddConverter<ulong>(new CsvUInt64Converter());
            AddConverter<Uri>(new CsvUriConverter());
            AddConverter<ushort?>(new CsvUInt16Converter());
            AddConverter<ushort>(new CsvUInt16Converter());
        }
    }
}