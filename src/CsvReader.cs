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
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Enbrea.Csv
{
    /// <summary>
    /// Implementation of a raw CSV Reader.
    /// </summary>
    /// <remarks>
    /// Common Format and MIME Type for Comma-Separated Values (CSV) Files: https://www.ietf.org/rfc/rfc4180.txt
    /// </remarks>
    public class CsvReader
    {
        private const int _bufferSize = 1024;
        private readonly char[] _buffer;
        private readonly CsvParser _csvParser;
        private readonly TextReader _textReader;
        private int _bufferEnd = 0;
        private int _bufferPosition = 0;
        private int _lineCount = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvReader"/> class for the specified text reader.
        /// </summary>
        /// <param name="textReader">The text reader to be used.</param>
        public CsvReader(TextReader textReader)
            : this(textReader, new CsvConfiguration())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvReader"/> class for the specified text reader.
        /// </summary>
        /// <param name="textReader">The text reader to be used.</param>
        /// <param name="configuration">Configuration parameters</param>
        public CsvReader(TextReader textReader, CsvConfiguration configuration)
        {
            _buffer = new char[_bufferSize];
            _csvParser = new CsvParser(configuration, ThrowException);
            _textReader = textReader;
        }

        /// <summary>
        /// Configuration parameter
        /// </summary>
        public CsvConfiguration Configuration 
        { 
            get { return _csvParser.Configuration; } 
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

            char nextCharAction() => NextChar();

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

            ValueTask<char> nextCharAction() => NextCharAsync();

            var valueCount = 0;
            do
            {
                if (await _csvParser.NextTokenAsync(nextCharAction).ConfigureAwait(false))
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
        private async ValueTask<char> NextCharAsync()
        {
            if (_bufferPosition >= _bufferEnd - 1)
            {
                _bufferEnd = await _textReader.ReadAsync(_buffer, 0, _bufferSize).ConfigureAwait(false);
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