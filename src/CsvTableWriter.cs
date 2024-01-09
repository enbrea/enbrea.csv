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
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Enbrea.Csv
{
    /// <summary>
    /// A CSV table writer which generates CSV data as stream
    /// </summary>
    public class CsvTableWriter : CsvTableAccess
    {
        private readonly CsvWriter _csvWriter;
        private string[] _csvValues = Array.Empty<string>();
        private bool _wasPreviousWrite = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvTableWriter"/> class.
        /// </summary>
        /// <param name="textWriter">The text writer to be used.</param>
        public CsvTableWriter(TextWriter textWriter)
            : base()
        {
            _csvWriter = new CsvWriter(textWriter);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvTableWriter"/> class.
        /// </summary>
        /// <param name="textWriter">The text writer to be used.</param>
        /// <param name="configuration">Configuration parameters</param>
        public CsvTableWriter(TextWriter textWriter, CsvConfiguration configuration)
            : base()
        {
            _csvWriter = new CsvWriter(textWriter, configuration);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvTableWriter"/> class.
        /// </summary>
        /// <param name="textWriter">The text writer to be used.</param>
        /// <param name="headers">List of csv headers</param>
        public CsvTableWriter(TextWriter textWriter, CsvHeaders headers)
            : base(headers)
        {
            _csvWriter = new CsvWriter(textWriter);
            Array.Resize(ref _csvValues, headers.Count);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvTableWriter"/> class.
        /// </summary>
        /// <param name="textWriter">The text writer to be used.</param>
        /// <param name="configuration">Configuration parameters</param>
        /// <param name="headers">List of csv headers</param>
        public CsvTableWriter(TextWriter textWriter, CsvConfiguration configuration, CsvHeaders headers)
            : base(headers)
        {
            _csvWriter = new CsvWriter(textWriter, configuration);
            Array.Resize(ref _csvValues, headers.Count);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvTableWriter"/> class.
        /// </summary>
        /// <param name="textWriter">The text writer to be used.</param>
        /// <param name="headers">List of csv headers</param>
        public CsvTableWriter(TextWriter textWriter, params string[] headers)
            : this(textWriter, new CsvHeaders(headers))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvTableWriter"/> class.
        /// </summary>
        /// <param name="textWriter">The text writer to be used.</param>
        /// <param name="configuration">Configuration parameters</param>
        /// <param name="headers">List of csv headers</param>
        public CsvTableWriter(TextWriter textWriter, CsvConfiguration configuration, params string[] headers)
            : this(textWriter, configuration, new CsvHeaders(headers))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvTableWriter"/> class.
        /// </summary>
        /// <param name="textWriter">The text writer to be used.</param>
        /// <param name="csvHeaders">List of csv headers</param>
        public CsvTableWriter(TextWriter textWriter, IList<string> csvHeaders)
            : this(textWriter, new CsvHeaders(csvHeaders))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvTableWriter"/> class.
        /// </summary>
        /// <param name="textWriter">The text writer to be used.</param>
        /// <param name="configuration">Configuration parameters</param>
        /// <param name="headers">List of csv headers</param>
        public CsvTableWriter(TextWriter textWriter, CsvConfiguration configuration, IList<string> headers)
            : this(textWriter, configuration, new CsvHeaders(headers))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvTableWriter"/> class.
        /// </summary>
        /// <param name="textWriter">The text writer to be used.</param>
        /// <param name="converterResolver">Your own implementation of a value converter resolver</param>
        public CsvTableWriter(TextWriter textWriter, ICsvConverterResolver converterResolver)
            : base(converterResolver)
        {
            _csvWriter = new CsvWriter(textWriter);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvTableWriter"/> class.
        /// </summary>
        /// <param name="textWriter">The text writer to be used.</param>
        /// <param name="configuration">Configuration parameters</param>
        /// <param name="converterResolver">Your own implementation of a value converter resolver</param>
        public CsvTableWriter(TextWriter textWriter, CsvConfiguration configuration, ICsvConverterResolver converterResolver)
            : base(converterResolver)
        {
            _csvWriter = new CsvWriter(textWriter, configuration);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvTableWriter"/> class.
        /// </summary>
        /// <param name="textWriter">The text writer to be used.</param>
        /// <param name="converterResolver">Your own implementation of a value converter resolver</param>
        /// <param name="headers">List of csv headers</param>
        public CsvTableWriter(TextWriter textWriter, ICsvConverterResolver converterResolver, CsvHeaders headers)
            : base(headers, converterResolver)
        {
            _csvWriter = new CsvWriter(textWriter);
            Array.Resize(ref _csvValues, headers.Count);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvTableWriter"/> class.
        /// </summary>
        /// <param name="textWriter">The text writer to be used.</param>
        /// <param name="configuration">Configuration parameters</param>
        /// <param name="converterResolver">Your own implementation of a value converter resolver</param>
        /// <param name="headers">List of csv headers</param>
        public CsvTableWriter(TextWriter textWriter, CsvConfiguration configuration, ICsvConverterResolver converterResolver, CsvHeaders headers)
            : base(headers, converterResolver)
        {
            _csvWriter = new CsvWriter(textWriter, configuration);
            Array.Resize(ref _csvValues, headers.Count);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvTableWriter"/> class.
        /// </summary>
        /// <param name="textWriter">The text writer to be used.</param>
        /// <param name="converterResolver">Your own implementation of a value converter resolver</param>
        /// <param name="headers">List of csv headers</param>
        public CsvTableWriter(TextWriter textWriter, ICsvConverterResolver converterResolver, params string[] headers)
            : this(textWriter, converterResolver, new CsvHeaders(headers))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvTableWriter"/> class.
        /// </summary>
        /// <param name="textWriter">The text writer to be used.</param>
        /// <param name="configuration">Configuration parameters</param>
        /// <param name="converterResolver">Your own implementation of a value converter resolver</param>
        /// <param name="headers">List of csv headers</param>
        public CsvTableWriter(TextWriter textWriter, CsvConfiguration configuration, ICsvConverterResolver converterResolver, params string[] headers)
            : this(textWriter, configuration, converterResolver, new CsvHeaders(headers))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvTableWriter"/> class.
        /// </summary>
        /// <param name="textWriter">The text writer to be used.</param>
        /// <param name="converterResolver">Your own implementation of a value converter resolver</param>
        /// <param name="headers">List of csv headers</param>
        public CsvTableWriter(TextWriter textWriter, ICsvConverterResolver converterResolver, IList<string> headers)
            : this(textWriter, converterResolver, new CsvHeaders(headers))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvTableWriter"/> class.
        /// </summary>
        /// <param name="textWriter">The text writer to be used.</param>
        /// <param name="configuration">Configuration parameters</param>
        /// <param name="converterResolver">Your own implementation of a value converter resolver</param>
        /// <param name="headers">List of csv headers</param>
        public CsvTableWriter(TextWriter textWriter, CsvConfiguration configuration, ICsvConverterResolver converterResolver, IList<string> headers)
            : this(textWriter, configuration, converterResolver, new CsvHeaders(headers))
        {
        }

        /// <summary>
        /// Configuration parameter
        /// </summary>
        public CsvConfiguration Configuration
        {
            get { return _csvWriter.Configuration; }
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
        /// Gets and sets the value of the current csv record at the position of the specified header name.
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
        /// Sets the value of the current csv record at the position of the specified header name.
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <param name="name">Name of a header</param>
        /// <param name="value">A value</param>
        public void SetValue<T>(string name, T value)
        {
            SetValue(name, value, ConverterResolver.GetConverter<T>());
        }

        /// <summary>
        /// Sets the value of the current csv record at the position of the specified header name.
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
        /// <param name="name">Name of a header</param>
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
        /// Applies all relevant values from the csv custom object to the current csv record.
        /// </summary>
        /// <param name="record">Pointer to the csv custom object instance</param>
        /// <returns>Number of values applied</returns>
        public int SetValues<TEntity>(TEntity entity)
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
                    var value = CsvClassMapperResolverFactory.GetResolver().GetMapper<TEntity>().GetValue(entity, header);
                    SetValue(CsvClassMapperResolverFactory.GetResolver().GetMapper<TEntity>().GetValueType(header), header, value);
                    c++;
                }
            }
            return c;
        }

        /// <summary>
        /// Gives back the list of values for the current csv record
        /// </summary>
        /// <returns>List of values</returns>
        public List<string> ToList()
        {
            return _csvValues.ToList();
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
        /// Tries to set the value of the current csv record at the position of the specified header name.
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
        /// Tries to set the value of the current csv record at the position of the specified header name.
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

        /// <summary>
        /// Writes the current csv record to the stream and clears its content
        /// </summary>
        public void Write()
        {
            if (_wasPreviousWrite)
            {
                _csvWriter.WriteLine();
            }
            _csvWriter.WriteValues(_csvValues);
            _wasPreviousWrite = true;
            Array.Fill(_csvValues, null);
        }

        /// <summary>
        /// Writes the current csv record to the stream and clears its content
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task WriteAsync()
        {
            if (_wasPreviousWrite)
            {
                await _csvWriter.WriteLineAsync().ConfigureAwait(false);
            }
            await _csvWriter.WriteValuesAsync(_csvValues).ConfigureAwait(false);
            _wasPreviousWrite = true;
            Array.Fill(_csvValues, null);
        }

        /// <summary>
        /// Writes the csv headers to the stream.
        /// </summary>
        public void WriteHeaders()
        {
            int i = 0;
            foreach (var value in Headers)
            {
                SetValue(i, value);
                i++;
            }
            Write();
        }

        /// <summary>
        /// Writes the csv headers to the stream.
        /// </summary>
        /// <param name="csvHeaders">List of csv headers</param>
        public void WriteHeaders(IEnumerable<string> csvHeaders)
        {
            Headers.Replace(csvHeaders);
            Array.Resize(ref _csvValues, csvHeaders.Count());
            WriteHeaders();
        }

        /// <summary>
        /// Writes the csv header values to the stream.
        /// </summary>
        /// <param name="csvHeaders">List of csv headers</param>
        public void WriteHeaders(params string[] csvHeaders)
        {
            WriteHeaders((IEnumerable<string>)csvHeaders);
        }

        /// <summary>
        /// Writes the csv headers to the stream using the member infos from the custom 
        /// csv object type
        /// </summary>
        public void WriteHeaders<TEntity>()
        {
            WriteHeaders(new CsvHeaders<TEntity>());
        }

        /// <summary>
        /// Writes the csv header values to the stream.
        /// </summary>
        /// <param name="csvHeaders">List of csv headers as expression lambda</param>
        public void WriteHeaders<TEntity>(Expression<Func<TEntity, object>> csvHeaders)
        {
            WriteHeaders(new CsvHeaders<TEntity>(csvHeaders));
        }

        /// <summary>
        /// Writes the csv headers to the stream.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task WriteHeadersAsync()
        {
            int i = 0;
            foreach (var value in Headers)
            {
                SetValue(i, value);
                i++;
            }
            await WriteAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Writes the csv headers to the stream.
        /// </summary>
        /// <param name="csvHeaders">List of csv headers</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task WriteHeadersAsync(IEnumerable<string> csvHeaders)
        {
            Headers.Replace(csvHeaders);
            Array.Resize(ref _csvValues, csvHeaders.Count());
            await WriteHeadersAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Writes the csv headers to the stream.
        /// </summary>
        /// <param name="csvHeaders">List of csv headers</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task WriteHeadersAsync(params string[] csvHeaders)
        {
            await WriteHeadersAsync((IEnumerable<string>)csvHeaders).ConfigureAwait(false);
        }

        /// <summary>
        /// Writes the csv headers to the stream using the member infos from the custom 
        /// csv object type
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task WriteHeadersAsync<TEntity>()
        {
            await WriteHeadersAsync(new CsvHeaders<TEntity>()).ConfigureAwait(false);
        }

        /// <summary>
        /// Writes the csv headers to the stream.
        /// </summary>
        /// <param name="csvHeaders">List of csv headers as expression lambda</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task WriteHeadersAsync<TEntity>(Expression<Func<TEntity, object>> csvHeaders)
        {
            await WriteHeadersAsync(new CsvHeaders<TEntity>(csvHeaders)).ConfigureAwait(false);
        }
    }
}
