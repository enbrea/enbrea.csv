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

using System.Text;

namespace Enbrea.Csv
{
    /// <summary>
    /// A builder for single CSV lines
    /// </summary>
    /// <remarks>
    /// Common Format and MIME Type for Comma-Separated Values (CSV) Files: https://www.ietf.org/rfc/rfc4180.txt
    /// </remarks>
    public class CsvLineBuilder
    {
        private readonly char[] _charsToBeQuoted = new char[] { '\"', '\n', '\r' };
        private readonly StringBuilder _strBuilder;
        private bool _wasPreviousToken;

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvLineBuilder"/> class.
        /// </summary>
        public CsvLineBuilder()
            : this(new CsvConfiguration())
        {
            _strBuilder = new StringBuilder();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvLineParser"/> class.
        /// </summary>
        /// <param name="configuration">Configuration parameters</param>
        public CsvLineBuilder(CsvConfiguration configuration)
        {
            _strBuilder = new StringBuilder();
            Configuration = configuration;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvLineBuilder"/> class.
        /// </summary>
        /// <param name="separator">Specifies the charactor for seperating values</param>
        public CsvLineBuilder(char separator)
            : this(new CsvConfiguration())
        {
            Configuration.Separator = separator;
        }

        /// <summary>
        /// Configuration parameter
        /// </summary>
        public CsvConfiguration Configuration { get; }

        /// <summary>
        /// Appends a value to the internal csv line.
        /// </summary>
        /// <param name="values">The value</param>
        public void Append(string value)
        {
            Write(value);
        }

        /// <summary>
        /// Removes all values from the internal csv line.
        /// </summary>
        public void Clear()
        {
            _wasPreviousToken = false;
            _strBuilder.Clear();
        }

        /// <summary>
        /// Gives back the internal csv line as string
        /// </summary>
        /// <returns>Internal csv line</returns>
        public override string ToString()
        {
            return _strBuilder.ToString();
        }

        /// <summary>
        /// Appends a token to the internal StringBuilder
        /// </summary>
        /// <param name="token">The token to be appended</param>
        private void Write(string token)
        {
            if (_wasPreviousToken)
            {
                _strBuilder.Append(Configuration.Separator);
            }

            if (!string.IsNullOrEmpty(token))
            {
                if (token.IndexOfAny(_charsToBeQuoted) != -1)
                {
                    _strBuilder.Append("\"");

                    foreach (char c in token)
                    {
                        if (c == '"')
                        {
                            _strBuilder.Append("\"\"");
                        }
                        else
                        {
                            _strBuilder.Append(c);
                        }
                    }

                    _strBuilder.Append("\"");
                }
                else if ((Configuration.ForceQuotes) || (token.Contains(Configuration.Separator)))
                {
                    _strBuilder.Append("\"");
                    _strBuilder.Append(token);
                    _strBuilder.Append("\"");
                }
                else
                {
                    _strBuilder.Append(token);
                }
            }
            else
            {
                if (Configuration.ForceQuotes)
                {
                    _strBuilder.Append("\"\"");
                }
            }

            _wasPreviousToken = true;
        }

    }
}