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
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Enbrea.Csv.Tests
{
    /// <summary>
    /// Unit tests for <see cref="CsvReader"/>.
    /// </summary>
    public class TestCsvReader
    {
        [Fact]
        public async Task SupportComments()
        {
            var csvData =
                "# Comment 1" + Environment.NewLine +
                "a1;b1;c1" + Environment.NewLine +
                "# Comment 2" + Environment.NewLine +
                "# Comment 3" + Environment.NewLine +
                "a2;b2;c2# No comment";

            using var csvReader = new CsvReader(csvData);

            csvReader.Configuration.AllowComments = true;

            Assert.NotNull(csvReader);

            var fields = new List<string>();

            await csvReader.ReadLineAsync(fields);

            // 1. line: Comment
            // 2. line: No comment
            Assert.Equal(3, fields.Count);
            Assert.Equal("a1", fields[0]);
            Assert.Equal("b1", fields[1]);
            Assert.Equal("c1", fields[2]);

            await csvReader.ReadLineAsync(fields);

            // 3. line: Comment
            // 4. line: Comment
            // 5. line: No comment
            Assert.Equal(3, fields.Count);
            Assert.Equal("a2", fields[0]);
            Assert.Equal("b2", fields[1]);
            Assert.Equal("c2# No comment", fields[2]);
        }

        [Fact]
        public async Task SupportDefaultFields()
        {
            var csvData =
                "aaa1;bbb1;ccc1" + Environment.NewLine +
                "aaa2;bbb2;ccc2" + Environment.NewLine +
                "aaa3;bbb3";

            using var csvReader = new CsvReader(csvData);

            Assert.NotNull(csvReader);

            var fields = new List<string>();

            await csvReader.ReadLineAsync(fields);

            Assert.Equal(3, fields.Count);
            Assert.Equal("aaa1", fields[0]);
            Assert.Equal("bbb1", fields[1]);
            Assert.Equal("ccc1", fields[2]);

            await csvReader.ReadLineAsync(fields);

            Assert.Equal(3, fields.Count);
            Assert.Equal("aaa2", fields[0]);
            Assert.Equal("bbb2", fields[1]);
            Assert.Equal("ccc2", fields[2]);

            await csvReader.ReadLineAsync(fields);

            Assert.Equal(2, fields.Count);
            Assert.Equal("aaa3", fields[0]);
            Assert.Equal("bbb3", fields[1]);
        }

        [Fact]
        public async Task SupportEmptyRowSkipping()
        {
            var csvData =
                "aaa1;bbb1;ccc1" + Environment.NewLine + Environment.NewLine + "aaa2;bbb2;ccc2";

            var fields = new List<string>();

            using var csvReader = new CsvReader(csvData);

            Assert.NotNull(csvReader);

            int count = 0;

            while (await csvReader.ReadLineAsync(fields) > 0) { count++; };

            Assert.Equal(2, count);
        }

        [Fact]
        public async Task SupportMultilineValues()
        {
            var csvData =
                "aaa1;bbb1;\"A long" + Environment.NewLine +
                "multi line" + Environment.NewLine +
                "text\"" + Environment.NewLine +
                "aaa2;bbb2;ccc2";

            using var csvReader = new CsvReader(csvData);

            Assert.NotNull(csvReader);

            var fields = new List<string>();

            await csvReader.ReadLineAsync(fields);

            // 1. line: 2 simple field + 1 multiline field
            Assert.Equal(3, fields.Count);
            Assert.Equal("aaa1", fields[0]);
            Assert.Equal("bbb1", fields[1]);
            Assert.Equal("A long" + Environment.NewLine +
                         "multi line" + Environment.NewLine +
                         "text", fields[2]);

            await csvReader.ReadLineAsync(fields);

            // 2. line: 3 simple fields
            Assert.Equal(3, fields.Count);
            Assert.Equal("aaa2", fields[0]);
            Assert.Equal("bbb2", fields[1]);
            Assert.Equal("ccc2", fields[2]);
        }

        [Fact]
        public async Task SupportNonStandardQuotedFields()
        {
            var csvData =
                "%a a a%;%b b b%;%c c c%" + Environment.NewLine +
                "%a%%a%%a%;%b;b;b%;%c c c%";

            using var csvReader = new CsvReader(csvData);
            csvReader.Configuration.Quote = '%';

            Assert.NotNull(csvReader);

            var fields = new List<string>();

            await csvReader.ReadLineAsync(fields);

            Assert.Equal(3, fields.Count);
            Assert.Equal("a a a", fields[0]);
            Assert.Equal("b b b", fields[1]);
            Assert.Equal("c c c", fields[2]);

            await csvReader.ReadLineAsync(fields);

            Assert.Equal(3, fields.Count);
            Assert.Equal("a%a%a", fields[0]);
            Assert.Equal("b;b;b", fields[1]);
            Assert.Equal("c c c", fields[2]);
        }

        [Fact]
        public async Task SupportNormalizing()
        {
            var csvData =
                "aaa1;bbb1;\"A long" + Environment.NewLine +
                "multi line" + Environment.NewLine +
                "text\"" + Environment.NewLine +
                "# A comment" + Environment.NewLine +
                "aaa2;bbb2;ccc2";

            using var csvReader = new CsvReader(csvData);

            csvReader.Configuration.AllowComments = true;

            Assert.NotNull(csvReader);

            IAsyncEnumerator<string> enumerator = csvReader.ReadAllLinesAsync().GetAsyncEnumerator();

            // 1. line: 2 simple field + 1 multiline field
            Assert.True(await enumerator.MoveNextAsync());
            Assert.Equal("aaa1;bbb1;\"A long" + Environment.NewLine +
                         "multi line" + Environment.NewLine +
                         "text\"", enumerator.Current);

            // 2. line: comment
            // 3. line: 3 simple fields
            Assert.True(await enumerator.MoveNextAsync());
            Assert.Equal("aaa2;bbb2;ccc2", enumerator.Current);
        }

        [Fact]
        public async Task SupportQuotedFields()
        {
            var csvData =
                "\"a a a\";\"b b b\";\"c c c\"" + Environment.NewLine +
                "\"a\"\"a\"\"a\";\"b;b;b\";\"c c c\"";

            using var csvReader = new CsvReader(csvData);

            Assert.NotNull(csvReader);

            var fields = new List<string>();

            await csvReader.ReadLineAsync(fields);

            Assert.Equal(3, fields.Count);
            Assert.Equal("a a a", fields[0]);
            Assert.Equal("b b b", fields[1]);
            Assert.Equal("c c c", fields[2]);

            await csvReader.ReadLineAsync(fields);

            Assert.Equal(3, fields.Count);
            Assert.Equal("a\"a\"a", fields[0]);
            Assert.Equal("b;b;b", fields[1]);
            Assert.Equal("c c c", fields[2]);
        }
        [Fact]
        public async Task SupportStrangeCases()
        {
            var csvData =
                ";;" + Environment.NewLine +
                "" + Environment.NewLine +
                "\"   \" " + Environment.NewLine +
                "   ";

            using var csvReader = new CsvReader(csvData);

            Assert.NotNull(csvReader);

            var fields = new List<string>();

            await csvReader.ReadLineAsync(fields);

            // 1. line: 2 separators = 3 empty fields
            Assert.Equal(3, fields.Count);
            Assert.Equal("", fields[0]);
            Assert.Equal("", fields[1]);
            Assert.Equal("", fields[2]);

            await csvReader.ReadLineAsync(fields);

            // 2. line: Empty line = ignored
            // 3. line: filled with quoted whitespaces 
            Assert.Single(fields);
            Assert.Equal("   ", fields[0]);

            await csvReader.ReadLineAsync(fields);

            // 2. line: Empty line = ignored
            // 3. line: filled with whitespaces
            Assert.Single(fields);
            Assert.Equal("   ", fields[0]);
        }

        [Fact]
        public async Task SupportUmlaute()
        {
            var csvData =
                "äöü;\"ä ö ü\"" + Environment.NewLine +
                "ÄÖÜ;\"Ä Ö Ü\"";

            var fields = new List<string>();

            using var csvReader = new CsvReader(csvData);

            Assert.NotNull(csvReader);

            await csvReader.ReadLineAsync(fields);

            Assert.Equal(2, fields.Count);
            Assert.Equal("äöü", fields[0]);
            Assert.Equal("ä ö ü", fields[1]);

            await csvReader.ReadLineAsync(fields);

            Assert.Equal(2, fields.Count);
            Assert.Equal("ÄÖÜ", fields[0]);
            Assert.Equal("Ä Ö Ü", fields[1]);
        }

        [Fact]
        public async Task SupportWhitespace()
        {
            var csvData =
                "1 ; 2 ; 3" + Environment.NewLine +
                " ;  ;   ";

            using var csvReader = new CsvReader(csvData);

            Assert.NotNull(csvReader);

            var fields = new List<string>();

            await csvReader.ReadLineAsync(fields);

            Assert.Equal(3, fields.Count);
            Assert.Equal("1 ", fields[0]);
            Assert.Equal(" 2 ", fields[1]);
            Assert.Equal(" 3", fields[2]);

            await csvReader.ReadLineAsync(fields);

            Assert.Equal(3, fields.Count);
            Assert.Equal(" ", fields[0]);
            Assert.Equal("  ", fields[1]);
            Assert.Equal("   ", fields[2]);
        }
    }
}
