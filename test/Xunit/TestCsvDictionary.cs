﻿#region ENBREA.CSV - Copyright (c) STÜBER SYSTEMS GmbH
/*    
 *    ENBREA.CSV 
 *    
 *    Copyright (c) STÜBER SYSTEMS GmbH
 *
 *    Licensed under the MIT License, Version 2.0. 
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
    /// Unit tests for <see cref="CsvDictionary"/>.
    /// </summary>
    public class TestCsvDictionary
    {
        [Fact]
        public async Task SmokeTest()
        {
            var csvData =
                "A;a" + Environment.NewLine +
                "B;b" + Environment.NewLine +
                "C;c";

            var sb = new StringBuilder();

            using var strWriter = new StringWriter(sb);

            var csvDictionary = new CsvDictionary(new CsvConfiguration { Separator = ';' });

            csvDictionary["A"] = "a";
            csvDictionary["B"] = "b";
            csvDictionary["C"] = "c";

            await csvDictionary.StoreAsync(strWriter);

            Assert.Equal(csvData, sb.ToString());

            using var strReader = new StringReader(csvData);

            Assert.NotNull(csvDictionary);

            await csvDictionary.LoadAsync(strReader);

            Assert.Equal("a", csvDictionary["A"]);
            Assert.Equal("b", csvDictionary["B"]);
            Assert.Equal("c", csvDictionary["C"]);
        }

        [Fact]
        public async Task TestEmpty()
        {
            var csvData = "";

            using var strReader = new StringReader(csvData);

            var csvDictionary = new CsvDictionary(new CsvConfiguration { Separator = ';' });

            Assert.NotNull(csvDictionary);

            await csvDictionary.LoadAsync(strReader);
            Assert.Equal(0, csvDictionary.Count);
        }

        [Fact]
        public async Task TestEntity()
        {
            var csvData =
                "A;22" + Environment.NewLine +
                "B;Text" + Environment.NewLine +
                "C;true" + Environment.NewLine +
                "D;01.01.2010" + Environment.NewLine +
                "e;" + Environment.NewLine +
                "G;A";

            var sb = new StringBuilder();

            using var strWriter = new StringWriter(sb);

            var csvDictionary = new CsvDictionary(new CsvConfiguration { Separator = ';' });

            csvDictionary.SetFormats<DateTime>("dd.MM.yyyy");
            csvDictionary.SetTrueFalseString<bool>("true", "false");

            csvDictionary["A"] = null;
            csvDictionary["B"] = null;
            csvDictionary["C"] = null;
            csvDictionary["D"] = null;
            csvDictionary["e"] = null;
            csvDictionary["G"] = null;

            await csvDictionary.StoreAsync(strWriter, new SampleObject() { A = 22, B = "Text", C = true, D = new DateTime(2010, 1, 1) });

            Assert.Equal(csvData, sb.ToString());

            using var strReader = new StringReader(csvData);

            Assert.NotNull(csvDictionary);

            await csvDictionary.LoadAsync(strReader);

            Assert.Equal(new SampleObject() { A = 22, B = "Text", C = true, D = new DateTime(2010, 1, 1), E = "", G = SampleEnum.A }, csvDictionary.CreateAndGetValues<SampleObject>());
        }

        [Fact]
        public async Task TestGetTypedValues()
        {
            var csvData =
                "A;22" + Environment.NewLine +
                "B;Text" + Environment.NewLine +
                "C;true" + Environment.NewLine +
                "D;01.01.2010" + Environment.NewLine +
                "E;A";

            using var strReader = new StringReader(csvData);

            var csvDictionary = new CsvDictionary(new CsvConfiguration { Separator = ';' });

            Assert.NotNull(csvDictionary);

            csvDictionary.SetFormats<DateTime>("dd.MM.yyyy");

            await csvDictionary.LoadAsync(strReader);
            Assert.Equal(22, csvDictionary.GetValue<int>("A"));
            Assert.Equal("Text", csvDictionary.GetValue<string>("B"));
            Assert.True(csvDictionary.GetValue<bool>("C"));
            Assert.Equal(new DateTime(2010, 1, 1), csvDictionary.GetValue<DateTime>("D"));
            Assert.Equal(SampleEnum.A, csvDictionary.GetValue<SampleEnum>("E"));
        }
        [Fact]
        public async Task TestSetTypedValues()
        {
            var csvData =
                "A;22" + Environment.NewLine +
                "B;Text" + Environment.NewLine +
                "C;true" + Environment.NewLine +
                "D;01.01.2010";

            var sb = new StringBuilder();

            using var strWriter = new StringWriter(sb);

            var csvDictionary = new CsvDictionary(new CsvConfiguration { Separator = ';' });

            csvDictionary.SetFormats<DateTime>("dd.MM.yyyy");
            csvDictionary.SetTrueFalseString<bool>("true", "false");

            csvDictionary.SetValue("A", 22);
            csvDictionary.SetValue("B", "Text");
            csvDictionary.SetValue("C", true);
            csvDictionary.SetValue("D", new DateTime(2010, 1, 1));

            await csvDictionary.StoreAsync(strWriter);

            Assert.Equal(csvData, sb.ToString());
        }

        [Fact]
        public async Task TestTryGetValue()
        {
            var csvData =
                "A;a" + Environment.NewLine +
                "B;b" + Environment.NewLine +
                "C;c";

            using var strReader = new StringReader(csvData);

            var csvDictionary = new CsvDictionary(new CsvConfiguration { Separator = ';' });

            Assert.NotNull(csvDictionary);

            await csvDictionary.LoadAsync(strReader);

            Assert.True(csvDictionary.TryGetValue("A", out string v1));
            Assert.Equal("a", v1);
            Assert.True(csvDictionary.TryGetValue("B", out string v2));
            Assert.Equal("b", v2);
            Assert.True(csvDictionary.TryGetValue("C", out string v3));
            Assert.Equal("c", v3);
            Assert.False(csvDictionary.TryGetValue("D", out string v4));
            Assert.Null(v4);
        }

        [Fact]
        public async Task TestUseValue()
        {
            var csvData =
                "A;a" + Environment.NewLine +
                "B;b" + Environment.NewLine +
                "C;c";

            using var strReader = new StringReader(csvData);

            var csvDictionary = new CsvDictionary(new CsvConfiguration { Separator = ';' });

            Assert.NotNull(csvDictionary);

            await csvDictionary.LoadAsync(strReader);

            csvDictionary.UseValue("A", s => Assert.Equal("a", s));
            csvDictionary.UseValue("B", s => Assert.Equal("b", s));
            csvDictionary.UseValue("C", s => Assert.Equal("c", s));
        }
    }
}
