#region ENBREA.CSV - Copyright (C) 2023 STÜBER SYSTEMS GmbH
/*    
 *    ENBREA.CSV 
 *    
 *    Copyright (C) 2023 STÜBER SYSTEMS GmbH
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
        /// <param name="type">The type</param>
        /// <param name="converter">The converter instance</param>
        public void AddConverter(Type type, ICsvConverter converter)
        {
            _converters[type] = converter;
        }

        /// <summary>
        /// Gives back the value converter for a given type
        /// </summary>
        /// <param name="type">The type</param>
        /// <returns>The converter instance</returns>
        public ICsvConverter GetConverter(Type type)
        {
            var underlyingType = Nullable.GetUnderlyingType(type);

            if (underlyingType != null) type = underlyingType;

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
        /// Unregisters all value converters
        /// </summary>
        public void RemoveAllConverters()
        {
            _converters.Clear();
        }

        /// <summary>
        /// Unregisters the value converter for a given type
        /// </summary>
        /// <param name="type">The type</param>
        public void RemoveConverter(Type type)
        {
            _converters.Remove(type);
        }

        protected virtual void RegisterCustomConverters()
        {
        }

        protected virtual void RegisterDefaultConverters()
        {
            AddConverter(typeof(bool), new CsvBooleanConverter());
            AddConverter(typeof(byte), new CsvByteConverter());
            AddConverter(typeof(char), new CsvCharConverter());
            AddConverter(typeof(DateTime), new CsvDateTimeConverter());
            AddConverter(typeof(DateTimeOffset), new CsvDateTimeOffsetConverter());
            AddConverter(typeof(decimal), new CsvDecimalConverter());
            AddConverter(typeof(double), new CsvDoubleConverter());
            AddConverter(typeof(Guid), new CsvGuidConverter());
            AddConverter(typeof(int), new CsvInt32Converter());
            AddConverter(typeof(long), new CsvInt64Converter());
            AddConverter(typeof(sbyte), new CsvSByteConverter());
            AddConverter(typeof(short), new CsvInt16Converter());
            AddConverter(typeof(string), new CsvStringConverter());
            AddConverter(typeof(TimeSpan), new CsvTimeSpanConverter());
            AddConverter(typeof(uint), new CsvUInt32Converter());
            AddConverter(typeof(uint), new CsvUInt32Converter());
            AddConverter(typeof(ulong), new CsvUInt64Converter());
            AddConverter(typeof(ulong), new CsvUInt64Converter());
            AddConverter(typeof(Uri), new CsvUriConverter());
            AddConverter(typeof(ushort), new CsvUInt16Converter());
#if NET6_0_OR_GREATER
            AddConverter(typeof(DateOnly), new CsvDateOnlyConverter());
            AddConverter(typeof(TimeOnly), new CsvTimeOnlyConverter());
#endif
        }
    }
}