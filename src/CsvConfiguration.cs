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

namespace Enbrea.Csv
{
    /// <summary>
    /// Configuration parameter for <see cref="CsvReader"/>, <see cref="CsvWriter"/>, 
    /// <see cref="CsvLineParser"/> and <see cref="CsvLineBuilder"/>.
    /// </summary>
    public class CsvConfiguration
    {
        /// <summary>
        /// Specifies whether comments are allowed or not inside a CSV line
        /// </summary>
        public bool AllowComments { get; set; } = false;

        /// <summary>
        /// Specifies whether parsed values should be cached and reused
        /// </summary>
        public bool CacheValues { get; set; } = true;

        /// <summary>
        /// Specifies the character for signaling a comment
        /// </summary>
        public char Comment { get; set; } = '#';

        /// <summary>
        /// Specifies whether values should be always quoted.
        /// </summary>
        public bool ForceQuotes { get; set; } = false;

        /// <summary>
        /// Specifies whether quotes should be ignored and handled as normal characters
        /// </summary>
        public bool IgnoreQuotes { get; set; } = false;

        /// <summary>
        /// Specifies the character for quoting values 
        /// </summary>
        public char Quote { get; set; } = '"';

        /// <summary>
        /// Specifies the character for separating values
        /// </summary>
        public char Separator { get; set; } = ';';
    }
}