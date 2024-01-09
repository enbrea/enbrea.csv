#region ENBREA.CSV - Copyright (c) STÜBER SYSTEMS GmbH
/*    
 *    ENBREA.CSV 
 *    
 *    Copyright (c) STÜBER SYSTEMS GmbH
 *
 *    Licensed under the MIT License, Version 2.0. 
 */
#endregion

using System;
using Xunit;

namespace Enbrea.Csv.Tests
{
    /// <summary>
    /// Unit tests for <see cref="CsvTableLineParser"/>.
    /// </summary>
    public class TestCsvTableLineParser
    {
        [Fact]
        public void SmokeTest()
        {
            var csvLine1 = "A;B;C";
            var csvLine2 = "a1;b1;c1";
            var csvLine3 = "a2;b2;c2";

            var csvTableParser = new CsvTableLineParser(new CsvConfiguration { Separator = ';' });

            Assert.NotNull(csvTableParser);

            csvTableParser.ParseHeaders(csvLine1);
            
            Assert.Equal(3, csvTableParser.Headers.Count);
            Assert.Equal("A", csvTableParser.Headers[0]);
            Assert.Equal("B", csvTableParser.Headers[1]);
            Assert.Equal("C", csvTableParser.Headers[2]);

            csvTableParser.Parse(csvLine2);

            Assert.Equal(3, csvTableParser.Headers.Count);
            Assert.Equal("a1", csvTableParser[0]);
            Assert.Equal("b1", csvTableParser[1]);
            Assert.Equal("c1", csvTableParser[2]);

            csvTableParser.Parse(csvLine3);

            Assert.Equal(3, csvTableParser.Headers.Count);
            Assert.Equal("a2", csvTableParser["A"]);
            Assert.Equal("b2", csvTableParser["B"]);
            Assert.Equal("c2", csvTableParser["C"]);
        }

        [Fact]
        public void TestJsonValues()
        {
            var csvLine2 = "42;\"{\"\"IntValue\"\":42,\"\"StrValue\"\":\"\"Forty-Two\"\"}\"";
            var csvLine3 = "5;\"{\"\"IntValue\"\":5,\"\"StrValue\"\":\"\"Five\"\"}\"";

            var csvTableParser = new CsvTableLineParser(new CsvConfiguration { Separator = ';' }, "A","B");

            Assert.NotNull(csvTableParser);

            csvTableParser.AddConverter<CustomType>(new CustomTypeConverter());

            Assert.Equal(2, csvTableParser.Headers.Count);
            Assert.Equal("A", csvTableParser.Headers[0]);
            Assert.Equal("B", csvTableParser.Headers[1]);

            csvTableParser.Parse(csvLine2);
            
            Assert.Equal(42, csvTableParser.GetValue<int>("A"));
            var o1 = csvTableParser.GetValue<CustomType>("B");
            Assert.Equal(42, o1.IntValue);
            Assert.Equal("Forty-Two", o1.StrValue);

            csvTableParser.Parse(csvLine3);

            Assert.Equal(5, csvTableParser.GetValue<int>("A"));
            var o2 = csvTableParser.GetValue<CustomType>("B");
            Assert.Equal(5, o2.IntValue);
            Assert.Equal("Five", o2.StrValue);
        }

