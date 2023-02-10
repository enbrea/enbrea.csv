#region ENBREA.CSV - Copyright (C) 2023 STÜBER SYSTEMS GmbH
/*    
 *    ENBREA.CSV 
 *    
 *    Copyright (C) 2023 STÜBER SYSTEMS GmbH
 *
 *    Licensed under the MIT License, Version 2.0. 
 */
#endregion

namespace Enbrea.Csv
{
    /// <summary>
    /// An abstract base class for <see cref="CsvTableAccess"/> and <see cref="CsvDictionary"/>
    /// </summary>
    public abstract class CsvAccess
    {
        private ICsvConverterResolver _csvConverterResolver;

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvAccess"/> class.
        /// </summary>
        public CsvAccess()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvAccess"/> class.
        /// </summary>
        /// <param name="csvConverterResolver">Your own implementation of a value converter resolver</param>
        public CsvAccess(ICsvConverterResolver csvConverterResolver)
            : this()
        {
            _csvConverterResolver = csvConverterResolver;
        }

        /// <summary>
        /// The value converter resolver
        /// </summary>
        public ICsvConverterResolver ConverterResolver { get { return GetOrCreateDefaultConverterResolver(); } }

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
