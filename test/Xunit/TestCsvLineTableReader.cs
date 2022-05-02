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
using Xunit;

namespace Enbrea.Csv.Tests
{
    /// <summary>
    /// Unit tests for <see cref="CsvLineTableReader"/>.
    /// </summary>
    public class TestCsvLineTableReader
    {
        [Fact]
        public void SmokeTest()
        {
            var csvLine1 = "A;B;C";
            var csvLine2 = "a1;b1;c1";
            var csvLine3 = "a2;b2;c2";

            var csvTableReader = new CsvLineTableReader(new CsvConfiguration { Separator = ';' });

            Assert.NotNull(csvTableReader);

            csvTableReader.ReadHeaders(csvLine1);
            
            Assert.Equal(3, csvTableReader.Headers.Count);
            Assert.Equal("A", csvTableReader.Headers[0]);
            Assert.Equal("B", csvTableReader.Headers[1]);
            Assert.Equal("C", csvTableReader.Headers[2]);

            csvTableReader.Read(csvLine2);

            Assert.Equal(3, csvTableReader.Headers.Count);
            Assert.Equal("a1", csvTableReader[0]);
            Assert.Equal("b1", csvTableReader[1]);
            Assert.Equal("c1", csvTableReader[2]);

            csvTableReader.Read(csvLine3);

            Assert.Equal(3, csvTableReader.Headers.Count);
            Assert.Equal("a2", csvTableReader["A"]);
            Assert.Equal("b2", csvTableReader["B"]);
            Assert.Equal("c2", csvTableReader["C"]);
        }

        [Fact]
        public void TestJsonValues()
        {
            var csvLine2 = "42;\"{\"\"IntValue\"\":42,\"\"StrValue\"\":\"\"Forty-Two\"\"}\"";
            var csvLine3 = "5;\"{\"\"IntValue\"\":5,\"\"StrValue\"\":\"\"Five\"\"}\"";

            var csvTableReader = new CsvLineTableReader(new CsvConfiguration { Separator = ';' }, "A","B");

            Assert.NotNull(csvTableReader);

            csvTableReader.AddConverter<CustomType>(new CustomTypeConverter());

            Assert.Equal(2, csvTableReader.Headers.Count);
            Assert.Equal("A", csvTableReader.Headers[0]);
            Assert.Equal("B", csvTableReader.Headers[1]);

            csvTableReader.Read(csvLine2);
            
            Assert.Equal(42, csvTableReader.GetValue<int>("A"));
            var o1 = csvTableReader.GetValue<CustomType>("B");
            Assert.Equal(42, o1.IntValue);
            Assert.Equal("Forty-Two", o1.StrValue);

            csvTableReader.Read(csvLine3);

            Assert.Equal(5, csvTableReader.GetValue<int>("A"));
            var o2 = csvTableReader.GetValue<CustomType>("B");
            Assert.Equal(5, o2.IntValue);
            Assert.Equal("Five", o2.StrValue);
        }

        [Fact]
        public void TestTryGetValue()
        {
            var csvLine1 = "A;B;C";
            var csvLine2 = "a1;b1;c1";
            var csvLine3 = "a2;b2;c2";

            var csvTableReader = new CsvLineTableReader(new CsvConfiguration { Separator = ';' });

            Assert.NotNull(csvTableReader);

            csvTableReader.ReadHeaders(csvLine1);

            csvTableReader.Read(csvLine2);

            Assert.Equal(3, csvTableReader.Headers.Count);
            Assert.True(csvTableReader.TryGetValue(0, out string v1));
            Assert.Equal("a1", v1);
            Assert.True(csvTableReader.TryGetValue(1, out string v2));
            Assert.Equal("b1", v2);
            Assert.True(csvTableReader.TryGetValue(2, out string v3));
            Assert.Equal("c1", v3);
            Assert.False(csvTableReader.TryGetValue(3, out string v4));
            Assert.Null(v4);

            csvTableReader.Read(csvLine3);

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
        public void TestTypedValues()
        {
            var csvLine1 = "A;B;C;D";
            var csvLine2 = "22;Text;true;01.01.2010";
            var csvLine3 = "-31;A long text;false;20.01.2050";
            var csvLine4 = "55;\"A text with ;\";;31.07.1971";

            var csvTableReader = new CsvLineTableReader(new CsvConfiguration { Separator = ';' });

            Assert.NotNull(csvTableReader);

#if NET6_0_OR_GREATER
            csvTableReader.SetFormats<DateOnly>("dd.MM.yyyy");
#else
            csvTableReader.SetFormats<DateTime>("dd.MM.yyyy");
#endif

            csvTableReader.ReadHeaders(csvLine1);

            Assert.Equal(4, csvTableReader.Headers.Count);
            Assert.Equal("A", csvTableReader.Headers[0]);
            Assert.Equal("B", csvTableReader.Headers[1]);
            Assert.Equal("C", csvTableReader.Headers[2]);
            Assert.Equal("D", csvTableReader.Headers[3]);

            csvTableReader.Read(csvLine2);

            Assert.Equal(22, csvTableReader.GetValue<int>("A"));
            Assert.Equal("Text", csvTableReader.GetValue<string>("B"));
            Assert.True(csvTableReader.GetValue<bool>("C"));
#if NET6_0_OR_GREATER
            Assert.Equal(new DateOnly(2010, 1, 1), csvTableReader.GetValue<DateOnly>("D"));
#else
            Assert.Equal(new DateTime(2010, 1, 1), csvTableReader.GetValue<DateTime>("D"));
#endif

            csvTableReader.Read(csvLine3);

            Assert.Equal(-31, csvTableReader.GetValue<int>("A"));
            Assert.Equal("A long text", csvTableReader.GetValue<string>("B"));
            Assert.False(csvTableReader.GetValue<bool>("C"));
#if NET6_0_OR_GREATER
            Assert.Equal(new DateOnly(2050, 1, 20), csvTableReader.GetValue<DateOnly>("D"));
#else
            Assert.Equal(new DateTime(2050, 1, 20), csvTableReader.GetValue<DateTime>("D"));
#endif

            csvTableReader.Read(csvLine4);

            Assert.True(csvTableReader.TryGetValue<int>("A", out var a));
            Assert.Equal(55, a);
            Assert.True(csvTableReader.TryGetValue<string>("B", out var b));
            Assert.Equal("A text with ;", b);
            Assert.True(csvTableReader.TryGetValue<bool?>("C", out var c));
            Assert.Null(c);
#if NET6_0_OR_GREATER
            Assert.True(csvTableReader.TryGetValue<DateOnly>("D", out var d));
            Assert.Equal(new DateOnly(1971, 7, 31), d);
#else
            Assert.True(csvTableReader.TryGetValue<DateTime>("D", out var d));
            Assert.Equal(new DateTime(1971, 7, 31), d);
#endif
        }
    }
}