        [Fact]
        public void TestTryGetValue()
        {
            var csvLine1 = "A;B;C";
            var csvLine2 = "a1;b1;c1";
            var csvLine3 = "a2;b2;c2";

            var csvTableParser = new CsvTableLineParser(new CsvConfiguration { Separator = ';' });

            Assert.NotNull(csvTableParser);

            csvTableParser.ParseHeaders(csvLine1);

            csvTableParser.Parse(csvLine2);

            Assert.Equal(3, csvTableParser.Headers.Count);
            Assert.True(csvTableParser.TryGetValue(0, out string v1));
            Assert.Equal("a1", v1);
            Assert.True(csvTableParser.TryGetValue(1, out string v2));
            Assert.Equal("b1", v2);
            Assert.True(csvTableParser.TryGetValue(2, out string v3));
            Assert.Equal("c1", v3);
            Assert.False(csvTableParser.TryGetValue(3, out string v4));
            Assert.Null(v4);

            csvTableParser.Parse(csvLine3);

            Assert.Equal(3, csvTableParser.Headers.Count);
            Assert.True(csvTableParser.ParseHeaders("A", out string v5));
            Assert.Equal("a2", v5);
            Assert.True(csvTableParser.ParseHeaders("B", out string v6));
            Assert.Equal("b2", v6);
            Assert.True(csvTableParser.ParseHeaders("C", out string v7));
            Assert.Equal("c2", v7);
            Assert.False(csvTableParser.ParseHeaders("D", out string v8));
            Assert.Null(v8);
        }

        [Fact]
        public void TestTypedValues()
        {
            var csvLine1 = "A;B;C;D";
            var csvLine2 = "22;Text;true;01.01.2010";
            var csvLine3 = "-31;A long text;false;20.01.2050";
            var csvLine4 = "55;\"A text with ;\";;31.07.1971";

            var csvTableParser = new CsvTableLineParser(new CsvConfiguration { Separator = ';' });

            Assert.NotNull(csvTableParser);

#if NET6_0_OR_GREATER
            csvTableParser.SetFormats<DateOnly>("dd.MM.yyyy");
#else
            csvTableParser.SetFormats<DateTime>("dd.MM.yyyy");
#endif

            csvTableParser.ParseHeaders(csvLine1);

            Assert.Equal(4, csvTableParser.Headers.Count);
            Assert.Equal("A", csvTableParser.Headers[0]);
            Assert.Equal("B", csvTableParser.Headers[1]);
            Assert.Equal("C", csvTableParser.Headers[2]);
            Assert.Equal("D", csvTableParser.Headers[3]);

            csvTableParser.Parse(csvLine2);

            Assert.Equal(22, csvTableParser.GetValue<int>("A"));
            Assert.Equal("Text", csvTableParser.GetValue<string>("B"));
            Assert.True(csvTableParser.GetValue<bool>("C"));
#if NET6_0_OR_GREATER
            Assert.Equal(new DateOnly(2010, 1, 1), csvTableParser.GetValue<DateOnly>("D"));
#else
            Assert.Equal(new DateTime(2010, 1, 1), csvTableParser.GetValue<DateTime>("D"));
#endif

            csvTableParser.Parse(csvLine3);

            Assert.Equal(-31, csvTableParser.GetValue<int>("A"));
            Assert.Equal("A long text", csvTableParser.GetValue<string>("B"));
            Assert.False(csvTableParser.GetValue<bool>("C"));
#if NET6_0_OR_GREATER
            Assert.Equal(new DateOnly(2050, 1, 20), csvTableParser.GetValue<DateOnly>("D"));
#else
            Assert.Equal(new DateTime(2050, 1, 20), csvTableParser.GetValue<DateTime>("D"));
#endif

            csvTableParser.Parse(csvLine4);

            Assert.True(csvTableParser.TryGetValue<int>("A", out var a));
            Assert.Equal(55, a);
            Assert.True(csvTableParser.TryGetValue<string>("B", out var b));
            Assert.Equal("A text with ;", b);
            Assert.True(csvTableParser.TryGetValue<bool?>("C", out var c));
            Assert.Null(c);
#if NET6_0_OR_GREATER
            Assert.True(csvTableParser.TryGetValue<DateOnly>("D", out var d));
            Assert.Equal(new DateOnly(1971, 7, 31), d);
#else
            Assert.True(csvTableParser.TryGetValue<DateTime>("D", out var d));
            Assert.Equal(new DateTime(1971, 7, 31), d);
#endif
        }
    }
}
