#region ENBREA.CSV - Copyright (C) 2021 STÜBER SYSTEMS GmbH
/*    
 *    ENBREA.CSV 
 *    
 *    Copyright (C) 2021 STÜBER SYSTEMS GmbH
 *
 *    Licensed under the MIT License, Version 2.0. 
 * 
 */
#endregion

using System;

namespace Enbrea.Csv
{
    /// <summary>
    /// A raw parser for single CSV lines
    /// </summary>
    /// <remarks>
    /// Common Format and MIME Type for Comma-Separated Values (CSV) Files: https://www.ietf.org/rfc/rfc4180.txt
    /// </remarks>
    public class CsvLineParser
    {
        private readonly CsvParser _csvParser;
        private string _line;
        private int _position;

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvLineParser"/> class.
        /// </summary>
        public CsvLineParser()
            : base()
        {
            _csvParser = new CsvParser(ThrowException);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvLineParser"/> class.
        /// </summary>
        /// <param name="separator">Specifies the charactor for seperating values</param>
        public CsvLineParser(char separator)
            : this()
        {
            Configuration.Separator = separator;
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
        /// Parses a CSV text line and calls for every detected field value the given action.
        /// </summary>
        /// <param name="line">A CSV formated text line.</param>
        /// <param name="valueAction">Action with one string parameter.</param>
        /// <returns>
        /// Number of parsed values
        /// </returns>
        public int Read(string line, Action<int, string> valueAction)
        {
            if (valueAction == null)
            {
                throw new ArgumentNullException("valueAction");
            }

            _line = line;
            _position = 0;
            _csvParser.ResetState();

            var valueCount = 0;
            do
            {
                if (_csvParser.NextToken(() => NextChar()))
                {
                    valueAction(valueCount, _csvParser.Token.ToString());
                    valueCount++;
                }
            }
            while (_csvParser.State != CsvParser.TokenizerState.IsEndOfLine);

            return valueCount;
        }

        /// <summary>
        /// Asks for the next character from the CSV source.
        /// </summary>
        /// <returns>The next character from the CSV source or EoF if nothing to read.</returns>
        private char NextChar()
        {
            if (_position < _line.Length)
            {
                _position++;
                return _line[_position - 1];
            }
            else
            {
                return '\0';
            }
        }

        /// <summary>
        /// Creates a new parser exception. 
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <returns>The newly craeted Exception instance</returns>
        private Exception ThrowException(string message)
        {
            return new CsvParserException(message);
        }
    }
}