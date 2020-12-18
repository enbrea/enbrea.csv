﻿#region ENBREA.CSV - Copyright (C) 2020 STÜBER SYSTEMS GmbH
/*    
 *    ENBREA.CSV 
 *    
 *    Copyright (C) 2020 STÜBER SYSTEMS GmbH
 *
 *    Licensed under the MIT License, Version 2.0. 
 * 
 */
#endregion

using System;
using System.Threading.Tasks;
using Xunit;

namespace Enbrea.Csv.Tests
{
    /// <summary>
    /// Unit tests for <see cref="CsvTableReader"/>.
    /// </summary>
    public class TestCsvTableReader
    {
        [Fact]
        public async Task SmokeTest()
        {
            var csvData =
                "A;B;C" + Environment.NewLine +
                "a1;b1;c1" + Environment.NewLine +
                "a2;b2;c2";

            using var csvReader = new CsvReader(csvData);

            var csvTableReader = new CsvTableReader(csvReader);

            Assert.NotNull(csvTableReader);

            await csvTableReader.ReadHeadersAsync();
            Assert.Equal(3, csvTableReader.Headers.Count);
            Assert.Equal("A", csvTableReader.Headers[0]);
            Assert.Equal("B", csvTableReader.Headers[1]);
            Assert.Equal("C", csvTableReader.Headers[2]);

            await csvTableReader.ReadAsync();

            Assert.Equal(3, csvTableReader.Headers.Count);
            Assert.Equal("a1", csvTableReader[0]);
            Assert.Equal("b1", csvTableReader[1]);
            Assert.Equal("c1", csvTableReader[2]);

            await csvTableReader.ReadAsync();

            Assert.Equal(3, csvTableReader.Headers.Count);
            Assert.Equal("a2", csvTableReader["A"]);
            Assert.Equal("b2", csvTableReader["B"]);
            Assert.Equal("c2", csvTableReader["C"]);
        }

        [Fact]
        public async Task TestJsonValues()
        {
            var csvData =
                "A;B" + Environment.NewLine +
                "42;\"{\"\"IntValue\"\":42,\"\"StrValue\"\":\"\"Forty-Two\"\"}\"" + Environment.NewLine +
                "5;\"{\"\"IntValue\"\":5,\"\"StrValue\"\":\"\"Five\"\"}\"";

            using var csvReader = new CsvReader(csvData);

            var csvTableReader = new CsvTableReader(csvReader);

            Assert.NotNull(csvTableReader);

            csvTableReader.AddConverter<CustomType>(new CustomTypeConverter());

            await csvTableReader.ReadHeadersAsync();
            Assert.Equal(2, csvTableReader.Headers.Count);
            Assert.Equal("A", csvTableReader.Headers[0]);
            Assert.Equal("B", csvTableReader.Headers[1]);

            await csvTableReader.ReadAsync();
            Assert.Equal(42, csvTableReader.GetValue<int>("A"));
            var o1 = csvTableReader.GetValue<CustomType>("B");
            Assert.Equal(42, o1.IntValue);
            Assert.Equal("Forty-Two", o1.StrValue);

            await csvTableReader.ReadAsync();
            Assert.Equal(5, csvTableReader.GetValue<int>("A"));
            var o2 = csvTableReader.GetValue<CustomType>("B");
            Assert.Equal(5, o2.IntValue);
            Assert.Equal("Five", o2.StrValue);
        }

