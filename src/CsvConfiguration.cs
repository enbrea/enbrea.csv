#region ENBREA.CSV - Copyright (C) 2020 STÜBER SYSTEMS GmbH
/*    
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
    /// Configuration parameter for <see cref="CsvReader"/>, <see cref="CsvWriter"/>, 
    /// <see cref="CsvLineParser"/> and <see cref="CsvLineBuilder"/>.
    /// </summary>
    public class CsvConfiguration
    {
        /// <summary>
        /// Specifies wether comments are allowed or not inside a CSV line
        /// </summary>
        public bool AllowComments { get; set; } = false;

        /// <summary>
        /// Specifies the character for signaling a comment
        /// </summary>
        public char Comment { get; set; } = '#';

        /// <summary>
        /// Specifies wether values should be always quoted.
        /// </summary>
        public bool ForceQuotes { get; set; } = false;

        /// <summary>
        /// Specifies wether quotes should be ignored and handled as normal characters
        /// </summary>
        public bool IgnoreQuotes { get; set; } = false;

        /// <summary>
        /// Specifies the charactor for seperating values
        /// </summary>
        public char Separator { get; set; } = ';';
   }
}