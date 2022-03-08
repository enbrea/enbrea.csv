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
    /// Unit tests for <see cref="CsvLineTableWriter"/>.
    /// </summary>
    public class TestCsvLineTableWriter
    {
        [Fact]
        public void SmokeTest()
        {
            var csvLine1 = "A;B;C";
            var csvLine2 = "a1;b1;c1";
            var csvLine3 = "a2;b2;c2";

            var csvTableWriter = new CsvLineTableWriter();

            Assert.Equal(csvLine1, csvTableWriter.WriteHeaders("A", "B", "C"));

            Assert.Equal(3, csvTableWriter.Headers.Count);

            csvTableWriter[0] = "a1";
            csvTableWriter[1] = "b1";
            csvTableWriter[2] = "c1";

            Assert.Equal(csvLine2, csvTableWriter.Write());

            csvTableWriter["A"] = "a2";
            csvTableWriter["B"] = "b2";
            csvTableWriter["C"] = "c2";

            Assert.Equal(csvLine3, csvTableWriter.Write());
        }

        [Fact]
        public void TestJsonValues()
        {
            var csvLine2 = "42;\"{\"\"IntValue\"\":42,\"\"StrValue\"\":\"\"Forty-Two\"\"}\"";
            var csvLine3 = "5;\"{\"\"IntValue\"\":5,\"\"StrValue\"\":\"\"Five\"\"}\"";

            var csvTableWriter = new CsvLineTableWriter("A", "B");

            csvTableWriter.AddConverter<CustomType>(new CustomTypeConverter());

            Assert.Equal(2, csvTableWriter.Headers.Count);

            csvTableWriter.SetValue("A", 42);
            csvTableWriter.SetValue("B", new CustomType { IntValue = 42, StrValue = "Forty-Two" });

            Assert.Equal(csvLine2, csvTableWriter.Write());

            csvTableWriter.SetValue("A", 5);
            csvTableWriter.SetValue("B", new CustomType { IntValue = 5, StrValue = "Five" });

            Assert.Equal(csvLine3, csvTableWriter.Write());
        }

        [Fact]
        public void TestTypedValues()
        {
            var csvLine1 = "A;B;C;D";
            var csvLine2 = "22;Text;true;01.01.2010";
            var csvLine3 = "-31;A long text;false;20.01.2050";
            var csvLine4 = "55;\"A text with ;\";;31.07.1971";

            var csvTableWriter = new CsvLineTableWriter();

#if NET6_0_OR_GREATER
            csvTableWriter.SetFormats<DateOnly>("dd.MM.yyyy");
#else
            csvTableWriter.SetFormats<DateTime>("dd.MM.yyyy");
#endif
            csvTableWriter.SetTrueFalseString<bool>("true", "false");

            Assert.Equal(csvLine1, csvTableWriter.WriteHeaders("A", "B", "C", "D"));

            Assert.Equal(4, csvTableWriter.Headers.Count);

            csvTableWriter.SetValue("A", 22);
            csvTableWriter.SetValue("B", "Text");
            csvTableWriter.SetValue("C", true);
#if NET6_0_OR_GREATER
            csvTableWriter.SetValue("D", new DateOnly(2010, 1, 1));
#else
            csvTableWriter.SetValue("D", new DateTime(2010, 1, 1));
#endif

            Assert.Equal(csvLine2, csvTableWriter.Write());

            csvTableWriter.SetValue("A", -31);
            csvTableWriter.SetValue("B", "A long text");
            csvTableWriter.SetValue("C", false);
#if NET6_0_OR_GREATER
            csvTableWriter.SetValue("D", new DateOnly(2050, 1, 20));
#else
            csvTableWriter.SetValue("D", new DateTime(2050, 1, 20));
#endif

            Assert.Equal(csvLine3, csvTableWriter.Write());

            Assert.True(csvTableWriter.TrySetValue("A", 55));
            Assert.True(csvTableWriter.TrySetValue("B", "A text with ;"));
            Assert.True(csvTableWriter.TrySetValue("C", null));
#if NET6_0_OR_GREATER
            Assert.True(csvTableWriter.TrySetValue("D", new DateOnly(1971, 7, 31)));
#else
            Assert.True(csvTableWriter.TrySetValue("D", new DateTime(1971, 7, 31)));
#endif

            Assert.Equal(csvLine4, csvTableWriter.Write());
        }
    }
}