        [Fact]
        public async Task TestTryGetValue()
        {
            var csvData =
                "A;B;C" + Environment.NewLine +
                "a1;b1;c1" + Environment.NewLine +
                "a2;b2;c2";

            using var csvReader = new CsvReader(csvData);

            var csvTableReader = new CsvTableReader(csvReader);

            Assert.NotNull(csvTableReader);

            await csvTableReader.ReadHeadersAsync();
            await csvTableReader.ReadAsync();

            Assert.Equal(3, csvTableReader.Headers.Count);
            Assert.True(csvTableReader.TryGetValue(0, out string v1));
            Assert.Equal("a1", v1);
            Assert.True(csvTableReader.TryGetValue(1, out string v2));
            Assert.Equal("b1", v2);
            Assert.True(csvTableReader.TryGetValue(2, out string v3));
            Assert.Equal("c1", v3);
            Assert.False(csvTableReader.TryGetValue(3, out string v4));
            Assert.Null(v4);

            await csvTableReader.ReadAsync();

            Assert.Equal(3, csvTableReader.Headers.Count);
            Assert.True(csvTableReader.TryGetValue("A", out string v5));
            Assert.Equal("a2", v5);
            Assert.True(csvTableReader.TryGetValue("B", out string v6));
            Assert.Equal("b2", v6);
            Assert.True(csvTableReader.TryGetValue("C", out string v7));
            Assert.Equal("c2", v7);
            Assert.False(csvTableReader.TryGetValue("D", out string v8));
            Assert.Null(v8);
        }

        [Fact]
        public async Task TestTypedValues()
        {
            var csvData =
                "A;B;C;D" + Environment.NewLine +
                "22;Text;true;01.01.2010" + Environment.NewLine +
                "-31;A long text;false;20.01.2050" + Environment.NewLine +
                "55;\"A text with ;\";;31.07.1971";

            using var csvReader = new CsvReader(csvData);

            var csvTableReader = new CsvTableReader(csvReader);

            Assert.NotNull(csvTableReader);

            csvTableReader.SetFormats<DateTime>("dd.MM.yyyy");

            await csvTableReader.ReadHeadersAsync();
            Assert.Equal(4, csvTableReader.Headers.Count);
            Assert.Equal("A", csvTableReader.Headers[0]);
            Assert.Equal("B", csvTableReader.Headers[1]);
            Assert.Equal("C", csvTableReader.Headers[2]);
            Assert.Equal("D", csvTableReader.Headers[3]);

            await csvTableReader.ReadAsync();
            Assert.Equal(22, csvTableReader.GetValue<int>("A"));
            Assert.Equal("Text", csvTableReader.GetValue<string>("B"));
            Assert.True(csvTableReader.GetValue<bool>("C"));
            Assert.Equal(new DateTime(2010, 1, 1), csvTableReader.GetValue<DateTime>("D"));

            await csvTableReader.ReadAsync();
            Assert.Equal(-31, csvTableReader.GetValue<int>("A"));
            Assert.Equal("A long text", csvTableReader.GetValue<string>("B"));
            Assert.False(csvTableReader.GetValue<bool>("C"));
            Assert.Equal(new DateTime(2050, 1, 20), csvTableReader.GetValue<DateTime>("D"));

            await csvTableReader.ReadAsync();
            Assert.True(csvTableReader.TryGetValue<int>("A", out var a));
            Assert.Equal(55, a);
            Assert.True(csvTableReader.TryGetValue<string>("B", out var b));
            Assert.Equal("A text with ;", b);
            Assert.True(csvTableReader.TryGetValue<bool?>("C", out var c));
            Assert.Null(c);
            Assert.True(csvTableReader.TryGetValue<DateTime>("D", out var d));
            Assert.Equal(new DateTime(1971, 7, 31), d);
        }

        [Fact]
        public async Task TestUseValue()
        {
            var csvData =
                "A;B;C" + Environment.NewLine +
                "a1;b1;c1" + Environment.NewLine +
                "a2;b2;c2";

            using var csvReader = new CsvReader(csvData);

            var csvTableReader = new CsvTableReader(csvReader);

            Assert.NotNull(csvTableReader);

            await csvTableReader.ReadHeadersAsync();
            await csvTableReader.ReadAsync();

            csvTableReader.UseValue(0, s => Assert.Equal("a1", s));
            csvTableReader.UseValue(1, s => Assert.Equal("b1", s));
            csvTableReader.UseValue(2, s => Assert.Equal("c1", s));

            await csvTableReader.ReadAsync();

            csvTableReader.UseValue("A", s => Assert.Equal("a2", s));
            csvTableReader.UseValue("B", s => Assert.Equal("b2", s));
            csvTableReader.UseValue("C", s => Assert.Equal("c2", s));
        }
    }
}
