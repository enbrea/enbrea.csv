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
    /// Unit tests for <see cref="CsvTableLineBuilder"/>.
    /// </summary>
    public class TestCsvTableLineBuilder
    {
        [Fact]
        public void SmokeTest()
        {
            var csvLine1 = "A;B;C";
            var csvLine2 = "a1;b1;c1";
            var csvLine3 = "a2;b2;c2";

            var csvTableBuilder = new CsvTableLineBuilder(new CsvConfiguration { Separator = ';' });

            Assert.Equal(csvLine1, csvTableBuilder.AssignHeaders("A", "B", "C"));

            Assert.Equal(3, csvTableBuilder.Headers.Count);

            csvTableBuilder[0] = "a1";
            csvTableBuilder[1] = "b1";
            csvTableBuilder[2] = "c1";

            Assert.Equal(csvLine2, csvTableBuilder.ToString());

            csvTableBuilder["A"] = "a2";
            csvTableBuilder["B"] = "b2";
            csvTableBuilder["C"] = "c2";

            Assert.Equal(csvLine3, csvTableBuilder.ToString());
        }

        [Fact]
        public void TestJsonValues()
        {
            var csvLine2 = "42;\"{\"\"IntValue\"\":42,\"\"StrValue\"\":\"\"Forty-Two\"\"}\"";
            var csvLine3 = "5;\"{\"\"IntValue\"\":5,\"\"StrValue\"\":\"\"Five\"\"}\"";

            var csvTableBuilder = new CsvTableLineBuilder(new CsvConfiguration() { Separator = ';'}, "A", "B");

            csvTableBuilder.AddConverter<CustomType>(new CustomTypeConverter());

            Assert.Equal(2, csvTableBuilder.Headers.Count);

            csvTableBuilder.SetValue("A", 42);
            csvTableBuilder.SetValue("B", new CustomType { IntValue = 42, StrValue = "Forty-Two" });

            Assert.Equal(csvLine2, csvTableBuilder.ToString());

            csvTableBuilder.SetValue("A", 5);
            csvTableBuilder.SetValue("B", new CustomType { IntValue = 5, StrValue = "Five" });

            Assert.Equal(csvLine3, csvTableBuilder.ToString());
        }

        [Fact]
        public void TestTypedValues()
        {
            var csvLine1 = "A;B;C;D";
            var csvLine2 = "22;Text;true;01.01.2010";
            var csvLine3 = "-31;A long text;false;20.01.2050";
            var csvLine4 = "55;\"A text with ;\";;31.07.1971";

            var csvTableBuilder = new CsvTableLineBuilder(new CsvConfiguration { Separator = ';' });

#if NET6_0_OR_GREATER
            csvTableBuilder.SetFormats<DateOnly>("dd.MM.yyyy");
#else
            csvTableBuilder.SetFormats<DateTime>("dd.MM.yyyy");
#endif
            csvTableBuilder.SetTrueFalseString<bool>("true", "false");

            Assert.Equal(csvLine1, csvTableBuilder.AssignHeaders("A", "B", "C", "D"));

            Assert.Equal(4, csvTableBuilder.Headers.Count);

            csvTableBuilder.SetValue("A", 22);
            csvTableBuilder.SetValue("B", "Text");
            csvTableBuilder.SetValue("C", true);
#if NET6_0_OR_GREATER
            csvTableBuilder.SetValue("D", new DateOnly(2010, 1, 1));
#else
            csvTableBuilder.SetValue("D", new DateTime(2010, 1, 1));
#endif

            Assert.Equal(csvLine2, csvTableBuilder.ToString());

            csvTableBuilder.SetValue("A", -31);
            csvTableBuilder.SetValue("B", "A long text");
            csvTableBuilder.SetValue("C", false);
#if NET6_0_OR_GREATER
            csvTableBuilder.SetValue("D", new DateOnly(2050, 1, 20));
#else
            csvTableBuilder.SetValue("D", new DateTime(2050, 1, 20));
#endif

            Assert.Equal(csvLine3, csvTableBuilder.ToString());

            Assert.True(csvTableBuilder.TrySetValue("A", 55));
            Assert.True(csvTableBuilder.TrySetValue("B", "A text with ;"));
            Assert.True(csvTableBuilder.TrySetValue("C", null));
#if NET6_0_OR_GREATER
            Assert.True(csvTableBuilder.TrySetValue("D", new DateOnly(1971, 7, 31)));
#else
            Assert.True(csvTableBuilder.TrySetValue("D", new DateTime(1971, 7, 31)));
#endif

            Assert.Equal(csvLine4, csvTableBuilder.ToString());
        }
    }
}
