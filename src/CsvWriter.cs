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
using System.Text;
using System.Threading.Tasks;

namespace Enbrea.Csv
{
    /// <summary>
    /// Implementation of a raw CSV Writer.
    /// </summary>
    /// <remarks>
    /// Common Format and MIME Type for Comma-Separated Values (CSV) Files: https://www.ietf.org/rfc/rfc4180.txt
    /// </remarks>
    public class CsvWriter : IDisposable
    {
        private readonly char[] _charsToBeQuoted = new char[] { '\n', '\r' };
        private TextWriter _textWriter;
        private bool _wasPreviousToken = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvWriter"/> class for the specified stream.
        /// </summary>
        /// <param name="stream">The stream to write to.</param>
        /// <param name="encoding">The character encoding to use.</param>
        public CsvWriter(Stream stream, Encoding encoding)
        {
            _textWriter = new StreamWriter(stream, encoding);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvWriter"/> class for the specified stream.
        /// </summary>
        /// <param name="stream">The stream to write to.</param>
        /// <param name="encoding">The character encoding to use.</param>
        /// <param name="bufferSize">The buffer size, in bytes.</param>
        public CsvWriter(Stream stream, Encoding encoding, int bufferSize)
        {
            _textWriter = new StreamWriter(stream, encoding, bufferSize);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvWriter "/> class for the specified file name.
        /// </summary>
        /// <param name="path">The complete file path to write to.</param>
        /// <param name="append">
        /// true to append data to the file; false to overwrite the file. If the specified file does not exist, 
        /// this parameter has no effect, and the constructor creates a new file.
        /// </param>
        public CsvWriter(string path, bool append)
        {
            _textWriter = new StreamWriter(path, append);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvWriter "/> class for the specified file name.
        /// </summary>
        /// <param name="path">The complete file path to write to.</param>
        /// <param name="append">
        /// true to append data to the file; false to overwrite the file. If the specified file does not exist, 
        /// this parameter has no effect, and the constructor creates a new file.
        /// </param>
        /// <param name="encoding">The character encoding to use.</param>
        public CsvWriter(string path, bool append, Encoding encoding)
        {
            _textWriter = new StreamWriter(path, append, encoding);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvWriter "/> class for the specified file name.
        /// </summary>
        /// <param name="path">The complete file path to write to.</param>
        /// <param name="append">
        /// true to append data to the file; false to overwrite the file. If the specified file does not exist, 
        /// this parameter has no effect, and the constructor creates a new file.
        /// </param>
        /// <param name="encoding">The character encoding to use.</param>
        /// <param name="bufferSize">The buffer size, in bytes.</param>
        public CsvWriter(string path, bool append, Encoding encoding, int bufferSize)
        {
            _textWriter = new StreamWriter(path, append, encoding, bufferSize);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvWriter "/> class for the specified string builder.
        /// </summary>
        /// <param name="sb">The string builder to write to.</param>
        public CsvWriter(StringBuilder sb)
        {
            _textWriter = new StringWriter(sb);
        }

        /// <summary>
        /// Configuration parameter
        /// </summary>
        public CsvConfiguration Configuration { get; set; } = new CsvConfiguration();

        /// <summary>
        /// Closes the internal text writer.
        /// </summary>
        public void Dispose()
        {
            _textWriter.Close();
        }

        /// <summary>
        /// Writes a comment row to the CSV stream
        /// </summary>.
        public void WriteComment(string text)
        {
            if (_wasPreviousToken)
            {
                WriteLine();
            }
            Write(Configuration.Comment + text);
            WriteLine();
        }

        /// <summary>
        /// Writes a comment row to the CSV stream
        /// </summary>.
        /// <returns>A task that represents the asynchronous write operation.</returns>
        public async Task WriteCommentAsync(string text)
        {
            if (_wasPreviousToken)
            {
                await WriteLineAsync();
            }
            await WriteAsync(Configuration.Comment + text);
            await WriteLineAsync();
        }

        /// <summary>
        /// Closes the current row within the CSV stream.
        /// </summary>
        public void WriteLine()
        {
            _wasPreviousToken = false;
            _textWriter.WriteLine();
        }

        /// <summary>
        /// Closes the current row within the CSV stream.
        /// </summary>
        /// <returns>A task that represents the asynchronous write operation.</returns>
        public async Task WriteLineAsync()
        {
            _wasPreviousToken = false;
            await _textWriter.WriteLineAsync();
        }

        /// <summary>
        /// Writes a single value to the CSV stream
        /// </summary>
        /// <param name="value">The value to be written.</param>
        /// <param name="endOfLine">If true, closes the current row.</param>
        public void WriteValue(string value, bool endOfLine = false)
        {
            Write(value);
            if (endOfLine) WriteLine();
        }

        /// <summary>
        /// Writes a single value to the CSV stream
        /// </summary>
        /// <param name="value">The value to be written.</param>
        /// <param name="endOfLine">If true, closes the current row.</param>
        /// <returns>A task that represents the asynchronous write operation.</returns>
        public async Task WriteValueAsync(string value, bool endOfLine = false)
        {
            await WriteAsync(value);
            if (endOfLine) await WriteLineAsync();
        }

        /// <summary>
        /// Writes a token to the CSV stream
        /// </summary>
        /// <param name="token">The token to be written</param>
        private void Write(string token)
        {
            if (_wasPreviousToken)
            {
                _textWriter.Write(Configuration.Separator);
            }

            if (!string.IsNullOrEmpty(token))
            {
                if (token.IndexOfAny(_charsToBeQuoted) != -1 || token.IndexOf(Configuration.Quote) != -1)
                {
                    _textWriter.Write(Configuration.Quote);

                    foreach (char c in token)
                    {
                        if (c == Configuration.Quote)
                        {
                            _textWriter.Write(Configuration.Quote);
                            _textWriter.Write(Configuration.Quote);
                        }
                        else
                        {
                            _textWriter.Write(c);
                        }
                    }

                    _textWriter.Write(Configuration.Quote);
                }
                else if ((Configuration.ForceQuotes) || (token.Contains(Configuration.Separator)))
                {
                    _textWriter.Write(Configuration.Quote);
                    _textWriter.Write(token);
                    _textWriter.Write(Configuration.Quote);
                }
                else
                {
                    _textWriter.Write(token);
                }
            }
            else
            {
                if (Configuration.ForceQuotes)
                {
                    _textWriter.Write(Configuration.Quote);
                    _textWriter.Write(Configuration.Quote);
                }
            }

            _wasPreviousToken = true;
        }

        /// <summary>
        /// Writes a token to the CSV stream
        /// </summary>
        /// <param name="token">The token to be written</param>
        /// <returns>A task that represents the asynchronous write operation.</returns>
        private async Task WriteAsync(string token)
        {
            if (_wasPreviousToken)
            {
                await _textWriter.WriteAsync(Configuration.Separator);
            }

            if (!string.IsNullOrEmpty(token))
            {
                if (token.IndexOfAny(_charsToBeQuoted) != -1 || token.IndexOf(Configuration.Quote) != -1)
                {
                    await _textWriter.WriteAsync(Configuration.Quote);

                    foreach (char c in token)
                    {
                        if (c == Configuration.Quote)
                        {
                            await _textWriter.WriteAsync(Configuration.Quote);
                            await _textWriter.WriteAsync(Configuration.Quote);
                        }
                        else
                        {
                            await _textWriter.WriteAsync(c);
                        }
                    }

                    await _textWriter.WriteAsync(Configuration.Quote);
                }
                else if ((Configuration.ForceQuotes) || (token.Contains(Configuration.Separator)))
                {
                    await _textWriter.WriteAsync(Configuration.Quote);
                    await _textWriter.WriteAsync(token);
                    await _textWriter.WriteAsync(Configuration.Quote);
                }
                else
                {
                    await _textWriter.WriteAsync(token);
                }
            }
            else
            {
                if (Configuration.ForceQuotes)
                {
                    await _textWriter.WriteAsync(Configuration.Quote);
                    await _textWriter.WriteAsync(Configuration.Quote);
                }
            }

            _wasPreviousToken = true;
        }
    }
}

