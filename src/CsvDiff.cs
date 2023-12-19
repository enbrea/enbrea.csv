#region ENBREA.CSV - Copyright (C) 2023 STÜBER SYSTEMS GmbH
/*    
 *    ENBREA.CSV 
 *    
 *    Copyright (C) 2023 STÜBER SYSTEMS GmbH
 *
 *    Licensed under the MIT License, Version 2.0. 
 */
#endregion

using System.Collections.Generic;
using System.IO;
using System.IO.Hashing;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Enbrea.Csv
{
    /// <summary>
    /// Computes differences between 2 CSV files and generates the result as CSV
    /// </summary>
    public class CsvDiff
    {
        private readonly Crc32 _hashAlgorithm = new();
        private readonly CsvTableWriter _tableWriter;

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvDiff"/> class.
        /// </summary>
        /// <param name="textWriter">The text writer to be used.</param>
        public CsvDiff(TextWriter textWriter)
            : this(textWriter, new CsvHeaders())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvDiff"/> class.
        /// </summary>
        /// <param name="textWriter">The text writer to be used.</param>
        /// <param name="configuration">Configuration parameters</param>
        public CsvDiff(TextWriter textWriter, CsvConfiguration configuration)
            : this(textWriter, configuration, new CsvHeaders())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvDiff"/> class.
        /// </summary>
        /// <param name="textWriter">The text writer to be used.</param>
        /// <param name="keyHeaders">List of csv key headers</param>
        public CsvDiff(TextWriter textWriter, CsvHeaders keyHeaders)
        {
            _tableWriter = new CsvTableWriter(textWriter);
            UniqueKeyHeaders = keyHeaders;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvDiff"/> class.
        /// </summary>
        /// <param name="textWriter">The text writer to be used.</param>
        /// <param name="configuration">Configuration parameters</param>
        /// <param name="uniqueKeyHeaders">List of csv key headers</param>
        public CsvDiff(TextWriter textWriter, CsvConfiguration configuration, CsvHeaders uniqueKeyHeaders)
        {
            _tableWriter = new CsvTableWriter(textWriter, configuration);
            UniqueKeyHeaders = uniqueKeyHeaders;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvDiff"/> class.
        /// </summary>
        /// <param name="textWriter">The text writer to be used.</param>
        /// <param name="keyHeaders">List of csv key headers</param>
        public CsvDiff(TextWriter textWriter, params string[] keyHeaders)
            : this(textWriter, new CsvHeaders(keyHeaders))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvDiff"/> class.
        /// </summary>
        /// <param name="textWriter">The text writer to be used.</param>
        /// <param name="configuration">Configuration parameters</param>
        /// <param name="uniqueKeyHeaders">List of csv headers</param>
        public CsvDiff(TextWriter textWriter, CsvConfiguration configuration, params string[] uniqueKeyHeaders)
            : this(textWriter, configuration, new CsvHeaders(uniqueKeyHeaders))
        {
        }

        /// <summary>
        /// Configuration parameter
        /// </summary>
        public CsvConfiguration Configuration
        {
            get { return _tableWriter.Configuration; }
        }

        /// <summary>
        /// List of unique csv key headers
        /// </summary>
        public CsvHeaders UniqueKeyHeaders { get; }

        /// <summary>
        /// Reads the contents of two CSV files and creates a new CSV file with all rows that have changed (depending on the given diff type)
        /// </summary>
        /// <param name="type">Type of diff operation</param>
        /// <param name="readerOfPreviousTable">The <see cref="CsvTableReader"/> for the old CSV file</param>
        /// <param name="readerOfCurrentTable">The <see cref="CsvTableReader"/> for the new CSV file</param>
        /// <returns>Number of written rows</returns>
        public int Generate(CsvDiffType type, CsvTableReader readerOfPreviousTable, CsvTableReader readerOfCurrentTable)
        {
            var rowCount = 0;

            if (type == CsvDiffType.DeletedOnly)
            {
                var hashMapOfCurrentTable = GenerateRowHashMap(readerOfCurrentTable);

                readerOfPreviousTable.ReadHeaders();

                _tableWriter.WriteHeaders(readerOfPreviousTable.Headers);

                while (readerOfPreviousTable.Read() > 0)
                {
                    var keyHashValue = GetKeyHashValue(readerOfPreviousTable);

                    if (!hashMapOfCurrentTable.ContainsKey(keyHashValue))
                    {
                        _tableWriter.Write(values: readerOfPreviousTable.ToList());
                        rowCount++;
                    }
                }
            }
            else
            {
                var hashMapOfChangedRows = new Dictionary<uint, uint>();
                var hashMapOfPreviousTable = GenerateRowHashMap(readerOfPreviousTable);

                readerOfCurrentTable.ReadHeaders();

                _tableWriter.WriteHeaders(readerOfCurrentTable.Headers);

                while (readerOfCurrentTable.Read() > 0)
                {
                    var keyHashValue = GetKeyHashValue(readerOfCurrentTable);
                    var rowHashValue = GetRowHashValue(readerOfCurrentTable);

                    if (hashMapOfPreviousTable.TryGetValue(keyHashValue, out var rowHashValueOfPreviousTable))
                    {
                        if ((type == CsvDiffType.UpdatedOnly || type == CsvDiffType.AddedOrUpdatedOnly) && rowHashValue != rowHashValueOfPreviousTable)
                        {
                            _tableWriter.Write(values: readerOfCurrentTable.ToList());
                            rowCount++;
                            hashMapOfChangedRows.TryAdd(keyHashValue, rowHashValue);
                        }
                    }
                    else
                    {
                        if ((type == CsvDiffType.AddedOnly || type == CsvDiffType.AddedOrUpdatedOnly) && !hashMapOfChangedRows.ContainsKey(keyHashValue))
                        {
                            _tableWriter.Write(values: readerOfCurrentTable.ToList());
                            rowCount++;
                            hashMapOfChangedRows.Add(keyHashValue, rowHashValue);
                        }
                    }
                }
            }

            return rowCount;
        }

        /// <summary>
        /// Reads the contents of two CSV files and creates a new CSV file with all rows that have changed (depending on the given diff type)
        /// </summary>
        /// <param name="type">Type of diff operation</param>
        /// <param name="readerOfPreviousTable">The <see cref="CsvTableReader"/> for the old CSV file</param>
        /// <param name="readerOfCurrentTable">The <see cref="CsvTableReader"/> for the new CSV file</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The value of the TResult
        /// parameter contains the number of written rows.<returns>
        public async Task<int> GenerateAsync(CsvDiffType type, CsvTableReader readerOfPreviousTable, CsvTableReader readerOfCurrentTable, CancellationToken cancellationToken = default)
        {
            var rowCount = 0;

            if (type == CsvDiffType.DeletedOnly)
            {
                var hashMapOfCurrentTable = await GenerateRowHashMapAsync(readerOfCurrentTable, cancellationToken);

                readerOfPreviousTable.ReadHeaders();

                _tableWriter.WriteHeaders(readerOfPreviousTable.Headers);

                while (await readerOfPreviousTable.ReadAsync() > 0)
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    var keyHashValue = GetKeyHashValue(readerOfPreviousTable);

                    if (!hashMapOfCurrentTable.ContainsKey(keyHashValue))
                    {
                        await _tableWriter.WriteAsync(values: readerOfPreviousTable.ToList());
                        rowCount++;
                    }
                }
            }
            else
            {
                var hashMapOfChangedRows = new Dictionary<uint, uint>();
                var hashMapOfPreviousTable = await GenerateRowHashMapAsync(readerOfPreviousTable, cancellationToken);

                readerOfCurrentTable.ReadHeaders();

                _tableWriter.WriteHeaders(readerOfCurrentTable.Headers);

                while (await readerOfCurrentTable.ReadAsync() > 0)
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    var keyHashValue = GetKeyHashValue(readerOfCurrentTable);
                    var rowHashValue = GetRowHashValue(readerOfCurrentTable);

                    if (hashMapOfPreviousTable.TryGetValue(keyHashValue, out var rowHashValueOfPreviousTable))
                    {
                        if ((type == CsvDiffType.UpdatedOnly || type == CsvDiffType.AddedOrUpdatedOnly) && rowHashValue != rowHashValueOfPreviousTable)
                        {
                            await _tableWriter.WriteAsync(values: readerOfCurrentTable.ToList());
                            rowCount++;
                            hashMapOfChangedRows.TryAdd(keyHashValue, rowHashValue);
                        }
                    }
                    else
                    {
                        if ((type == CsvDiffType.AddedOnly || type == CsvDiffType.AddedOrUpdatedOnly) && !hashMapOfChangedRows.ContainsKey(keyHashValue))
                        {
                            await _tableWriter.WriteAsync(values: readerOfCurrentTable.ToList());
                            rowCount++;
                            hashMapOfChangedRows.Add(keyHashValue, rowHashValue);
                        }
                    }
                }
            }

            return rowCount;
        }

        /// <summary>
        /// Generates hash values for all rows from a given CSV file
        /// </summary>
        /// <param name="tableReader">The <see cref="CsvTableReader"/> as source</param>
        /// <returns>A dictionary with row id as key and row hash as value.</returns>
        private Dictionary<uint, uint> GenerateRowHashMap(CsvTableReader tableReader)
        {
            var hashMap = new Dictionary<uint, uint>();

            hashMap.Clear();

            tableReader.ReadHeaders();

            while (tableReader.Read() > 0)
            {
                var keyHashValue = GetKeyHashValue(tableReader);
                var rowHashValue = GetRowHashValue(tableReader);
                hashMap.TryAdd(keyHashValue, rowHashValue);
            }

            return hashMap;
        }

        /// <summary>
        /// Generates hash values for all rows from a given given CSV file
        /// </summary>
        /// <param name="tableReader">The <see cref="CsvTableReader"/> as source</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The value of the TResult
        /// parameter contains a dictionary with row id as key and row hash as value.
        /// <returns>
        private async Task<Dictionary<uint, uint>> GenerateRowHashMapAsync(CsvTableReader tableReader, CancellationToken cancellationToken)
        {
            var hashMap = new Dictionary<uint, uint>();

            await tableReader.ReadHeadersAsync();

            while (await tableReader.ReadAsync() > 0)
            {
                cancellationToken.ThrowIfCancellationRequested();

                var keyHashValue = GetKeyHashValue(tableReader);
                var rowHashValue = GetRowHashValue(tableReader);
                hashMap.TryAdd(keyHashValue, rowHashValue);

            }

            return hashMap;
        }

        /// <summary>
        /// Calculates an hash value of the current csv row key
        /// </summary>
        /// <param name="tableReader">The <see cref="CsvTableReader"/> as source</param>
        /// <returns>The Hash value</returns>
        private uint GetKeyHashValue(CsvTableReader tableReader)
        {
            if ((UniqueKeyHeaders != null) && (UniqueKeyHeaders.Count > 0))
            {
                foreach (var header in UniqueKeyHeaders)
                {
                    _hashAlgorithm.Append(Encoding.UTF8.GetBytes(tableReader[header]));
                }

                return Crc32.HashToUInt32(_hashAlgorithm.GetHashAndReset());
            }
            else
            {
                return GetRowHashValue(tableReader);
            }
        }

        /// <summary>
        /// Calculates an hash value of the current csv row
        /// </summary>
        /// <param name="tableReader">The <see cref="CsvTableReader"/> as source</param>
        /// <returns>The Hash value</returns>
        private uint GetRowHashValue(CsvTableReader tableReader)
        {
            foreach (var header in tableReader.Headers)
            {
                _hashAlgorithm.Append(Encoding.UTF8.GetBytes(tableReader[header]));
            }

            return Crc32.HashToUInt32(_hashAlgorithm.GetHashAndReset());
        }
    }
}
