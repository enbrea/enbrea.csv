#region ENBREA.CSV - Copyright (c) STÜBER SYSTEMS GmbH
/*    
 *    ENBREA.CSV 
 *    
 *    Copyright (c) STÜBER SYSTEMS GmbH
 *
 *    Licensed under the MIT License, Version 2.0. 
 */
#endregion

using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace Enbrea.Csv.Tests
{
    /// <summary>
    /// Unit tests for <see cref="CsvDiff"/>.
    /// </summary>
    public class TestCsvDiff
    {
        [Fact]
        public async Task TestAddedOnly()
        {
            var csvPreviousTable =
                """
                Id;F2;F3
                11;c1;b1
                22;c2;b2
                33;c3;b3
                """;
            var csvCurrentTable =
                """
                Id;F2;F3
                11;c1;b1
                22;c22;b22
                44;c3;b3
                """;
            var csvAddedOnlyTable =
                """
                Id;F2;F3
                44;c3;b3
                """;

            using var csvTextReaderForPreviousTable = new StringReader(csvPreviousTable);
            using var csvTextReaderrForCurrentTable = new StringReader(csvCurrentTable);

            var csvReaderForPreviousTable = new CsvTableReader(csvTextReaderForPreviousTable, new CsvConfiguration { Separator = ';' });
            var csvReaderrForCurrentTable = new CsvTableReader(csvTextReaderrForCurrentTable, new CsvConfiguration { Separator = ';' });

            using var csvTextWriter = new StringWriter();

            var csvDiff = new CsvDiff(csvTextWriter, new CsvConfiguration { Separator = ';' }, "Id");

            var numberOfRows = await csvDiff.GenerateAsync(CsvDiffType.AddedOnly, csvReaderForPreviousTable, csvReaderrForCurrentTable);

            Assert.Equal(1, numberOfRows);
            Assert.Equal(csvAddedOnlyTable, csvTextWriter.ToString());
        }

        [Fact]
        public async Task TestAddedOrUpdatedOnly()
        {
            var csvPreviousTable =
                """
                Id;F2;F3
                11;c1;b1
                22;c2;b2
                33;c3;b3
                """;
            var csvCurrentTable =
                """
                Id;F2;F3
                11;c1;b1
                22;c22;b22
                44;c3;b3
                """;
            var csvAddedOrUpdatedOnlyTable =
                """
                Id;F2;F3
                22;c22;b22
                44;c3;b3
                """;

            using var csvTextReaderForPreviousTable = new StringReader(csvPreviousTable);
            using var csvTextReaderrForCurrentTable = new StringReader(csvCurrentTable);

            var csvReaderForPreviousTable = new CsvTableReader(csvTextReaderForPreviousTable, new CsvConfiguration { Separator = ';' });
            var csvReaderrForCurrentTable = new CsvTableReader(csvTextReaderrForCurrentTable, new CsvConfiguration { Separator = ';' });

            using var csvTextWriter = new StringWriter();

            var csvDiff = new CsvDiff(csvTextWriter, new CsvConfiguration { Separator = ';' }, "Id");

            var numberOfRows = await csvDiff.GenerateAsync(CsvDiffType.AddedOrUpdatedOnly, csvReaderForPreviousTable, csvReaderrForCurrentTable);

            Assert.Equal(2, numberOfRows);
            Assert.Equal(csvAddedOrUpdatedOnlyTable, csvTextWriter.ToString());
        }

        [Fact]
        public async Task TestDeletedOnly()
        {
            var csvPreviousTable =
                """
                Id;F2;F3
                11;c1;b1
                22;c2;b2
                33;c3;b3
                """;
            var csvCurrentTable =
                """
                Id;F2;F3
                11;c1;b1
                22;c22;b22
                22;c22;b22
                44;c3;b3
                """;
            var csvDeletedOnlyTable =
                """
                Id;F2;F3
                33;c3;b3
                """;

            using var csvTextReaderForPreviousTable = new StringReader(csvPreviousTable);
            using var csvTextReaderrForCurrentTable = new StringReader(csvCurrentTable);

            var csvReaderForPreviousTable = new CsvTableReader(csvTextReaderForPreviousTable, new CsvConfiguration { Separator = ';' });
            var csvReaderrForCurrentTable = new CsvTableReader(csvTextReaderrForCurrentTable, new CsvConfiguration { Separator = ';' });

            using var csvTextWriter = new StringWriter();

            var csvDiff = new CsvDiff(csvTextWriter, new CsvConfiguration { Separator = ';' }, "Id");

            var numberOfRows = await csvDiff.GenerateAsync(CsvDiffType.DeletedOnly, csvReaderForPreviousTable, csvReaderrForCurrentTable);

            Assert.Equal(1, numberOfRows);
            Assert.Equal(csvDeletedOnlyTable, csvTextWriter.ToString());
        }

        [Fact]
        public async Task TestUpdatedOnly()
        {
            var csvPreviousTable =
                """
                Id;F2;F3
                11;c1;b1
                22;c2;b2
                33;c3;b3
                """;
            var csvCurrentTable =
                """
                Id;F2;F3
                11;c1;b1
                22;c22;b22
                44;c3;b3
                """;
            var csvUpdatedOnlyTable =
                """
                Id;F2;F3
                22;c22;b22
                """;

            using var csvTextReaderForPreviousTable = new StringReader(csvPreviousTable);
            using var csvTextReaderrForCurrentTable = new StringReader(csvCurrentTable);

            var csvReaderForPreviousTable = new CsvTableReader(csvTextReaderForPreviousTable, new CsvConfiguration { Separator = ';' });
            var csvReaderrForCurrentTable = new CsvTableReader(csvTextReaderrForCurrentTable, new CsvConfiguration { Separator = ';' });

            using var csvTextWriter = new StringWriter();

            var csvDiff = new CsvDiff(csvTextWriter, new CsvConfiguration { Separator = ';' }, "Id");

            var numberOfRows = await csvDiff.GenerateAsync(CsvDiffType.UpdatedOnly, csvReaderForPreviousTable, csvReaderrForCurrentTable);

            Assert.Equal(1, numberOfRows);
            Assert.Equal(csvUpdatedOnlyTable, csvTextWriter.ToString());
        }
    }
}
