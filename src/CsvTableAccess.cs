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
    /// An abstract base class for <see cref="CsvTableReader"/> <see cref="CsvTableWriter"/>, <see cref="CsvTableLineParser"/>,
    /// <see cref="CsvTableLineBuilder"/> and <see cref="CsvDictionary"/>
    /// </summary>
    public abstract class CsvTableAccess : CsvAccess
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CsvTableAccess"/> class.
        /// </summary>
        public CsvTableAccess()
            : base()
        {
            Headers = new CsvHeaders();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvTableAccess"/> class.
        /// </summary>
        /// <param name="csvHeaders">List of csv headers</param>
        public CsvTableAccess(CsvHeaders csvHeaders)
            : base()
        {
            Headers = csvHeaders;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvTableAccess"/> class.
        /// </summary>
        /// <param name="csvConverterResolver">Your own implementation of a value converter resolver</param>
        public CsvTableAccess(ICsvConverterResolver csvConverterResolver)
            : base(csvConverterResolver)
        {
            Headers = new CsvHeaders();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvTableAccess"/> class.
        /// </summary>
        /// <param name="csvHeaders">List of csv headers</param>
        /// <param name="csvConverterResolver">Your own implementation of a value converter resolver</param>
        public CsvTableAccess(CsvHeaders csvHeaders, ICsvConverterResolver csvConverterResolver)
            : base(csvConverterResolver)
        {
            Headers = csvHeaders;
        }

        /// <summary>
        /// List of csv headers
        /// </summary>
        public CsvHeaders Headers { get; }
    }
}
