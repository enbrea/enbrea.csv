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

using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Enbrea.Csv.Tests
{

    /// <summary>
    /// Unit tests for <see cref="CsvTableWriter"/>.
    /// </summary>
    public class TestCsvTableWriter
    {
        [Fact]
        public async Task SmokeTest()
        {
            var csvData =
                "A;B;C" + Environment.NewLine +
                "a1;b1;c1" + Environment.NewLine +
                "a2;b2;c2";

            var sb = new StringBuilder();

            using var strWriter = new StringWriter(sb);

            var csvTableWriter = new CsvTableWriter(strWriter, new CsvConfiguration { Separator = ';' });

            await csvTableWriter.WriteHeadersAsync("A", "B", "C");

            Assert.Equal(3, csvTableWriter.Headers.Count);

            csvTableWriter[0] = "a1";
            csvTableWriter[1] = "b1";
            csvTableWriter[2] = "c1";

            await csvTableWriter.WriteAsync();

            csvTableWriter["A"] = "a2";
            csvTableWriter["B"] = "b2";
            csvTableWriter["C"] = "c2";

            await csvTableWriter.WriteAsync();

            Assert.Equal(csvData, sb.ToString());
        }

        [Fact]
        public async Task TestJsonValues()
        {
            var csvData =
                "A;B" + Environment.NewLine +
                "42;\"{\"\"IntValue\"\":42,\"\"StrValue\"\":\"\"Forty-Two\"\"}\"" + Environment.NewLine +
                "5;\"{\"\"IntValue\"\":5,\"\"StrValue\"\":\"\"Five\"\"}\"";

            var sb = new StringBuilder();

            using var strWriter = new StringWriter(sb);

            var csvTableWriter = new CsvTableWriter(strWriter, new CsvConfiguration { Separator = ';' });

            csvTableWriter.AddConverter<CustomType>(new CustomTypeConverter());

            await csvTableWriter.WriteHeadersAsync("A", "B");

            Assert.Equal(2, csvTableWriter.Headers.Count);

            csvTableWriter.SetValue("A", 42);
            csvTableWriter.SetValue("B", new CustomType { IntValue = 42, StrValue = "Forty-Two" });

            await csvTableWriter.WriteAsync();

            csvTableWriter.SetValue("A", 5);
            csvTableWriter.SetValue("B", new CustomType { IntValue = 5, StrValue = "Five" });

            await csvTableWriter.WriteAsync();

            Assert.Equal(csvData, sb.ToString());
        }

        [Fact]
        public async Task TestStronglyTypedValues()
        {
            var csvData =
                "A;B;C;D" + Environment.NewLine +
                "22;Text;true;01.01.2010" + Environment.NewLine +
                "-31;A long text;false;20.01.2050" + Environment.NewLine +
                "55;\"A text with ;\";;31.07.1971";

            var sb = new StringBuilder();

            using var strWriter = new StringWriter(sb);

            var csvTableWriter = new CsvTableWriter(strWriter, new CsvConfiguration { Separator = ';' });

            csvTableWriter.SetFormats<DateTime>("dd.MM.yyyy");
            csvTableWriter.SetTrueFalseString<bool>("true", "false");

            await csvTableWriter.WriteHeadersAsync("A", "B", "C", "D");

            Assert.Equal(4, csvTableWriter.Headers.Count);

            csvTableWriter.SetValue("A", 22);
            csvTableWriter.SetValue("B", "Text");
            csvTableWriter.SetValue("C", true);
            csvTableWriter.SetValue("D", new DateTime(2010, 1, 1));

            await csvTableWriter.WriteAsync();

            csvTableWriter.SetValue("A", -31);
            csvTableWriter.SetValue("B", "A long text");
            csvTableWriter.SetValue("C", false);
            csvTableWriter.SetValue("D", new DateTime(2050, 1, 20));

            await csvTableWriter.WriteAsync();

            Assert.True(csvTableWriter.TrySetValue("A", 55));
            Assert.True(csvTableWriter.TrySetValue("B", "A text with ;"));
            Assert.True(csvTableWriter.TrySetValue("C", null));
            Assert.True(csvTableWriter.TrySetValue("D", new DateTime(1971, 7, 31)));

            await csvTableWriter.WriteAsync();

            Assert.Equal(csvData, sb.ToString());
        }

        [Fact]
        public async Task TestStronglyTypedWriter()
        {
            var csvData =
                "A;B;C;D" + Environment.NewLine +
                "22;Text;true;01.01.2010" + Environment.NewLine +
                "-31;A long text;false;20.01.2050" + Environment.NewLine +
                "55;\"A text with ;\";;31.07.1971";

            var sb = new StringBuilder();

            using var strWriter = new StringWriter(sb);

            var csvTableWriter = new CsvTableWriter(strWriter, new CsvConfiguration { Separator = ';' });

            csvTableWriter.SetFormats<DateTime>("dd.MM.yyyy");
            csvTableWriter.SetTrueFalseString<bool>("true", "false");

            await csvTableWriter.WriteHeadersAsync<SampleObject>(x => new { x.A, x.B, x.C, x.D });

            Assert.Equal(4, csvTableWriter.Headers.Count);

            await csvTableWriter.WriteAsync(new SampleObject() { A = 22, B = "Text", C = true, D = new DateTime(2010, 1, 1) });
            await csvTableWriter.WriteAsync(new SampleObject() { A = -31, B = "A long text", C = false, D = new DateTime(2050, 1, 20) });
            await csvTableWriter.WriteAsync(new SampleObject() { A = 55, B = "A text with ;", C = null, D = new DateTime(1971, 7, 31) });

            Assert.Equal(csvData, sb.ToString());
        }

        [Fact]
        public async Task TestToString()
        {
            var csvLine1 = "22;Text;true;01.01.2010";
            var csvLine2 = "55;\"A text with ;\";;31.07.1971";

            var sb = new StringBuilder();

            using var strWriter = new StringWriter(sb);

            var csvTableWriter = new CsvTableWriter(strWriter, new CsvConfiguration { Separator = ';' });

            csvTableWriter.SetFormats<DateTime>("dd.MM.yyyy");
            csvTableWriter.SetTrueFalseString<bool>("true", "false");

            await csvTableWriter.WriteHeadersAsync(new string[] { "A", "B", "C", "D" });

            Assert.Equal(4, csvTableWriter.Headers.Count);

            csvTableWriter.SetValue("A", 22);
            csvTableWriter.SetValue("B", "Text");
            csvTableWriter.SetValue("C", true);
            csvTableWriter.SetValue("D", new DateTime(2010, 1, 1));

            Assert.Equal(csvLine1, csvTableWriter.ToString());

            csvTableWriter.SetValue("A", 55);
            csvTableWriter.SetValue("B", "A text with ;");
            csvTableWriter.SetValue("C", (string)null);
            csvTableWriter.SetValue("D", new DateTime(1971, 7, 31));

            Assert.Equal(csvLine2, csvTableWriter.ToString());
        }
    }
}
