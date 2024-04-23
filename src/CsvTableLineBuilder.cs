#region ENBREA.CSV - Copyright (c) STÜBER SYSTEMS GmbH
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
using System.Linq.Expressions;

namespace Enbrea.Csv
{
    /// <summary>
    /// A CSV table builder which generates CSV data line by line
    /// </summary>
    public class CsvTableLineBuilder : CsvTableAccess
    {
        private readonly CsvLineBuilder _csvLineBuilder;
        private string[] _csvValues = [];

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvTableLineBuilder"/> class.
        /// </summary>
        public CsvTableLineBuilder()
            : base()
        {
            _csvLineBuilder = new();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvTableLineBuilder"/> class.
        /// </summary>
        /// <param name="configuration">Configuration parameters</param>
        public CsvTableLineBuilder(CsvConfiguration configuration)
            : base()
        {
            _csvLineBuilder = new(configuration);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvTableLineBuilder"/> class.
        /// </summary>
        /// <param name="headers">List of csv headers</param>
        public CsvTableLineBuilder(CsvHeaders headers)
            : base(headers)
        {
            _csvLineBuilder = new();
            Array.Resize(ref _csvValues, headers.Count);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvTableLineBuilder"/> class.
        /// </summary>
        /// <param name="configuration">Configuration parameters</param>
        /// <param name="headers">List of csv headers</param>
        public CsvTableLineBuilder(CsvConfiguration configuration, CsvHeaders headers)
            : base(headers)
        {
            _csvLineBuilder = new(configuration);
            Array.Resize(ref _csvValues, headers.Count);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvTableLineBuilder"/> class.
        /// </summary>
        /// <param name="headers">List of csv headers</param>
        public CsvTableLineBuilder(params string[] headers)
            : this(new CsvHeaders(headers))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvTableLineBuilder"/> class.
        /// </summary>
        /// <param name="configuration">Configuration parameters</param>
        /// <param name="headers">List of csv headers</param>
        public CsvTableLineBuilder(CsvConfiguration configuration, params string[] headers)
            : this(configuration, new CsvHeaders(headers))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvTableLineBuilder"/> class.
        /// </summary>
        /// <param name="headers">List of csv headers</param>
        public CsvTableLineBuilder(IList<string> headers)
            : this(new CsvHeaders(headers))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvTableLineBuilder"/> class.
        /// </summary>
        /// <param name="configuration">Configuration parameters</param>
        /// <param name="headers">List of csv headers</param>
        public CsvTableLineBuilder(CsvConfiguration configuration, IList<string> headers)
            : this(configuration, new CsvHeaders(headers))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvTableLineBuilder"/> class.
        /// </summary>
        /// <param name="converterResolver">Your own implementation of a value converter resolver</param>
        public CsvTableLineBuilder(ICsvConverterResolver converterResolver)
            : base(converterResolver)
        {
            _csvLineBuilder = new();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvTableLineBuilder"/> class.
        /// </summary>
        /// <param name="configuration">Configuration parameters</param>
        /// <param name="converterResolver">Your own implementation of a value converter resolver</param>
        public CsvTableLineBuilder(CsvConfiguration configuration, ICsvConverterResolver converterResolver)
            : base(converterResolver)
        {
            _csvLineBuilder = new(configuration);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvTableLineBuilder"/> class.
        /// </summary>
        /// <param name="converterResolver">Your own implementation of a value converter resolver</param>
        /// <param name="headers">List of csv headers</param>
        public CsvTableLineBuilder(ICsvConverterResolver converterResolver, CsvHeaders headers)
            : base(headers, converterResolver)
        {
            _csvLineBuilder = new();
            Array.Resize(ref _csvValues, headers.Count);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvTableLineBuilder"/> class.
        /// </summary>
        /// <param name="configuration">Configuration parameters</param>
        /// <param name="converterResolver">Your own implementation of a value converter resolver</param>
        /// <param name="headers">List of csv headers</param>
        public CsvTableLineBuilder(CsvConfiguration configuration, ICsvConverterResolver converterResolver, CsvHeaders headers)
            : base(headers, converterResolver)
        {
            _csvLineBuilder = new(configuration);
            Array.Resize(ref _csvValues, headers.Count);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvTableLineBuilder"/> class.
        /// </summary>
        /// <param name="converterResolver">Your own implementation of a value converter resolver</param>
        /// <param name="headers">List of csv headers</param>
        public CsvTableLineBuilder(ICsvConverterResolver converterResolver, params string[] headers)
            : this(converterResolver, new CsvHeaders(headers))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvTableLineBuilder"/> class.
        /// </summary>
        /// <param name="configuration">Configuration parameters</param>
        /// <param name="converterResolver">Your own implementation of a value converter resolver</param>
        /// <param name="headers">List of csv headers</param>
        public CsvTableLineBuilder(CsvConfiguration configuration, ICsvConverterResolver converterResolver, params string[] headers)
            : this(configuration, converterResolver, new CsvHeaders(headers))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvTableLineBuilder"/> class.
        /// </summary>
        /// <param name="converterResolver">Your own implementation of a value converter resolver</param>
        /// <param name="headers">List of csv headers</param>
        public CsvTableLineBuilder(ICsvConverterResolver converterResolver, IList<string> headers)
            : this(converterResolver, new CsvHeaders(headers))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvTableLineBuilder"/> class.
        /// </summary>
        /// <param name="configuration">Configuration parameters</param>
        /// <param name="converterResolver">Your own implementation of a value converter resolver</param>
        /// <param name="headers">List of csv headers</param>
        public CsvTableLineBuilder(CsvConfiguration configuration, ICsvConverterResolver converterResolver, IList<string> headers)
            : this(configuration, converterResolver, new CsvHeaders(headers))
        {
        }

        /// <summary>
        /// Configuration parameter
        /// </summary>
        public CsvConfiguration Configuration
        {
            get { return _csvLineBuilder.Configuration; }
        }

        /// <summary>
        /// Gets and sets the value of the current csv record at the specified index.
        /// </summary>
        /// <param name="i">Index of the value</param>
        /// <returns>A string value</returns>
        public string this[int i]
        {
            get
            {
                return _csvValues[i];
            }
            set
            {
                _csvValues[i] = value;
            }
        }

        /// <summary>
        /// Gets and sets the value of the current csv record at the posiiton of the specified header name.
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
            set
            {
                var i = Headers.IndexOf(x => x == name);
                if (i != -1)
                {
                    _csvValues[i] = value;
                }
                else
                {
                    throw new CsvHeaderNotFoundException($"CSV Header \"{name}\" not found");
                }
            }
        }

        /// <summary>
        /// Assigns the csv headers to the builder.
        /// </summary>
        /// <param name="csvHeaders">List of csv headers</param>
        /// <returns>
        /// A CSV formatted string if headers are available; otherwise null.
        /// </returns>
        public string AssignHeaders(IEnumerable<string> csvHeaders)
        {
            Headers.Replace(csvHeaders);
            Array.Resize(ref _csvValues, csvHeaders.Count());
            return HeadersToString();
        }

        /// <summary>
        /// Assigns the csv headers to the builder.
        /// </summary>
        /// <param name="csvHeaders">List of csv headers</param>
        /// <returns>
        /// A CSV formatted string if headers are available; otherwise null.
        /// </returns>
        public string AssignHeaders(params string[] csvHeaders)
        {
            return AssignHeaders((IEnumerable<string>)csvHeaders);
        }

        /// <summary>
        /// Assigns the csv headers to the builder.
        /// </summary>
        /// <param name="csvHeaders">List of csv headers as expression lambda</param>
        /// <returns>
        /// A CSV formatted string if headers are available; otherwise null.
        /// </returns>
        public void AssignHeaders<TEntity>(Expression<Func<TEntity, object>> csvHeaders)
        {
            AssignHeaders(new CsvHeaders<TEntity>(csvHeaders));
        }

        /// <summary>
        /// Sets all internal values to null.
        /// </summary>
        public void Clear()
        {
            Array.Fill(_csvValues, null);
        }

        /// <summary>
        /// Gives back the headers as CSV text line.
        /// </summary>
        /// <returns>
        /// A CSV formatted string if headers are available; otherwise null.
        /// </returns>
        public string HeadersToString()
        {
            int i = 0;
            foreach (var value in Headers)
            {
                SetValue(i, value);
                i++;
            }
            return ToString();
        }

        /// <summary>
        /// Sets the value of the current csv record at the posiiton of the specified header name.
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <param name="name">Name of a header</param>
        /// <param name="value">A value</param>
        public void SetValue<T>(string name, T value)
        {
            SetValue(name, value, ConverterResolver.GetConverter<T>());
        }

        /// <summary>
        /// Sets the value of the current csv record at the posiiton of the specified header name.
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <param name="name">Name of a header</param>
        /// <param name="value">A value</param>
        public void SetValue<T>(string name, T value, ICsvConverter valueConverter)
        {
            this[name] = valueConverter.ToString(value);
        }

        /// <summary>
        /// Sets the value of the current csv record at the specified index. 
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <param name="index">Index within the current csv record</param>
        /// <param name="value">A value</param>
        public void SetValue<T>(int index, T value)
        {
            SetValue(index, value, ConverterResolver.GetConverter<T>());
        }

        /// <summary>
        /// Sets the value of the current csv record at the specified index. 
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <param name="index">Index within the current csv record</param>
        /// <param name="value">A value</param>
        public void SetValue<T>(int index, T value, ICsvConverter valueConverter)
        {
            this[index] = valueConverter.ToString(value);
        }

        /// <summary>
        /// Sets the value of the current csv record at the position of the specified header name.
        /// </summary>
        /// <param name="type">The type</param>
        /// <param name="index">Index within the current csv record</param>
        /// <param name="value">A value</param>
        public void SetValue(Type type, string name, object value)
        {
            SetValue(name, value, ConverterResolver.GetConverter(type));
        }

        /// <summary>
        /// Sets the value of the current csv record at the specified index. 
        /// </summary>
        /// <param name="type">The type</param>
        /// <param name="index">Index within the current csv record</param>
        /// <param name="value">A value</param>
        public void SetValue(Type type, int index, object value)
        {
            SetValue(index, value, ConverterResolver.GetConverter(type));
        }

        /// <summary>
        /// Applies all relvant values from the strongly typed csv object to the current csv record.
        /// </summary>
        /// <param name="entity">Pointer to the strongly typed csv object instance</param>
        /// <returns>Number of values applied</returns>
        public int SetValues<TEntity>(TEntity entity)
        {
            int c = 0;
            foreach (var header in Headers)
            {
                if (CsvClassMapperResolverFactory.GetResolver().GetMapper<TEntity>().ContainsValue(header))
                {
                    var value = CsvClassMapperResolverFactory.GetResolver().GetMapper<TEntity>().GetValue(entity, header);
                    SetValue(CsvClassMapperResolverFactory.GetResolver().GetMapper<TEntity>().GetValueType(header), header, value);
                    c++;
                }
            }
            return c;
        }

        /// <summary>
        /// The current csv record as list of values.
        /// </summary>
        /// <returns>List of values</returns>
        public List<string> ToList()
        {
            return _csvValues.ToList();
        }

        /// <summary>
        /// Gives back the current csv record as csv text string.
        /// </summary>
        /// <returns>
        /// A CSV formatted string if values are available; otherwise null.
        /// </returns>
        public override string ToString()
        {
            return _csvLineBuilder.ToString(_csvValues);
        }

        /// <summary>
        /// Tries to set the value of the current csv record at the posiiton of the specified header name.
        /// </summary>
        /// <param name="name">Name of a header</param>
        /// <param name="value">A value</param>
        /// <returns>true if position within the current csv record was found; otherwise, false.</returns>
        public bool TrySetValue(string name, string value)
        {
            var i = Headers.IndexOf(x => x == name);
            if (i != -1)
            {
                return TrySetValue(i, value);
            }
            return false;
        }

        /// <summary>
        /// Tries to set the value of the current csv record at the specified index. 
        /// </summary>
        /// <param name="index">Index within the current csv record</param>
        /// <param name="value">A value</param>
        /// <returns>true if position within the current csv record was found; otherwise, false.</returns>
        public bool TrySetValue(int index, string value)
        {
            if (Enumerable.Range(0, Headers.Count).Contains(index))
            {
                _csvValues[index] = value;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Tries to set the value of the current csv record at the posiiton of the specified header name.
        /// </summary>
        /// <param name="name">Name of a header</param>
        /// <param name="value">A value</param>
        /// <returns>true if position within the current csv record was found; otherwise, false.</returns>
        public bool TrySetValue<T>(string name, T value)
        {
            var i = Headers.IndexOf(x => x == name);
            if (i != -1)
            {
                return TrySetValue(i, value);
            }
            return false;
        }

        /// <summary>
        /// Tries to set the value of the current csv record at the specified index. 
        /// </summary>
        /// <param name="index">Index within the current csv record</param>
        /// <param name="value">A value</param>
        /// <returns>true if position within the current csv record was found; otherwise, false.</returns>
        public bool TrySetValue<T>(int index, T value)
        {
            if (Enumerable.Range(0, Headers.Count).Contains(index))
            {
                SetValue(index, value);
                return true;
            }
            return false;
        }
    }
}
