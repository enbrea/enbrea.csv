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
using System.Linq;
using System.Threading.Tasks;

namespace Enbrea.Csv
{
    /// <summary>
    /// A CSV table writer which generates CSV data as stream
    /// </summary>
    public class CsvTableWriter : CsvTableAccess
    {
        private readonly CsvWriter _csvWriter;
        private string[] _csvValues = new string[0];
        private bool _wasPreviousWrite = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvTableWriter"/> class.
        /// </summary>
        public CsvTableWriter(CsvWriter csvWriter)
            : base()
        {
            _csvWriter = csvWriter;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvTableWriter"/> class.
        /// </summary>
        /// <param name="csvWriter">The <see cref="CsvWriter"/> as source</param>
        /// <param name="csvHeaders">List of csv headers</param>
        public CsvTableWriter(CsvWriter csvWriter, CsvHeaders csvHeaders)
            : base(csvHeaders)
        {
            _csvWriter = csvWriter;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvTableWriter"/> class.
        /// </summary>
        /// <param name="csvWriter">The <see cref="CsvWriter"/> as source</param>
        /// <param name="csvHeaders">List of csv headers</param>
        public CsvTableWriter(CsvWriter csvWriter, params string[] csvHeaders)
            : this(csvWriter, new CsvHeaders(csvHeaders))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvTableWriter"/> class.
        /// </summary>
        /// <param name="csvWriter">The <see cref="CsvWriter"/> as source</param>
        /// <param name="csvHeaders">List of csv headers</param>
        public CsvTableWriter(CsvWriter csvWriter, IList<string> csvHeaders)
            : this(csvWriter, new CsvHeaders(csvHeaders))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvTableWriter"/> class.
        /// </summary>
        /// <param name="csvWriter">The <see cref="CsvWriter"/> as source</param>
        /// <param name="csvConverterResolver">Your own implementation of a value converter resolver</param>
        public CsvTableWriter(CsvWriter csvWriter, ICsvConverterResolver csvConverterResolver)
            : base(csvConverterResolver)
        {
            _csvWriter = csvWriter;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvTableWriter"/> class.
        /// </summary>
        /// <param name="csvWriter">The <see cref="CsvWriter"/> as source</param>
        /// <param name="csvConverterResolver">Your own implementation of a value converter resolver</param>
        /// <param name="csvHeaders">List of csv headers</param>
        public CsvTableWriter(CsvWriter csvWriter, ICsvConverterResolver csvConverterResolver, CsvHeaders csvHeaders)
            : base(csvHeaders, csvConverterResolver)
        {
            _csvWriter = csvWriter;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvTableWriter"/> class.
        /// </summary>
        /// <param name="csvWriter">The <see cref="CsvWriter"/> as source</param>
        /// <param name="csvConverterResolver">Your own implementation of a value converter resolver</param>
        /// <param name="csvHeaders">List of csv headers</param>
        public CsvTableWriter(CsvWriter csvWriter, ICsvConverterResolver csvConverterResolver, params string[] csvHeaders)
            : this(csvWriter, csvConverterResolver, new CsvHeaders(csvHeaders))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvTableWriter"/> class.
        /// </summary>
        /// <param name="csvWriter">The <see cref="CsvWriter"/> as source</param>
        /// <param name="csvConverterResolver">Your own implementation of a value converter resolver</param>
        /// <param name="csvHeaders">List of csv headers</param>
        public CsvTableWriter(CsvWriter csvWriter, ICsvConverterResolver csvConverterResolver, IList<string> csvHeaders)
            : this(csvWriter, csvConverterResolver, new CsvHeaders(csvHeaders))
        {
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
        /// Sets the value of the current csv record at the posiiton of the specified header name.
        /// </summary>
        /// <param name="name">Name of a header</param>
        /// <param name="value">A value</param>
        public void SetValue<T>(string name, T value)
        {
            SetValue(name, value, ConverterResolver.GetConverter<T>());
        }

        /// <summary>
        /// Sets the value of the current csv record at the posiiton of the specified header name.
        /// </summary>
        /// <param name="name">Name of a header</param>
        /// <param name="value">A value</param>
        public void SetValue<T>(string name, T value, ICsvConverter valueConverter)
        {
            this[name] = valueConverter.ToString(value);
        }

        /// <summary>
        /// Sets the value of the current csv record at the specified index. 
        /// </summary>
        /// <param name="index">Index within the current csv record</param>
        /// <param name="value">A value</param>
        public void SetValue<T>(int index, T value)
        {
            SetValue(index, value, ConverterResolver.GetConverter<T>());
        }

        /// <summary>
        /// Sets the value of the current csv record at the specified index. 
        /// </summary>
        /// <param name="index">Index within the current csv record</param>
        /// <param name="value">A value</param>
        public void SetValue<T>(int index, T value, ICsvConverter valueConverter)
        {
            this[index] = valueConverter.ToString(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<string> ToList()
        {
            return _csvValues.ToList();
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
                await _csvWriter.WriteLineAsync();
            }
            await _csvWriter.WriteValuesAsync(_csvValues);
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
            await WriteAsync();
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
            await WriteHeadersAsync();
        }

        /// <summary>
        /// Writes the csv headers to the stream.
        /// </summary>
        /// <param name="csvHeaders">List of csv headers</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task WriteHeadersAsync(params string[] csvHeaders)
        {
            await WriteHeadersAsync((IEnumerable<string>)csvHeaders);
        }
    }
}
