#region ENBREA.CSV - Copyright (C) 2023 STÜBER SYSTEMS GmbH
/*    
 *    ENBREA.CSV 
 *    
 *    Copyright (C) 2023 STÜBER SYSTEMS GmbH
 *
 *    Licensed under the MIT License, Version 2.0. 
 */
#endregion

using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Enbrea.Csv
{
    /// <summary>
    /// Implementation of a raw CSV Writer.
    /// </summary>
    /// <remarks>
    /// Common Format and MIME Type for Comma-Separated Values (CSV) Files: https://www.ietf.org/rfc/rfc4180.txt
    /// </remarks>
    public class CsvWriter
    {
        private readonly char[] _charsToBeQuoted = new char[] { '\n', '\r' };
        private readonly TextWriter _textWriter;
        private bool _wasPreviousToken = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvWriter"/> class for the specified text writert.
        /// </summary>
        /// <param name="textWriter">The text writer to be used.</param>
        public CsvWriter(TextWriter textWriter)
            : this(textWriter, new CsvConfiguration())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvWriter"/> class for the specified text writert.
        /// </summary>
        /// <param name="textWriter">The text writer to be used.</param>
        /// <param name="configuration">Configuration parameters</param>
        public CsvWriter(TextWriter textWriter, CsvConfiguration configuration)
        {
            _textWriter = textWriter;
            Configuration = configuration;
        }

        /// <summary>
        /// Configuration parameter
        /// </summary>
        public CsvConfiguration Configuration { get; }

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
                await WriteLineAsync().ConfigureAwait(false);
            }
            await WriteAsync($"{Configuration.Comment}{text}").ConfigureAwait(false);
            await WriteLineAsync().ConfigureAwait(false);
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
            await _textWriter.WriteLineAsync().ConfigureAwait(false);
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
            if (endOfLine) await WriteLineAsync().ConfigureAwait(false);
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
                await _textWriter.WriteAsync(Configuration.Separator).ConfigureAwait(false);
            }

            if (!string.IsNullOrEmpty(token))
            {
                if (token.IndexOfAny(_charsToBeQuoted) != -1 || token.IndexOf(Configuration.Quote) != -1)
                {
                    await _textWriter.WriteAsync(Configuration.Quote).ConfigureAwait(false);

                    foreach (char c in token)
                    {
                        if (c == Configuration.Quote)
                        {
                            await _textWriter.WriteAsync(Configuration.Quote).ConfigureAwait(false);
                            await _textWriter.WriteAsync(Configuration.Quote).ConfigureAwait(false);
                        }
                        else
                        {
                            await _textWriter.WriteAsync(c).ConfigureAwait(false);
                        }
                    }

                    await _textWriter.WriteAsync(Configuration.Quote);
                }
                else if ((Configuration.ForceQuotes) || (token.Contains(Configuration.Separator)))
                {
                    await _textWriter.WriteAsync(Configuration.Quote).ConfigureAwait(false);
                    await _textWriter.WriteAsync(token).ConfigureAwait(false);
                    await _textWriter.WriteAsync(Configuration.Quote).ConfigureAwait(false);
                }
                else
                {
                    await _textWriter.WriteAsync(token).ConfigureAwait(false);
                }
            }
            else
            {
                if (Configuration.ForceQuotes)
                {
                    await _textWriter.WriteAsync(Configuration.Quote).ConfigureAwait(false);
                    await _textWriter.WriteAsync(Configuration.Quote).ConfigureAwait(false);
                }
            }

            _wasPreviousToken = true;
        }
    }
}

