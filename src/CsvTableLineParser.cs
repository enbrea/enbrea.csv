﻿#region ENBREA.CSV - Copyright (c) STÜBER SYSTEMS GmbH
/*    
 *    ENBREA.CSV 
 *    
 *    Copyright (c) STÜBER SYSTEMS GmbH
 *
 *    Licensed under the MIT License, Version 2.0. 
 */
#endregion

using System;
using System.Collections.Generic;
using System.Linq;

namespace Enbrea.Csv
{
    /// <summary>
    /// A CSV table parser which consumes CSV data line by line
    /// </summary>
    public class CsvTableLineParser : CsvTableAccess
    {
        private readonly CsvLineParser _csvLineParser;
        private readonly List<string> _csvValues = [];

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvTableLineParser"/> class.
        /// </summary>
        public CsvTableLineParser()
            : base()
        {
            _csvLineParser = new();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvTableLineParser"/> class.
        /// </summary>
        /// <param name="configuration">Configuration parameters</param>
        public CsvTableLineParser(CsvConfiguration configuration)
            : base()
        {
            _csvLineParser = new(configuration);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvTableLineParser"/> class.
        /// </summary>
        /// <param name="csvHeaders">List of csv headers</param>
        public CsvTableLineParser(CsvHeaders csvHeaders)
            : base(csvHeaders)
        {
            _csvLineParser = new();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvTableLineParser"/> class.
        /// </summary>
        /// <param name="configuration">Configuration parameters</param>
        /// <param name="headers">List of csv headers</param>
        public CsvTableLineParser(CsvConfiguration configuration, CsvHeaders headers)
            : base(headers)
        {
            _csvLineParser = new(configuration);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvTableLineParser"/> class.
        /// </summary>
        /// <param name="headers">List of csv headers</param>
        public CsvTableLineParser(params string[] headers)
            : this(new CsvHeaders(headers))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvTableLineParser"/> class.
        /// </summary>
        /// <param name="configuration">Configuration parameters</param>
        /// <param name="headers">List of csv headers</param>
        public CsvTableLineParser(CsvConfiguration configuration, params string[] headers)
            : this(configuration, new CsvHeaders(headers))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvTableLineParser"/> class.
        /// </summary>
        /// <param name="csvHeaders">List of csv headers</param>
        public CsvTableLineParser(IList<string> csvHeaders)
            : this(new CsvHeaders(csvHeaders))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvTableLineParser"/> class.
        /// </summary>
        /// <param name="configuration">Configuration parameters</param>
        /// <param name="headers">List of csv headers</param>
        public CsvTableLineParser(CsvConfiguration configuration, IList<string> headers)
            : this(configuration, new CsvHeaders(headers))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvTableLineParser"/> class.
        /// </summary>
        /// <param name="converterResolver">Your own implementation of a value converter resolver</param>
        public CsvTableLineParser(ICsvConverterResolver converterResolver)
            : base(converterResolver)
        {
            _csvLineParser = new();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvTableLineParser"/> class.
        /// </summary>
        /// <param name="configuration">Configuration parameters</param>
        /// <param name="converterResolver">Your own implementation of a value converter resolver</param>
        public CsvTableLineParser(CsvConfiguration configuration, ICsvConverterResolver converterResolver)
            : base(converterResolver)
        {
            _csvLineParser = new(configuration); 
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvTableLineParser"/> class.
        /// </summary>
        /// <param name="converterResolver">Your own implementation of a value converter resolver</param>
        /// <param name="headers">List of csv headers</param>
        public CsvTableLineParser(ICsvConverterResolver converterResolver, CsvHeaders headers)
            : base(headers, converterResolver)
        {
            _csvLineParser = new();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvTableLineParser"/> class.
        /// </summary>
        /// <param name="configuration">Configuration parameters</param>
        /// <param name="converterResolver">Your own implementation of a value converter resolver</param>
        /// <param name="headers">List of csv headers</param>
        public CsvTableLineParser(CsvConfiguration configuration, ICsvConverterResolver converterResolver, CsvHeaders headers)
            : base(headers, converterResolver)
        {
            _csvLineParser = new(configuration); 
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvTableLineParser"/> class.
        /// </summary>
        /// <param name="converterResolver">Your own implementation of a value converter resolver</param>
        /// <param name="headers">List of csv headers</param>
        public CsvTableLineParser(ICsvConverterResolver converterResolver, params string[] headers)
            : this(converterResolver, new CsvHeaders(headers))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvTableLineParser"/> class.
        /// </summary>
        /// <param name="configuration">Configuration parameters</param>
        /// <param name="converterResolver">Your own implementation of a value converter resolver</param>
        /// <param name="headers">List of csv headers</param>
        public CsvTableLineParser(CsvConfiguration configuration, ICsvConverterResolver converterResolver, params string[] headers)
            : this(configuration, converterResolver, new CsvHeaders(headers))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvTableLineParser"/> class.
        /// </summary>
        /// <param name="converterResolver">Your own implementation of a value converter resolver</param>
        /// <param name="headers">List of csv headers</param>
        public CsvTableLineParser(ICsvConverterResolver converterResolver, IList<string> headers)
            : this(converterResolver, new CsvHeaders(headers))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvTableLineParser"/> class.
        /// </summary>
        /// <param name="configuration">Configuration parameters</param>
        /// <param name="converterResolver">Your own implementation of a value converter resolver</param>
        /// <param name="headers">List of csv headers</param>
        public CsvTableLineParser(CsvConfiguration configuration, ICsvConverterResolver converterResolver, IList<string> headers)
            : this(configuration, converterResolver, new CsvHeaders(headers))
        {
        }

        /// <summary>
        /// Configuration parameter
        /// </summary>
        public CsvConfiguration Configuration
        {
            get { return _csvLineParser.Configuration; }
        }

        /// <summary>
        /// Gets the value of the current csv record at the specified index.
        /// </summary>
        /// <param name="i">Index of the value</param>
        /// <returns>A string value</returns>
        public string this[int i]
        {
            get
            {
                return _csvValues[i];
            }
        }

        /// <summary>
        /// Gets the value of the current csv record at the posiiton of the specified header name.
        /// </summary>
        /// <param name="name">Name of the csv header</param>
        /// <returns>A string value</returns>
        public string this[string name]
        {
            get
            {
                var i = Headers.IndexOf(x => x == name);
                if (i != -1)
                {
                    return _csvValues[i];
                }
                else
                {
                    throw new CsvHeaderNotFoundException($"CSV Header \"{name}\" not found"); 
                }
            }
        }

        /// <summary>
        /// Creates a new strongly types csv opbject and tries to assign all values from the current csv record. 
        /// </summary>
        /// <returns>Pointer to the newly crfeated strongly typed csv object instance</returns>
        public TEntity GetEntity<TEntity>()
        {
            var record = Activator.CreateInstance<TEntity>();
            GetEntity(record);
            return record;
        }

        /// <summary>
        /// Tries to assign all values from the current csv record to the strongly types csv object. 
        /// </summary>
        /// <param name="entity">Pointer to the strongly typed csv object instance</param>
        /// <returns>Number of assigned values</returns>
        public int GetEntity<TEntity>(TEntity entity)
        {
            int c = 0;
            foreach (var header in Headers)
            {
                if (CsvClassMapperResolverFactory.GetResolver().GetMapper<TEntity>().ContainsValue(header))
                {
                    if (TryGetValue(CsvClassMapperResolverFactory.GetResolver().GetMapper<TEntity>().GetValueType(header), header, out var value))
                    {
                        CsvClassMapperResolverFactory.GetResolver().GetMapper<TEntity>().SetValue(entity, header, value);
                        c++;
                    }
                }
            }
            return c;
        }

        /// <summary>
        /// Reads the value of the current csv record at the posiiton of the specified header name. 
        /// </summary>
        /// <param name="name">Name of a header</param>
        /// <returns>A string value</returns>
        public string GetValue(string name)
        {
            return this[name];
        }

        /// <summary>
        /// Read the value of the current csv record at the specified index. 
        /// </summary>
        /// <param name="index">Index within the current csv record</param>
        /// <returns>A string value</returns>
        public string GetValue(int index)
        {
            return this[index];
        }

        /// <summary>
        /// Reads the typed value of the current csv record at the posiiton of the specified header name. 
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <param name="name">Name of a header</param>
        /// <returns>A typed value</returns>
        public T GetValue<T>(string name)
        {
            return GetValue<T>(name, ConverterResolver.GetConverter<T>());
        }

        /// <summary>
        /// Reads the typed value of the current csv record at the posiiton of the specified header name. 
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <param name="name">Name of a header</param>
        /// <param name="converter">Instance of <see cref="ICsvConverter""></param>
        /// <returns>A typed value</returns>
        public T GetValue<T>(string name, ICsvConverter converter)
        {
            return (T)GetValue(name, converter);
        }

        /// <summary>
        /// Read the typed value of the current csv record at the specified index. 
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <param name="index">Index within the current csv record</param>
        /// <returns>A typed value</returns>
        public T GetValue<T>(int index)
        {
            return GetValue<T>(index, ConverterResolver.GetConverter<T>());
        }

        /// <summary>
        /// Read the typed value of the current csv record at the specified index. 
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <param name="index">Index within the current csv record</param>
        /// <param name="converter">Instance of <see cref="ICsvConverter"> to be used</param>
        /// <returns>A typed value</returns>
        public T GetValue<T>(int index, ICsvConverter converter)
        {
            return (T)GetValue(index, converter);
        }

        /// <summary>
        /// Reads the typed value of the current csv record at the posiiton of the specified header name. 
        /// </summary>
        /// <param name="name">Name of a header</param>
        /// <returns>A typed value</returns>
        public object GetValue(Type type, string name)
        {
            return GetValue(name, ConverterResolver.GetConverter(type));
        }

        /// <summary>
        /// Reads the typed value of the current csv record at the posiiton of the specified header name. 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="converter">Instance of <see cref="ICsvConverter"> to be used</param>
        /// <returns></returns>
        public object GetValue(string name, ICsvConverter converter)
        {
            return converter.FromString(this[name]);
        }

        /// <summary>
        /// Read the value of the current csv record at the specified index. 
        /// </summary>
        /// <param name="index">Index within the current csv record</param>
        /// <returns>A string value</returns>
        public object GetValue(Type type, int index)
        {
            return GetValue(index, ConverterResolver.GetConverter(type));
        }

        /// <summary>
        /// Read the typed value of the current csv record at the specified index. 
        /// </summary>
        /// <param name="index">Index within the current csv record</param>
        /// <param name="converter">Instance of <see cref="ICsvConverter"> to be used</param>
        /// <returns></returns>
        public object GetValue(int index, ICsvConverter converter)
        {
            return converter.FromString(this[index]);
        }

        /// <summary>
        /// Parses a CSV text line and stores the values
        /// </summary>
        /// <param name="line">A CSV text string</param>
        /// <returns>
        /// Number of values of the current record
        /// </returns>
        public int Parse(string line)
        {
            _csvValues.Clear();
            return _csvLineParser.Parse(line, (i, s) => { _csvValues.Add(s); });
        }

        /// <summary>
        /// Parses a CSV text line and stores the values as headers.
        /// </summary>
        /// <param name="line">A CSV text string</param>
        /// <returns>
        /// Number of headers
        /// </returns>
        public int ParseHeaders(string line)
        {
            Headers.Clear();
            return _csvLineParser.Parse(line, (i, s) => { Headers.Add(s); });
        }

        /// <summary>
        /// Parses a CSV text line and stores the values as headers.
        /// </summary>
        /// <param name="line">A CSV text string</param>
        /// <param name="transform">Method which is called for every value, which gives you 
        /// the possiblity to change the header name</param>
        /// <returns>
        /// Number of headers
        /// </returns>
        public int ParseHeaders(string line, Func<string, string> transform)
        {
            Headers.Clear();
            return _csvLineParser.Parse(line, (i, s) => Headers.Add(transform(s)));
        }

        /// <summary>
        /// Tries to read the value of the current csv record at the posiiton of the specified header name. 
        /// </summary>
        /// <param name="name">Name of a header</param>
        /// <param name="value">If position within the cuurent csv record was found, contains the value. If not contains null</param>
        /// <returns>true if position within the cuurent csv record was found; otherwise, false.</returns>
        public bool ParseHeaders(string name, out string value)
        {
            var i = Headers.IndexOf(x => x.Equals(name, StringComparison.InvariantCultureIgnoreCase));
            if (i != -1)
            {
                return TryGetValue(i, out value);
            }
            value = null;
            return false;
        }

        /// <summary>
        /// The current csv record as list of values
        /// </summary>
        /// <returns>List of values</returns>
        public List<string> ToList()
        {
            return _csvValues.ToList();
        }

        /// <summary>
        /// Tries to read the value of the current csv record at the posiiton of the specified header name. 
        /// </summary>
        /// <param name="name">Name of a header</param>
        /// <param name="value">If position within the cuurent csv record was found, contains the value. If not contains null</param>
        /// <returns>true if position within the cuurent csv record was found; otherwise, false.</returns>
        public bool TryGetValue(string name, out string value)
        {
            var i = Headers.IndexOf(x => x.Equals(name, StringComparison.InvariantCultureIgnoreCase));
            if (i != -1)
            {
                return TryGetValue(i, out value);
            }
            value = null;
            return false;
        }

        /// <summary>
        /// Tries to read the typed value of the current csv record at the posiiton of the specified header name. 
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <param name="name">Name of a header</param>
        /// <param name="value">If position within the cuurent csv record was found, contains the value. If not contains null</param>
        /// <returns>true if position within the cuurent csv record was found; otherwise, false.</returns>
        public bool TryGetValue<T>(string name, out T value)
        {
            var i = Headers.IndexOf(x => x.Equals(name, StringComparison.InvariantCultureIgnoreCase));
            if (i != -1)
            {
                return TryGetValue<T>(i, out value);
            }
            value = default;
            return false;
        }

        /// <summary>
        /// Tries to read the typed value of the current csv record at the posiiton of the specified header name. 
        /// </summary>
        /// <param name="type">Type of value</param>
        /// <param name="name">Name of a header</param>
        /// <param name="value">If position within the cuurent csv record was found, contains the value. If not contains null</param>
        /// <returns>true if position within the cuurent csv record was found; otherwise, false.</returns>
        public bool TryGetValue(Type type, string name, out object value)
        {
            var i = Headers.IndexOf(x => x.Equals(name, StringComparison.InvariantCultureIgnoreCase));
            if (i != -1)
            {
                return TryGetValue(type, i, out value);
            }
            value = default;
            return false;
        }

        /// <summary>
        /// Tries to read the value of the current csv record at the specified index. 
        /// </summary>
        /// <param name="index">Index within the current csv record</param>
        /// <param name="value">If position within the current csv record was found, contains the value. If not contains null</param>
        /// <returns>true if position within the current csv record was found; otherwise, false.</returns>
        public bool TryGetValue(int index, out string value)
        {
            if (Enumerable.Range(0, _csvValues.Count).Contains(index))
            {
                value = _csvValues[index];
                return true;
            }
            value = null;
            return false;
        }

        /// <summary>
        /// Tries to read the typed value of the current csv record at the specified index. 
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <param name="index">Index within the current csv record</param>
        /// <param name="value">If position within the current csv record was found, contains the value. If not contains null</param>
        /// <returns>true if position within the current csv record was found; otherwise, false.</returns>
        public bool TryGetValue<T>(int index, out T value)
        {
            if (Enumerable.Range(0, _csvValues.Count).Contains(index))
            {
                value = GetValue<T>(index);
                return true;
            }
            value = default;
            return false;
        }

        /// <summary>
        /// Tries to read the typed value of the current csv record at the specified index. 
        /// </summary>
        /// <param name="type">Type of value</param>
        /// <param name="index">Index within the current csv record</param>
        /// <param name="value">If position within the current csv record was found, contains the value. If not contains null</param>
        /// <returns>true if position within the current csv record was found; otherwise, false.</returns>
        public bool TryGetValue(Type type, int index, out object value)
        {
            if (Enumerable.Range(0, _csvValues.Count).Contains(index))
            {
                value = GetValue(type, index);
                return true;
            }
            value = default;
            return false;
        }
    }
}
