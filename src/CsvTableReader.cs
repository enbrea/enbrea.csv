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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Enbrea.Csv
{
    /// <summary>
    /// A CSV table reader which consumes CSV data as stream
    /// </summary>
    public class CsvTableReader : CsvTableAccess
    {
        private readonly CsvReader _csvReader;
        private readonly List<string> _csvValues = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvTableReader"/> class.
        /// </summary>
        /// <param name="textReader">The text reader to be used.</param>
        public CsvTableReader(TextReader textReader)
            : base()
        {
            _csvReader = new CsvReader(textReader);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvTableReader"/> class.
        /// </summary>
        /// <param name="textReader">The text reader to be used.</param>
        /// <param name="configuration">Configuration parameters</param>
        public CsvTableReader(TextReader textReader, CsvConfiguration configuration)
            : base()
        {
            _csvReader = new CsvReader(textReader, configuration);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvTableReader"/> class.
        /// </summary>
        /// <param name="textReader">The text reader to be used.</param>
        /// <param name="headers">List of csv headers</param>
        public CsvTableReader(TextReader textReader, CsvHeaders headers)
            : base(headers)
        {
            _csvReader = new CsvReader(textReader);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvTableReader"/> class.
        /// </summary>
        /// <param name="textReader">The text reader to be used.</param>
        /// <param name="configuration">Configuration parameters</param>
        /// <param name="headers">List of csv headers</param>
        public CsvTableReader(TextReader textReader, CsvConfiguration configuration, CsvHeaders headers)
            : base(headers)
        {
            _csvReader = new CsvReader(textReader, configuration);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvTableReader"/> class.
        /// </summary>
        /// <param name="textReader">The text reader to be used.</param>
        /// <param name="headers">List of csv headers</param>
        public CsvTableReader(TextReader textReader, params string[] headers)
            : this(textReader, new CsvHeaders(headers))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvTableReader"/> class.
        /// </summary>
        /// <param name="textReader">The text reader to be used.</param>
        /// <param name="configuration">Configuration parameters</param>
        /// <param name="headers">List of csv headers</param>
        public CsvTableReader(TextReader textReader, CsvConfiguration configuration, params string[] headers)
            : this(textReader, configuration, new CsvHeaders(headers))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvTableReader"/> class.
        /// </summary>
        /// <param name="textReader">The text reader to be used.</param>
        /// <param name="headers">List of csv headers</param>
        public CsvTableReader(TextReader textReader, IList<string> headers)
            : this(textReader, new CsvHeaders(headers))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvTableReader"/> class.
        /// </summary>
        /// <param name="textReader">The text reader to be used.</param>
        /// <param name="configuration">Configuration parameters</param>
        /// <param name="headers">List of csv headers</param>
        public CsvTableReader(TextReader textReader, CsvConfiguration configuration, IList<string> headers)
            : this(textReader, configuration, new CsvHeaders(headers))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvTableReader"/> class.
        /// </summary>
        /// <param name="textReader">The text reader to be used.</param>
        /// <param name="converterResolver">Your own implementation of a value converter resolver</param>
        public CsvTableReader(TextReader textReader, ICsvConverterResolver converterResolver)
            : base(converterResolver)
        {
            _csvReader = new CsvReader(textReader);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvTableReader"/> class.
        /// </summary>
        /// <param name="textReader">The text reader to be used.</param>
        /// <param name="configuration">Configuration parameters</param>
        /// <param name="converterResolver">Your own implementation of a value converter resolver</param>
        public CsvTableReader(TextReader textReader, CsvConfiguration configuration, ICsvConverterResolver converterResolver)
            : base(converterResolver)
        {
            _csvReader = new CsvReader(textReader, configuration);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvTableReader"/> class.
        /// </summary>
        /// <param name="textReader">The text reader to be used.</param>
        /// <param name="converterResolver">Your own implementation of a value converter resolver</param>
        /// <param name="headers">List of csv headers</param>
        public CsvTableReader(TextReader textReader, ICsvConverterResolver converterResolver, CsvHeaders headers)
            : base(headers, converterResolver)
        {
            _csvReader = new CsvReader(textReader);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvTableReader"/> class.
        /// </summary>
        /// <param name="textReader">The text reader to be used.</param>
        /// <param name="configuration">Configuration parameters</param>
        /// <param name="converterResolver">Your own implementation of a value converter resolver</param>
        /// <param name="headers">List of csv headers</param>
        public CsvTableReader(TextReader textReader, CsvConfiguration configuration, ICsvConverterResolver converterResolver, CsvHeaders headers)
            : base(headers, converterResolver)
        {
            _csvReader = new CsvReader(textReader, configuration);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvTableReader"/> class.
        /// </summary>
        /// <param name="textReader">The text reader to be used.</param>
        /// <param name="converterResolver">Your own implementation of a value converter resolver</param>
        /// <param name="headers">List of csv headers</param>
        public CsvTableReader(TextReader textReader, ICsvConverterResolver converterResolver, params string[] headers)
            : this(textReader, converterResolver, new CsvHeaders(headers))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvTableReader"/> class.
        /// </summary>
        /// <param name="textReader">The text reader to be used.</param>
        /// <param name="configuration">Configuration parameters</param>
        /// <param name="converterResolver">Your own implementation of a value converter resolver</param>
        /// <param name="headers">List of csv headers</param>
        public CsvTableReader(TextReader textReader, CsvConfiguration configuration, ICsvConverterResolver converterResolver, params string[] headers)
            : this(textReader, configuration, converterResolver, new CsvHeaders(headers))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvTableReader"/> class.
        /// </summary>
        /// <param name="textReader">The text reader to be used.</param>
        /// <param name="converterResolver">Your own implementation of a value converter resolver</param>
        /// <param name="headers">List of csv headers</param>
        public CsvTableReader(TextReader textReader, ICsvConverterResolver converterResolver, IList<string> headers)
            : this(textReader, converterResolver, new CsvHeaders(headers))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvTableReader"/> class.
        /// </summary>
        /// <param name="textReader">The text reader to be used.</param>
        /// <param name="configuration">Configuration parameters</param>
        /// <param name="converterResolver">Your own implementation of a value converter resolver</param>
        /// <param name="headers">List of csv headers</param>
        public CsvTableReader(TextReader textReader, CsvConfiguration configuration, ICsvConverterResolver converterResolver, IList<string> headers)
            : this(textReader, configuration, converterResolver, new CsvHeaders(headers))
        {
        }

        /// <summary>
        /// Configuration parameter
        /// </summary>
        public CsvConfiguration Configuration
        {
            get { return _csvReader.Configuration; }
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
                if (i < _csvValues.Count)
                {
                    return _csvValues[i];
                }
                else
                {
                    throw new CsvValueNotFoundException($"CSV value at index {i} not found");
                }
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
                    if (i < _csvValues.Count)
                    {
                        return _csvValues[i];
                    }
                    else
                    {
                        throw new CsvValueNotFoundException($"CSV value for \"{name}\" not found");
                    }
                }
                else
                {
                    throw new CsvHeaderNotFoundException($"CSV header \"{name}\" not found"); 
                }
            }
        }

        /// <summary>
        /// Creates a new custom csv object and tries to assign all values from the current csv record. 
        /// </summary>
        /// <returns>Pointer to the newly created custom csv object instance</returns>
        public TEntity Get<TEntity>()
        {
            var entity = Activator.CreateInstance<TEntity>();
            GetValues(entity);
            return entity;       
        }

        /// <summary>
        /// Reads the typed value of the current csv record at the specified index. 
        /// </summary>
        /// <param name="index">Index within the current csv record</param>
        /// <param name="converter">The value converter</param>
        /// <returns>A typed value</returns>
        public object GetValue(int index, ICsvConverter converter)
        {
            return converter.FromString(this[index]);
        }

        /// <summary>
        /// Reads the typed value of the current csv record at the posiiton of the specified header name. 
        /// </summary>
        /// <param name="name">Name of a header</param>
        /// <param name="converter">The value converter</param>
        /// <returns>A typed value</returns>
        public object GetValue(string name, ICsvConverter converter)
        {
            return converter.FromString(this[name]);
        }

        /// <summary>
        /// Reads the typed value of the current csv record at the specified index. 
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <param name="index">Index within the current csv record</param>
        /// <returns>A typed value</returns>
        public T GetValue<T>(int index)
        {
            return GetValue<T>(index, ConverterResolver.GetConverter<T>());
        }

        /// <summary>
        /// Reads the typed value of the current csv record at the specified index. 
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <param name="index">Index within the current csv record</param>
        /// <param name="converter">INstance of value converter to be used</param>
        /// <returns>A typed value</returns>
        public T GetValue<T>(int index, ICsvConverter converter)
        {
            return (T)GetValue(index, converter);
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
        /// <returns>A typed value</returns>
        public T GetValue<T>(string name, ICsvConverter converter)
        {
            return (T)GetValue(name, converter);
        }

        /// <summary>
        /// Read the value of the current csv record at the specified index. 
        /// </summary>
        /// <param name="type">Type of value</param>
        /// <param name="index">Index within the current csv record</param>
        /// <returns>A string value</returns>
        public object GetValue(Type type, int index)
        {
            return GetValue(index, ConverterResolver.GetConverter(type));
        }

        /// <summary>
        /// Reads the typed value of the current csv record at the posiiton of the specified header name. 
        /// </summary>
        /// <param name="type">Type of value</param>
        /// <param name="name">Name of a header</param>
        /// <returns>A typed value</returns>
        public object GetValue(Type type, string name)
        {
            return GetValue(name, ConverterResolver.GetConverter(type));
        }

        /// <summary>
        /// Tries to assign all values from the current csv record to a custom csv object. 
        /// </summary>
        /// <param name="entity">Pointer to the custom csv object instance</param>
        /// <returns>Number of assigned values</returns>
        public int GetValues<TEntity>(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

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
        /// Gives back whether the value of the current csv record at the specified index is empty. 
        /// </summary>
        /// <param name="index">Index within the current csv record</param>
        /// <returns>TRUE, value is empty</returns>
        public bool IsEmpty(int index)
        {
            return string.IsNullOrEmpty(this[index]);
        }

        /// <summary>
        /// Gives back whether the value of the current csv record at the posiiton of the specified header name is empty. 
        /// </summary>
        /// <param name="name">Name of a header</param>
        /// <returns>TRUE, value is empty</returns>
        public bool IsEmpty(string name)
        {
            return string.IsNullOrEmpty(this[name]);
        }

        /// <summary>
        /// Reads the next csv record out of the stream.
        /// </summary>
        /// <returns>
        /// Number of values of the current record
        /// </returns>
        public int Read()
        {
            _csvValues.Clear();
            return _csvReader.ReadLine((i, s) => { _csvValues.Add(s); });
        }

        /// <summary>
        /// Reads the next csv record out of the stream.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The value of the TResult
        /// parameter contains the number of values of the current record.</returns>
        public async Task<int> ReadAsync()
        {
            _csvValues.Clear();
            return await _csvReader.ReadLineAsync((i, s) => { _csvValues.Add(s); }).ConfigureAwait(false);
        }

        /// <summary>
        /// Reads the next csv record out of the stream and stores the values as headers.
        /// </summary>
        /// <returns>
        /// Number of headers
        /// </returns>
        public int ReadHeaders()
        {
            Headers.Clear();
            return _csvReader.ReadLine((i, s) => Headers.Add(s));
        }

        /// <summary>
        /// Reads the next csv record out of the stream and stores the values as headers.
        /// </summary>
        /// <param name="transform">Method which is called for every value, which gives you 
        /// the possiblity to change the header name</param>
        /// <returns>
        /// Number of headers
        /// </returns>
        public int ReadHeaders(Func<string, string> transform)
        {
            Headers.Clear();
            return _csvReader.ReadLine((i, s) => Headers.Add(transform(s)));
        }

        /// <summary>
        /// Reads the next csv record out of the stream and stores the values as headers.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The value of the TResult
        /// parameter contains the number of headers.</returns>
        public async Task<int> ReadHeadersAsync()
        {
            Headers.Clear();
            return await _csvReader.ReadLineAsync((i, s) => Headers.Add(s)).ConfigureAwait(false);
        }

        /// <summary>
        /// Reads the next csv record out of the stream and stores the values as headers.
        /// </summary>
        /// <param name="transform">Method which is called for every value, which gives you 
        /// the possiblity to change the header name</param>
        /// <returns>A task that represents the asynchronous operation. The value of the TResult
        /// parameter contains the number of headers.</returns>
        /// <summary>
        public async Task<int> ReadHeadersAsync(Func<string, string> transform)
        {
            Headers.Clear();
            return await _csvReader.ReadLineAsync((i, s) => Headers.Add(transform(s))).ConfigureAwait(false);
        }

        /// <summary>
        /// Bypasses a specified number of csv record in the stream.
        /// </summary>
        /// <param name="count">The number of csv records to skip</param>
        public void Skip(uint count = 1)
        {
            _csvValues.Clear();
            for (int c = 0; c < count; c++)
            {
                _csvReader.ReadLine((i, s) => {});
            }
        }

        /// <summary>
        /// Bypasses a specified number of csv record in the stream.
        /// </summary>
        /// <param name="count">The number of csv records to skip</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task SkipAsync(uint count = 1)
        {
            _csvValues.Clear();
            for (int c = 0; c < count; c++)
            {
                await _csvReader.ReadLineAsync((i, s) => {}).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// The current csv record as list of values
        /// </summary>
        /// <returns>List of values</returns>
        public List<string> ToList()
        {
            return _csvValues;
        }


        /// <summary>
        /// Gives back the current csv line as string
        /// </summary>
        /// <returns>Current csv line</returns>
        public override string ToString()
        {
            var csvLineBuilder = new CsvLineBuilder(Configuration);
            ToList().ForEach(x => csvLineBuilder.Append(x));
            return csvLineBuilder.ToString();
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

        /// <summary>
        /// Tries to read the value of the current csv record at the posiiton of the specified header name. 
        /// The value is used as param for the specified delegate. 
        /// </summary>
        /// <param name="name">Name of a header</param>
        /// <param name="useAction">Method which is called if a value could be read</param>
        public void UseValue(string name, Action<string> useAction)
        {
            if (useAction == null)
            {
                throw new ArgumentNullException(nameof(useAction));
            }

            if (TryGetValue(name, out string value))
            {
                useAction(value);
            }
        }

        /// <summary>
        /// Tries to read the typed value of the current csv record at the posiiton of the specified header name. 
        /// The value is used as param for the specified delegate. 
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <param name="name">Name of a header</param>
        /// <param name="useAction">Method which is called if a value could be read</param>
        public void UseValue<T>(string name, Action<T> useAction)
        {
            if (useAction == null)
            {
                throw new ArgumentNullException(nameof(useAction));
            }

            if (TryGetValue(name, out T value))
            {
                useAction(value);
            }
        }

        /// <summary>
        /// Tries to read the value of the current csv record at the specified index. 
        /// The value is used as param for the specified delegate. 
        /// </summary>
        /// <param name="index">Index of a header</param>
        /// <param name="useAction">Method which is called if a value could be read</param>
        public void UseValue(int index, Action<string> useAction)
        {
            if (useAction == null)
            {
                throw new ArgumentNullException(nameof(useAction));
            }

            if (TryGetValue(index, out string value))
            {
                useAction(value);
            }
        }

        /// <summary>
        /// Tries to read the typed value of the current csv record at the specified index. 
        /// The value is used as param for the specified delegate. 
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <param name="index">Index of a header</param>
        /// <param name="useAction">Method which is called if a value could be read</param>
        /// <summary>
        public void UseValue<T>(int index, Action<T> useAction)
        {
            if (useAction == null)
            {
                throw new ArgumentNullException(nameof(useAction));
            }

            if (TryGetValue(index, out T value))
            {
                useAction(value);
            }
        }
    }
}
