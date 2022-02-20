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
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Enbrea.Csv
{
    /// <summary>
    /// A raw stream based CSV parser/reader.
    /// </summary>
    /// <remarks>
    /// Common Format and MIME Type for Comma-Separated Values (CSV) Files: https://www.ietf.org/rfc/rfc4180.txt
    /// </remarks>
    public class CsvReader : IDisposable
    {
        private const int _bufferSize = 1024;
        private readonly char[] _buffer;
        private readonly CsvParser _csvParser;
        private readonly TextReader _textReader;
        private int _bufferEnd = 0;
        private int _bufferPosition = 0;
        private int _lineCount = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvReader"/> class for the specified stream.
        /// </summary>
        /// <param name="stream">The stream to be read.</param>
        /// <param name="detectEncodingFromByteOrderMarks">
        /// Indicates whether to look for byte order marks at the beginning of the file.
        /// </param>
        public CsvReader(Stream stream, bool detectEncodingFromByteOrderMarks)
            : this()
        {
            _textReader = new StreamReader(stream, detectEncodingFromByteOrderMarks);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvReader"/> class for the specified stream.
        /// </summary>
        /// <param name="stream">The stream to be read.</param>
        /// <param name="encoding">The character encoding to use.</param>
        public CsvReader(Stream stream, Encoding encoding)
            : this()
        {
            _textReader = new StreamReader(stream, encoding);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvReader"/> class for the specified stream.
        /// </summary>
        /// <param name="stream">The stream to be read.</param>
        /// <param name="encoding">The character encoding to use.</param>
        /// <param name="detectEncodingFromByteOrderMarks">
        /// Indicates whether to look for byte order marks at the beginning of the file.
        /// </param>
        public CsvReader(Stream stream, Encoding encoding, bool detectEncodingFromByteOrderMarks)
            : this()
        {
            _textReader = new StreamReader(stream, encoding, detectEncodingFromByteOrderMarks);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvReader"/> class for the specified stream.
        /// </summary>
        /// <param name="stream">The stream to be read.</param>
        /// <param name="encoding">The character encoding to use.</param>
        /// <param name="detectEncodingFromByteOrderMarks">
        /// Indicates whether to look for byte order marks at the beginning of the file.
        /// </param>
        /// <param name="bufferSize">The minimum buffer size.</param>
        public CsvReader(Stream stream, Encoding encoding, bool detectEncodingFromByteOrderMarks, int bufferSize)
            : this()
        {
            _textReader = new StreamReader(stream, encoding, detectEncodingFromByteOrderMarks, bufferSize);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvReader"/> class for the specified file name.
        /// </summary>
        /// <param name="path">The complete file path to be read.</param>
        /// <param name="detectEncodingFromByteOrderMarks">
        /// Indicates whether to look for byte order marks at the beginning of the file.
        /// </param>
        public CsvReader(string path, bool detectEncodingFromByteOrderMarks)
            : this()
        {
            _textReader = new StreamReader(path, detectEncodingFromByteOrderMarks);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvReader"/> class for the specified file name.
        /// </summary>
        /// <param name="path">The complete file path to be read.</param>
        /// <param name="encoding">The character encoding to use.</param>
        public CsvReader(string path, Encoding encoding)
            : this()
        {
            _textReader = new StreamReader(path, encoding);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvReader"/> class for the specified file name.
        /// </summary>
        /// <param name="path">The complete file path to be read.</param>
        /// <param name="encoding">The character encoding to use.</param>
        /// <param name="detectEncodingFromByteOrderMarks">
        /// Indicates whether to look for byte order marks at the beginning of the file.
        /// </param>
        public CsvReader(string path, Encoding encoding, bool detectEncodingFromByteOrderMarks)
            : this()
        {
            _textReader = new StreamReader(path, encoding, detectEncodingFromByteOrderMarks);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvReader"/> class for the specified file name.
        /// </summary>
        /// <param name="path">The complete file path to be read.</param>
        /// <param name="encoding">The character encoding to use.</param>
        /// <param name="detectEncodingFromByteOrderMarks">
        /// Indicates whether to look for byte order marks at the beginning of the file.
        /// </param>
        /// <param name="bufferSize">The minimum buffer size.</param>
        public CsvReader(string path, Encoding encoding, bool detectEncodingFromByteOrderMarks, int bufferSize)
            : this()
        {
            _textReader = new StreamReader(path, encoding, detectEncodingFromByteOrderMarks, bufferSize);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvReader"/> class for the specified text string.
        /// </summary>
        /// <param name="content">The complete text content to be read.</param>
        public CsvReader(string content)
            : this()
        {
            _textReader = new StringReader(content);
        }

        /// <summary>
        /// Private constructor as base for other constructors
        /// </summary>
        private CsvReader()
        {
            _buffer = new char[_bufferSize];
            _csvParser = new CsvParser(ThrowException);
        }

        /// <summary>
        /// Configuration parameter
        /// </summary>
        public CsvConfiguration Configuration 
        { 
            get { return _csvParser.Configuration; } 
            set { _csvParser.Configuration = value; }  
        }

        /// <summary>
        /// Closes the internal text reader.
        /// </summary>
        public void Dispose()
        {
            _textReader.Close();
        }

        /// <summary>
        /// Reads out the next row out of the current CSV stream and fills the values
        /// of a given string array.
        /// </summary>
        /// <param name="csvReader">The <see cref="CsvReader"/></param>
        /// <param name="values">Array of parsed values.</param>
        /// <returns>
        /// Number of parsed values
        /// <returns>
        /// <exception cref="ArgumentNullException"></exception>
        public int ReadLine(string[] values)
        {
            if (values == null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            Func<char> nextCharAction = () => NextChar();

            var valueCount = 0;
            do
            {
                if (_csvParser.NextToken(nextCharAction))
                {
                    if (values.Length > valueCount)
                    {
                        values[valueCount] = _csvParser.GetToken();
                    }
                    valueCount++;
                }
            }
            while (_csvParser.State != CsvParser.TokenizerState.IsEndOfLine);

            _lineCount++;

            return valueCount;
        }

        /// <summary>
        /// Reads out the next row out of the current CSV stream and calls for every detected
        /// field value the given action.
        /// </summary>
        /// <param name="valueAction">Action with one string parameter.</param>
        /// <returns>
        /// Number of parsed values
        /// </returns>
        public int ReadLine(Action<int, string> valueAction)
        {
            if (valueAction == null)
            {
                throw new ArgumentNullException(nameof(valueAction));
            }

            Func<char> nextCharAction = () => NextChar();

            var valueCount = 0;
            do
            {
                if (_csvParser.NextToken(nextCharAction))
                {
                    valueAction(valueCount, _csvParser.GetToken());
                    valueCount++;
                }
            }
            while (_csvParser.State != CsvParser.TokenizerState.IsEndOfLine);

            _lineCount++;

            return valueCount;
        }

        /// <summary>
        /// Reads out the next row out of the current CSV stream and fills the values
        /// of a given string array.
        /// </summary>
        /// <param name="csvReader">The <see cref="CsvReader"/></param>
        /// <param name="values">Array of parsed values.</param>
        /// <returns>A task that represents the asynchronous operation. The value of the TResult
        /// parameter contains the number of parsed values.</returns>
        /// <returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<int> ReadLineAsync(string[] values)
        {
            if (values == null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            Func<ValueTask<char>> nextCharAction = () => NextCharAsync();

            var valueCount = 0;
            do
            {
                if (await _csvParser.NextTokenAsync(nextCharAction))
                {
                    if (values.Length > valueCount)
                    {
                        values[valueCount] = _csvParser.GetToken();
                    }
                    valueCount++;
                }
            }
            while (_csvParser.State != CsvParser.TokenizerState.IsEndOfLine);

            _lineCount++;

            return valueCount;
        }

        /// <summary>
        /// Reads out the next row out of the current CSV stream and calls for every detected
        /// field value the given action.
        /// </summary>
        /// <param name="valueAction">Action with one string parameter.</param>
        /// <returns>A task that represents the asynchronous operation. The value of the TResult
        /// parameter contains the number of parsed values.</returns>
        /// <returns>
        public async Task<int> ReadLineAsync(Action<int, string> valueAction)
        {
            if (valueAction == null)
            {
                throw new ArgumentNullException(nameof(valueAction));
            }

            Func<ValueTask<char>> nextCharAction = () => NextCharAsync();

            var valueCount = 0;
            do
            {
                if (await _csvParser.NextTokenAsync(nextCharAction))
                {
                    valueAction(valueCount, _csvParser.GetToken());
                    valueCount++;
                }
            }
            while (_csvParser.State != CsvParser.TokenizerState.IsEndOfLine);

            _lineCount++;

            return valueCount;
        }

        /// <summary>
        /// Asks for the next character from the CSV source.
        /// </summary>
        /// <returns>The next character from the CSV source or EoF if nothing to read.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private char NextChar()
        {
            if (_bufferPosition >= _bufferEnd - 1)
            {
                _bufferEnd = _textReader.Read(_buffer, 0, _bufferSize);
                if (_bufferEnd > 0)
                {
                    _bufferPosition = 0;
                    return _buffer[_bufferPosition];
                }
                else
                {
                    return '\0';
                }
            }
            else
            {
                _bufferPosition++;
                return _buffer[_bufferPosition];
            }
        }

        /// <summary>
        /// Asks for the next character from the CSV source. 
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The value of the TResult
        /// parameter contains the next character from the CSV source or EoF if nothing to read.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private async ValueTask<char> NextCharAsync()
        {
            if (_bufferPosition >= _bufferEnd - 1)
            {
                _bufferEnd = await _textReader.ReadAsync(_buffer, 0, _bufferSize);
                if (_bufferEnd > 0)
                {
                    _bufferPosition = 0;
                    return _buffer[_bufferPosition];
                }
                else
                {
                    return '\0';
                }
            }
            else
            {
                _bufferPosition++;
                return _buffer[_bufferPosition];
            }
        }

        /// <summary>
        /// Creates a new parser exception.
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <returns>The newly craeted Exception instance</returns>
        private Exception ThrowException(string message)
        {
            return new CsvParserException($"Line {_lineCount + 1}: {message}");
        }
    }
}