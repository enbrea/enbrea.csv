#region ENBREA.CSV - Copyright (C) 2020 STÜBER SYSTEMS GmbH
/*    Copyright (C) 2020 STÜBER SYSTEMS GmbH
 *    ENBREA.CSV 
 *    
 *    Copyright (C) 2020 STÜBER SYSTEMS GmbH
 *
 *    Licensed under the MIT License, Version 2.0. 
 * 
 */
#endregion

namespace Enbrea.Csv
{
    /// <summary>
    /// An abstract base class for <see cref="CsvTableReader"/> <see cref="CsvTableWriter"/>, <see cref="CsvLineTableReader"/> 
    /// and <see cref="CsvLineTableWriter"/>
    /// </summary>
    public abstract class CsvTableAccess
    {
        private ICsvConverterResolver _csvConverterResolver;

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvTableAccess"/> class.
        /// </summary>
        public CsvTableAccess()
        {
            Headers = new CsvHeaders();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvTableAccess"/> class.
        /// </summary>
        /// <param name="csvHeaders">List of csv headers</param>
        public CsvTableAccess(CsvHeaders csvHeaders)
            : this()
        {
            Headers = csvHeaders;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvTableAccess"/> class.
        /// </summary>
        /// <param name="csvConverterResolver">Your own implementation of a value converter resolver</param>
        public CsvTableAccess(ICsvConverterResolver csvConverterResolver)
            : this()
        {
            _csvConverterResolver = csvConverterResolver;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvTableAccess"/> class.
        /// </summary>
        /// <param name="csvHeaders">List of csv headers</param>
        /// <param name="csvConverterResolver">Your own implementation of a value converter resolver</param>
        public CsvTableAccess(CsvHeaders csvHeaders, ICsvConverterResolver csvConverterResolver)
            : this(csvHeaders)
        {
            _csvConverterResolver = csvConverterResolver;
        }

        /// <summary>
        /// The value converter resolver
        /// </summary>
        public ICsvConverterResolver ConverterResolver { get { return GetOrCreateDefaultConverterResolver(); } }

        /// <summary>
        /// List of csv headers
        /// </summary>
        public CsvHeaders Headers { get; }

        /// <summary>
        /// Gives always back a value converter resolver. If the internal value converter resolver does not exst 
        /// it will be created.
        /// </summary>
        /// <returns>The value converer resolver</returns>
        private ICsvConverterResolver GetOrCreateDefaultConverterResolver()
        {
            if (_csvConverterResolver == null)
            {
                _csvConverterResolver = new CsvDefaultConverterResolver();
            }
            return _csvConverterResolver;
        }
    }
}
